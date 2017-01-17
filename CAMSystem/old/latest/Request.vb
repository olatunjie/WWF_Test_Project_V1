Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient

Public Class Request

    Private docType As String
    Private reqName As String
    Private stat As String
    Private origUser As Object
    Private remk As String
    Private entdate As Date
    Private errormsg As String
    Private storex As New DAL
    Private dataLayer As WorkFlow

    Public Property Doc_Type() As String
        Get
            Return docType
        End Get
        Set(ByVal newvalue As String)
            docType = newvalue
        End Set
    End Property

    Public Property RequestName() As String
        Get
            Return reqName
        End Get
        Set(ByVal value As String)
            reqName = value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return stat
        End Get
        Set(ByVal value As String)
            stat = value
        End Set
    End Property

    Public Property OriginatingUser() As Object
        Get
            Return origUser
        End Get
        Set(ByVal value As Object)
            origUser = value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return remk
        End Get
        Set(ByVal value As String)
            remk = value
        End Set
    End Property

    Public Property EntryDate() As Date
        Get
            Return entdate
        End Get
        Set(ByVal value As Date)
            entdate = value
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            Return errormsg
        End Get
        Set(ByVal value As String)
            errormsg = value
        End Set
    End Property

    Public Sub SaveRequest(ByVal doctype As String, ByVal reqname As String, ByVal status As String, ByVal origuser As Object, ByVal entrydate As Date, ByVal remark As String, ByVal task_id As String, ByVal action As String, ByVal constr As String, ByVal table As String, ByVal field As String)
        Try
            Me.ErrorMessage = String.Empty
            storex = New DAL
            Me.Doc_Type = doctype
            Me.RequestName = reqname
            Me.Status = status
            Me.OriginatingUser = origuser
            Me.EntryDate = entrydate
            Me.Remark = remark

            storex.SaveRequest(Me)
            ProcessRequest(task_id, action, Me.Doc_Type, Me.OriginatingUser, constr, table, field)
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::SaveRequest()")
        End Try
    End Sub

    Public Sub SaveRequest(ByVal doctype As String, ByVal reqname As String, ByVal status As String, ByVal origuser As Object, ByVal entrydate As Date, ByVal remark As String, ByVal task_id As String, ByVal action As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim storex As New DAL
            Me.Doc_Type = doctype
            Me.RequestName = reqname
            Me.Status = status
            Me.OriginatingUser = origuser
            Me.EntryDate = entrydate
            Me.Remark = remark

            storex.SaveRequest(Me)
            ProcessRequest(task_id, action, Me.Doc_Type, Me.OriginatingUser)
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::SaveRequest()")
        End Try
    End Sub

    Public Sub SaveRequest(ByVal doctype As String, ByVal reqname As String, ByVal status As String, ByVal origuser As Object, ByVal entrydate As Date, ByVal remark As String, ByVal task_id As String, ByVal action As String, ByVal comment As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim storex As New DAL
            Me.Doc_Type = doctype
            Me.RequestName = reqname
            Me.Status = status
            Me.OriginatingUser = origuser
            Me.EntryDate = entrydate
            Me.Remark = remark

            storex.SaveRequest(Me)
            ProcessRequest(task_id, action, Me.Doc_Type, comment, Me.OriginatingUser)
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::SaveRequest()")
        End Try
    End Sub

    Public Sub ProcessRequest(ByVal taskID As String, ByVal requestname As String, ByVal doctype As String, ByVal owner As Object, ByVal connectionstr As String, ByVal tablename As String, ByVal fieldnames As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim result As New ArrayList
            Dim action As String = String.Empty
            Dim user_rule As String = String.Empty
            Dim msgaction As String = String.Empty
            Dim origrule As String = String.Empty
            Dim assignedUsers As New ArrayList
            Dim storex As New DAL
            Dim parser As New RuleParser
            Dim task As New Task
            Dim assign As New Assignment
            Dim assuser As New AssignedUsers
            Dim state As String = String.Empty
            Dim taskStatus As String = String.Empty

            assignedUsers.Clear()
            result.Clear()
            If (storex.GetCondition(doctype, requestname) = String.Empty) Then

            Else
                parser.Execute(storex.GetCondition(doctype, requestname), doctype, owner)
                result = parser.GetResults()

                If (result.Count > 0) Then
                    action = result.Item(0)
                    origrule = result.Item(2)
                    If (InStr(result.Item(1), """") > 0) Then
                        msgaction = result.Item(1).ToString.Trim("""")
                    Else
                        'Check existence of rule
                        user_rule = result.Item(1)
                        If Not (storex.GetCondition(doctype, user_rule) = String.Empty) Then
                            assignedUsers.Clear()
                            assignedUsers = assuser.GetAssignedUsers(user_rule, owner, doctype, connectionstr, tablename, fieldnames)

                            For i As Integer = 0 To assignedUsers.Count - 1
                                ' taskStatus = storex.GetTaskStatus(taskID, owner, doctype)
                                'If taskStatus = String.Empty Then
                                task.SaveTask(taskID, owner, assignedUsers(i), action, "Pending", "", DateTime.Now, doctype)
                                'Else
                                'task.SaveTask(taskID, owner, assignedUsers(i), action, taskStatus, "", DateTime.Now.Date, doctype)
                                'End If
                            Next

                            state = storex.GetRequestStatus(doctype, owner, requestname, DateTime.Now.Date)

                            If (state = "Processing") Then
                                storex.UpdateRequestStatus(owner, "Assigned", requestname, DateTime.Now.Date, doctype)
                            Else
                                Me.SaveRequest(doctype, requestname, "Processing", owner, DateTime.Now.Date, msgaction, taskID, action, connectionstr, tablename, fieldnames)
                            End If

                        End If
                    End If
                End If
                'assign.Assignment(owner, doctype, action, origrule, msgaction)
            End If

        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::ProcessRequest()")
        End Try

    End Sub

    Public Sub ProcessRequest(ByVal taskID As String, ByVal requestname As String, ByVal doctype As String, ByVal owner As Object)
        Try
            Me.ErrorMessage = String.Empty
            Dim result As New ArrayList
            Dim action As String = String.Empty
            Dim user_rule As String = String.Empty
            Dim msgaction As String = String.Empty
            Dim origrule As String = String.Empty
            Dim assignedUsers As New ArrayList
            Dim storex As New DAL
            Dim parser As New RuleParser
            Dim task As New Task
            Dim assign As New Assignment
            Dim assuser As New AssignedUsers
            Dim state As String = String.Empty
            Dim taskStatus As String = String.Empty

            assignedUsers.Clear()
            result.Clear()
            If (storex.GetCondition(doctype, requestname) = String.Empty) Then

            Else
                parser.Execute(storex.GetCondition(doctype, requestname), doctype, owner)
                result = parser.GetResults()

                If (result.Count > 0) Then
                    action = result.Item(0)
                    origrule = result.Item(2)
                    If (InStr(result.Item(1), """") > 0) Then
                        msgaction = result.Item(1).ToString.Trim("""")
                    Else
                        'Check existence of rule
                        user_rule = result.Item(1)
                        If Not (storex.GetCondition(doctype, user_rule) = String.Empty) Then
                            assignedUsers.Clear()
                            assignedUsers = assuser.GetAssignedUsers(user_rule, owner, doctype)

                            For i As Integer = 0 To assignedUsers.Count - 1
                                task.SaveTask(taskID, owner, assignedUsers(i), action, "Pending", "", DateTime.Now, doctype)
                            Next

                            state = storex.GetRequestStatus(doctype, owner, requestname, DateTime.Now.Date)

                            If (state = "Processing") Then
                                storex.UpdateRequestStatus(owner, "Assigned", requestname, DateTime.Now.Date, doctype)
                            Else
                                Me.SaveRequest(doctype, requestname, "Processing", owner, DateTime.Now.Date, msgaction, taskID, action)
                            End If

                        End If
                    End If
                End If
                'assign.Assignment(owner, doctype, action, origrule, msgaction)
            End If

        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::ProcessRequest()")
        End Try

    End Sub

    Public Sub ProcessRequest(ByVal taskID As String, ByVal requestname As String, ByVal doctype As String, ByVal comment As String, ByVal owner As Object)
        Try
            Me.ErrorMessage = String.Empty
            Dim result As New ArrayList
            Dim action As String = String.Empty
            Dim user_rule As String = String.Empty
            Dim msgaction As String = String.Empty
            Dim origrule As String = String.Empty
            Dim assignedUsers As New ArrayList
            Dim storex As New DAL
            Dim parser As New RuleParser
            Dim task As New Task
            Dim assign As New Assignment
            Dim assuser As New AssignedUsers
            Dim state As String = String.Empty
            Dim taskStatus As String = String.Empty

            assignedUsers.Clear()
            result.Clear()
            If (storex.GetCondition(doctype, requestname) = String.Empty) Then

            Else
                parser.Execute(storex.GetCondition(doctype, requestname), doctype, owner)
                result = parser.GetResults()

                If (result.Count > 0) Then
                    action = result.Item(0)
                    origrule = result.Item(2)
                    If (InStr(result.Item(1), """") > 0) Then
                        msgaction = result.Item(1).ToString.Trim("""")
                    Else
                        'Check existence of rule
                        user_rule = result.Item(1)
                        If Not (storex.GetCondition(doctype, user_rule) = String.Empty) Then
                            assignedUsers.Clear()
                            assignedUsers = assuser.GetAssignedUsers(user_rule, owner, doctype)

                            For i As Integer = 0 To assignedUsers.Count - 1
                                task.SaveTask(taskID, owner, assignedUsers(i), action, "Pending", comment, DateTime.Now, doctype)
                            Next

                            state = storex.GetRequestStatus(doctype, owner, requestname, DateTime.Now.Date)

                            If (state = "Processing") Then
                                storex.UpdateRequestStatus(owner, "Assigned", requestname, DateTime.Now.Date, doctype)
                            Else
                                Me.SaveRequest(doctype, requestname, "Processing", owner, DateTime.Now.Date, msgaction, taskID, action, comment)
                            End If

                        End If
                    End If
                End If
                'assign.Assignment(owner, doctype, action, origrule, msgaction)
            End If

        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Request::ProcessRequest()")
        End Try
    End Sub

    Public Function GetApprovalLimit(ByVal connectionString As String, ByVal tablename As String, ByVal fieldnames As String, ByVal rulestr As String) As Double
        Try
            If InStr(connectionString, "HOBANK1") > 0 Then
                Dim connect As New OracleConnection(connectionString)
                Dim command As OracleCommand
                Dim reader As OracleDataReader

                command = New OracleCommand("SELECT DISTINCT " & fieldnames & " FROM " & tablename & " WHERE " & rulestr, connect)

                If connect.State = ConnectionState.Closed Then
                    connect.Open()
                End If
                command.CommandType = CommandType.Text
                command.CommandTimeout = 0
                reader = command.ExecuteReader()
                While (reader.Read)

                End While
                reader.Close()
                connect.Close()
            Else
                Dim conn As New SqlConnection(connectionString)
                Dim cmd As SqlCommand
                Dim reader As SqlDataReader
                Dim approvalLimit As Double = 0.0
                cmd = New SqlCommand("SELECT " & fieldnames & " FROM " & tablename & " WHERE " & rulestr, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader()
                While (reader.Read)
                    approvalLimit = reader.GetValue(0)
                End While
                reader.Close()
                Return approvalLimit
                conn.Close()
            End If
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(docType, ex.Message & " - Module:- Request::GetApprovalLimit()")
            Return 0.0
        End Try
    End Function
End Class
