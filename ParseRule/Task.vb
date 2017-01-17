Public Class Task
    Private user_id As Integer
    Private actn As String
    Private stat As String
    Private msg As String
    Private storex As DAL
    Private errormsg As String
    Private origUser As Integer

    Public Property OriginatingUser() As Integer
        Get
            Return origUser
        End Get
        Set(ByVal value As Integer)
            origUser = value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return user_id
        End Get
        Set(ByVal newvalue As Integer)
            user_id = newvalue
        End Set
    End Property

    Public Property Action() As String
        Get
            Return actn
        End Get
        Set(ByVal value As String)
            actn = value
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

    Public Property Message() As String
        Get
            Return msg
        End Get
        Set(ByVal value As String)
            msg = value
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

    Public Sub SaveTask(ByVal origusr As Integer, ByVal userid As Integer, ByVal action As String, ByVal status As String, ByVal message As String)
        Try
            Me.OriginatingUser = origusr
            Me.UserID = userid
            Me.Action = action
            Me.Status = status
            Me.Message = message

            storex.SaveTask(Me)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub
End Class
