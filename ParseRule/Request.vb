Public Class Request
    Private storex As New DAL
    Private docType As String
    Private reqName As String
    Private stat As String
    Private origUser As String
    Private remk As String
    Private errormsg As String
    Private parser As New RuleParser
    Private assuser As New AssignedUsers
    Private task As New Task
    Private assign As New Assignment

    Public Property Doc_Type() As String
        Get
            Return docType
        End Get
        Set(ByVal newvalue As String)
            docType = newvalue
        End Set
    End Property

    Public Property RequestName() As String
        Get
            Return reqName
        End Get
        Set(ByVal value As String)
            reqName = value
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

    Public Property OriginatingUser() As Integer
        Get
            Return origUser
        End Get
        Set(ByVal value As Integer)
            origUser = value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return remk
        End Get
        Set(ByVal value As String)
            remk = value
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

    Public Sub SaveRequest(ByVal doctype As String, ByVal reqname As String, ByVal status As String, ByVal origuser As Integer, ByVal remark As String)
        Try
            Me.Doc_Type = doctype
            Me.RequestName = reqname
            Me.Status = status
            Me.OriginatingUser = origuser
            Me.Remark = remark

            storex.SaveRequest(Me)
            ProcessRequest(Me.RequestName)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Sub ProcessRequest(ByVal requestname As String)
        Try
            Dim result As New ArrayList
            Dim action As String = String.Empty
            Dim user_rule As String = String.Empty
            Dim msgaction As String = String.Empty
            Dim origrule As String = String.Empty
            Dim assignedUsers As New ArrayList
            assignedUsers.Clear()
            result.Clear()
            If (storex.getcondition(Me.Doc_Type, requestname) = String.Empty) Then

            Else
                parser.Execute(storex.getcondition(Me.Doc_Type, requestname), Me.Doc_Type, Me.OriginatingUser)
                result = parser.GetResults()

                If (result.Count > 0) Then
                    action = result.Item(0)
                    origrule = result.Item(2)
                    If (InStr(result.Item(1), """") > 0) Then
                        msgaction = result.Item(1)
                    Else
                        user_rule = result.Item(1)
                        assignedUsers = assuser.GetAssignedUsers(user_rule)

                        For i As Integer = 0 To assignedUsers.Count - 1
                            task.SaveTask(Me.OriginatingUser, assignedUsers(i), action, "Pending", "")
                        Next
                    End If
                End If
                assign.Assignment(Me.OriginatingUser, Me.Doc_Type, action, origrule, msgaction)
            End If

        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub
End Class
