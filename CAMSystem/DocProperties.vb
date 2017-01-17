Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OracleClient

Public Class DocProperties
    Private valindex As Integer
    Private docprop As New DocProperty
    Private storex As DAL
    Private errormsg As String = String.Empty
    Private userrule As New Rule

    Public Sub AddProperties(ByVal type As Object, ByVal value As Object, ByVal docID As Integer, ByVal doctype As Object, ByVal docpropid As Integer, ByVal owner As String)
        Try
            Me.ErrorMessage = String.Empty
            storex = New DAL
            storex.SaveDocProperty(type, value, docID, doctype, docpropid, owner)
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- DocProperties::AddProperties()")
        End Try
    End Sub

    Public Property ErrorMessage() As String
        Get
            Return errormsg
        End Get
        Set(ByVal value As String)
            errormsg = value
        End Set
    End Property

    Public Sub GenerateProperties(ByVal connectionString As String, ByVal tablename As String, ByVal fieldnames As String, ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            storex = New DAL
            'Dim doctype As String = storex.GetDocType

            rule = propRule

            If (InStr(rule, "Doc.Owner") > 0) Then
                rule = Replace(rule, "Doc.Owner", "'" & dataowner & "'")
            End If

            If (InStr(rule, "Doc.") > 0) Then
                While InStr(rule, "Doc.") > 0
                    str = Mid(rule, InStr(rule, "Doc."))

                    If (InStr(str.Trim, " ") > 0) Then
                        str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                    Else
                        str2 = Mid(str, InStr(str, ".") + 1)
                    End If
                    rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                End While
            End If

            If InStr(connectionString, "HOBANK1") > 0 Then
                Dim connect As New OracleConnection(connectionString)
                Dim command As OracleCommand
                Dim reader As OracleDataReader

                command = New OracleCommand("SELECT DISTINCT " & fieldnames & " FROM " & tablename & " WHERE " & rule, connect)

                If connect.State = ConnectionState.Closed Then
                    connect.Open()
                End If
                command.CommandType = CommandType.Text
                command.CommandTimeout = 0
                reader = command.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                connect.Close()
            Else
                Dim conn As New SqlConnection(connectionString)
                Dim cmd As SqlCommand
                Dim reader As SqlDataReader
                cmd = New SqlCommand("SELECT " & fieldnames & " FROM " & tablename & " WHERE " & rule, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                conn.Close()
            End If
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- DocProperties::GenerateProperties()")
        End Try
    End Sub

    Public Sub GeneratePropertiesSQL(ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
        Try
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim creader As SqlDataReader
            Dim connectstr As String = String.Empty
            Dim fields As String = String.Empty
            Dim tables As String = String.Empty

            rule = propRule
            If (InStr(rule, "Doc.") > 0) Then
                While InStr(rule, "Doc.") > 0
                    str = Mid(rule, InStr(rule, "Doc."))

                    If (InStr(str.Trim, " ") > 0) Then
                        str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                    Else
                        str2 = Mid(str, InStr(str, ".") + 1)
                    End If
                    rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                End While
            End If

            creader = storex.GetUserConnection(doctype)

            If creader.HasRows Then
                While creader.Read
                    If InStr(creader.GetValue(0).ToString, "HOBANK1") <= 0 Then
                        connectstr = creader.GetValue(0).ToString
                        tables = creader.GetValue(1).ToString
                        fields = creader.GetValue(2).ToString
                        Exit While
                    End If
                End While
            End If

            If connectstr <> String.Empty And tables <> String.Empty And fields <> String.Empty Then
                Dim conn As New SqlConnection(connectstr)
                Dim cmd As SqlCommand
                Dim reader As SqlDataReader
                cmd = New SqlCommand("SELECT " & fields & " FROM " & tables & " WHERE " & rule, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.getDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                conn.Close()
            End If
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- DocProperties::GeneratePropertiesSQL()")
        End Try
    End Sub

    Public Sub GeneratePropertiesBANKS(ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
        Try
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            Dim creader As SqlDataReader
            Dim connectstr As String = String.Empty
            Dim fields As String = String.Empty
            Dim tables As String = String.Empty

            rule = propRule
            If (InStr(rule, "Doc.") > 0) Then
                While InStr(rule, "Doc.") > 0
                    str = Mid(rule, InStr(rule, "Doc."))

                    If (InStr(str.Trim, " ") > 0) Then
                        str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                    Else
                        str2 = Mid(str, InStr(str, ".") + 1)
                    End If
                    rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                End While
            End If

            creader = storex.GetUserConnection(doctype)

            If creader.HasRows Then
                While creader.Read
                    If InStr(creader.GetValue(0).ToString, "HOBANK1") > 0 Then
                        connectstr = creader.GetValue(0).ToString
                        tables = creader.GetValue(1).ToString
                        fields = creader.GetValue(2).ToString
                        Exit While
                    End If
                End While
            End If

            If connectstr <> String.Empty And tables <> String.Empty And fields <> String.Empty Then
                Dim connect As New OracleConnection(connectstr)
                Dim command As OracleCommand
                Dim reader As OracleDataReader

                command = New OracleCommand("SELECT DISTINCT " & fields & " FROM " & tables & " WHERE " & rule, connect)

                If connect.State = ConnectionState.Closed Then
                    connect.Open()
                End If
                command.CommandType = CommandType.Text
                command.CommandTimeout = 0
                reader = command.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.getDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                connect.Close()
            End If
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- DocProperties::GeneratePropertiesBANKS()")
        End Try
    End Sub

    Public Sub GenerateDocProperties(ByVal connectionString As String, ByVal tablename As String, ByVal fieldnames As String, ByVal propRule As String, ByVal dataowner As String, ByVal doctype As String)
        Try
            Me.ErrorMessage = String.Empty
            Dim rule As String = String.Empty
            Dim str As String = String.Empty
            Dim str2 As String = String.Empty
            storex = New DAL
            'Dim doctype As String = storex.GetDocType

            rule = propRule

            If (InStr(rule, "Doc.Owner") > 0) Then
                rule = Replace(rule, "Doc.Owner", "'" & dataowner & "'")
            End If

            If (InStr(rule, "Doc.") > 0) Then
                While InStr(rule, "Doc.") > 0
                    str = Mid(rule, InStr(rule, "Doc."))

                    If (InStr(str.Trim, " ") > 0) Then
                        str2 = Mid(str, InStr(str, ".") + 1, InStr(str, " ") - InStr(str, ".") - 1)
                    Else
                        str2 = Mid(str, InStr(str, ".") + 1)
                    End If
                    rule = Replace(rule, "Doc." & str2, userrule.SQLParser(str2, dataowner, doctype))
                End While
            End If

            If InStr(connectionString, "HOBANK1") > 0 Then
                Dim connect As New OracleConnection(connectionString)
                Dim command As OracleCommand
                Dim reader As OracleDataReader

                command = New OracleCommand("SELECT DISTINCT " & fieldnames & " FROM " & tablename & " WHERE " & rule, connect)

                If connect.State = ConnectionState.Closed Then
                    connect.Open()
                End If
                command.CommandType = CommandType.Text
                command.CommandTimeout = 0
                reader = command.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                connect.Close()
            Else
                Dim conn As New SqlConnection(connectionString)
                Dim cmd As SqlCommand
                Dim reader As SqlDataReader
                cmd = New SqlCommand("SELECT " & fieldnames & " FROM " & tablename & " WHERE " & rule, conn)

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 0
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                While (reader.Read)
                    For i As Integer = 0 To reader.FieldCount - 1
                        If storex.GetDocProperties(doctype, reader.GetName(i), dataowner) = String.Empty Then
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 0, dataowner)
                        Else
                            Me.AddProperties(reader.GetName(i), reader.GetValue(reader.GetOrdinal(reader.GetName(i))).ToString, storex.GetDocID(doctype).ToString, doctype, 1, dataowner)
                        End If
                    Next
                End While
                reader.Close()
                conn.Close()
            End If
        Catch ex As Exception
            storex = New DAL
            storex.SaveErrorMsg(doctype, ex.Message & " - Module:- DocProperties::GenerateProperties()")
        End Try
    End Sub
End Class
