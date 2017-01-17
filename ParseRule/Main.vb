'Imports WFSystem

Public Class FrmMain
    Private doc As New Document
    Private docprops As New DocProperties
    Private assign As Assignment
    Private storex As New DAL
    Private val As String
    Private reader As SqlClient.SqlDataReader
    Private reader2 As SqlClient.SqlDataReader
    Dim rowcount As Integer = 0
    Dim paramsList As New ArrayList
    Dim rule As New Rule
    Dim parser As RuleParser
    Dim userlist As New ArrayList
    Dim col As Integer
    Dim rcount As Integer = 0
    Dim rwcount As Integer = 0
    Dim user As New Users
    Dim propaggreg As New ArrayList
    Dim props As New ArrayList
    Dim properties As New ArrayList

    Private Sub RetrieveProp()
        lstProp.Items.Clear()
        rowcount = 0
        If (txtDocID.Text.Trim <> String.Empty And txtDocType.Text.Trim <> String.Empty) Then
            reader = storex.getAllDocProp(txtDocType.Text.Trim)
            While (reader.Read)
                lstProp.Items.Add(reader.GetString(0))
                lstProp.Items(rowcount).SubItems.Add(reader.GetValue(1).ToString)
                rowcount += 1
            End While
            reader.Close()
        End If
    End Sub

    Private Sub btnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        storex.UpdateCurrentDoc(txtDocType.Text.Trim)
        'paramsList = doc.RequestAssignment(storex.GetDocID(storex.GetDocType), storex.GetDocType)
        'userlist.Clear()
        props.Clear()
        propaggreg.Clear()

        grpAssign.Enabled = True

        'tDocID.Text = Integer.Parse(paramsList.Item(0).ToString)
        'tDocType.Text = paramsList.Item(1).ToString
        'txtPTask.Text = paramsList.Item(4).ToString
        'txtAssUser.Text = paramsList.Item(2).ToString
        'txtState.Text = paramsList.Item(5).ToString

        'txtOrigUser.Text = paramsList.Item(3).ToString

        'txtState.Enabled = False
        'txtPTask.Enabled = False
        'txtAssUser.Enabled = False
        assign.AssignmentDoc(storex.getDocProperties(storex.GetDocType, "UserID"), storex.GetDocType, "INIT", "", "")

    End Sub

    Private Sub btnSaveRule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRule.Click

        If (txtRuleName.Text.Trim <> String.Empty And txtCondition.Text.Trim <> String.Empty) Then
            rule.UpdateRule(0, storex.GetDocType, txtRuleName.Text.Trim, txtact.Text.Trim, txtCondition.Text.Trim, "", 0, txtelseact.Text.Trim)
            MessageBox.Show("Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GetRule()
        Else
            MessageBox.Show("Please make sure you the enter Rule Name, and Condition", "Warning")
        End If

    End Sub

    Private Sub proceedbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnNextAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextAction.Click
        'Dim assid As Integer = 0
        'Dim action As String = String.Empty
        'Dim elseaction As String = String.Empty
        'Dim condition As String = String.Empty

        If (txtreq.Text.Trim = String.Empty) Then
            MessageBox.Show("Please enter Request", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Try
                'assign = New Assignment()
                'assign.Assignment(storex.getDocProperties(storex.GetDocType, "UserID"), storex.GetDocType, txtreq.Text.Trim, "", "")
                'assign.AssignmentDoc(0, txtTask.Text, Integer.Parse(paramsList.Item(0).ToString), paramsList.Item(1).ToString, paramsList.Item(2).ToString, paramsList.Item(3).ToString, paramsList.Item(4).ToString, paramsList.Item(5).ToString, txtRequest.Text.Trim)
                'rule.Doc_ID = Integer.Parse(paramsList.Item(0).ToString)
                'rule.Doc_Type = paramsList.Item(1).ToString
                'rule.Rule_Name = txtRequest.Text.Trim
                'txtRule.Text = rule.Rule_Name
                'reader2 = storex.getRule(rule.Doc_Type, rule.Rule_Name)
                'While (reader2.Read)
                '    condition = reader2.GetString(0)
                '    action = reader2.GetString(1)
                '    elseaction = reader2.GetString(2)
                'End While
                'reader2.Close()
                'txtcond.Text = condition
                'parser = New WFSystem.RuleParser()
                'parser.Execute(condition, rule.Doc_Type, storex.getDocProperties(rule.Doc_Type, "UserID"))
                'If (parser.GetResult = "True") Then
                '    txtAction.Text = action
                '    txtelseaction.Text = "Nil"
                '    txtErrorMsg.Text = "Nil"
                'ElseIf (parser.GetResult = "False") Then
                '    txtelseaction.Text = elseaction
                '    txtAction.Text = "Nil"
                '    txtErrorMsg.Text = "Nil"
                'Else
                '    txtErrorMsg.Text = parser.GetErrorMsg
                '    txtelseaction.Text = "Nil"
                '    txtAction.Text = "Nil"
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub

    Private Sub btnSaveAgg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAgg.Click
        Try
            If (lstagg.SelectedItems.Count <> 0) Then
                storex.ResetUserAggProp(storex.GetDocType)
                For i As Integer = 0 To lstagg.SelectedItems.Count - 1
                    storex.saveUserAggProperty(lstagg.SelectedItems.Item(i).ToString, storex.GetDocType)
                Next
                MessageBox.Show("User Aggregate Property(ies) Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Please select the properties you wish to aggregate!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnAddProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddProp.Click
        If (txttype.Text.Trim <> String.Empty And txtvalue.Text.Trim <> String.Empty) Then
            'docprops.AddProperties(txttype.Text.Trim, txtvalue.Text.Trim, storex.GetDocID(storex.GetDocType), storex.GetDocType, 0, storex.getDocProperties(storex.GetDocType, "UserID"))
            RetrieveProp()
        Else
            MessageBox.Show("Please specify a type and value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
    End Sub

    Private Sub btnGetProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetProp.Click
        RetrieveProp()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        'Dim readerauser As SqlClient.SqlDataReader
        'If (txtAction.Text = "Next Action") Then
        '    storex.ResetAssignment()
        '    lstusers.Items.Clear()

        '    assign = New Assignment()
        '    assign.AssUsers()

        '    'assign.Assignment(storex.GetDocID(storex.GetDocType), storex.GetDocType)
        '    readerauser = storex.getAssignment()
        '    While (readerauser.Read)
        '        lstusers.Items.Add(readerauser.GetString(4))
        '    End While
        '    readerauser.Close()
        '    PopulateAssignment()
        'Else
        '    MessageBox.Show("You are not authorized to proceed beyond this point.")
        'End If
    End Sub

    Private Sub FrmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'RetrieveProp()
        'Dim reader3 As SqlClient.SqlDataReader

        'reader3 = storex.getAllDocProp("User")
        'While (reader3.Read)
        '    If (Not (lstagg.Items.Contains(reader3.GetString(0)))) Then
        '        lstagg.Items.Add(reader3.GetString(0))
        '    End If
        'End While
        'reader3.Close()

        'GetRule()

    End Sub

    Private Sub lstusers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstusers.SelectedIndexChanged
        RetrieveProp(lstusers.SelectedIndex, lstusers.SelectedItem.ToString)
    End Sub

    Private Sub RetrieveProp(ByVal index As Integer, ByVal value As String)
        Dim userprp As SqlClient.SqlDataReader
        Dim counter As Integer = 0
        lstuprop.Items.Clear()
        userprp = storex.GetUserPropValues(value)
        While (userprp.Read)
            lstuprop.Items.Add(userprp.GetString(0))
            lstuprop.Items(counter).SubItems.Add(userprp.GetString(1))
            counter += 1
        End While
        userprp.Close()
    End Sub

    Private Sub GetRule()
        Dim readerrule As SqlClient.SqlDataReader
        Dim counter As Integer = 0

        lstRule.Items.Clear()
        readerrule = storex.GetRules()
        While (readerrule.Read)
            lstRule.Items.Add(readerrule.GetString(1))
            lstRule.Items(counter).SubItems.Add(readerrule.GetString(3))
            counter += 1
        End While
        readerrule.Close()
    End Sub

    Private Sub PopulateAssignment()
        Dim readerass As SqlClient.SqlDataReader
        Dim rows As Integer = 0
        Dim count As Integer = 0
        readerass = storex.getAssignment()
        lstreq.Clear()
        For i As Integer = 1 To readerass.FieldCount - 1
            lstreq.Columns.Add(readerass.GetName(i), 100, HorizontalAlignment.Center)
        Next
        While (readerass.Read)
            lstreq.Items.Add(readerass.GetString(1))
            For i As Integer = 2 To readerass.FieldCount - 1
                lstreq.Items(rows).SubItems.Add(readerass.GetValue(i))
            Next
            rows += 1
        End While
        readerass.Close()
    End Sub

    Private Sub PopulateRequests()
        Dim readerass As SqlClient.SqlDataReader
        Dim rows As Integer = 0
        Dim count As Integer = 0
        readerass = storex.GetAllRequests(storex.getDocProperties(storex.GetDocType, "UserID"))
        lstreq.Clear()
        For i As Integer = 1 To readerass.FieldCount - 1
            lstreq.Columns.Add(readerass.GetName(i), 100, HorizontalAlignment.Center)
        Next
        While (readerass.Read)
            lstreq.Items.Add(readerass.GetString(1))
            For i As Integer = 2 To readerass.FieldCount - 1
                lstreq.Items(rows).SubItems.Add(readerass.GetValue(i))
            Next
            rows += 1
        End While
        readerass.Close()
    End Sub

    Private Sub PopulateTasks()
        Dim readerass As SqlClient.SqlDataReader
        Dim rows As Integer = 0
        Dim count As Integer = 0
        readerass = storex.GetAllTasks(storex.getDocProperties(storex.GetDocType, "UserID"))
        lstTask.Clear()
        For i As Integer = 1 To readerass.FieldCount - 1
            lstTask.Columns.Add(readerass.GetName(i), 100, HorizontalAlignment.Center)
        Next
        While (readerass.Read)
            lstTask.Items.Add(readerass.GetString(1))
            For i As Integer = 2 To readerass.FieldCount - 1
                lstTask.Items(rows).SubItems.Add(readerass.GetValue(i))
            Next
            rows += 1
        End While
        readerass.Close()
    End Sub

    Private Sub btnUpdRule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdRule.Click
        If (txtRuleName.Text.Trim <> String.Empty And txtCondition.Text.Trim <> String.Empty And txtact.Text.Trim <> String.Empty) Then
            rule.UpdateRule(1, storex.GetDocType, txtRuleName.Text.Trim, txtact.Text.Trim, txtCondition.Text.Trim, "", 0, txtelseact.Text.Trim)
            MessageBox.Show("Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            GetRule()
        Else
            MessageBox.Show("Please make sure you the enter Rule Name, Condition, and Action", "Warning")
        End If
    End Sub

    Private Sub btnsaveconn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsaveconn.Click
        If (txtconnstr.Text.Trim <> String.Empty And txttabname.Text.Trim <> String.Empty) Then
            storex.SaveUserConnection(txtconnstr.Text.Trim, txttabname.Text.Trim)
        Else
            MessageBox.Show("Please specify the ConnectionString and TableName for extracting User Properties", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        PopulateRequests()
    End Sub

    Private Sub btnrefresh2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh2.Click
        PopulateTasks()
    End Sub
End Class