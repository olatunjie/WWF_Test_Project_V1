Imports System.Text.RegularExpressions
Imports System

Public Class frmParser
    Private stack(500) As String
    Private stack2(500) As String
    Private br_stack(500) As String
    Private br_stack2(500) As String
    Private stack3(500) As String
    Private rules_stack(500) As String
    Private logicalop_stack(500) As String
    Private evaluate_stack(500) As String
    Private val As String = ""
    Private op As String = ""
    Private br_val As String = ""
    Private brl_val As String = ""
    Private br_op As String = ""
    Private brl_op As String = ""
    Dim condition As String
    Dim finaloutput As String
    Dim result As String
    Dim index As Integer = 0
    Dim index2 As Integer = 0
    Dim br_index As Integer = 0
    Dim br_index2 As Integer = 0
    Dim index3 As Integer = 0
    Dim index4 As Integer = 0
    Dim index5 As Integer = 0
    Dim index6 As Integer = 0
    Dim varindex As Integer = 0
    Dim type, new_type, brl_type As Object
    Dim value, newvalue, brl_value As Object
    Dim relop As String
    Dim newtype As Object
    Dim new_rule As String = String.Empty
    'Dim storeprop As DocProperty = New DocProperty("", "")

    Private Sub btnParse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParse.Click
        Dim rule As String
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()

        If (lstpreced.Text = "SELECT PRECEDENCE FOR RULE") Then
            MessageBox.Show("Please select a Precedence", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If (lstpreced.SelectedItem.ToString.Substring(0, 1) = "0") Then

            rule = Trim(txtRule.Text)
            Stack_Parenthesis(rule)
            EvaluateRule3()
        End If

        If (lstpreced.SelectedItem.ToString.Substring(0, 1) = "1") Then

            rule = Trim(txtRule.Text)
            Stack_Parenthesis(rule)
            EvaluateRule2()
        End If

        If (lstpreced.SelectedItem.ToString.Substring(0, 1) = "2") Then

            rule = Trim(txtRule.Text)
            Stack_Parenthesis(rule)
            EvaluateRule()
        End If

        If (lstpreced.SelectedItem.ToString.Substring(0, 1) = "3") Then

            rule = Trim(txtRule.Text)
            Stack_Parenthesis(rule)
            EvaluateRule4()
        End If

        'Transfer content of stack
        For k As Integer = 0 To stack.Length - 1
            If (stack(k) <> "") Then
                stack3(index6) = stack(k)
                ListBox1.Items.Add(stack(k))
            Else
                Exit For
            End If
            index6 += 1
        Next

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                ListBox2.Items.Add(rules_stack(i))
            End If
        Next

        For i As Integer = 0 To evaluate_stack.Length - 1
            If (evaluate_stack(i) = String.Empty And i = 0) Then
                Exit For
            Else
                If (evaluate_stack(i) = "") Then
                    Exit For
                End If
                ListBox3.Items.Add(evaluate_stack(i))
            End If
        Next

        EmptyStacks()
        resetVars()
        index5 = 0
        index6 = 0
        'MessageBox.Show("Done!", "Success")
    End Sub

    Private Sub Stack_Parenthesis(ByVal cur_rule As String)
        Dim counterStr As Integer
        counterStr = cur_rule.Length

        For i As Integer = 0 To counterStr - 1
            If (cur_rule.Chars(i).ToString.Equals("(") And (i = 0)) Then
                stack(index) = cur_rule.Chars(i).ToString.Trim
                index += 1

            ElseIf (cur_rule.Chars(i).ToString.Equals("(") And (i > 0) And cur_rule.Chars(i - 1).ToString <> "(") Then
                logicalop_stack(index4) = val.Trim
                stack(index) = val.Trim
                index += 1
                rules_stack(index3) = val.Trim
                index3 += 1
                evaluate_stack(index5) = val.Trim
                index5 += 1
                stack(index) = cur_rule.Chars(i).ToString.Trim
                index += 1
                index4 += 1
                val = ""

            ElseIf (cur_rule.Chars(i).ToString.Equals("(") And (i > 0) And cur_rule.Chars(i - 1).ToString = "(") Then
                stack(index) = cur_rule.Chars(i).ToString.Trim
                index += 1
                rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
                index3 += 1
                evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
                index5 += 1

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i = counterStr - 1)) Then
                If (op <> "" And val <> "") Then
                    stack(index) = op.Trim
                    index += 1
                    stack(index) = val.Trim
                    index += 1
                    value = val
                    val = ""
                    op = ""
                End If
                stack2(index2) = cur_rule.Chars(i).ToString.Trim
                index2 += 1
                If (cur_rule.Chars(i - 1).ToString <> ")") Then
                    For j As Integer = (i - 1) To 0 Step -1
                        If (stack(j) <> "(") Then
                            stack2(index2) = stack(j)
                            index2 += 1
                        ElseIf (stack(j) = "(") Then
                            stack2(index2) = stack(j)
                            index2 += 1
                            Exit For
                        Else
                            Exit For
                        End If
                    Next
                    stack(index) = cur_rule.Chars(i).ToString.Trim
                    CreateRule()
                ElseIf (cur_rule.Chars(i - 1).ToString = ")") Then
                    rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
                    index3 += 1
                    evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
                    index5 += 1
                    stack(index) = cur_rule.Chars(i).ToString.Trim
                Else
                End If

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i < counterStr - 1) And cur_rule.Chars(i - 1).ToString <> ")") Then
                If (op <> "" And val <> "") Then
                    stack(index) = op.Trim
                    index += 1
                    stack(index) = val.Trim
                    index += 1
                    value = val
                    val = ""
                    op = ""
                End If
                stack2(index2) = cur_rule.Chars(i).ToString.Trim
                index2 += 1
                For j As Integer = (i - 1) To 0 Step -1
                    If (stack(j) <> "(") Then
                        stack2(index2) = stack(j)
                        index2 += 1
                    ElseIf (stack(j) = "(") Then
                        stack2(index2) = stack(j)
                        index2 += 1
                        Exit For
                    Else
                        Exit For
                    End If

                Next
                stack(index) = cur_rule.Chars(i).ToString.Trim
                index += 1
                CreateRule()

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And (i < counterStr - 1) And cur_rule.Chars(i - 1).ToString = ")") Then
                stack(index) = cur_rule.Chars(i).ToString.Trim
                index += 1
                rules_stack(index3) = cur_rule.Chars(i).ToString.Trim
                index3 += 1
                evaluate_stack(index5) = cur_rule.Chars(i).ToString.Trim
                index5 += 1

            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
                val += cur_rule.Chars(i).ToString.Trim

            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
                Try
                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString.Trim <> String.Empty) Then
                        stack(index) = val.Trim
                        index += 1
                        type = val.Trim
                        val = ""
                    End If
                    op += cur_rule.Chars(i).ToString.Trim
                Catch
                    op += cur_rule.Chars(i).ToString.Trim
                End Try

            Else
                Continue For
            End If
        Next

    End Sub

    Private Sub Break_Rule(ByVal cur_rule As String)
        Dim counterStr As Integer
        counterStr = cur_rule.Length

        For i As Integer = 0 To counterStr - 1
            If (cur_rule.Chars(i).ToString.Equals("(") And i = 0) Then
                br_stack(br_index) = cur_rule.Chars(i).ToString.Trim
                br_index += 1

            ElseIf (cur_rule.Chars(i).ToString.Equals(")") And i = counterStr - 1) Then
                If (br_op <> "" And br_val <> "") Then
                    br_stack(br_index) = br_op.Trim
                    br_index += 1
                    br_stack(br_index) = br_val.Trim
                    newvalue = br_val.Trim

                End If
                For j As Integer = (i - 1) To 0 Step -1
                    If (br_stack(j) <> "(") Then
                        br_stack2(br_index2) = br_stack(j)
                    Else
                        Exit For
                    End If
                    br_index2 += 1
                Next

            ElseIf ((cur_rule.Chars(i).ToString <> "<") And (cur_rule.Chars(i).ToString <> ">") And (cur_rule.Chars(i).ToString <> "=") And (Trim(cur_rule.Chars(i).ToString) <> "")) Then
                br_val += cur_rule.Chars(i).ToString.Trim

            ElseIf ((cur_rule.Chars(i).ToString = "<") Or (cur_rule.Chars(i).ToString = ">") Or (cur_rule.Chars(i).ToString = "=")) Then
                Try
                    If ((cur_rule.Chars(i + 1).ToString <> "<") And (cur_rule.Chars(i + 1).ToString <> ">") And (cur_rule.Chars(i + 1).ToString <> "=") And cur_rule.Chars(i + 1).ToString <> String.Empty) Then
                        br_stack(br_index) = br_val.Trim
                        new_type = br_val.Trim
                        br_val = ""

                    End If
                    br_op += cur_rule.Chars(i).ToString.Trim
                    br_index += 1

                Catch
                    br_op += cur_rule.Chars(i).ToString.Trim
                End Try

            Else
                Continue For
            End If
        Next
    End Sub

    Private Sub Break_Rule_Logical(ByVal cur_rule As String)
        brl_op = String.Empty
        brl_val = String.Empty
        brl_type = String.Empty
        brl_value = String.Empty
        Dim counterStr As Integer
        cur_rule = cur_rule.Trim
        counterStr = cur_rule.Length

        For i As Integer = 0 To counterStr - 1
            If (cur_rule.Chars(i).ToString.Trim <> String.Empty And cur_rule.Substring(i, 2).ToUpper = "OR") Then
                brl_op = "OR"
                brl_val = cur_rule.Substring(i + 2, (cur_rule.Length - 1) - (i + 2)).Trim
                brl_value = brl_val.Trim

                Exit For
            ElseIf (cur_rule.Chars(i).ToString <> String.Empty And cur_rule.Substring(i, 3).ToUpper = "AND") Then
                brl_op = "AND"
                brl_val = cur_rule.Substring(i + 3, (cur_rule.Length - 1) - (i + 3)).Trim
                brl_value = brl_val.Trim

                Exit For
            ElseIf (cur_rule.Chars(i).ToString <> String.Empty And cur_rule.Substring(i, 3).ToUpper = "NOT") Then
                brl_op = "NOT"
                brl_val = cur_rule.Substring(i + 3, (cur_rule.Length - 1) - (i + 3)).Trim
                brl_value = brl_val.Trim

                Exit For
            ElseIf (i = 1 And cur_rule.Chars(i).ToString.Trim <> String.Empty) Then
                Dim j As Integer = i
                While (1)
                    If (cur_rule.Substring(j, 2).ToUpper = "OR" Or cur_rule.Substring(j, 3).ToUpper = "AND" Or cur_rule.Substring(j, 3).ToUpper = "NOT") Then
                        Exit While
                    Else
                        brl_val += cur_rule.Chars(j).ToString.Trim
                        j += 1
                    End If
                End While
                brl_type = brl_val.Trim

            Else
                Continue For
            End If
        Next
        condition = ParseLRule(brl_type, brl_value, brl_op)

        'End While

    End Sub

    Private Sub StoreRules(ByVal newrule As String)
        'resetVars()
        evaluate_stack(index5) = newrule

        EmptyCompStacks()
        Break_Rule(newrule)
        result = Parse(new_type, newvalue, br_op)
        rules_stack(index3) = result
        index3 += 1
        index5 += 1

        newrule = String.Empty

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
        For p As Integer = evaluate_stack.Length - 1 To 0 Step -1
            evaluate_stack(p) = String.Empty
        Next
        For k As Integer = br_stack.Length - 1 To 0 Step -1
            br_stack(k) = String.Empty
        Next
        For p As Integer = br_stack2.Length - 1 To 0 Step -1
            br_stack2(p) = String.Empty
        Next
    End Sub

    Private Sub EmptyCompStacks()
        For k As Integer = br_stack.Length - 1 To 0 Step -1
            br_stack(k) = String.Empty
        Next
        For p As Integer = br_stack2.Length - 1 To 0 Step -1
            br_stack2(p) = String.Empty
        Next
        br_index = 0
        br_index2 = 0
        br_val = ""
        br_op = ""
        brl_val = ""
        brl_op = ""
    End Sub

    Private Function Parse(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
        'Dim reg_exp As New Regex("(\d+)|(\d+\.\d+)")
        Dim reg_exp As New Regex("\d+(\.\d+)?")
        Try
            Stop
            newtype = "150000"
            Select Case nop
                Case ">"
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) > Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) > Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case "<"
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) < Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) < Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case "<="
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) <= Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) <= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case ">="
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) >= Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) >= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case "="
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) = Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) = Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case "<>"
                    If reg_exp.IsMatch(newtype) Then
                        If (Double.Parse(newtype) <> Double.Parse(nvalue)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    Else
                        If (Convert.ChangeType(newtype, TypeCode.Object) <> Convert.ChangeType(nvalue, TypeCode.Object)) Then
                            Parse = "True"
                        Else
                            Parse = "False"
                        End If
                    End If
                Case Else
                    Parse = "Invalid Operator!"
            End Select
        Catch ex As Exception
            Parse = ex.Message
        End Try
    End Function

    Private Function ParseLRule(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
        Try
            Select Case nop.ToUpper
                Case "OR"
                    If (Boolean.Parse(ntype) Or Boolean.Parse(nvalue)) Then
                        ParseLRule = "True"
                    Else
                        ParseLRule = "False"
                    End If
                Case "AND"
                    If (Boolean.Parse(ntype) And Boolean.Parse(nvalue)) Then
                        ParseLRule = "True"
                    Else
                        ParseLRule = "False"
                    End If
                Case "NOT"
                    ParseLRule = "Pending"
                Case Else
                    ParseLRule = "Invalid Operator!"
            End Select
        Catch ex As Exception
            ParseLRule = ex.Message
        End Try
    End Function

    Private Function LParse(ByVal rule As String) As String
        If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")" And rule.Length <= 5) Then
            LParse = rule
            'txtcondition.Text = LParse
        Else
            LParse = LogicalTest(rule)
        End If
    End Function

    Private Function LogicalTest(ByVal rule As String) As String
        Dim currentrule As String

        If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")") Then
            currentrule = "(" & rule & ")"
            Break_Rule_Logical(currentrule)

            LogicalTest = condition
        ElseIf (rule.Chars(0).ToString.Trim = "(" And rule.Chars(rule.Length - 1).ToString.Trim = ")") Then
            currentrule = rule
            'EmptyCompStacks()
            Break_Rule_Logical(currentrule)

            LogicalTest = condition
        Else
            LogicalTest = ""
        End If
    End Function

    Private Sub EvaluateRule()
        Dim logicalrule As String = ""
        Dim newstack1(500) As String
        Dim newstack2(500) As String
        Dim stackindex As Integer = -1
        Dim stackindex2 As Integer = -1
        Dim indexcoll As New ArrayList
        Dim listindex As Integer = 0
        Dim counter As Integer = 0
        Dim stack_rules() As String
        Dim count As Integer = 0
        Dim coindex As Integer = 0

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                counter += 1
            End If
        Next

        'Exit Sub
        Dim length As Integer = (counter + 2) - 1
        ReDim stack_rules(length)
        stack_rules(0) = "("
        stack_rules(stack_rules.Length - 1) = ")"

        counter = 1
        For i As Integer = 0 To stack_rules.Length - 1

            If (rules_stack(i) = "") Then
                Exit For
            Else
                stack_rules(counter) = rules_stack(i)
            End If
            counter += 1
        Next

        'Clear stack
        For p As Integer = rules_stack.Length - 1 To 0 Step -1
            rules_stack(p) = String.Empty
        Next

        For i As Integer = 0 To stack_rules.Length - 1
            If (stack_rules(i) = "" And i = 0) Then

                Exit For
            Else
                If (stack_rules(i) = "") Then

                    Exit For
                End If
                If (stack_rules(i) = "(") Then
                    stackindex2 = stackindex + 1
                    newstack2(stackindex2) = stack_rules(i)
                    indexcoll.Add(stackindex2)

                ElseIf (stack_rules(i).ToUpper = "TRUE" Or stack_rules(i).ToUpper = "FALSE") Then
                    stackindex += 1
                    newstack1(stackindex) = stack_rules(i)

                ElseIf (stack_rules(i).ToUpper = "AND" Or stack_rules(i).ToUpper = "OR") Then
                    stackindex += 1
                    newstack1(stackindex) = stack_rules(i)

                ElseIf (stack_rules(i) = ")") Then
                    count = 0
                    coindex = 0
                    logicalrule = ""
                    stackindex2 = Integer.Parse(indexcoll.Item(indexcoll.Count - 1).ToString)
                    If (stackindex - stackindex2 > 2) Then
                        For t As Integer = stackindex2 To stackindex
                            If (newstack1(t) = "") Then
                                Exit For
                            Else
                                rules_stack(coindex) = newstack1(t)
                            End If
                            coindex += 1
                        Next
                    End If
                    While (stackindex >= stackindex2)
                        logicalrule += newstack1(stackindex)
                        newstack1(stackindex) = ""
                        stackindex -= 1
                        count += 1
                    End While
                    newstack2(stackindex2) = ""
                    indexcoll.RemoveAt(indexcoll.Count - 1)
                    'stackindex2 -= 1
                    stackindex += 1
                    If (count > 3) Then
                        EvaluateRule2()
                        newstack1(stackindex) = txtcondition.Text
                    ElseIf (count = 3) Then
                        newstack1(stackindex) = LParse(logicalrule)
                    Else
                        MessageBox.Show("This Rule cannot be parsed! Please check you rule.")
                    End If

                Else
                End If
            End If
        Next
        If (newstack1(1) = "") Then
            finaloutput = newstack1(0)
            txtcondition.Text = newstack1(0)
        Else
            MessageBox.Show("Your rule does not conform with the acceptable standard! Correct your rule and try again.")
        End If

    End Sub

    Private Sub EvaluateRule2()
        Dim logicalrule As String = ""
        Dim stackindex As Integer = -1
        Dim stackindex2 As Integer = -1
        Dim indexcoll As New ArrayList
        Dim newstack1 As New ArrayList
        Dim newstack2 As New ArrayList
        Dim listindex As Integer = 0
        Dim counter As Integer = 0
        Dim stack_rules() As String
        Dim count As Integer
        Dim listcounter1 As Integer = 0
        Dim listcounter2 As Integer = 0

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                counter += 1
            End If
        Next
        Dim length As Integer = (counter + 1) - 1
        ReDim stack_rules(length)
        stack_rules(stack_rules.Length - 1) = ")"

        counter = 0
        For i As Integer = 0 To stack_rules.Length - 1

            If (rules_stack(i) = "") Then
                Exit For
            Else
                stack_rules(counter) = rules_stack(i)
            End If
            counter += 1
        Next

        For i As Integer = 0 To stack_rules.Length - 1
            If (stack_rules(i) = "" And i = 0) Then
                Exit For
            Else
                If (stack_rules(i) = "") Then
                    Exit For
                End If
                If (stack_rules(i).ToUpper = "AND" Or stack_rules(i).ToUpper = "OR") Then
                    stackindex += 1
                    newstack1.Insert(stackindex, stack_rules(i))

                ElseIf (stack_rules(i).ToUpper = "TRUE" Or stack_rules(i).ToUpper = "FALSE") Then
                    stackindex += 1
                    newstack1.Insert(stackindex, stack_rules(i))

                ElseIf (stack_rules(i) = ")") Then
                    logicalrule = ""
                    count = 0

                    While (count >= 0)
                        logicalrule = ""

                        If (count = 0) Then
                            For k As Integer = 0 To newstack1.Count - 1
                                logicalrule = ""

                                If (newstack1.Count = k) Then
                                    Exit For
                                End If
                                If (newstack1.Item(k).ToString.ToUpper = "AND") Then
                                    stackindex = k
                                    logicalrule += newstack1.Item(k - 1).ToString
                                    logicalrule += newstack1.Item(k).ToString
                                    logicalrule += newstack1.Item(k + 1).ToString
                                    newstack1.RemoveAt(k + 1)
                                    newstack1.RemoveAt(k)
                                    newstack1.RemoveAt(k - 1)

                                    stackindex -= 1

                                    newstack1.Insert(stackindex, LParse(logicalrule))
                                    If (newstack1.Count = 1) Then
                                        Exit For
                                    Else
                                        k = -1
                                    End If

                                Else
                                End If
                            Next
                        Else
                            If (newstack1.Count > 1) Then
                                For k As Integer = 0 To newstack1.Count - 1
                                    logicalrule = ""

                                    If (newstack1.Count = k) Then
                                        Exit For
                                    End If
                                    If (newstack1.Item(k).ToString.ToUpper = "OR") Then
                                        stackindex = k
                                        logicalrule += newstack1.Item(k - 1).ToString
                                        logicalrule += newstack1.Item(k).ToString
                                        logicalrule += newstack1.Item(k + 1).ToString
                                        newstack1.RemoveAt(k + 1)
                                        newstack1.RemoveAt(k)
                                        newstack1.RemoveAt(k - 1)

                                        stackindex -= 1

                                        newstack1.Insert(stackindex, LParse(logicalrule))
                                        If (newstack1.Count = 1) Then
                                            Exit For
                                        Else
                                            k = -1
                                        End If

                                    Else
                                    End If
                                Next

                            Else
                                Exit For
                            End If
                        End If
                        count += 1
                    End While
                    Exit For
                Else
                End If
            End If
        Next
        If (newstack1.Count = 1) Then
            finaloutput = newstack1.Item(0).ToString
            txtcondition.Text = newstack1.Item(0).ToString
        Else
            MessageBox.Show("Your rule does not conform with the acceptable standard! Correct your rule and try again.")
        End If

    End Sub

    Private Sub EvaluateRule3()
        Dim logicalrule As String = ""
        Dim stackindex As Integer = -1
        Dim stackindex2 As Integer = -1
        Dim indexcoll As New ArrayList
        Dim newstack1 As New ArrayList
        Dim newstack2 As New ArrayList
        Dim listindex As Integer = 0
        Dim counter As Integer = 0
        Dim stack_rules() As String
        Dim listcounter1 As Integer = 0
        Dim listcounter2 As Integer = 0

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                counter += 1
            End If
        Next
        Dim length As Integer = (counter + 1) - 1
        ReDim stack_rules(length)
        stack_rules(stack_rules.Length - 1) = ")"

        counter = 0
        For i As Integer = 0 To stack_rules.Length - 1

            If (rules_stack(i) = "") Then
                Exit For
            Else
                stack_rules(counter) = rules_stack(i)
            End If
            counter += 1
        Next

        For i As Integer = 0 To stack_rules.Length - 1
            If (stack_rules(i) = "" And i = 0) Then
                Exit For
            Else
                If (stack_rules(i) = "") Then
                    Exit For
                End If
                If (stack_rules(i).ToUpper = "AND" Or stack_rules(i).ToUpper = "OR") Then
                    stackindex += 1
                    newstack1.Insert(stackindex, stack_rules(i))

                ElseIf (stack_rules(i).ToUpper = "TRUE" Or stack_rules(i).ToUpper = "FALSE") Then
                    stackindex += 1
                    newstack1.Insert(stackindex, stack_rules(i))
                ElseIf (stack_rules(i) = ")") Then
                    logicalrule = ""
                    For k As Integer = 0 To newstack1.Count - 1
                        logicalrule = ""
                        If (newstack1.Count = k) Then
                            Exit For
                        End If
                        If (newstack1.Item(k).ToString.ToUpper = "AND" Or newstack1.Item(k).ToString.ToUpper = "OR") Then
                            stackindex = k
                            logicalrule += newstack1.Item(k - 1).ToString
                            logicalrule += newstack1.Item(k).ToString
                            logicalrule += newstack1.Item(k + 1).ToString
                            newstack1.RemoveAt(k + 1)
                            newstack1.RemoveAt(k)
                            newstack1.RemoveAt(k - 1)

                            stackindex -= 1

                            newstack1.Insert(stackindex, LParse(logicalrule))
                            If (newstack1.Count = 1) Then
                                Exit For
                            Else
                                k = -1
                            End If

                        Else
                        End If
                    Next
                Else
                End If
            End If
        Next
        If (newstack1.Count = 1) Then
            finaloutput = newstack1.Item(0).ToString
            txtcondition.Text = newstack1.Item(0).ToString
        Else
            MessageBox.Show("Your rule does not conform with the acceptable standard! Correct your rule and try again.")
        End If

    End Sub

    Private Sub EvaluateRule4()
        Dim logicalrule As String = ""
        Dim newstack1(500) As String
        Dim newstack2(500) As String
        Dim stackindex As Integer = -1
        Dim stackindex2 As Integer = -1
        Dim indexcoll As New ArrayList
        Dim listindex As Integer = 0
        Dim counter As Integer = 0
        Dim stack_rules() As String
        Dim count As Integer = 0
        Dim coindex As Integer = 0

        For i As Integer = 0 To rules_stack.Length - 1
            If (rules_stack(i) = "" And i = 0) Then
                Exit For
            Else
                If (rules_stack(i) = "") Then
                    Exit For
                End If
                counter += 1
            End If
        Next

        'Exit Sub
        Dim length As Integer = (counter + 2) - 1
        ReDim stack_rules(length)
        stack_rules(0) = "("
        stack_rules(stack_rules.Length - 1) = ")"

        counter = 1
        For i As Integer = 0 To stack_rules.Length - 1

            If (rules_stack(i) = "") Then
                Exit For
            Else
                stack_rules(counter) = rules_stack(i)
            End If
            counter += 1
        Next

        'Clear stack
        For p As Integer = rules_stack.Length - 1 To 0 Step -1
            rules_stack(p) = String.Empty
        Next

        For i As Integer = 0 To stack_rules.Length - 1
            If (stack_rules(i) = "" And i = 0) Then

                Exit For
            Else
                If (stack_rules(i) = "") Then

                    Exit For
                End If
                If (stack_rules(i) = "(") Then
                    stackindex2 = stackindex + 1
                    newstack2(stackindex2) = stack_rules(i)
                    indexcoll.Add(stackindex2)

                ElseIf (stack_rules(i).ToUpper = "TRUE" Or stack_rules(i).ToUpper = "FALSE") Then
                    stackindex += 1
                    newstack1(stackindex) = stack_rules(i)

                ElseIf (stack_rules(i).ToUpper = "AND" Or stack_rules(i).ToUpper = "OR") Then
                    stackindex += 1
                    newstack1(stackindex) = stack_rules(i)

                ElseIf (stack_rules(i) = ")") Then
                    count = 0
                    coindex = 0
                    logicalrule = ""
                    stackindex2 = Integer.Parse(indexcoll.Item(indexcoll.Count - 1).ToString)
                    If (stackindex - stackindex2 > 2) Then
                        For t As Integer = stackindex2 To stackindex
                            If (newstack1(t) = "") Then
                                Exit For
                            Else
                                rules_stack(coindex) = newstack1(t)
                            End If
                            coindex += 1
                        Next
                    End If
                    While (stackindex >= stackindex2)
                        logicalrule += newstack1(stackindex)
                        newstack1(stackindex) = ""
                        stackindex -= 1
                        count += 1
                    End While
                    newstack2(stackindex2) = ""
                    indexcoll.RemoveAt(indexcoll.Count - 1)
                    stackindex += 1
                    If (count > 3) Then
                        EvaluateRule3()
                        newstack1(stackindex) = txtcondition.Text
                    ElseIf (count = 3) Then
                        newstack1(stackindex) = LParse(logicalrule)
                    Else
                        MessageBox.Show("This Rule cannot be parsed! Please check you rule.")
                    End If

                Else
                End If
            End If
        Next
        If (newstack1(1) = "") Then
            finaloutput = newstack1(0)
            txtcondition.Text = newstack1(0)
        Else
            MessageBox.Show("Your rule does not conform with the acceptable standard! Correct your rule and try again.")
        End If

    End Sub

    Private Sub resetVars()
        index = 0
        index2 = 0
        index3 = 0
        index4 = 0
        index5 = 0
        val = String.Empty
        op = String.Empty
        new_rule = String.Empty
    End Sub

    Private Sub frmParser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim storeproperty As New DocProperties
        'storeproperty.propListType.Clear()
        'storeproperty.propListValue.Clear()
        'storeprop.StoreParameters()

        'txtRule.Text = "(amount>150000)"

    End Sub

    Private Sub btnlogictest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogictest.Click
        txtcondition.Text = LParse(Trim(txtRule.Text))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim str As String
        Dim list As New ArrayList
        str = txtRule.Text
        MessageBox.Show(str.Substring(5, 2))

        list.Add("I")
        list.Add("Love")
        list.Add("My")
        list.Add("Mummy")

        list.RemoveAt(list.IndexOf("My"))
        For i As Integer = 0 To list.Count - 1
            MessageBox.Show(list.Item(i) & ": " & list.IndexOf(list.Item(i)))
        Next
    End Sub
End Class
