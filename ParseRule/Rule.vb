Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Rule
    Private RuleID As Integer
    Private Name As Object
    Private Action As Object
    Private Condition As Object
    Private ErrorMsg As String = String.Empty
    Private Precedence As Object
    Private ElseAction As Object
    Private documentID As Object
    Private documentType As Object
    Private storex As New DAL

    Public Sub New()

    End Sub

    Public Property Doc_ID() As Integer
        Get
            Return documentID
        End Get
        Set(ByVal newvalue As Integer)
            documentID = newvalue
        End Set
    End Property

    Public Property Doc_Type() As Object
        Get
            Return documentType
        End Get
        Set(ByVal newvalue As Object)
            documentType = newvalue
        End Set
    End Property

    Public Property Rule_ID() As Integer
        Get
            Return RuleID
        End Get
        Set(ByVal newvalue As Integer)
            RuleID = newvalue
        End Set
    End Property

    Public Property Rule_Name() As Object
        Get
            Return Name
        End Get
        Set(ByVal newvalue As Object)
            Name = newvalue
        End Set
    End Property

    Public Property Rule_Action() As Object
        Get
            Return Action
        End Get
        Set(ByVal newvalue As Object)
            Action = newvalue
        End Set
    End Property

    Public Property Rule_Condition() As Object
        Get
            Return Condition
        End Get
        Set(ByVal newvalue As Object)
            Condition = newvalue
        End Set
    End Property

    Public Property Error_Msg() As Object
        Get
            Return ErrorMsg
        End Get
        Set(ByVal newvalue As Object)
            ErrorMsg = newvalue
        End Set
    End Property

    Public Property Rule_Precedence() As Integer
        Get
            Return Precedence
        End Get
        Set(ByVal newvalue As Integer)
            Precedence = newvalue
        End Set
    End Property

    Public Property Else_Action() As Object
        Get
            Return ElseAction
        End Get
        Set(ByVal newvalue As Object)
            ElseAction = newvalue
        End Set
    End Property

    Public Sub UpdateRule(ByVal ruleID As Integer, ByVal doctype As String, ByVal ruleName As String, ByVal action As String, ByVal condition As String, ByVal errormsg As String, ByVal precedence As Integer, ByVal elseaction As String)
        Try
            Me.Rule_ID = ruleID
            Me.Doc_Type = doctype
            Me.Rule_Name = ruleName
            Me.Rule_Action = action
            Me.Rule_Condition = condition
            Me.Error_Msg = errormsg
            Me.Rule_Precedence = precedence
            Me.Else_Action = elseaction
            SaveRule()
        Catch ex As Exception
            Me.Error_Msg = ex.Message
        End Try

    End Sub

    Public Sub SaveRule()
        Try
            storex.saveRule(Me)
        Catch ex As Exception
            Me.Error_Msg = ex.Message
        End Try

    End Sub

    Public Function SQLParser(ByVal val As Object) As Object
        Try
            Dim reg_exp As New Regex("\d+(\.\d+)?")
            Dim value As Object
            value = storex.getDocProperties(storex.GetDocType, val)

            If reg_exp.IsMatch(value) Then
                Return Integer.Parse(value)
            Else
                Return "'" & value & "'"
            End If
        Catch ex As Exception
            Me.Error_Msg = ex.Message
            Return ""
        End Try
    End Function
End Class
