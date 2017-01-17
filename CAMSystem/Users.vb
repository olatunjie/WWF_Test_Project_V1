Imports System.Data
Imports System.Data.SqlClient

Public Class Users
    Private docprop As New DocProperties
    Private usercollection As New ArrayList
    Private userrule As New Rule
    Private storex As New DAL
    Private count As Integer = 0
    Private propval As New ArrayList
    Private userprops As New ArrayList
    Private props As New ArrayList
    Private errormsg As String = String.Empty

    Public Sub New()

    End Sub

    Public Property ErrorMessage() As String
        Get
            Return errormsg
        End Get
        Set(ByVal value As String)
            errormsg = value
        End Set
    End Property

    Public Sub GenerateUser(ByVal connectionString As String, ByVal tablename As String, ByVal fieldnames As String, ByVal assUserRule As String, ByVal dataowner As String, ByVal doctype As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim conn As New SqlConnection(connectionString)
            Dim cmd As SqlCommand
            Dim reader As SqlDataReader
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim owner As String = String.Empty

            rule = assUserRule
            If (InStr(rule, "Doc.")) Then
                str = Mid(rule, InStr(rule, "Doc."))
                str2 = Mid(str, InStr(str, ".") + 1)
                rule = Replace(rule, str, userrule.SQLParser(str2, dataowner, doctype))
            End If

            cmd = New SqlCommand("SELECT " & fieldnames & " FROM " & tablename & " WHERE " & rule, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                owner = String.Empty
                owner = reader.GetValue(0)
                storex.ResetUserProperties("User_" & doctype, dataowner)
                'If (storex.VerifyOwner(owner, "User") <> String.Empty) Then
                '    For i As Integer = 0 To reader.FieldCount - 1
                '        docprop.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(storex.GetDocType).ToString, "User", 1, dataowner)
                '        storex.saveUserProperty(dataowner, reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DateTime.Now)
                '    Next
                'Else
                For i As Integer = 0 To reader.FieldCount - 1
                    docprop.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, "User_" & doctype, 0, dataowner)
                    storex.SaveUserProperty(dataowner, reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DateTime.Now)
                Next
                'End If

            End While
            reader.Close()
            conn.Close()
            'count += 1
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::GenerateUser()")
        End Try
    End Sub

    Public Sub GenerateUser(ByVal assUserRule As String, ByVal dataowner As String, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            Me.ErrorMessage = String.Empty
            storex.ResetUserProperties("User_" & doctype, dataowner)

            'Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            Dim cmd As SqlCommand
            Dim reader As SqlDataReader
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim owner As String = String.Empty
            Dim condcount As Integer = 0

            rule = assUserRule
            While (InStr(rule, "Doc.")) > 0
                str = Mid(rule, InStr(rule, "Doc."), InStr(rule, ")") - InStrRev(rule, "Doc.") + 1)
                If InStr(str.Trim, " ") > 0 Then
                    str = Mid(str, 1, InStr(str, " ") - 1)
                End If
                str = str.Trim(")")
                str2 = Mid(str, InStr(str, ".") + 1)
                rule = Replace(rule, str, userrule.SQLParser(str2.Trim, dataowner, doctype))
            End While

            Dim temprule As String = rule
            While (InStr(temprule, "Type")) > 0
                str = Mid(temprule, 1, InStr(temprule, "Type") + 3)
                temprule = Replace(temprule, str, "prop")
                condcount += 1
            End While

            cmd = New SqlCommand("SELECT DISTINCT owner,count(*) as Total FROM tblDocumentsProperties WHERE DocType in ('" & doctype & "','*')  and " & rule & " group by owner order by total desc", conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                owner = String.Empty
                owner = reader.GetValue(0)
                If reader.GetValue(1) = condcount Then
                    docprop.AddProperties("UserID", reader.GetValue(0).ToString, storex.GetDocID(doctype).ToString, "User_" & doctype, 0, dataowner)
                End If
            End While
            reader.Close()
            'conn.Close()
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::GenerateUser()")
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Sub

    'Public Sub GenerateUser(ByVal dataowner As String, ByVal doctype As String, ByVal SupervisorID As String, ByVal usingsupervisorid As Boolean)
    '    Try
    '        Me.ErrorMessage = String.Empty
    '        storex.ResetUserProperties("User", dataowner)
    '        'Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
    '        docprop.AddProperties("UserID", SupervisorID, storex.GetDocID(doctype).ToString, "User", 0, dataowner)

    '    Catch ex As Exception
    '        storex = New DAL
    '        storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::GenerateUser()")
    '    End Try
    'End Sub

    Public Sub GenerateUser(ByVal assUserRule As String, ByVal dataowner As String, ByVal doctype As String, ByVal getapprovals As Boolean, ByVal terminalpoint As Boolean)
        Try
            Me.ErrorMessage = String.Empty
            storex.ResetUserProperties("User_" & doctype, dataowner)
            Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            'Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            Dim cmd As SqlCommand
            Dim reader As SqlDataReader
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim owner As String = String.Empty
            Dim docid As Integer = storex.GetDocID(doctype)

            rule = assUserRule
            While (InStr(rule, "Doc.")) > 0
                str = Mid(rule, InStr(rule, "Doc."), InStr(rule, ")") - InStrRev(rule, "Doc.") + 1)
                str = str.Trim(")")
                str2 = Mid(str, InStr(str, ".") + 1)
                rule = Replace(rule, str, userrule.SQLParser(str2.Trim, dataowner, doctype))
            End While

            cmd = New SqlCommand("SELECT DISTINCT owner FROM tblDocumentsProperties WHERE DocType='" & doctype & "'  and " & rule, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                owner = String.Empty
                owner = reader.GetValue(0)
                docprop.AddProperties("UserID", owner, storex.GetDocID(doctype).ToString, "User_" & doctype, 0, dataowner)
            End While
            reader.Close()
            conn.Close()
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::GenerateUser()")
        End Try
    End Sub

    Public Sub GenerateUser(ByVal assUserRule As String, ByVal dataowner As String, ByVal doctype As String, ByVal usingsupervisorid As Boolean)
        Try
            Me.ErrorMessage = String.Empty
            storex.ResetUserProperties("User_" & doctype, dataowner)
            Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            'Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            Dim cmd As SqlCommand
            Dim reader As SqlDataReader
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim owner As String = String.Empty

            rule = assUserRule
            While (InStr(rule, "Doc.")) > 0
                str = Mid(rule, InStr(rule, "Doc."), InStr(rule, ")") - InStrRev(rule, "Doc.") + 1)
                str = str.Trim(")")
                str2 = Mid(str, InStr(str, ".") + 1)
                rule = Replace(rule, str, userrule.SQLParser(str2.Trim, dataowner, doctype))
            End While

            cmd = New SqlCommand("SELECT DISTINCT value FROM tblDocumentsProperties WHERE DocType='" & doctype & "'  and " & rule, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                owner = String.Empty
                owner = reader.GetValue(0)
                docprop.AddProperties("UserID", owner, storex.GetDocID(doctype).ToString, "User_" & doctype, 0, dataowner)
            End While
            reader.Close()
            conn.Close()
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::GenerateUser()")
        End Try
    End Sub

    Public Function UserProperties(ByVal infoOwner As String, ByVal doctype As String) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim reader As SqlClient.SqlDataReader
            reader = storex.GetUserProp(infoOwner, doctype)
            userprops.Clear()

            While (reader.Read)
                userprops.Add(reader.GetString(0))
            End While
            reader.Close()
            Return userprops
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::UserProperties()")
            Return New ArrayList
        End Try
    End Function

    Public Function AggregateProp(ByVal doctype As String) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim reader As SqlDataReader
            Dim index As Integer = 0
            propval.Clear()
            props.Clear()
            reader = storex.GetUserAggProp(doctype)
            While (reader.Read)
                props.Add(reader.GetString(index))
                index += 1
            End While
            reader.Close()
            For i As Integer = 0 To props.Count - 1
                propval.Add(storex.GetPropVal(props.Item(i).ToString))
            Next
            Return propval
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Users::AggregateProp()")
            Return New ArrayList
        End Try
    End Function
End Class
