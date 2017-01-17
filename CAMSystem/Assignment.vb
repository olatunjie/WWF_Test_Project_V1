Imports System.Data
Imports System.Data.SqlClient

Public Class Assignment
    Private DocType As Object
    Private m_assID As Integer
    Private m_task As String
    Private m_assignedUser As String
    Private m_originatingUser As Object
    Private m_parenttask As Object
    Private m_state As String
    Private m_request As String
    Public docID As Integer
    'Private assuser As New AssignedUsers
    'Private assignedusers As New ArrayList
    'Private sqlparser As New Rule
    Private reader As SqlDataReader
    Private readerstate As SqlDataReader
    Private propvalue As New ArrayList
    Private curstate As String = String.Empty
    'Private parser As AggParser
    'Private aggresult As Boolean
    'Private aggresultTest As Boolean
    Private errormsg As String = String.Empty
    Private m_errormsg As String = String.Empty

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

    Public Property OriginatingUser() As Object
        Get
            Return m_originatingUser
        End Get
        Set(ByVal value As Object)
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

    Public Property ErrorMessage() As String
        Get
            Return m_errormsg
        End Get
        Set(ByVal value As String)
            m_errormsg = value
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

    Public Property RequestName() As String
        Get
            Return m_request
        End Get
        Set(ByVal value As String)
            m_request = value
        End Set
    End Property

    Public Sub Assignment(ByVal origUser As Object, ByVal DocType As String, ByVal req As String, ByVal origreq As String, ByVal remark As String)
        Try
            Dim storex As New DAL
            Dim request As New Request

            Me.Doc_Type = DocType
            Me.OriginatingUser = origUser
            If (origreq = String.Empty) Then
                Me.State = "Pending"
            Else
                'Me.State = storex.GetRequestStatus(DocType, origUser, origreq)

                If (Me.State = "Pending" Or Me.State = String.Empty) Then
                    Me.State = "Processing"
                ElseIf (Me.State = "Processing") Then
                    Me.State = "Assigned"
                Else
                End If
            End If

            Me.RequestName = req
            'request.SaveRequest(Me.Doc_Type, Me.RequestName, Me.State, Me.OriginatingUser, remark, DateTime.Now.Date)
            'Me.SaveAssignment()
            'Me.SaveAssignmentLog()
            'Next
        Catch ex As Exception
            Me.ErrorMessage = ex.Message.ToString
        End Try
    End Sub

    Public Sub AssUsers()
        Try
            'assignedusers = assuser.GetAssignedUsers
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub AssignmentDoc(ByVal origUser As Object, ByVal DocType As String, ByVal req As String, ByVal origreq As String, ByVal remark As String)
        Try
            Dim request As New Request
            'Me.AssID = Ass_ID
            'Me.Task = Task
            'Me.Doc_ID = DocID
            Me.Doc_Type = DocType
            Me.OriginatingUser = origUser
            If (origreq = String.Empty) Then
                Me.State = "Assigned"
            End If

            Me.RequestName = req
            'request.SaveRequest(Me.Doc_Type, Me.RequestName, Me.State, Me.OriginatingUser, remark, DateTime.Now.Date)
            'storex.ResetAllAssignments()
            'Me.SaveAssignment()
            'Me.SaveAssignmentLog()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveAssignment()
        Try
            'storex.saveAssignment(Me)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveAssignmentLog()
        Try
            'storex.saveAssignmentLog(Me, DateTime.Now)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub New()

    End Sub
End Class