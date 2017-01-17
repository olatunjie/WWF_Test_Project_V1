Public Class MainV3
    Private doc As New Document
    Private docprops As New DocProperties
    Private assign As New Assignment
    Private storex As New DAL
    Private val As String
    Dim rowcount As Integer = 0
    Dim paramsList As New ArrayList
    Dim rule As New Rule
    Dim parser As Parser

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If (txtType.Text.Trim <> String.Empty And txtValue.Text.Trim <> String.Empty) Then
            lstProp.Items.Add(txtType.Text.Trim)
            lstProp.Items(rowcount).SubItems.Add(txtValue.Text.Trim)
            rowcount += 1
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (txtDocID.Text.Trim <> String.Empty And txtDocType.Text.Trim <> String.Empty And lstProp.SelectedItems.Count > 0) Then
            For i As Integer = 0 To lstProp.SelectedItems.Count - 1
                doc.UpdateProperties(lstProp.SelectedItems.Item(i).Text, lstProp.SelectedItems.Item(i).SubItems(1).Text, txtDocID.Text.Trim, txtDocType.Text.Trim)
            Next
            MessageBox.Show("Properties Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Please select the properties you are saving and supply the DocID and DocType values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

    End Sub

    Private Sub btnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        paramsList = doc.RequestAssignment(txtDocID.Text.Trim, txtDocType.Text.Trim)

        grpAssign.Enabled = True

        tDocID.Text = Integer.Parse(paramsList.Item(0).ToString)
        tDocType.Text = paramsList.Item(1).ToString
        txtPTask.Text = paramsList.Item(4).ToString
        txtAssUser.Text = paramsList.Item(2).ToString
        txtState.Text = paramsList.Item(5).ToString

        txtOrigUser.Text = paramsList.Item(3).ToString

        txtState.Enabled = False
        txtPTask.Enabled = False
        txtAssUser.Enabled = False

    End Sub

    Private Sub btnSaveAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAssign.Click
        Dim assid As Integer = 0
        If (txtTask.Text.Trim = String.Empty Or txtRequest.Text.Trim = String.Empty) Then
            MessageBox.Show("Please supply values for empty fields", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            assign.Assignment(assid, txtTask.Text, Integer.Parse(paramsList.Item(0).ToString), paramsList.Item(1).ToString, paramsList.Item(2).ToString, paramsList.Item(3).ToString, paramsList.Item(4).ToString, paramsList.Item(5).ToString, txtRequest.Text.Trim)
            MessageBox.Show("All Assignment parameters have been saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnCreateRule.Enabled = True
        End If
    End Sub

    Private Sub btnParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParse.Click
        If (txtRuleName.Text.Trim = String.Empty Or txtCondition.Text.Trim = String.Empty Or lstpreced.Text = "SELECT PRECEDENCE FOR RULE") Then
            MessageBox.Show("Please supply values for empty field(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            parser = New Parser()
            parser.Run(txtCondition.Text.Trim, lstpreced, rule.Doc_ID)
            If (parser.GetResult = "True") Then
                txtAction.Text = "APPROVED"
                txtElseAction.Text = "Nil"
                txtErrorMsg.Text = "Nil"
            ElseIf (parser.GetResult = "False") Then
                txtElseAction.Text = "REJECTED"
                txtAction.Text = "Nil"
                txtErrorMsg.Text = "Nil"
            Else
                txtErrorMsg.Text = parser.GetErrorMsg
                txtElseAction.Text = "Nil"
                txtAction.Text = "Nil"
            End If
            rule.UpdateRule(1, rule.Doc_Type, rule.Rule_Name, txtAction.Text, txtCondition.Text.Trim, parser.GetErrorMsg, Integer.Parse(lstpreced.SelectedItem.ToString.Substring(0, 1)), txtElseAction.Text)
        End If
    End Sub

    Private Sub btnCreateRule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateRule.Click
        grpRule.Enabled = True
        rule.Doc_ID = Integer.Parse(paramsList.Item(0).ToString)
        rule.Doc_Type = paramsList.Item(1).ToString
        rule.Rule_Name = txtRequest.Text.Trim
        txtRuleName.Text = rule.Rule_Name
        txtCondition.Text = storex.getRule(rule.Doc_Type, rule.Rule_Name)
    End Sub

    Private Sub btnSaveRule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRule.Click
        rule.SaveRule()
        MessageBox.Show("Update Successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class