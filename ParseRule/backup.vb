Imports System.Text.RegularExpressions
Imports System
Imports System.Reflection
Imports Microsoft.VisualBasic

Public Class backup
    Private storex As Object()
    Private stack(500) As String
    Private stack2(500) As String
    Private rules_stack(500) As String
    Private logicalop_stack(500) As String
    Private bool_result_stack(500) As String
    'Private stack3 As Object() = Nothing
    Private val As String = ""
    Private op As String = ""
    Dim index As Integer = 0
    Dim index2 As Integer = 0
    Dim index3 As Integer = 0
    Dim index4 As Integer = 0
    Dim index5 As Integer = 0
    Dim varindex As Integer = 0
    Public type, new_type As Object
    Public value, newvalue As Object
    Dim relop As String
    Public fieldinf As FieldInfo
    Public newtype As Object
    Dim loopvar As Integer = 0
    Dim loopvar2 As Integer = 0
    Dim new_rule As String = String.Empty
    'Dim storeprop As DocProperty = New DocProperty("", "")

    Private Sub btnParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParse.Click
        Dim rule As String

        rule = Trim(txtRule.Text)
        'resetVars()
        'EmptyStacks()
        Stack_Parenthesis(rule)
        'storeprop = New DocProperty(Convert.ChangeType(type, TypeCode.Object), Convert.ChangeType(Trim(txtvalue.text), TypeCode.Object))
        'storeprop.StoreParameters()

        'ListView2.Items.Add(type.ToString)
        ''ListView2.Items(loopvar).SubItems.Add(type.ToString)
        'ListView2.Items(loopvar2).SubItems.Add(storeprop.RetrieveValue(type).ToString)

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                resetVars()
                EmptyCompStacks()
                Break_Rule("(" & rules_stack(i) & ")")
                Parse(new_type, newvalue, op)

            End If
        Next
        '---------------------------------------------TESTING SECTION-------------------------------------------------------------------
        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "") Then
                Exit For
            Else
                MessageBox.Show(rules_stack(i))
            End If
        Next

        For i As Integer = 0 To logicalop_stack.Length - 1
            If (logicalop_stack(i) = "") Then
                Exit For
            Else
                MessageBox.Show(logicalop_stack(i))
            End If
        Next
        '-----------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To bool_result_stack.Length - 1
            If (bool_result_stack(i) = String.Empty And i = 0) Then
                Exit For

            Else
                If (bool_result_stack(i) = "") Then
                    Exit For
                End If
                MessageBox.Show(bool_result_stack(i))
            End If
        Next
        'loopvar2 += 1
        EmptyStacks()
        resetVars()
        index5 = 0
        MessageBox.Show("Done!", "Success")
    End Sub

    Private Sub Stack_Parenthesis(ByVal cur_rule As String)
        Dim counterStr As Integer
        counterStr = cur_rule.Length

        'While (1)
        For i As Integer = 0 To counterStr - 1
            If (cur_rule.Chars(i).ToString.Equals("(") And (i = 0)) Then
                stack(index) = cur_rule.Chars(i).ToString
                index += 1

            ElseIf (cur_rule.Chars(i).ToString.Equals("(") And (Trim(cur_rule.Chars(i - 1).ToString) <> String.Empty)) Then
                logicalop_stack(index4) = val

                stack(index) = cur_rule.Chars(i).ToString
                index += 1
                index4 += 1
                val = ""

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i = counterStr - 1)) Then
                If (op <> "" And val <> "") Then
                    stack(index) = op
                    index += 1
                    stack(index) = val
                    index += 1
                    value = val
                    val = ""
                    op = ""
                End If

                For j As Integer = (i - 1) To 0 Step -1
                    If (stack(j) <> "(") Then
                        stack2(index2) = stack(j)
                        index2 += 1
                    Else
                        Exit For
                    End If

                Next
                CreateRule()
                stack(index) = cur_rule.Chars(i).ToString

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (Trim(cur_rule.Chars(i + 1).ToString) <> String.Empty)) Then
                If (op <> "" And val <> "") Then
                    stack(index) = op
                    index += 1
                    stack(index) = val
                    index += 1
                    value = val
                    val = ""
                    op = ""
                End If

                For j As Integer = (i - 1) To 0 Step -1
                    If (stack(j) <> "(") Then
                        stack2(index2) = stack(j)
                        index2 += 1
                    Else
                        Exit For
                    End If

                Next
                CreateRule()
                stack(index) = cur_rule.Chars(i).ToString
                index += 1

            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
                val += cur_rule.Chars(i).ToString

            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
                Try
                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString <> String.Empty) Then
                        stack(index) = val
                        index += 1
                        type = val
                        val = ""
                    End If
                    op += cur_rule.Chars(i).ToString
                    'index += 1
                Catch
                    op += cur_rule.Chars(i).ToString
                End Try

            Else
                Continue For
            End If
        Next
        'End While

    End Sub

    Private Sub Break_Rule(ByVal cur_rule As String)
        Dim counterStr As Integer
        counterStr = cur_rule.Length

        'While (1)
        For i As Integer = 0 To counterStr - 1
            If (cur_rule.Chars(i).ToString.Equals("(") And i = 0) Then
                stack(index) = cur_rule.Chars(i).ToString
                index += 1

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And i = counterStr - 1) Then
                If (op <> "" And val <> "") Then
                    stack(index) = op
                    index += 1
                    stack(index) = val
                    newvalue = val

                End If
                For j As Integer = (i - 1) To 0 Step -1
                    If (stack(j) <> "(") Then
                        stack2(index2) = stack(j)
                    Else
                        Exit For
                    End If
                    index2 += 1
                Next

            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
                val += cur_rule.Chars(i).ToString

            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
                Try
                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString <> String.Empty) Then
                        stack(index) = val
                        new_type = val
                        val = ""

                    End If
                    op += cur_rule.Chars(i).ToString
                    index += 1

                Catch
                    op += cur_rule.Chars(i).ToString
                End Try

            Else
                Continue For
            End If
        Next


        'End While

    End Sub

    Private Sub StoreRules(ByVal newrule As String)
        rules_stack(index3) = newrule
        index3 += 1

        newrule = String.Empty

        'For i As Integer = 0 To rules_stack.Length - 1
        '    ListView2.Items.Add(rules_stack(i))
        '    'ListView2.Items(loopvar).SubItems.Add(logicalop_stack(i))
        'Next
        'loopvar += 1
    End Sub

    Private Sub CreateRule()
        For i As Integer = stack2.Length - 1 To 0 Step -1
            If (stack2(i) = "") Then
                Continue For
            Else
                new_rule += stack2(i)
            End If
        Next

        StoreRules(new_rule)
        For p As Integer = stack2.Length - 1 To 0 Step -1
            stack2(p) = String.Empty
        Next
        new_rule = String.Empty
        index2 = 0
    End Sub

    Private Sub EmptyStacks()
        For k As Integer = stack.Length - 1 To 0 Step -1
            stack(k) = String.Empty
        Next
        For p As Integer = stack2.Length - 1 To 0 Step -1
            stack2(p) = String.Empty
        Next
        For p As Integer = rules_stack.Length - 1 To 0 Step -1
            rules_stack(p) = String.Empty
        Next
        For p As Integer = logicalop_stack.Length - 1 To 0 Step -1
            logicalop_stack(p) = String.Empty
        Next
        For p As Integer = bool_result_stack.Length - 1 To 0 Step -1
            bool_result_stack(p) = String.Empty
        Next
    End Sub

    Private Sub EmptyCompStacks()
        For k As Integer = stack.Length - 1 To 0 Step -1
            stack(k) = String.Empty
        Next
        For p As Integer = stack2.Length - 1 To 0 Step -1
            stack2(p) = String.Empty
        Next
    End Sub

    Private Sub Parse(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String)
        Dim reg_exp As New Regex("\d+")
        'ListView.Items.Add(new_rule)
        'ListView.Items(loopvar).SubItems.Add(ntype)
        'ListView.Items(loopvar).SubItems.Add(nvalue)
        'ListView.Items(loopvar).SubItems.Add(nop)
        'newvar(varindex) = ntype
        Try
            'newtype = Me.GetType()
            'fieldinf = newtype.GetField(ntype)
            'fieldinf.SetValue(Me, Trim(txtvalue.Text))
            'newtype = storeprop.RetrieveValue(ntype)
            'newtype = Trim(txtvalue.Text)
            Select Case nop
                Case ">"
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) > Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) > Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case "<"
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) < Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) < Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case "<="
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) <= Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) <= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case ">="
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) >= Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) >= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case "="
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) = Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) = Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case "<>"
                    If reg_exp.IsMatch(newtype) Then
                        If (Integer.Parse(newtype) <> Integer.Parse(nvalue)) Then
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) <> Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            'ListView.Items(loopvar).SubItems.Add("True")
                            bool_result_stack(index5) = "True"
                        Else
                            'ListView.Items(loopvar).SubItems.Add("False")
                            bool_result_stack(index5) = "False"
                        End If
                    End If
                    index5 += 1
                Case Else
                    MessageBox.Show("Invalid Operator!")
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'loopvar += 1
    End Sub

    Private Sub resetVars()
        index = 0
        index2 = 0
        index3 = 0
        index4 = 0
        'index5 = 0
        val = String.Empty
        op = String.Empty
        new_rule = String.Empty
        'new_type = ""
        'newvalue = 0
    End Sub

    Private Sub frmParser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim storeproperty As New DocProperties
        'storeproperty.propListType.Clear()
        'storeproperty.propListValue.Clear()
        'storeprop.StoreParameters()

        ''txtRule.Text = "(amount>150000)"

        'EmptyStacks()
    End Sub
