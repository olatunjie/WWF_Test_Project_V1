Public Class AssignedUsers
    Private users As New Users
    Private userslist As New ArrayList
    Private aggreglist As New ArrayList
    Private storex As New DAL
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

    Public Function GetAssignedUsers() As ArrayList
        Try
            Dim doctype As String = String.Empty
            Dim usrule As String = String.Empty
            doctype = storex.GetDocType
            usrule = storex.getAssignRule(doctype, "RMUsers")
            Dim reader As SqlClient.SqlDataReader

            reader = storex.GetUserConnection
            While (reader.Read)
                users.GenerateUser(reader.GetString(0), reader.GetString(1), usrule)

                Exit While
            End While
            userslist = users.UserProperties()
            Return userslist
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return New ArrayList
        End Try

    End Function

    Public Function GetAggregation() As ArrayList
        Try
            aggreglist = users.AggregateProp()
            Return aggreglist
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
            Return New ArrayList
        End Try
    End Function
End Class
