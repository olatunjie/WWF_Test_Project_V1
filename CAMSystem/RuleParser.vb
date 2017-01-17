Imports System.Text.RegularExpressions
Imports System

Public Class RuleParser
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
    Private documentID As Integer
    Private documentType As String = ""
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
    Dim newtype As String
    Dim new_rule As String = String.Empty
    'Dim doc As New Document
    'Dim assign As New Assignment
    Dim storex As New DAL
    Dim errorMsg As String = ""
    Dim user As String = String.Empty
    Private ruleparts As New ArrayList
    Private ruleparts2 As New ArrayList
    Private done As Boolean = False
    Private actions As New ArrayList
    Private tracker As Integer = 0
    Private ruleLength As Integer = 0
    Private state As Boolean = False
    Dim countIndex As Integer = 0

    Public Sub New()
        ruleparts.Clear()
        ruleparts2.Clear()
        done = False
        actions.Clear()
        tracker = 0
        ruleLength = 0
        state = False
        countIndex = 0
        errorMsg = String.Empty
        user = String.Empty
        documentType = String.Empty
    End Sub

    Public Sub Execute(ByVal current_rule As String, ByVal doctype As String, ByVal owner As String)
        Try
            Dim rule As String
            Dim count As Integer = 1
            Dim cond As String = String.Empty
            Dim action As String = String.Empty
            Dim constmt As String = String.Empty
            Dim truthval As String = String.Empty
            Dim bracepos As Integer = 0
            tracker += 1
            documentType = doctype
            user = owner
            ruleparts.Clear()

            rule = current_rule.Trim

            If (tracker = 1) Then
                ruleLength = rule.Length
            End If

            If (done = True) Then
                ExecuteRule(ruleparts2)
                Exit Sub
            End If

            If (Left(rule, 2).ToUpper = "IF") Then
                ruleparts.Add(Left(rule, 2))
                count = 2
            End If
            If (Left(rule, 6).ToUpper = "ELSEIF") Then
                ruleparts.Add(Left(rule, 6))
                count = 6
            End If
            If (Left(rule, 4).ToUpper = "ELSE" And Left(rule, 6).ToUpper <> "ELSEIF") Then
                ruleparts.Add(Left(rule, 4))
                count = 4
            End If

            If (Mid(rule, count + 1, 1) = " ") Then
                count = count + 1
            End If

            If (Mid(rule, count + 1, 1) = "(") Then
                ruleparts.Add(Mid(rule, count + 1, InStr(rule, "{") - InStr(rule, "(")))
                count = InStr(rule, "{") - 1
            End If

            If (Mid(rule, count + 1, 1) = " ") Then
                count = count + 1
            End If

            If (Mid(rule, count + 1, 1) = "{") Then
                ruleparts.Add("{")
                bracepos = MatchBraceIndex(rule)
                ruleparts.Add(Mid(rule, count + 2, bracepos - (count + 2)))
                ruleparts.Add("}")
                count = bracepos
            End If

            constmt = ruleparts.Item(0).ToString.ToUpper
            If (constmt = "IF") Then
                cond = ruleparts.Item(1).ToString
                ruleparts2.Add(constmt)
                truthval = Run(cond)
                ruleparts2.Add(truthval)
                If (Boolean.Parse(truthval)) Then
                    ruleparts2.Add("{")
                    action = ruleparts.Item(3).ToString
                    If (Left(action.Trim, 2).ToUpper = "IF" Or Left(action.Trim, 4).ToUpper = "ELSE" Or Left(action.Trim, 6).ToUpper = "ELSEIF") Then
                        Execute(action, documentType, user)
                    Else
                        ruleparts2.Add(action)
                        ruleparts2.Add("}")

                        If (Mid(rule, count, 1) = "}") Then
                            count = 1
                            rule = ""
                            done = True
                            Execute(rule, documentType, user)
                        End If
                    End If
                Else
                    bracepos = MatchBraceIndex(rule)
                    action = Mid(rule, bracepos + 1)
                    If (Left(action.Trim, 2).ToUpper = "IF" Or Left(action.Trim, 4).ToUpper = "ELSE" Or Left(action.Trim, 6).ToUpper = "ELSEIF") Then
                        Execute(action, documentType, user)
                    Else
                        If (Mid(rule, count, 1) = "}" And Mid(rule, count + 1).Trim <> String.Empty) Then
                            action = Mid(rule, count + 1)
                            Execute(action, documentType, user)
                        Else
                            'If (count = rule.Length) Then
                            count = 1
                            rule = ""
                            done = True
                            Execute(rule, documentType, user)
                            'Else
                            'End If
                        End If
                    End If
                End If
            ElseIf (constmt = "ELSEIF") Then
                cond = ruleparts.Item(1).ToString
                ruleparts2.Add(constmt)
                truthval = Run(cond)
                ruleparts2.Add(truthval)
                If (Boolean.Parse(truthval)) Then
                    ruleparts2.Add("{")
                    action = ruleparts.Item(3).ToString
                    If (Left(action.Trim, 2).ToUpper = "IF" Or Left(action.Trim, 4).ToUpper = "ELSE" Or Left(action.Trim, 6).ToUpper = "ELSEIF") Then
                        Execute(action, documentType, user)
                    Else
                        ruleparts2.Add(action)
                        ruleparts2.Add("}")

                        If (Mid(rule, count, 1) = "}") Then
                            count = 1
                            rule = ""
                            done = True

                            Execute(rule, documentType, user)
                        End If
                    End If
                Else
                    bracepos = MatchBraceIndex(rule)
                    action = Mid(rule, bracepos + 1)
                    If (Left(action.Trim, 2).ToUpper = "IF" Or Left(action.Trim, 4).ToUpper = "ELSE" Or Left(action.Trim, 6).ToUpper = "ELSEIF") Then
                        Execute(action, documentType, user)
                    Else

                        If (Mid(rule, count, 1) = "}" And Mid(rule, count + 1).Trim <> String.Empty) Then
                            action = Mid(rule, count + 1)
                            Execute(action, documentType, user)
                        Else
                            'If (count = rule.Length) Then
                            count = 1
                            rule = ""
                            done = True
                            Execute(rule, documentType, user)
                            'Else
                            'End If
                        End If
                    End If
                End If
            ElseIf (constmt = "ELSE") Then
                ruleparts2.Add(constmt)
                action = ruleparts.Item(2).ToString
                If (Left(action.Trim, 2).ToUpper = "IF") Then
                    Execute(action, documentType, user)
                Else
                    ruleparts2.Add("{")
                    ruleparts2.Add(action)
                    ruleparts2.Add("}")
                    'If (count = rule.Length) Then
                    count = 1
                    rule = ""
                    done = True
                    Execute(rule, documentType, user)
                    'Else
                    'End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            errorMsg += " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Execute()")
        End Try
    End Sub

    Function MatchBraceIndex(ByVal currule As String) As Integer
        Dim stack As New ArrayList

        For i As Integer = 0 To currule.Length - 1
            If (currule.Chars(i).ToString = "{") Then
                stack.Add(currule.Chars(i).ToString)
            End If

            If (currule.Chars(i).ToString = "}") Then
                stack.RemoveAt(stack.Count - 1)
                If (stack.Count = 0) Then
                    Return i + 1
                End If
            End If
        Next
    End Function

    Sub ExecuteRule(ByVal rulelist As ArrayList)
        Try
            If (state = True) Then
                Exit Sub
            End If

            While (countIndex <= rulelist.Count - 1)
                If (rulelist.Count > 0) Then
                    If (rulelist.Item(countIndex).ToString.ToUpper = "IF" And rulelist.Item(countIndex + 1).ToString.ToUpper = "TRUE") Then
                        countIndex = countIndex + 3
                        ExecuteRule(rulelist)
                    ElseIf (rulelist.Item(countIndex).ToString.ToUpper = "ELSEIF" And rulelist.Item(countIndex + 1).ToString.ToUpper = "TRUE") Then
                        countIndex = countIndex + 3
                        ExecuteRule(rulelist)
                    ElseIf (rulelist.Item(countIndex).ToString.ToUpper = "ELSE") Then
                        If (rulelist.Item(countIndex + 1).ToString.ToUpper = "IF" Or rulelist.Item(countIndex + 1).ToString.ToUpper = "ELSEIF") Then
                            countIndex = countIndex + 1
                            ExecuteRule(rulelist)
                        Else
                            countIndex = countIndex + 2
                            ExecuteRule(rulelist)
                        End If
                    ElseIf (rulelist.Item(countIndex).ToString.ToUpper = "IF" And rulelist.Item(countIndex + 1).ToString.ToUpper = "FALSE") Then
                        countIndex = countIndex + 2
                        ExecuteRule(rulelist)
                    ElseIf (rulelist.Item(countIndex).ToString.ToUpper = "ELSEIF" And rulelist.Item(countIndex + 1).ToString.ToUpper = "FALSE") Then
                        countIndex = countIndex + 2
                        ExecuteRule(rulelist)
                    Else
                        state = True
                        If (rulelist.Item(countIndex).ToString <> String.Empty) Then
                            ParseStmt(rulelist.Item(countIndex).ToString)
                        End If
                        countIndex = countIndex + 2
                        ExecuteRule(rulelist)
                    End If
                End If
            End While
        Catch ex As Exception
            errorMsg += " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::ExecuteRule()")
        End Try
        
    End Sub

    Sub ParseStmt(ByVal actn As String)
        Try
            Dim pos As Integer = InStr(actn, ";")
            Dim actns As String()

            If (pos > 0) Then
                If (Mid(actn, pos + 1).Trim <> String.Empty) Then
                    actns = Split(actn, ";")
                    For i As Integer = 0 To actns.Length - 1
                        actions.Add(actns(i))
                    Next
                ElseIf (Mid(actn, pos + 1).Trim = String.Empty) Then
                    actn = actn.Trim
                    actions.Add(Left(actn, actn.Length - 1))
                ElseIf (actn.Trim = String.Empty) Then
                    Exit Sub
                Else
                    Exit Sub
                End If
            End If
            If (pos <= 0) Then
                actn = actn.Trim
                actions.Add(Left(actn, actn.Length))
            End If
        Catch ex As Exception
            errorMsg += " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::ParseStmt()")
        End Try
        
    End Sub

    Public Function Run(ByVal crule As String) As String
        Try
            EmptyStacks()
            resetVars()
            index5 = 0
            index6 = 0
            Stack_Parenthesis(crule)
            EvaluateRule()
            Return GetResult()
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Run()")
            Return String.Empty
        End Try

    End Function

    Public Sub Stack_Parenthesis(ByVal cur_rule As String)
        Try
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

        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Stack_Parenthesis()")
        End Try

    End Sub

    Public Sub Break_Rule(ByVal cur_rule As String)
        Try
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
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Break_Rule()")
        End Try

    End Sub

    Public Sub Break_Rule_Logical(ByVal cur_rule As String)
        Try
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
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Break_Rule_Logical()")
        End Try

    End Sub

    Public Sub StoreRules(ByVal newrule As String)
        Try
            evaluate_stack(index5) = newrule

            EmptyCompStacks()
            Break_Rule(newrule)
            result = Parse(new_type, newvalue, br_op)
            rules_stack(index3) = result
            index3 += 1
            index5 += 1

            newrule = String.Empty
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::StoreRules()")
        End Try

    End Sub

    Public Sub CreateRule()
        Try
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
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::CreateRule()")
        End Try

    End Sub

    Public Sub EmptyStacks()
        Try
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
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EmptyStacks()")
        End Try

    End Sub

    Public Sub EmptyCompStacks()
        Try
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
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EmptyCompStacks()")
        End Try

    End Sub

    Public Function Parse(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
        Dim reg_exp As New Regex("\d+(\.\d+)?")
        Dim reg_exp_str As New Regex("^\w*$")

        Try

            newtype = storex.getPropertiesVal(documentType, ntype, user)

            Select Case nop
                Case ">"
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) > Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) > Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) > Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try

                Case "<"
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) < Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) < Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else

                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) < Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try
                Case "<="
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) <= Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) <= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else

                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) <= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try
                Case ">="
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) >= Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) >= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else

                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) >= Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try
                Case "="
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) = Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) = Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        Else
                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) = Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try
                Case "<>"
                    Try
                        If reg_exp.IsMatch(newtype) Then

                            If (Double.Parse(newtype) <> Double.Parse(nvalue)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If

                        ElseIf reg_exp_str.IsMatch(newtype) Then
                            If (Convert.ChangeType(newtype, TypeCode.Object) <> Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else

                            Parse = "False"
                        End If
                    Catch ex As Exception
                        If reg_exp_str.IsMatch(newtype) Then

                            If (Convert.ChangeType(newtype, TypeCode.Object) <> Convert.ChangeType(nvalue, TypeCode.Object)) Then
                                Parse = "True"
                            Else
                                Parse = "False"
                            End If
                        Else
                            Parse = "False"
                        End If
                    End Try
                Case Else
                    Parse = "False"
                    errorMsg = errorMsg + " " + "Invalid Operator!"
                    SaveErrorMsg(documentType, "Invalid Operator!" & " - Module:- RuleParser::Parse()")
            End Select
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::Parse()")
            Parse = "False"
        End Try
    End Function

    Public Function ParseLRule(ByVal ntype As String, ByVal nvalue As String, ByVal nop As String) As String
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
                    ParseLRule = "False"
                    errorMsg = errorMsg + " " + "Invalid Operator!"
            End Select
        Catch ex As Exception
            ParseLRule = "False"
            errorMsg = errorMsg + " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::ParseLRule()")
        End Try
    End Function

    Public Function LParse(ByVal rule As String) As String
        Try
            If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")" And rule.Length <= 5) Then
                LParse = rule
            Else
                LParse = LogicalTest(rule)
            End If
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::LParse()")
            LParse = ""
        End Try

    End Function

    Public Function LogicalTest(ByVal rule As String) As String
        Try
            Dim currentrule As String

            If (rule.Chars(0).ToString.Trim <> "(" And rule.Chars(rule.Length - 1).ToString.Trim <> ")") Then
                currentrule = "(" & rule & ")"
                Break_Rule_Logical(currentrule)

                LogicalTest = condition
            ElseIf (rule.Chars(0).ToString.Trim = "(" And rule.Chars(rule.Length - 1).ToString.Trim = ")") Then
                currentrule = rule
                Break_Rule_Logical(currentrule)

                LogicalTest = condition
            Else
                LogicalTest = ""
            End If
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::LogicalTest()")
            LogicalTest = ""
        End Try

    End Function

    Public Sub EvaluateRule()
        Try
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
                            EvaluateRule2()
                            newstack1(stackindex) = finaloutput
                        ElseIf (count = 3) Then
                            newstack1(stackindex) = LParse(logicalrule)
                        ElseIf (count = 1) Then
                            newstack1(stackindex) = logicalrule
                        Else
                            errorMsg = errorMsg + " " + "This Rule cannot be parsed! Please check your rule."
                        End If

                    Else
                    End If
                End If
            Next
            If (newstack1(1) = "") Then
                finaloutput = newstack1(0)
            Else
                errorMsg = errorMsg + " " + "Your rule does not conform with the acceptable standard! Correct your rule and try again."
                SaveErrorMsg(documentType, "Your rule does not conform with the acceptable standard! Correct your rule and try again.")
            End If
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EvaluateRule()")
        End Try
    End Sub

    Public Sub EvaluateRule2()
        Try
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
            Else
                errorMsg = errorMsg + " " + "Your rule does not conform with the acceptable standard! Correct your rule and try again."
                SaveErrorMsg(documentType, "Your rule does not conform with the acceptable standard! Correct your rule and try again." & " - Module:- RuleParser::EvaluateRule2()")
            End If
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EvaluateRule2()")
        End Try
    End Sub

    Public Sub EvaluateRule3()
        Try
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
            Else
                errorMsg = errorMsg + " " + "Your rule does not conform with the acceptable standard! Correct your rule and try again."
                SaveErrorMsg(documentType, "Your rule does not conform with the acceptable standard! Correct your rule and try again." & " - Module:- RuleParser::EvaluateRule3()")
            End If

        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EvaluateRule3()")
        End Try

    End Sub

    Public Sub EvaluateRule4()
        Try
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
                            newstack1(stackindex) = finaloutput
                        ElseIf (count = 3) Then
                            newstack1(stackindex) = LParse(logicalrule)
                        Else
                            errorMsg = errorMsg + " " + "This Rule cannot be parsed! Please check you rule."
                        End If

                    Else
                    End If
                End If
            Next
            If (newstack1(1) = "") Then
                finaloutput = newstack1(0)
            Else
                errorMsg = errorMsg + " " + "Your rule does not conform with the acceptable standard! Correct your rule and try again."
                SaveErrorMsg(documentType, "Your rule does not conform with the acceptable standard! Correct your rule and try again." & " - Module:- RuleParser::EvaluateRule4()")
            End If
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::EvaluateRule4()")
        End Try
    End Sub

    Public Sub resetVars()
        Try
            index = 0
            index2 = 0
            index3 = 0
            index4 = 0
            index5 = 0
            val = String.Empty
            op = String.Empty
            new_rule = String.Empty
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::resetVars()")
        End Try

    End Sub

    Public Function GetResult() As String
        Try
            Return finaloutput
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::GetResult()")
            Return String.Empty
        End Try
    End Function

    Public Function GetResults() As ArrayList
        Try
            Return actions
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            SaveErrorMsg(documentType, ex.Message & " - Module:- RuleParser::GetResults()")
            Return New ArrayList
        End Try
    End Function

    Public Function GetErrorMsg() As String
        Try
            Return errorMsg
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
            Return errorMsg
        End Try
    End Function

    Public Sub SaveErrorMsg(ByVal doctype As String, ByVal msg As String)
        Try
            storex = New DAL
            storex.SaveErrorMsg(doctype, msg)
        Catch ex As Exception
            errorMsg = errorMsg + " " + ex.Message.ToString
        End Try
    End Sub
    
End Class