End Class

'Imports System.Text.RegularExpressions
'Imports System
'Imports System.Reflection
'Imports Microsoft.VisualBasic

'Public Class frmParser
'    Private stack(500) As String
'    Private stack2(500) As String
'    Private br_stack(500) As String
'    Private br_stack2(500) As String
'    Private stack3(500) As String
'    Private rules_stack(500) As String
'    Private logicalop_stack(500) As String
'    Private evaluate_stack(500) As String
'    Private val As String = ""
'    Private op As String = ""
'    Private br_val As String = ""
'    Private brl_val As String = ""
'    Private br_op As String = ""
'    Private brl_op As String = ""
'    Private condition As String
'    Dim result As String
'    Dim index As Integer = 0
'    Dim index2 As Integer = 0
'    Dim br_index As Integer = 0
'    Dim br_index2 As Integer = 0
'    Dim index3 As Integer = 0
'    Dim index4 As Integer = 0
'    Dim index5 As Integer = 0
'    Dim index6 As Integer = 0
'    Dim varindex As Integer = 0
'    Dim type, new_type, brl_type As Object
'    Dim value, newvalue, brl_value As Object
'    Dim relop As String
'    Dim newtype As Object
'    Dim new_rule As String = String.Empty
'    Dim storeprop As DocProperty = New DocProperty("", "")

