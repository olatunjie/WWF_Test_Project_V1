Imports System.Text
Imports WFSystem
Imports Oracle.ManagedDataAccess.Client

Module Module1

    Sub Main()
        Console.WriteLine(DateTime.Now)
        'Dim ds As DataSet = WFSystem.WorkFlow.DAL.GetTasksByRole_DS_Extended("CSO", "SWIFTPro", "FT%", "Pending", 1, "BraCode", "223")
        Dim ds As DataSet = WFSystem.WorkFlow.DAL.GetPendingTasks_DS("oyedarat", "SWIFTPro", "FT%", 1)
        'Dim pro As String = WFSystem.WorkFlow.DAL.GetPropertiesVal("Swiftpro", "role", "kenng")
        Console.WriteLine(DateTime.Now)

        Console.ReadKey()
    End Sub
    Private Function FormatAmountTo3DecimalPlaces(ByVal amountString As String) As String
        If Not String.IsNullOrEmpty(amountString) Then
            If amountString.Contains("."c) Then
                Dim wholePart As String = amountString.Split("."c)(0).Trim
                Dim decimalPart As String = amountString.Split("."c)(1).Trim
                If Not String.IsNullOrEmpty(decimalPart) AndAlso decimalPart.Length >= 3 Then
                    Dim buf As New StringBuilder
                    buf.Append(wholePart)
                    buf.Append(".")
                    buf.Append(decimalPart.Substring(0, 3))

                    Return buf.ToString
                Else
                    Return amountString
                End If
            Else
                Return amountString
            End If
        Else
            Throw New ApplicationException("The amount string is Nothing/NULL or the empty string")
        End If
    End Function



    Public Sub btnCreateUser_Click()

        Dim username As String = String.Empty
        Dim email As String = String.Empty
        Dim bracode As String = String.Empty
        Dim role As String = String.Empty
        Dim name As String = String.Empty
        Dim owner As String = username
        Dim tellerid As String = String.Empty
        Dim doctype As String = "SWIFTPro"
        Dim prop() As String = New String() {"UserID", "BraCode", "Email", "Name", "Role", "TellerID"}


        Dim roles As New ArrayList

        roles.Add("CSO")
        roles.Add("HOP")
        roles.Add("TRADE")
        roles.Add("TRADEAPPROVAL")

        Dim branchcodes As New ArrayList
        Dim sql As String = "select distinct bra_code from branch where bra_code<>0"
        Dim cn As String = "user id=appusr;password=testapp;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=10.0.0.176)(PORT=1590))(CONNECT_DATA=(SID = HOBANK3)))" 'OneConfig.Text.Get("oracle_appusr")
        Dim DS As New DataSet
        Dim oraCon As OracleConnection = New OracleConnection()
        oraCon.ConnectionString = ""
        'Dim sql As String = "SELECT COUNT(*) FROM teller WHERE MAT_DATE <= SYSDATE AND TELL_ID=" & tellerid

        Using oraCon
            Try
                oraCon.Open()
                Dim oraCmd As OracleCommand = New OracleCommand(sql, oraCon)
                Dim orAdapter As New OracleDataAdapter(oraCmd)
                Using orAdapter
                    orAdapter.Fill(DS)
                End Using

            Catch ex As Exception

            End Try

        End Using




        Try
            Dim userid As String = String.Empty

            Dim value As String = String.Empty

            Dim bra_code As String = String.Empty
            Dim docid As Integer = WorkFlow.DAL.GetDocID(doctype)

            For Each dr As DataRow In DS.Tables(0).Rows
                bra_code = dr(0).ToString()
                bracode = bra_code

                For Each retrievedrole As String In roles


                    Dim ustat As Integer = WorkFlow.DocProperties.AddProperties("UserID", username, docid, doctype, 0, username)

                    Dim bstat As Integer = WorkFlow.DocProperties.AddProperties("BraCode", bracode, docid, doctype, 0, username)

                    Dim estat As Integer = WorkFlow.DocProperties.AddProperties("Email", email, docid, doctype, 0, username)

                    Dim nstat As Integer = WorkFlow.DocProperties.AddProperties("Name", name, docid, doctype, 0, username)

                    Dim rstat As Integer = WorkFlow.DocProperties.AddProperties("Role", retrievedrole, docid, doctype, 0, username)

                    Dim tstat As Integer = WorkFlow.DocProperties.AddProperties("TellerID", tellerid, docid, doctype, 0, username)

                    If ustat = 1 And bstat = 1 And estat = 1 And nstat = 1 And rstat = 1 And tstat = 1 Then


                    Else
                        'Failed to create user
                    End If
                Next
                
            Next
            

        Catch ex As Exception

        End Try
    End Sub

    Public Sub psot()
        Dim DS As New DataSet

    End Sub
End Module
