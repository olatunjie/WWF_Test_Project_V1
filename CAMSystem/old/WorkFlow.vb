Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient
Imports System.Configuration
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Public Class WorkFlow
    Public Class DAL
        Private Shared connstr As String = "Data Source=10.0.0.155;Initial Catalog=WorkFlowDB;User ID=sa;Password=system"
        'Private Shared connstr As String = "Data Source=HQ-TECH-L27;Initial Catalog=WorkFlowDB;Integrated Security=true"
        Private Shared cmd As SqlCommand
        Private Shared properties As New ArrayList
        Private Shared errormsg As String = String.Empty

        Public Shared Sub Init()
            connstr = "Data Source=10.0.0.155;Initial Catalog=WorkFlowDB;User ID=sa;Password=system"
            'connstr = "Data Source=HQ-TECH-L27;Initial Catalog=WorkFlowDB;Integrated Security=true"
        End Sub

        Public Shared Property ErrorMessage() As String
            Get
                Return errormsg
            End Get
            Set(ByVal value As String)
                errormsg = value
            End Set
        End Property

        Public Shared Function GetDocProperties(ByVal docType As String, ByVal type As String, ByVal owner As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetDocumentsProperties", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@docType", SqlDbType.NVarChar).Value = docType
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type
                cmd.Parameters.Add("@owner", SqlDbType.NVarChar).Value = owner
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetValue(2)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetPropertiesVal(ByVal docType As String, ByVal type As String, ByVal owner As Object) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty

                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spPropertiesVal", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@docType", SqlDbType.NVarChar).Value = docType
                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type
                cmd.Parameters.Add("@owner", SqlDbType.NVarChar).Value = owner
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetValue(2)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetAllDocProp(ByVal docType As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetDocProperties", conn)
                cmd.Parameters.Add("@docType", SqlDbType.NVarChar).Value = docType
                cmd.CommandType = CommandType.StoredProcedure

                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetRules() As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetRules", conn)
                cmd.CommandType = CommandType.StoredProcedure
                reader = cmd.ExecuteReader()
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Function GetRequest(ByVal owner As Object, ByVal filter As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty

                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spRequestName", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar).Value = owner
                cmd.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = filter
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetValue(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetAllRequests(ByVal origUser As Object) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader

                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetRequests", conn)
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = origUser
                cmd.CommandType = CommandType.StoredProcedure

                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetAllTasks(ByVal UserID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = UserID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetOtherTasks(ByVal UserID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks4", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = UserID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetPendingOrReturnedTasks(ByVal UserID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks5", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = UserID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetPendingTasks(ByVal UserID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks6", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = UserID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetReturnedTasks(ByVal UserID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks7", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = UserID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetHistory(ByVal UserID As String, ByVal doctype As String) As System.Data.DataTable
            Dim TaskTable As System.Data.DataTable
            Dim reader1 As SqlDataReader
            Dim reader2 As SqlDataReader
            Dim counter As Integer = 1

            TaskTable = New System.Data.DataTable("TaskHistory")
            Try
                TaskTable.Columns.Add("DocumentID", Type.GetType("System.String"))
                TaskTable.Columns.Add("Initiator", Type.GetType("System.String"))
                TaskTable.Columns.Add("Supervisor", Type.GetType("System.String"))
                TaskTable.Columns.Add("Status", Type.GetType("System.String"))
                TaskTable.Columns.Add("Date", Type.GetType("System.String"))

                reader1 = GetTaskHistory(UserID, doctype)
                reader2 = GetOtherTasks(UserID, doctype)

                Dim row As DataRow

                If Not (reader2 Is Nothing) Then
                    While reader2.Read
                        row = TaskTable.NewRow()
                        row(0) = reader2.GetString(0)
                        row(1) = reader2.GetString(1)
                        row(2) = reader2.GetString(2)
                        row(3) = reader2.GetString(3)
                        row(4) = reader2.GetDateTime(4).ToString("dd/MM/yyyy")
                        TaskTable.Rows.Add(row)
                    End While
                    reader2.Close()
                End If
                If Not (reader1 Is Nothing) Then
                    While reader1.Read
                        row = TaskTable.NewRow()
                        row(0) = reader1.GetString(0)
                        row(1) = reader1.GetString(1)
                        row(2) = reader1.GetString(2)
                        row(3) = reader1.GetString(3)
                        row(4) = reader1.GetDateTime(4).ToString("dd/MM/yyyy")
                        TaskTable.Rows.Add(row)
                    End While
                    reader1.Close()
                End If
                Return TaskTable
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return Nothing
            End Try
        End Function

        Public Shared Function GetAllOriginatedTasks(ByVal OrigUser As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks2", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = OrigUser
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Function GetMovedTaskInfo(ByVal TaskID As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTasks3", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 50).Value = TaskID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Sub SaveDocProperty(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveDocumentsProperties", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocPropID", SqlDbType.Int).Value = docpropid
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 200).Value = type.ToString
                cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 200).Value = value.ToString
                cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 200).Value = ""
                cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = docID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype.ToString
                cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 100).Value = owner
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub SaveErrorMsg(ByVal docType As String, ByVal msg As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveErrorMsg", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 300).Value = msg
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub DeleteErrorMsg(ByVal docType As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spDelErrorMsg", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Function GetErrorMsg(ByVal docType As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim errorList As String = String.Empty
                Dim reader As SqlDataReader
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetErrorMsg", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader
                While reader.Read
                    errorList += "; " + reader.GetValue(0)
                End While
                reader.Close()
                errorList = errorList.Trim(";")
                errorList = errorList.Trim()
                Return errorList
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return String.Empty
            End Try
        End Function

        Public Shared Sub SaveUserProperty(ByVal UserID As String, ByVal Type As Object, ByVal Value As Object, ByVal entrydate As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spUserProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = Type.ToString
                cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = Value.ToString
                cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = entrydate
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub SaveUserAggProperty(ByVal aggproperty As String, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spUserAggProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Property", SqlDbType.NVarChar, 100).Value = aggproperty
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = doctype

                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Function GetUserAggProp(ByVal docType As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetAggProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType

                reader = cmd.ExecuteReader()
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try

        End Function

        Public Shared Sub ResetUserProperties(ByVal owner As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("ResetUserProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 50).Value = owner
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub ResetUserProperties2(ByVal docType As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("ResetUserProperties", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub ResetUserProperties(ByVal docType As String, ByVal userid As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spResetUserProperties", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 50).Value = userid
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Function VerifyOwner(ByVal owner As String, ByVal docType As String) As Boolean
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As Boolean = False
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spVerifyOwner", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 50).Value = owner
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = True
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return False
            End Try

        End Function

        Public Shared Function VerifyUser(ByVal userID As String, ByVal doctype As String) As Boolean
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As Boolean = False
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spVerifyUser", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = True
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return False
            End Try

        End Function

        Public Shared Function VerifyTask(ByVal taskID As String, ByVal status As String, ByVal doctype As String) As Boolean
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As Boolean = False
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spVerifyTask", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = taskID
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = True
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return False
            End Try

        End Function

        Public Shared Sub ResetAssignment()
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("ResetAssignment", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub ResetUserAggProp(ByVal docType As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("ResetUserAggProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Function GetAggregRule(ByVal docType As String, ByVal rulename As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetAggregRule", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
                cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetTaskSender(ByVal taskid As String, ByVal userid As String, ByVal doctype As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spTaskSender", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = taskid
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userid
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function Getcondition(ByVal docType As String, ByVal rulename As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty

                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetCondition", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
                cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function getAction(ByVal docType As String, ByVal rulename As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetAction", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
                cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetRequestStatus(ByVal docType As String, ByVal origUser As String, ByVal reqName As String, ByVal entdate As Date) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetStatus", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = origUser
                cmd.Parameters.Add("@ReqName", SqlDbType.NVarChar, 100).Value = reqName
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = entdate

                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetTaskStatus(ByVal taskid As String, ByVal userid As String, ByVal doctype As String) As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTaskStatus", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = taskid
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userid
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader()
                While (reader.Read)
                    returnval = reader.GetString(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Function GetPropVal(ByVal prop As String) As Double
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As Double
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("GetUserProp", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = prop

                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetDecimal(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return 0.0
            End Try

        End Function

        Public Shared Function GetUserProp(ByVal owner As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("GetUserProperties", conn)
                cmd.Parameters.Add("@owner", SqlDbType.NVarChar, 100).Value = owner
                cmd.CommandType = CommandType.StoredProcedure

                reader = cmd.ExecuteReader()
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Function GetUserConnection(ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetUserConn", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader()
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Function GetUserPropValues(ByVal userid As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("GetUserPropertiesVals", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = userid
                reader = cmd.ExecuteReader()
                Return reader
                reader.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Function GetTaskHistory(ByVal userid As String, ByVal doctype As String) As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim reader2 As SqlDataReader
                Dim tasks As String = String.Empty
                Dim returnval As String = String.Empty
                Dim cmd2 As SqlCommand
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetTaskID", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = userid
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                reader = cmd.ExecuteReader()
                While reader.Read
                    tasks = tasks & ",'" & reader.GetString(0) & "'"
                End While
                tasks = tasks.Trim(",")
                reader.Close()

                cmd2 = New SqlCommand("SELECT TaskID AS [Document ID],OrigUser AS [Originating User ID],UserID AS [Supervisor ID],Status,Date AS [Activity Date] FROM Task WHERE TaskID IN (" & tasks & ") AND UserID <> '" & userid & "' AND OrigUser <> UserID AND DocType='" & doctype & "' ORDER BY TaskID,Date", conn)
                cmd2.CommandType = CommandType.Text
                cmd2.CommandTimeout = 0
                reader2 = cmd2.ExecuteReader
                If reader2.HasRows Then
                    Return reader2
                Else
                    Return Nothing
                End If
                reader2.Close()
                conn.Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Sub saveRule(ByVal rule As Rule)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveRule", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@RuleID", SqlDbType.Int).Value = rule.Rule_ID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = rule.Doc_Type
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = rule.Rule_Name
                cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = rule.Rule_Action
                cmd.Parameters.Add("@Condition", SqlDbType.NVarChar, 2000).Value = rule.Rule_Condition
                cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 250).Value = rule.Error_Msg
                cmd.Parameters.Add("@Precedence", SqlDbType.Int).Value = rule.Rule_Precedence
                cmd.Parameters.Add("@ElseAction", SqlDbType.NVarChar, 100).Value = rule.Else_Action
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Function getAssignment() As SqlDataReader
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetAssignments", conn)
                cmd.CommandType = CommandType.StoredProcedure
                Return cmd.ExecuteReader()
                cmd.ExecuteReader().Close()
            Catch ex As Exception
                Dim reader As SqlDataReader = Nothing
                DAL.ErrorMessage = ex.Message
                Return reader
                reader.Close()
            End Try
        End Function

        Public Shared Sub saveAssignment(ByVal assign As Assignment)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveAssignment", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@AssID", SqlDbType.Int).Value = assign.AssID
                cmd.Parameters.Add("@Task", SqlDbType.NVarChar, 100).Value = assign.Task
                cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = assign.docID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = assign.Doc_Type
                cmd.Parameters.Add("@AssignedUser", SqlDbType.NVarChar, 100).Value = assign.AssignedUser
                cmd.Parameters.Add("@OriginatingUser", SqlDbType.NVarChar, 100).Value = assign.OriginatingUser
                cmd.Parameters.Add("@ParentTask", SqlDbType.NVarChar, 100).Value = assign.ParentTask
                cmd.Parameters.Add("@State", SqlDbType.NVarChar, 100).Value = assign.State
                cmd.Parameters.Add("@Request", SqlDbType.NVarChar, 100).Value = assign.RequestName
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub SaveRequest(ByVal request As Request)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveRequest", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = request.Doc_Type
                cmd.Parameters.Add("@ReqName", SqlDbType.NVarChar, 50).Value = request.RequestName
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = request.Status
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = request.OriginatingUser
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = request.EntryDate
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = request.Remark
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.InnerException.ToString
            End Try

        End Sub

        Public Shared Sub SaveTask(ByVal task As Task)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveTask", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = task.TaskID
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = task.OriginatingUser
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = task.UserID
                cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = task.Action
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = task.Status
                cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 1000).Value = task.Message
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = task.EntryDate
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = task.DocumentType
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub saveAssignmentLog(ByVal assign As Assignment, ByVal entrydate As Date)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spSaveAssignmentLog", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@AssID", SqlDbType.Int).Value = assign.AssID
                cmd.Parameters.Add("@Task", SqlDbType.NVarChar, 100).Value = assign.Task
                cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = assign.docID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = assign.Doc_Type
                cmd.Parameters.Add("@AssignedUser", SqlDbType.NVarChar, 100).Value = assign.AssignedUser
                cmd.Parameters.Add("@OriginatingUser", SqlDbType.NVarChar, 100).Value = assign.OriginatingUser
                cmd.Parameters.Add("@ParentTask", SqlDbType.NVarChar, 100).Value = assign.ParentTask
                cmd.Parameters.Add("@State", SqlDbType.NVarChar, 100).Value = assign.State
                cmd.Parameters.Add("@Request", SqlDbType.NVarChar, 100).Value = assign.RequestName
                cmd.Parameters.Add("@Date", SqlDbType.DateTime, 100).Value = entrydate
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub SaveUserConnection(ByVal connectStr As String, ByVal tablename As String, ByVal fieldnames As String, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spUserConn", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@ConnectString", SqlDbType.NVarChar, 100).Value = connectStr
                cmd.Parameters.Add("@Tablename", SqlDbType.NVarChar, 50).Value = tablename
                cmd.Parameters.Add("@Fieldnames", SqlDbType.NVarChar, 200).Value = fieldnames
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Function GetDocID(ByVal docType As String) As Integer
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As Integer
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetDocID", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetInt32(0)
                End While
                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return 0
            End Try
        End Function

        Public Shared Function GetDocType() As String
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                Dim reader As SqlDataReader
                Dim returnval As String = String.Empty
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spGetDocType", conn)
                cmd.CommandType = CommandType.StoredProcedure
                reader = cmd.ExecuteReader()

                While (reader.Read)
                    returnval = reader.GetString(0)
                End While

                reader.Close()
                Return returnval
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
                Return ""
            End Try
        End Function

        Public Shared Sub UpdateCurrentDoc(ByVal docType As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spUpdateCurDoc", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub ResetAllAssignments()
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spResetAllAssign", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub DeleteRequests()
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spDeleteRequest", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub DeleteTasks()
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spDeleteTask", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub DeleteUserTasks(ByVal userID As String, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spDeleteUserTasks", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub DeleteUserTask(ByVal taskid As String, ByVal origuser As String, ByVal userID As String, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spDeleteUserTask", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = taskid
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = origuser
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userID
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub UpdateTaskStatus(ByVal status As String, ByVal taskID As String, ByVal origUser As String, ByVal userID As String, ByVal entdate As Date, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If
                cmd = New SqlCommand("spUpdTaskStatus", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status
                cmd.Parameters.Add("@TaskID", SqlDbType.NVarChar, 100).Value = taskID
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = origUser
                cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userID
                'cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = action
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = entdate
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

        Public Shared Sub UpdateRequestStatus(ByVal origUser As String, ByVal status As String, ByVal request As String, ByVal entdate As Date, ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If
                cmd = New SqlCommand("spUpdRequestStatus", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status
                cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar, 50).Value = origUser
                cmd.Parameters.Add("@Request", SqlDbType.NVarChar, 50).Value = request
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = entdate
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub AddDocuments(ByVal doctype As String)
            Try
                DAL.ErrorMessage = String.Empty
                Dim conn As New SqlConnection(connstr)
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spAddDocuments", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                DAL.ErrorMessage = ex.Message
            End Try

        End Sub

    End Class

    Public Class DocProperties
        Private Shared valindex As Integer
        Private Shared docprop As New DocProperty
        'Private storex As New DAL
        Private Shared errormsg As String = String.Empty
        Private Shared userrule As New Rule

        Public Shared Sub AddProperties(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
            Try
                DocProperties.ErrorMessage = String.Empty
                DAL.SaveDocProperty(type, value, docID, doctype, docpropid, owner)
            Catch ex As Exception
                DocProperties.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Property ErrorMessage() As String
            Get
                Return errormsg
            End Get
            Set(ByVal value As String)
                errormsg = value
            End Set
        End Property

        Public Shared Sub GenerateProperties(ByVal connectionString As String, ByVal tablename As String, ByVal properties As String, ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
            Try
                DocProperties.ErrorMessage = String.Empty
                Dim rule As String = String.Empty
                Dim str As String = String.Empty
                Dim str2 As String = String.Empty
                'storex = New DAL
                'Dim doctype As String = DAL.GetDocType

                rule = propRule

                If (InStr(rule, "Doc.Owner") > 0) Then
                    rule = Replace(rule, "Doc.Owner", "'" & dataowner & "'")
                End If

                If (InStr(rule, "Doc.") > 0) Then
                    While InStr(rule, "Doc.") > 0
                        str = Mid(rule, InStr(rule, "Doc."))

                        If (InStr(str.Trim, " ") > 0) Then
                            str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                        Else
                            str2 = Mid(str, InStr(str, ".") + 1)
                        End If
                        rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                    End While
                End If

                If InStr(connectionString, "HOBANK1") > 0 Then
                    Dim connect As New OracleConnection(connectionString)
                    Dim command As OracleCommand
                    Dim reader As OracleDataReader

                    command = New OracleCommand("SELECT DISTINCT " & properties & " FROM " & tablename & " WHERE " & rule, connect)

                    If connect.State = ConnectionState.Closed Then
                        connect.Open()
                    End If
                    command.CommandType = CommandType.Text
                    command.CommandTimeout = 0
                    reader = command.ExecuteReader()
                    While (reader.Read)
                        For i As Integer = 0 To reader.FieldCount - 1
                            If DAL.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 0, dataowner)
                            Else
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 1, dataowner)
                            End If
                        Next
                    End While
                    reader.Close()
                    connect.Close()
                Else
                    Dim conn As New SqlConnection(connectionString)
                    Dim cmd As SqlCommand
                    Dim reader As SqlDataReader
                    cmd = New SqlCommand("SELECT " & properties & " FROM " & tablename & " WHERE " & rule, conn)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 0
                    reader = cmd.ExecuteReader()
                    While (reader.Read)
                        For i As Integer = 0 To reader.FieldCount - 1
                            If DAL.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 0, dataowner)
                            Else
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 1, dataowner)
                            End If
                        Next
                    End While
                    reader.Close()
                    conn.Close()
                End If
            Catch ex As Exception
                DocProperties.ErrorMessage = ex.Message
            End Try
        End Sub

        Public Shared Sub GenerateDocProperties(ByVal connectionString As String, ByVal tablename As String, ByVal fieldnames As String, ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
            Try
                DocProperties.ErrorMessage = String.Empty
                Dim rule As String = String.Empty
                Dim str As String = String.Empty
                Dim str2 As String = String.Empty
                'storex = New DAL
                'Dim doctype As String = DAL.GetDocType

                rule = propRule

                If (InStr(rule, "Doc.Owner") > 0) Then
                    rule = Replace(rule, "Doc.Owner", "'" & dataowner & "'")
                End If

                If (InStr(rule, "Doc.") > 0) Then
                    While InStr(rule, "Doc.") > 0
                        str = Mid(rule, InStr(rule, "Doc."))

                        If (InStr(str.Trim, " ") > 0) Then
                            str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                        Else
                            str2 = Mid(str, InStr(str, ".") + 1)
                        End If
                        rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                    End While
                End If

                If InStr(connectionString, "HOBANK1") > 0 Then
                    Dim connect As New OracleConnection(connectionString)
                    Dim command As OracleCommand
                    Dim reader As OracleDataReader

                    command = New OracleCommand("SELECT DISTINCT " & fieldnames & " FROM " & tablename & " WHERE " & rule, connect)

                    If connect.State = ConnectionState.Closed Then
                        connect.Open()
                    End If
                    command.CommandType = CommandType.Text
                    command.CommandTimeout = 0
                    reader = command.ExecuteReader()
                    While (reader.Read)
                        For i As Integer = 0 To reader.FieldCount - 1
                            If DAL.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 0, dataowner)
                            Else
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 1, dataowner)
                            End If
                        Next
                    End While
                    reader.Close()
                    connect.Close()
                Else
                    Dim conn As New SqlConnection(connectionString)
                    Dim cmd As SqlCommand
                    Dim reader As SqlDataReader
                    cmd = New SqlCommand("SELECT " & fieldnames & " FROM " & tablename & " WHERE " & rule, conn)

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    cmd.CommandType = CommandType.Text
                    cmd.CommandTimeout = 0
                    reader = cmd.ExecuteReader()
                    While (reader.Read)
                        For i As Integer = 0 To reader.FieldCount - 1
                            If DAL.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 0, dataowner)
                            Else
                                AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DAL.GetDocID(doctype).ToString, doctype, 1, dataowner)
                            End If
                        Next
                    End While
                    reader.Close()
                    conn.Close()
                End If
            Catch ex As Exception
                DocProperties.ErrorMessage = ex.Message
            End Try
        End Sub
    End Class

End Class