'    Private Sub btnParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParse.Click
'        Dim rule As String
'        ListBox1.Items.Clear()
'        ListBox2.Items.Clear()
'        ListBox3.Items.Clear()
'        ListBox4.Items.Clear()

'        rule = Trim(txtRule.Text)
'        Stack_Parenthesis(rule)
'        'EvaluateRule()

'        'Transfer content of stack
'        For k As Integer = 0 To stack.Length - 1
'            If (stack(k) <> "") Then
'                stack3(index6) = stack(k)
'                ListBox1.Items.Add(stack(k))
'            Else
'                Exit For
'            End If
'            index6 += 1
'        Next

'        For i As Integer = 0 To rules_stack.Length - 1
'            If (rules_stack(i) = "" And i = 0) Then
'                Exit For
'            Else
'                If (rules_stack(i) = "") Then
'                    Exit For
'                End If
'                'resetVars()
'                'EmptyCompStacks()
'                'Break_Rule("(" & rules_stack(i) & ")")
'                'Parse(new_type, newvalue, op)
'                ListBox2.Items.Add(rules_stack(i))
'            End If
'        Next

'        For i As Integer = 0 To evaluate_stack.Length - 1
'            If (evaluate_stack(i) = String.Empty And i = 0) Then
'                Exit For
'            Else
'                If (evaluate_stack(i) = "") Then
'                    Exit For
'                End If
'                'MessageBox.Show(bool_result_stack(i))
'                ListBox3.Items.Add(evaluate_stack(i))
'            End If
'        Next
'        'Stop
'        EvaluateRule()

