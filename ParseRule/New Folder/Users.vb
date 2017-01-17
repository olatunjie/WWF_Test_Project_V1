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

    Public Sub GenerateUser(ByVal connectionString As String, ByVal tablename As String, ByVal assUserRule As String)
        Try
            Dim conn As New SqlConnection(connectionString)
            Dim cmd As SqlCommand
            Dim reader As SqlDataReader
            Dim rule As String = String.Empty
            Dim index1 As Integer = 0
            Dim index2 As Integer = 0
            Dim index3 As Integer = 0
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty

            rule = assUserRule
            If (InStr(rule, "Doc.")) Then
                str = Mid(rule, InStr(rule, "Doc."))
                str2 = Mid(str, InStr(str, ".") + 1)
                rule = Replace(rule, str, userrule.SQLParser(str2))
            End If

            cmd = New SqlCommand("SELECT * FROM " & tablename & " WHERE " & rule, conn)

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 0
            reader = cmd.ExecuteReader()

            While (reader.Read)
                For i As Integer = 0 To reader.FieldCount - 1
                    If (count > 0) Then
                        docprop.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(storex.GetDocType).ToString, "User", 1, reader.GetString(1))
                        storex.saveUserProperty(reader.GetString(1), reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DateTime.Now)
                    Else
                        storex.ResetUserProperties()
                        docprop.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(storex.GetDocType).ToString, "User", 0, reader.GetString(1))
                        storex.saveUserProperty(reader.GetString(1), reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, DateTime.Now)
                    End If
                Next
            End While
            reader.Close()
            conn.Close()
            count += 1
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Function UserProperties() As ArrayList
        Try
            Dim reader As SqlClient.SqlDataReader
            reader = storex.GetUserProp
            userprops.Clear()

            While (reader.Read)
                userprops.Add(reader.GetString(0))
            End While
            reader.Close()
            Return userprops
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return New ArrayList
        End Try
    End Function

    Public Function AggregateProp() As ArrayList
        Try
            Dim reader As SqlDataReader
            Dim index As Integer = 0
            propval.Clear()
            props.Clear()
            reader = storex.GetUserAggProp(storex.GetDocType)
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
            Me.ErrorMessage = ex.Message
            Return New ArrayList
        End Try
    End Function
End Class
