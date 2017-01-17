Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class DAL
    Private conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
    'Private conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
    Private cmd As SqlCommand
    Private properties As New ArrayList
    Private errormsg As String = String.Empty

    Sub New()

    End Sub

    Public Property ErrorMessage() As String
        Get
            Return errormsg
        End Get
        Set(ByVal value As String)
            errormsg = value
        End Set
    End Property

    Public Function GetDocProperties(ByVal docType As String, ByVal type As String, ByVal owner As String) As String
        Try
            Me.ErrorMessage = String.Empty
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetValue(2)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function GetPropertiesVal(ByVal docType As String, ByVal type As String, ByVal owner As Object) As String
        Try
            Me.ErrorMessage = String.Empty
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetValue(2)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function GetAllDocProp(ByVal docType As String) As SqlDataReader
        Try
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
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try

    End Function

    Public Sub SaveErrorMsg(ByVal docType As String, ByVal msg As String)
        Try
            Me.ErrorMessage = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spSaveErrorMsg", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
            cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 300).Value = msg
            cmd.CommandTimeout = 0

            If msg.Trim <> String.Empty Then
                cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub DeleteErrorMsg(ByVal docType As String)
        Try
            Me.ErrorMessage = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spDelErrorMsg", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Function GetErrorMsg(ByVal docType As String) As String
        Try
            Me.ErrorMessage = String.Empty
            Dim errorList As String = String.Empty
            Dim reader As SqlDataReader
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
            Me.ErrorMessage = ex.Message
            Return String.Empty
        End Try
    End Function

    Public Function GetRules() As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetRules", conn)
            cmd.CommandType = CommandType.StoredProcedure
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            reader.Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try
    End Function

    Public Function GetRequest(ByVal owner As Object, ByVal filter As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty

            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spRequestName", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@OrigUser", SqlDbType.NVarChar).Value = owner
            cmd.Parameters.Add("@Filter", SqlDbType.NVarChar).Value = filter
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetValue(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function GetAllRequests(ByVal origUser As Object) As SqlDataReader
        Try
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
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try

    End Function

    Public Function GetAllTasks(ByVal UserID As Object, ByVal doctype As String) As SqlDataReader
        Try
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
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try

    End Function

    Public Sub SaveDocProperty(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
        Try
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
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub SaveUserProperty(ByVal UserID As String, ByVal Type As Object, ByVal Value As Object, ByVal entrydate As String)
        Try
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
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveUserAggProperty(ByVal aggproperty As String, ByVal doctype As String)
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spUserAggProp", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Property", SqlDbType.NVarChar, 100).Value = aggproperty
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = doctype

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Function GetUserAggProp(ByVal docType As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetAggProp", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            reader.Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try

    End Function

    Public Sub ResetUserProperties()
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("ResetUserProp", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub ResetUserProperties2(ByVal docType As String)
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("ResetUserProperties", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub ResetUserProperties(ByVal docType As String, ByVal userid As String)
        Try
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
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Function VerifyOwner(ByVal owner As String, ByVal docType As String) As Boolean
        Try
            Dim reader As SqlDataReader
            Dim returnval As Boolean = False
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spVerifyOwner", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 50).Value = owner
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = True
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return False
        End Try

    End Function

    Public Function VerifyUser(ByVal userID As String, ByVal doctype As String) As Boolean
        Try
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = True
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return False
        End Try

    End Function

    Public Function VerifyTask(ByVal taskID As String, ByVal status As String, ByVal doctype As String) As Boolean
        Try
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = True
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return False
        End Try

    End Function

    Public Sub ResetAssignment()
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("ResetAssignment", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub ResetUserAggProp(ByVal docType As String)
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("ResetUserAggProp", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Function GetAggregRule(ByVal docType As String, ByVal rulename As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetAggregRule", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function GetTaskSender(ByVal taskid As String, ByVal userid As String, ByVal doctype As String) As String
        Try
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function GetCondition(ByVal docType As String, ByVal rulename As String) As String
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Dim returnval As String = String.Empty
        Try
            Dim reader As SqlDataReader


            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetCondition", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()
            'Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            returnval = ""
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
        Return returnval
    End Function

    Public Function GetAction(ByVal docType As String, ByVal rulename As String) As String
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Dim returnval As String = String.Empty
        Try
            Dim reader As SqlDataReader

            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetAction", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()
            'Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            returnval = ""
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
        Return returnval
    End Function

    Public Function GetRequestStatus(ByVal docType As String, ByVal origUser As String, ByVal reqName As String, ByVal entdate As Date) As String
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Dim returnval As String = String.Empty
        Try
            Dim reader As SqlDataReader

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

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()

        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            returnval = ""
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
        Return returnval
    End Function

    Public Function GetTaskStatus(ByVal taskid As String, ByVal userid As String, ByVal doctype As String) As String
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While (reader.Read)
                returnval = reader.GetString(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Function

    Public Function GetPropVal(ByVal prop As String) As Double
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            Dim reader As SqlDataReader
            Dim returnval As Double
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("GetUserProp", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = prop

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetDecimal(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return 0.0
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Function

    Public Function GetUserProp(ByVal owner As String, ByVal doctype As String) As SqlDataReader

        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("GetUserProperties", conn)
            cmd.Parameters.Add("@owner", SqlDbType.NVarChar, 100).Value = owner
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
            cmd.CommandType = CommandType.StoredProcedure

            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            reader.Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try
    End Function

    Public Function GetUserConnection(ByVal doctype As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetUserConn", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = doctype
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            reader.Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try
    End Function

    Public Function GetUserPropValues(ByVal userid As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("GetUserPropertiesVals", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = userid
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Return reader
            reader.Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try
    End Function

    Public Sub saveRule(ByVal rule As Rule)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spSaveRule", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@RuleID", SqlDbType.Int).Value = rule.Rule_ID
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = rule.Doc_Type
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 100).Value = rule.Rule_Name
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 100).Value = rule.Rule_OldRule
            cmd.Parameters.Add("@Condition", SqlDbType.NVarChar, 2000).Value = rule.Rule_Condition
            cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 250).Value = rule.Error_Msg
            cmd.Parameters.Add("@Precedence", SqlDbType.Int).Value = rule.Rule_Precedence
            cmd.Parameters.Add("@ElseAction", SqlDbType.NVarChar, 100).Value = rule.Else_Action
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Sub

    Public Function getAssignment() As SqlDataReader
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetAssignments", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.ExecuteReader().Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Function

    Public Sub saveAssignment(ByVal assign As Assignment)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub SaveRequest(ByVal request As Request)
        'Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.InnerException.ToString
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Function SaveTask(ByVal task As Task) As Integer
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Dim returnVal As Integer = 0
        Try
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
            cmd.CommandTimeout = 0
            returnVal = cmd.ExecuteNonQuery()
            Return returnVal
        Catch ex As Exception
            If InStr(ex.Message, "IDX_TASK") > 0 Then
                If (conn.State = ConnectionState.Closed) Then
                    conn.Open()
                End If

                cmd = New SqlCommand("spUpdateTask", conn)
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
                cmd.CommandTimeout = 0
                returnVal = cmd.ExecuteNonQuery()
                Return returnVal
            Else
                Return 0
            End If
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Function

    Public Sub saveAssignmentLog(ByVal assign As Assignment, ByVal entrydate As Date)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Sub

    Public Sub SaveUserConnection(ByVal connectStr As String, ByVal tablename As String, ByVal fieldnames As String, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Function GetDocID(ByVal docType As String) As Integer
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            Dim reader As SqlDataReader
            Dim returnval As Integer
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetDocID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetInt32(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return 0
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Function

    Public Function GetDocType() As String
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetDocType", conn)
            cmd.CommandType = CommandType.StoredProcedure
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetString(0)
            End While

            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Function

    Public Sub UpdateCurrentDoc(ByVal docType As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spUpdateCurDoc", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Sub

    Public Sub ResetAllAssignments()
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spResetAllAssign", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub DeleteRequests()
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spDeleteRequest", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub DeleteTasks()
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spDeleteTask", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        Finally

            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub DeleteUserTasks(ByVal userID As String, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub DeleteUserTask(ByVal taskid As String, ByVal origuser As String, ByVal userID As String, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
            Me.ErrorMessage = ex.Message
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub UpdateTaskStatus(ByVal status As String, ByVal taskID As String, ByVal origUser As String, ByVal userID As String, ByVal entdate As Date, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

    Public Sub UpdateRequestStatus(ByVal origUser As String, ByVal status As String, ByVal request As String, ByVal entdate As Date, ByVal doctype As String)
        Dim conn As New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        Try
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
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try

    End Sub

End Class
