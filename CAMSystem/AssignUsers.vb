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

    Public Function GetAssignedUsers(ByVal rulename As String, ByVal owner As String, ByVal doctype As String, ByVal constr As String, ByVal table As String, ByVal fields As String) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim usrule As String = String.Empty
            userslist.Clear()
            usrule = storex.GetCondition(doctype, rulename)
            users.GenerateUser(constr, table, fields, usrule, owner, doctype)
            userslist = users.UserProperties(owner, doctype)

            Return userslist
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAssignedUsers()")
            Return New ArrayList
        End Try
    End Function

    Public Function GetAssignedUsers(ByVal rulename As String, ByVal owner As String, ByVal doctype As String) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim usrule As String = String.Empty
            userslist.Clear()
            usrule = storex.GetCondition(doctype, rulename)
            users.GenerateUser(usrule, owner, doctype)
            userslist = users.UserProperties(owner, doctype)
            Return userslist
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAssignedUsers()")
            Return New ArrayList
        End Try

    End Function

    'Public Function GetAssignedUsers(ByVal owner As String, ByVal doctype As String, ByVal supervisorid As String, ByVal usingsupervisorid As Boolean) As ArrayList
    '    Try
    '        Me.ErrorMessage = String.Empty
    '        userslist.Clear()
    '        users.GenerateUser(owner, doctype, supervisorid, True)
    '        userslist = users.UserProperties(owner, doctype)
    '        Return userslist
    '    Catch ex As Exception
    '        storex = New DAL
    '        storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAssignedUsers()")
    '        Return New ArrayList
    '    End Try

    'End Function

    Public Function GetAssignedUsers(ByVal rulename As String, ByVal owner As String, ByVal doctype As String, ByVal usingsupervisorid As Boolean) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim usrule As String = String.Empty
            userslist.Clear()
            usrule = storex.GetCondition(doctype, rulename)
            users.GenerateUser(usrule, owner, doctype, True)
            userslist = users.UserProperties(owner, doctype)
            Return userslist
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAssignedUsers()")
            Return New ArrayList
        End Try

    End Function

    Public Function GetAssignedUsers(ByVal rulename As String, ByVal owner As String, ByVal doctype As String, ByVal getapprovals As Boolean, ByVal terminalpoint As Boolean) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            Dim usrule As String = String.Empty
            userslist.Clear()
            usrule = storex.GetCondition(doctype, rulename)
            users.GenerateUser(usrule, owner, doctype, True, True)
            userslist = users.UserProperties(owner, doctype)
            Return userslist
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAssignedUsers()")
            Return New ArrayList
        End Try

    End Function

    Public Function GetAggregation(ByVal doctype As String) As ArrayList
        Try
            Me.ErrorMessage = String.Empty
            aggreglist = users.AggregateProp(doctype)
            Return aggreglist
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- AssignedUsers::GetAggregation()")
            Return New ArrayList
        End Try
    End Function
End Class
