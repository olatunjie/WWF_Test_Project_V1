Public Class DocProperties
    Private valindex As Integer
    Private docprop As New DocProperty
    Private storex As New DAL
    Private errormsg As String = String.Empty

    Public Sub AddProperties(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
        Try
            storex.saveDocProperty(type, value, docID, doctype, docpropid, owner)
        Catch ex As Exception
            Me.ErrorMessage = ex.Message
        End Try
    End Sub

    Public Property ErrorMessage() As String
        Get
            Return errormsg
        End Get
        Set(ByVal value As String)
            errormsg = value
        End Set
    End Property

    Public Sub New()

    End Sub
End Class
