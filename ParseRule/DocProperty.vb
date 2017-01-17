Public Class DocProperty
    Private m_docPropID As Integer
    Private type As Object
    Private value As Object
    Public documentID As Object
    Public documentType As Object
    Private errorMsg As String = String.Empty
    Private prop As DocProperties

    Public Sub New()

    End Sub

    Public Sub GetProperties(ByVal newType As Object, ByVal newvalue As Object, ByVal docID As Integer, ByVal docType As Object)
        Try
            type = newType
            value = newvalue
            documentID = docID
            documentType = docType
            Me.DocPropID = 0
            prop = New DocProperties()
            prop.AddProperties(Me.MType, Me.MValue, Me.documentID, Me.documentType, Me.DocPropID, String.Empty)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Property DocPropID() As Integer
        Get
            Return m_docPropID
        End Get
        Set(ByVal newvalue As Integer)
            m_docPropID = newvalue
        End Set
    End Property

    Public ReadOnly Property MType() As Object
        Get
            Return type
        End Get
    End Property

    Public ReadOnly Property MValue() As Object
        Get
            Return value
        End Get
    End Property

    Public Property ErrorMessage() As String
        Get
            Return errorMsg
        End Get
        Set(ByVal value As String)
            errorMsg = value
        End Set
    End Property
End Class