'        EmptyStacks()
'        resetVars()
'        index5 = 0
'        index6 = 0
'        MessageBox.Show("Done!", "Success")
'    End Sub

'    Private Sub Stack_Parenthesis(ByVal cur_rule As String)
'        Dim counterStr As Integer
'        counterStr = cur_rule.Length

'        For i As Integer = 0 To counterStr - 1
'            If (cur_rule.Chars(i).ToString.Equals("(") And (i = 0)) Then
'                stack(index) = cur_rule.Chars(i).ToString.Trim
'                index += 1

'            ElseIf (cur_rule.Chars(i).ToString.Equals("(") And (i > 0) And cur_rule.Chars(i - 1).ToString <> "(") Then
'                logicalop_stack(index4) = val.Trim
'                stack(index) = val.Trim
'                index += 1
'                rules_stack(index3) = val.Trim
'                index3 += 1
'                evaluate_stack(index5) = val.Trim
'                index5 += 1
'                stack(index) = cur_rule.Chars(i).ToString.Trim
'                index += 1
'                index4 += 1
'                val = ""

'            ElseIf (cur_rule.Chars(i).ToString.Equals("(") And (i > 0) And cur_rule.Chars(i - 1).ToString = "(") Then
'                stack(index) = cur_rule.Chars(i).ToString.Trim
'                index += 1
'                rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
'                index3 += 1
'                evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
'                index5 += 1

'            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i = counterStr - 1)) Then
'                If (op <> "" And val <> "") Then
'                    stack(index) = op.Trim
'                    index += 1
'                    stack(index) = val.Trim
'                    index += 1
'                    value = val
'                    val = ""
'                    op = ""
'                End If
'                stack2(index2) = cur_rule.Chars(i).ToString.Trim
'                index2 += 1
'                If (cur_rule.Chars(i - 1).ToString <> ")") Then
'                    For j As Integer = (i - 1) To 0 Step -1
'                        If (stack(j) <> "(") Then
'                            stack2(index2) = stack(j)
'                            index2 += 1
'                        ElseIf (stack(j) = "(") Then
'                            stack2(index2) = stack(j)
'                            index2 += 1
'                            Exit For
'                        Else
'                            Exit For
'                        End If
'                    Next
'                    stack(index) = cur_rule.Chars(i).ToString.Trim
'                    CreateRule()
'                ElseIf (cur_rule.Chars(i - 1).ToString = ")") Then
'                    rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
'                    index3 += 1
'                    evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
'                    index5 += 1
'                    stack(index) = cur_rule.Chars(i).ToString.Trim
'                Else
'                End If

'            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i < counterStr - 1) And cur_rule.Chars(i - 1).ToString <> ")") Then
'                If (op <> "" And val <> "") Then
'                    stack(index) = op.Trim
'                    index += 1
'                    stack(index) = val.Trim
'                    index += 1
'                    value = val
'                    val = ""
'                    op = ""
'                End If
'                stack2(index2) = cur_rule.Chars(i).ToString.Trim
'                index2 += 1
'                For j As Integer = (i - 1) To 0 Step -1
'                    If (stack(j) <> "(") Then
'                        stack2(index2) = stack(j)
'                        index2 += 1
'                    ElseIf (stack(j) = "(") Then
'                        stack2(index2) = stack(j)
'                        index2 += 1
'                        Exit For
'                    Else
'                        Exit For
'                    End If

'                Next
'                stack(index) = cur_rule.Chars(i).ToString.Trim
'                index += 1
'                CreateRule()

'            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i < counterStr - 1) And cur_rule.Chars(i - 1).ToString = ")") Then
'                stack(index) = cur_rule.Chars(i).ToString.Trim
'                index += 1
'                rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
'                index3 += 1
'                evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
'                index5 += 1

'            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
'                val += cur_rule.Chars(i).ToString.Trim

'            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
'                Try
'                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString.Trim <> String.Empty) Then
'                        stack(index) = val.Trim
'                        index += 1
'                        type = val.Trim
'                        val = ""
'                    End If
'                    op += cur_rule.Chars(i).ToString.Trim
'                Catch
'                    op += cur_rule.Chars(i).ToString.Trim
'                End Try

