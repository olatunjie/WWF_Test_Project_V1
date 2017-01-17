Public Class Document
    Private DocID As Integer
    Private DocType As Object
    Private currentType As Object
    Private currentValue As Object
    Private storex As New DAL
    Private docprop As New DocProperty
    Private assign As New Assignment
    Private parameters As New ArrayList
    Private errormsg As String = String.Empty

    Public Sub New()

    End Sub

    Public Property Doc_ID() As Integer
        Get
            Return DocID
        End Get
        Set(ByVal newvalue As Integer)
            DocID = newvalue
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

    Public Property Type() As Object
        Get
            Return currentType
        End Get
        Set(ByVal newvalue As Object)
            currentType = newvalue
        End Set
    End Property

    Public Property Value() As Object
        Get
            Return currentValue
        End Get
        Set(ByVal newvalue As Object)
            currentValue = newvalue
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

    Public Sub UpdateProperties(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object)
        Try
            Me.Doc_ID = docID
            Me.Doc_Type = doctype
            Me.Type = type
            Me.Value = value

            docprop.GetProperties(Me.Type, Me.Value, Me.Doc_ID, Me.Doc_Type)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try

    End Sub

    'Public Function RequestAssignment(ByVal docid As Integer, ByVal doctype As String) As ArrayList
    '    Try
    '        Me.Doc_ID = docid
    '        Me.Doc_Type = doctype
    '        parameters.Add(Me.Doc_ID)
    '        parameters.Add(Me.Doc_Type)
    '        parameters.Add("")
    '        parameters.Add(storex.getDocProperties(Me.Doc_Type, "UserID"))
    '        parameters.Add("0")
    '        parameters.Add("Pending")
    '        Return parameters
    '    Catch ex As Exception
    '        Me.ErrorMessage = ex.Message
    '        Return parameters
    '    End Try

    'End Function
End Class
