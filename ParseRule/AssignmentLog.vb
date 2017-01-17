Public Class AssignmentLog
    Private DocType As Object
    Private m_assID As Integer
    Private m_task As String
    Private m_assignedUser As String
    Private m_originatingUser As String
    Private m_parenttask As Object
    Private m_state As String
    Private m_request As String
    Private m_date As String
    Public docID As Integer
    Private storex As New DAL
    Private errormsg As String = String.Empty

    Public Property Doc_ID() As Integer
        Get
            Return docID
        End Get
        Set(ByVal newvalue As Integer)
            docID = newvalue
        End Set
    End Property

    Public Property Doc_Type() As Object
        Get
            Return DocType
        End Get
        Set(ByVal newvalue As Object)
            DocType = newvalue
        End Set
    End Property

    Public Property AssID() As Integer
        Get
            Return m_assID
        End Get
        Set(ByVal value As Integer)
            m_assID = value
        End Set
    End Property

    Public Property Task() As String
        Get
            Return m_task
        End Get
        Set(ByVal value As String)
            m_task = value
        End Set
    End Property

    Public Property AssignedUser() As String
        Get
            Return m_assignedUser
        End Get
        Set(ByVal value As String)
            m_assignedUser = value
        End Set
    End Property

    Public Property OriginatingUser() As String
        Get
            Return m_originatingUser
        End Get
        Set(ByVal value As String)
            m_originatingUser = value
        End Set
    End Property

    Public Property ParentTask() As String
        Get
            Return m_parenttask
        End Get
        Set(ByVal value As String)
            m_parenttask = value
        End Set
    End Property

    Public Property State() As String
        Get
            Return m_state
        End Get
        Set(ByVal value As String)
            m_state = value
        End Set
    End Property

    Public Property Request() As String
        Get
            Return m_request
        End Get
        Set(ByVal value As String)
            m_request = value
        End Set
    End Property

    Public Property EntryDate() As String
        Get
            Return m_date
        End Get
        Set(ByVal value As String)
            m_date = value
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

    Public Sub Assignment(ByVal Ass_ID As Integer, ByVal Task As String, ByVal DocID As Integer, ByVal DocType As String, ByVal AssignedUser As String, ByVal OriginatingUser As String, ByVal ParentTask As String, ByVal State As String, ByVal Request As String, ByVal entrydate As String)
        Try
            Me.AssID = Ass_ID
            Me.Task = Task
            Me.Doc_ID = DocID
            Me.Doc_Type = DocType
            Me.AssignedUser = AssignedUser
            Me.OriginatingUser = OriginatingUser
            Me.ParentTask = ParentTask
            Me.State = State
            Me.Request = Request
            Me.EntryDate = entrydate
            Me.SaveAssignmentLog()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveAssignmentLog()
        Try
            storex.saveAssignmentLog(Me)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub New()
    End Sub
End Class