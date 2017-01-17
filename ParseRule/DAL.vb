Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class DAL
    Private conn As New SqlConnection("Data Source=10.0.0.210;Initial Catalog=ORM;User ID=sa;password=system")
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

    Public Function getDocPropertiesByID(ByVal docID As Integer) As SqlDataReader
        Try
            cmd = New SqlCommand("spGetDocumentPropertiesByID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocID", SqlDbType.Int).Value = docID
            Return cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.ExecuteReader().Close()
        Catch ex As Exception
            Dim reader As SqlDataReader = Nothing
            Me.ErrorMessage = ex.Message
            Return reader
            reader.Close()
        End Try

    End Function

    Public Function getDocProperties(ByVal docType As String, ByVal type As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetDocumentsProperties", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@docType", SqlDbType.NVarChar).Value = docType
            cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = type
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

    Public Function getPropertiesVal(ByVal docType As String, ByVal type As String, ByVal owner As String) As String
        Try
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

    Public Function getAllDocProp(ByVal docType As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetDocProperties", conn)
            cmd.Parameters.Add("@docType", SqlDbType.NVarChar).Value = docType
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

    Public Function GetAllRequests(ByVal origUser As Integer) As SqlDataReader
        Try
            Dim reader As SqlDataReader

            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetRequests", conn)
            cmd.Parameters.Add("@OrigUser", SqlDbType.Int).Value = origUser
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

    Public Function GetAllTasks(ByVal origUser As Integer) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetTasks", conn)
            cmd.Parameters.Add("@OrigUser", SqlDbType.Int).Value = origUser
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

    Public Sub saveDocProperty(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
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

    Public Sub saveUserProperty(ByVal UserID As String, ByVal Type As Object, ByVal Value As Object, ByVal entrydate As String)
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

    Public Sub saveUserAggProperty(ByVal aggproperty As String, ByVal doctype As String)
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

    Public Function VerifyOwner(ByVal owner As String, ByVal docType As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spVerifyOwner", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Owner", SqlDbType.NVarChar, 50).Value = owner
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 50).Value = docType
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

    Public Function getRule(ByVal docType As String, ByVal rulename As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetRule", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            cmd.Parameters.Add("@RuleName", SqlDbType.NVarChar).Value = rulename

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

    Public Function getAssignRule(ByVal docType As String, ByVal rulename As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetAssignRule", conn)
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

    Public Function getAggregRule(ByVal docType As String, ByVal rulename As String) As String
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

    Public Function getcondition(ByVal docType As String, ByVal rulename As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty

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
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function getAction(ByVal docType As String, ByVal rulename As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
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
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return ""
        End Try
    End Function

    Public Function getElseAction(ByVal docType As String, ByVal rulename As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetEAction", conn)
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

    Public Function GetRequestStatus(ByVal docType As String, ByVal origUser As Integer, ByVal reqName As String) As String
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = docType
            cmd.Parameters.Add("@OrigUser", SqlDbType.Int).Value = origUser
            cmd.Parameters.Add("@ReqName", SqlDbType.NVarChar, 100).Value = reqName

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

    Public Function GetPropVal(ByVal prop As String) As Double
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
        End Try

    End Function

    Public Function GetTask(ByVal user As String) As SqlDataReader
        Try
            Dim reader As SqlDataReader
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetTask", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = user

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

    Public Function GetPState() As SqlDataReader
        Try
            Dim reader As SqlDataReader
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetState", conn)
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

    Public Function GetUserProp() As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("GetUserProperties", conn)
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

    Public Function GetUserConnection() As SqlDataReader
        Try
            Dim reader As SqlDataReader
            Dim returnval As String = String.Empty
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetUserConn", conn)
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
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spSaveRule", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@RuleID", SqlDbType.Int).Value = rule.Rule_ID
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = rule.Doc_Type
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = rule.Rule_Name
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = rule.Rule_Action
            cmd.Parameters.Add("@Condition", SqlDbType.NVarChar, 250).Value = rule.Rule_Condition
            cmd.Parameters.Add("@ErrorMsg", SqlDbType.NVarChar, 250).Value = rule.Error_Msg
            cmd.Parameters.Add("@Precedence", SqlDbType.Int).Value = rule.Rule_Precedence
            cmd.Parameters.Add("@ElseAction", SqlDbType.NVarChar, 100).Value = rule.Else_Action
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Function getAssignment() As SqlDataReader
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
        End Try
    End Function

    Public Sub saveAssignment(ByVal assign As Assignment)
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
        End Try

    End Sub

    Public Sub SaveRequest(ByVal request As Request)
        Try
            cmd = New SqlCommand("spSaveRequest", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar, 100).Value = request.Doc_Type
            cmd.Parameters.Add("@ReqName", SqlDbType.NVarChar, 100).Value = request.RequestName
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = request.Status
            cmd.Parameters.Add("@OrigUser", SqlDbType.Int).Value = request.OriginatingUser
            cmd.Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = request.Remark
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub SaveTask(ByVal task As Task)
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spSaveTask", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@OrigUser", SqlDbType.Int).Value = task.OriginatingUser
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = task.UserID
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 100).Value = task.Action
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = task.Status
            cmd.Parameters.Add("@Message", SqlDbType.NVarChar, 1000).Value = task.Message
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Sub saveAssignmentLog(ByVal assign As Assignment, ByVal entrydate As Date)
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
        End Try
    End Sub

    Public Sub SaveUserConnection(ByVal connectStr As String, ByVal tablename As String)
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spUserConn", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ConnectString", SqlDbType.NVarChar, 100).Value = connectStr
            cmd.Parameters.Add("@Tablename", SqlDbType.NVarChar, 50).Value = tablename

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    Public Function GetDocID(ByVal docType As String) As Integer
        Try
            Dim reader As SqlDataReader
            Dim returnval As Integer
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spGetDocID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@DocType", SqlDbType.NVarChar).Value = docType
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While (reader.Read)
                returnval = reader.GetInt32(0)
            End While
            reader.Close()
            Return returnval
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return 0
        End Try
    End Function

    Public Function GetDocType() As String
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
        End Try
    End Function

    Public Sub UpdateCurrentDoc(ByVal docType As String)
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
        End Try
    End Sub

    Public Sub ResetAllAssignments()
        Try
            If (conn.State = ConnectionState.Closed) Then
                conn.Open()
            End If

            cmd = New SqlCommand("spResetAllAssign", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub
End Class