'            Else
'                Continue For
'            End If
'        Next

'    End Sub

'    Private Sub Break_Rule(ByVal cur_rule As String)
'        Dim counterStr As Integer
'        counterStr = cur_rule.Length

'        For i As Integer = 0 To counterStr - 1
'            If (cur_rule.Chars(i).ToString.Equals("(") And i = 0) Then
'                br_stack(br_index) = cur_rule.Chars(i).ToString.Trim
'                br_index += 1

'            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And i = counterStr - 1) Then
'                If (br_op <> "" And br_val <> "") Then
'                    br_stack(br_index) = br_op.Trim
'                    br_index += 1
'                    br_stack(br_index) = br_val.Trim
'                    newvalue = br_val.Trim

'                End If
'                For j As Integer = (i - 1) To 0 Step -1
'                    If (br_stack(j) <> "(") Then
'                        br_stack2(br_index2) = br_stack(j)
'                    Else
'                        Exit For
'                    End If
'                    br_index2 += 1
'                Next

'            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
'                br_val += cur_rule.Chars(i).ToString.Trim

'            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
'                Try
'                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString <> String.Empty) Then
'                        br_stack(br_index) = br_val.Trim
'                        new_type = br_val.Trim
'                        br_val = ""

'                    End If
'                    br_op += cur_rule.Chars(i).ToString.Trim
'                    br_index += 1

'                Catch
'                    br_op += cur_rule.Chars(i).ToString.Trim
'                End Try

'            Else
'                Continue For
'            End If
'        Next


'        'End While

'    End Sub

'    Private Sub Break_Rule_Logical(ByVal cur_rule As String)
'        brl_op = String.Empty
'        brl_val = String.Empty
'        brl_type = String.Empty
'        brl_value = String.Empty
'        Dim counterStr As Integer
'        cur_rule = cur_rule.Trim
'        counterStr = cur_rule.Length

'        For i As Integer = 0 To counterStr - 1
'            If (cur_rule.Chars(i).ToString.Trim <> String.Empty And cur_rule.Substring(i, 2).ToUpper = "OR") Then
'                brl_op = "OR"
'                brl_val = cur_rule.Substring(i + 2, (cur_rule.Length - 1) - (i + 2)).Trim
'                brl_value = brl_val.Trim

'                Exit For
'            ElseIf (cur_rule.Chars(i).ToString <> String.Empty And cur_rule.Substring(i, 3).ToUpper = "AND") Then
'                brl_op = "AND"
'                brl_val = cur_rule.Substring(i + 3, (cur_rule.Length - 1) - (i + 3)).Trim
'                brl_value = brl_val.Trim

'                Exit For
'            ElseIf (cur_rule.Chars(i).ToString <> String.Empty And cur_rule.Substring(i, 3).ToUpper = "NOT") Then
'                brl_op = "NOT"
'                brl_val = cur_rule.Substring(i + 3, (cur_rule.Length - 1) - (i + 3)).Trim
'                brl_value = brl_val.Trim

'                Exit For
'            ElseIf (i = 1 And cur_rule.Chars(i).ToString.Trim <> String.Empty) Then
'                Dim j As Integer = i
'                While (1)
'                    If (cur_rule.Substring(j, 2).ToUpper = "OR" Or cur_rule.Substring(j, 3).ToUpper = "AND" Or cur_rule.Substring(j, 3).ToUpper = "NOT") Then
'                        Exit While
'                    Else
'                        brl_val += cur_rule.Chars(j).ToString.Trim
'                        j += 1
'                    End If
'                End While
'                brl_type = brl_val.Trim

'                'ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
'                '    br_val += cur_rule.Chars(i).ToString.Trim
'            Else
'                Continue For
'            End If
'        Next
'        condition = ParseLRule(brl_type, brl_value, brl_op)

'        'End While

'    End Sub

'    Private Sub StoreRules(ByVal newrule As String)
'        'resetVars()
'        evaluate_stack(index5) = newrule

'        EmptyCompStacks()
'        Break_Rule(newrule)
'        result = Parse(new_type, newvalue, br_op)
'        rules_stack(index3) = result
'        index3 += 1
'        index5 += 1

