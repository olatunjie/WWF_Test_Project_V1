'26-April-2014: Re-throw exceptions that occur in the Select Method for processing by callers.
Imports System.Data.SqlClient

Public Class Connect
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private query As String
    Public numRows As Integer
    Public persist As Boolean = False
    Public Sub New()
        conn = New SqlConnection("Data Source=10.0.0.124,1490;Initial Catalog=WorkFlowDB;User ID=appusr;Password=(#usr4*)")

        conn.Open()
    End Sub

    Public Sub SetQuery(ByVal q As String)
        query = q
        cmd = New SqlCommand(query, conn)
        cmd.CommandType = CommandType.Text

    End Sub

    Public Sub SetProcedure(ByVal q As String)
        query = q
        cmd = New SqlCommand(query, conn)
        cmd.CommandType = CommandType.StoredProcedure

    End Sub

    Public Function [Select]() As DataSet
        Dim ds As DataSet = New DataSet
        numRows = -1

        Try
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            numRows = ds.Tables(0).Rows.Count
        Catch ex As Exception
            Throw ex
        Finally
            Close()
        End Try
        
        Return ds
    End Function


    Public Function [Update]() As Integer
        Dim retInt As Integer = -1


        Try
            retInt = cmd.ExecuteNonQuery
        Catch ex As Exception
        Finally
            Close()
        End Try

        Return retInt
    End Function

    Public Function [Delete]() As Integer
        Dim retInt As Integer = -1


        Try
            retInt = cmd.ExecuteNonQuery
        Catch ex As Exception
        Finally
            Close()
        End Try

        Return retInt
    End Function

    Public Sub Close()
        If Not persist Then
            conn.Close()
            If conn IsNot Nothing Then
                conn.Dispose()
            End If
        End If

    End Sub

    Public Sub CloseAll()

        conn.Close()


    End Sub

    Public Sub AddParam(ByVal key As String, ByVal val As Object)
        cmd.Parameters.AddWithValue(key, val)


    End Sub

End Class
