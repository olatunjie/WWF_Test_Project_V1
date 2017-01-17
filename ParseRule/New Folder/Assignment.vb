Imports System.Data
Imports System.Data.SqlClient

Public Class Assignment
    Private DocType As Object
    Private m_assID As Integer
    Private m_task As String
    Private m_assignedUser As String
    Private m_originatingUser As String
    Private m_parenttask As Object
    Private m_state As String
    Private m_request As String
    Public docID As Integer
    Private storex As New DAL
    Private assuser As New AssignedUsers
    Private assignedusers As New ArrayList
    Private sqlparser As New Rule
    Private reader As SqlDataReader
    Private readerstate As SqlDataReader
    Private propvalue As New ArrayList
    Private curstate As String = String.Empty
    Private parser As AggParser
    Private aggresult As Boolean
    Private aggresultTest As Boolean
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

    Public Property Request() As String
        Get
            Return m_request
        End Get
        Set(ByVal value As String)
            m_request = value
        End Set
    End Property

    Public Sub Assignment(ByVal DocID As Integer, ByVal DocType As String)
        Try
            parser = New AggParser
            propvalue.Clear()
            propvalue = assuser.GetAggregation()
            parser.Run(storex.getAggregRule(DocType, "ApprovalRule"), propvalue)
            aggresult = Boolean.Parse(parser.GetResult())

            For userindex As Integer = 0 To assignedusers.Count - 1
                Me.AssID = 0
                reader = storex.GetTask(assignedusers.Item(userindex))
                While (reader.Read)
                    Me.Task = reader.GetString(0)
                    Me.ParentTask = reader.GetString(1)
                End While
                reader.Close()

                readerstate = storex.GetPState()
                While (readerstate.Read)
                    curstate = readerstate.GetString(0)
                End While
                readerstate.Close()

                Me.Doc_ID = DocID
                Me.Doc_Type = DocType
                Me.AssignedUser = assignedusers.Item(userindex)

                Me.OriginatingUser = storex.getDocProperties(storex.GetDocType, "UserID")

                If aggresult Then
                    Me.State = "Approved"

                    parser = New AggParser
                    parser.Run(storex.getAggregRule(DocType, "ApprovalRule"), propvalue)

                    aggresultTest = Boolean.Parse(parser.GetResult())

                    If (Not aggresultTest) Then
                        Me.ErrorMessage = "Error 211: This user does not satisfy the rule(s) for approval. " & storex.getAggregRule(DocType, "ApprovalRule")
                        Me.State = curstate
                    End If
                Else
                    Me.ErrorMessage = "Error 210: No user satisfies the rule(s) for approval. " & storex.getAggregRule(DocType, "ApprovalRule")
                    Me.State = curstate
                End If

                Me.Request = String.Empty

                Me.SaveAssignment()
                Me.SaveAssignmentLog()
            Next
        Catch ex As Exception
            Me.ErrorMessage = "No Assigned User! " & parser.GetErrorMsg
        End Try
    End Sub

    Public Sub AssUsers()
        Try
            assignedusers = assuser.GetAssignedUsers
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub AssignmentDoc(ByVal Ass_ID As Integer, ByVal Task As String, ByVal DocID As Integer, ByVal DocType As String, ByVal AssignedUser As String, ByVal OriginatingUser As String, ByVal ParentTask As String, ByVal State As String, ByVal Request As String)
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
            storex.ResetAllAssignments()
            Me.SaveAssignment()
            Me.SaveAssignmentLog()
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveAssignment()
        Try
            storex.saveAssignment(Me)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub SaveAssignmentLog()
        Try
            storex.saveAssignmentLog(Me, DateTime.Now)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub New()

    End Sub
End Class