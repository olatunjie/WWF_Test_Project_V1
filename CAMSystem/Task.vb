Public Class Task
    Private user_id As Object
    Private actn As String
    Private task_id As String
    Private stat As String
    Private msg As String
    Private entdate As Date
    Private storex As New DAL
    Private errormsg As String
    Private origUser As Object
    Private doc_type As String

    Public Property TaskID() As String
        Get
            Return task_id
        End Get
        Set(ByVal value As String)
            task_id = value
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

    Public Property UserID() As Object
        Get
            Return user_id
        End Get
        Set(ByVal newvalue As Object)
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

    Public Property DocumentType() As String
        Get
            Return doc_type
        End Get
        Set(ByVal value As String)
            doc_type = value
        End Set
    End Property

    Public Function SaveTask(ByVal taskid As String, ByVal origusr As String, ByVal userid As String, ByVal action As String, ByVal status As String, ByVal message As String, ByVal entrydate As Date, ByVal doctype As String) As Integer
        Try
            Dim returnVal As Integer = 0
            Me.ErrorMessage = String.Empty
            Me.TaskID = taskid
            Me.OriginatingUser = origusr
            Me.UserID = userid
            Me.Action = action
            Me.Status = status
            Me.Message = message
            Me.EntryDate = entrydate
            Me.DocumentType = doctype

            returnVal = storex.SaveTask(Me)
            If returnVal > 0 Then
                returnVal = 1
            Else
                returnVal = 0
            End If
            Return returnVal
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- Task::SaveTask()")
        End Try
    End Function
End Class