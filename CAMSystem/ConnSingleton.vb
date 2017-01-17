Imports System.Data.SqlClient

Public Class Singleton
    Private Shared m_instance As Singleton
    Private Shared m_conn As SqlConnection
    Private Sub New()
        m_conn = New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
        m_conn.Open()
    End Sub

    Public Shared ReadOnly Property Instance() As Singleton
        Get
            If m_instance Is Nothing Then
                m_instance = New Singleton()
            End If
            Return m_instance
        End Get
    End Property

    Public Shared ReadOnly Property Connection() As SqlConnection
        Get
            If Singleton.m_conn.ConnectionString.Trim.CompareTo(String.Empty) = 0 Then
                Singleton.m_conn.ConnectionString = "Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)"
            ElseIf Singleton.m_conn Is Nothing Then
                Singleton.m_conn = New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")
            ElseIf Singleton.m_conn.State = ConnectionState.Closed Then
                Singleton.m_conn.Open()
            End If

            Return Singleton.m_conn
        End Get
    End Property
End Class
