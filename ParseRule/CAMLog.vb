Public Class CAMLog
    Private connect As New System.Data.SqlClient.SqlConnection("Data Source=10.0.0.210;Initial Catalog=CAM;User ID=sa;User ID=sa;password=system")
    Private Sub StoreData()
        Try
            Dim command As New System.Data.SqlClient.SqlCommand("spLogData", connect)
            If connect.State = ConnectionState.Closed Then
                connect.Open()
            End If

            command.CommandType = CommandType.StoredProcedure
            command.Parameters.Add("@Date", Data.SqlDbType.DateTime).Value = Now()
            command.Parameters.Add("@User", Data.SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@Module", Data.SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@Control", Data.SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@Success", Data.SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@ErrorMessage", Data.SqlDbType.NVarChar).Value = ""
            command.Parameters.Add("@Remark", Data.SqlDbType.NVarChar).Value = ""

            command.ExecuteNonQuery()

            connect.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