'        newrule = String.Empty

'    End Sub

'    Private Sub CreateRule()
'        For i As Integer = stack2.Length - 1 To 0 Step -1
'            If (stack2(i) = "") Then
'                Continue For
'            Else
'                new_rule += stack2(i)
'            End If
'        Next

'        StoreRules(new_rule)
'        For p As Integer = stack2.Length - 1 To 0 Step -1
'            stack2(p) = String.Empty
'        Next
'        new_rule = String.Empty
'        index2 = 0
'    End Sub

'    Private Sub EmptyStacks()
'        For k As Integer = stack.Length - 1 To 0 Step -1
'            stack(k) = String.Empty
'        Next
'        For p As Integer = stack2.Length - 1 To 0 Step -1
'            stack2(p) = String.Empty
'        Next
'        For p As Integer = rules_stack.Length - 1 To 0 Step -1
'            rules_stack(p) = String.Empty
'        Next
'        For p As Integer = logicalop_stack.Length - 1 To 0 Step -1
'            logicalop_stack(p) = String.Empty
'        Next
'        'For p As Integer = evaluate_stack.Length - 1 To 0 Step -1
'        '    evaluate_stack(p) = String.Empty
'        'Next
'        For k As Integer = br_stack.Length - 1 To 0 Step -1
'            br_stack(k) = String.Empty
'        Next
'        For p As Integer = br_stack2.Length - 1 To 0 Step -1
'            br_stack2(p) = String.Empty
'        Next
'    End Sub

'    Private Sub EmptyCompStacks()
'        For k As Integer = br_stack.Length - 1 To 0 Step -1
'            br_stack(k) = String.Empty
'        Next
'        For p As Integer = br_stack2.Length - 1 To 0 Step -1
'            br_stack2(p) = String.Empty
'        Next
'        br_index = 0
'        br_index2 = 0
'        br_val = ""
'        br_op = ""
'        brl_val = ""
'        brl_op = ""
'    End Sub

'    Private Function Parse(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
'        Dim reg_exp As New Regex("\d+")
'        Try
'            newtype = storeprop.RetrieveValue(ntype)
'            Select Case nop
'                Case ">"
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) > Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            'ListView.Items(loopvar).SubItems.Add("False")
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) > Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case "<"
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) < Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) < Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case "<="
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) <= Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) <= Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case ">="
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) >= Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) >= Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case "="
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) = Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) = Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case "<>"
'                    If reg_exp.IsMatch(newtype) Then
'                        If (Integer.Parse(newtype) <> Integer.Parse(nvalue)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    Else
'                        If (Convert.ChangeType(newtype, TypeCode.Object) <> Convert.ChangeType(nvalue, TypeCode.Object)) Then
'                            Parse = "True"
'                        Else
'                            Parse = "False"
'                        End If
'                    End If
'                    'index5 += 1
'                Case Else
'                    Parse = "Invalid Operator!"
'            End Select
'        Catch ex As Exception
'            Parse = ex.Message
'        End Try
'    End Function

'    Private Function ParseLRule(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
'        Try
'            Select Case nop.ToUpper
'                Case "OR"
'                    If (Boolean.Parse(ntype) Or Boolean.Parse(nvalue)) Then
'                        ParseLRule = "True"
'                    Else
'                        ParseLRule = "False"
'                    End If
'                    'index5 += 1
'                Case "AND"
'                    If (Boolean.Parse(ntype) And Boolean.Parse(nvalue)) Then
'                        ParseLRule = "True"
'                    Else
'                        ParseLRule = "False"
'                    End If
'                    'index5 += 1
'                Case "NOT"
'                    ParseLRule = "Pending"
'                Case Else
'                    ParseLRule = "Invalid Operator!"
'            End Select
'        Catch ex As Exception
'            ParseLRule = ex.Message
'        End Try
'    End Function

'    Private Function LParse(ByVal rule As String) As String
'        If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")" And rule.Length <= 5) Then
'            LParse = rule
'            txtcondition.Text = LParse
'        Else
'            LParse = LogicalTest(rule)
'        End If
'    End Function

'    Private Function LogicalTest(ByVal rule As String) As String
'        Dim currentrule As String

'        If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")") Then
'            currentrule = "(" & rule & ")"
'            Break_Rule_Logical(currentrule)

'            LogicalTest = condition
'        ElseIf (rule.Chars(0).ToString.Trim = "(" And rule.Chars(rule.Length - 1).ToString.Trim = ")") Then
'            currentrule = rule
'            'EmptyCompStacks()
'            Break_Rule_Logical(currentrule)

'            LogicalTest = condition
'        Else
'            LogicalTest = ""
'        End If
'    End Function

'    Private Sub EvaluateRule()
'        Dim logicalrule As String = ""
'        Dim newstack1(500) As String
'        Dim newstack2(500) As String
'        Dim stackindex As Integer = -1
'        Dim stackindex2 As Integer = -1
'        Dim indexcoll As New ArrayList
'        Dim listindex As Integer = 0
'        Dim counter As Integer = 0
'        Dim stack_rules() As String

'        For i As Integer = 0 To rules_stack.Length - 1
'            If (rules_stack(i) = "" And i = 0) Then
'                Exit For
'            Else
'                If (rules_stack(i) = "") Then
'                    Exit For
'                End If
'                counter += 1
'            End If
'        Next

'        'Exit Sub
'        Dim length As Integer = (counter + 2) - 1
'        ReDim stack_rules(length)
'        stack_rules(0) = "("
'        stack_rules(stack_rules.Length - 1) = ")"

'        counter = 1
'        For i As Integer = 0 To stack_rules.Length - 1

'            If (rules_stack(i) = "") Then
'                Exit For
'            Else
'                stack_rules(counter) = rules_stack(i)
'            End If
'            counter += 1
'        Next


'        For i As Integer = 0 To stack_rules.Length - 1
'            If (stack_rules(i) = "" And i = 0) Then

'                Exit For
'            Else
'                If (stack_rules(i) = "") Then

'                    Exit For
'                End If
'                If (stack_rules(i) = "(") Then
'                    stackindex2 = stackindex + 1
'                    newstack2(stackindex2) = stack_rules(i)
'                    indexcoll.Add(stackindex2)

'                ElseIf (stack_rules(i).ToUpper = "TRUE" Or stack_rules(i).ToUpper = "FALSE") Then
'                    stackindex += 1
'                    newstack1(stackindex) = stack_rules(i)

'                ElseIf (stack_rules(i).ToUpper = "AND" Or stack_rules(i).ToUpper = "OR") Then
'                    stackindex += 1
'                    newstack1(stackindex) = stack_rules(i)

'                ElseIf (stack_rules(i) = ")") Then
'                    logicalrule = ""
'                    stackindex2 = Integer.Parse(indexcoll.Item(indexcoll.Count - 1).ToString)

'                    While (stackindex >= stackindex2)
'                        logicalrule += newstack1(stackindex)
'                        newstack1(stackindex) = ""
'                        stackindex -= 1
'                    End While

'                    newstack2(stackindex2) = ""
'                    indexcoll.RemoveAt(indexcoll.Count - 1)
'                    'stackindex2 -= 1
'                    stackindex += 1
'                    newstack1(stackindex) = LParse(logicalrule)

'                Else
'                End If
'            End If
'        Next
'        If (newstack1(1) = "") Then
'            txtcondition.Text = newstack1(0)
'        Else
'            MessageBox.Show("Your rule does not conform with the acceptable standard! Correct your rule and try again.")
'        End If

'    End Sub

'    Private Sub resetVars()
'        index = 0
'        index2 = 0
'        index3 = 0
'        index4 = 0
'        index5 = 0
'        val = String.Empty
'        op = String.Empty
'        new_rule = String.Empty
'    End Sub

'    Private Sub frmParser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Dim storeproperty As New DocProperties
'        storeproperty.propListType.Clear()
'        storeproperty.propListValue.Clear()
'        storeprop.StoreParameters()

'        txtRule.Text = "(amount>150000)"

'    End Sub

'    Private Sub btnlogictest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogictest.Click
'        txtcondition.Text = LParse(Trim(txtRule.Text))
'    End Sub

'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
'        Dim str As String
'        str = txtRule.Text
'        MessageBox.Show(str.Substring(5, 2))
'    End Sub
'End Class
