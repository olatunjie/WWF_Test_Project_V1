<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainV2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.grpRule = New System.Windows.Forms.GroupBox
        Me.btnAddVal = New System.Windows.Forms.Button
        Me.txtval = New System.Windows.Forms.TextBox
        Me.cbopar = New System.Windows.Forms.ComboBox
        Me.cbolop = New System.Windows.Forms.ComboBox
        Me.txtcondition = New System.Windows.Forms.TextBox
        Me.cborop = New System.Windows.Forms.ComboBox
        Me.cboType = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtElseAction = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtAction = New System.Windows.Forms.TextBox
        Me.txtErrorMsg = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.btnParse = New System.Windows.Forms.Button
        Me.lstpreced = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnSaveRule = New System.Windows.Forms.Button
        Me.txtRuleName = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.grpAssign = New System.Windows.Forms.GroupBox
        Me.btnCreateRule = New System.Windows.Forms.Button
        Me.txtRequest = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.tDocType = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPTask = New System.Windows.Forms.TextBox
        Me.txtState = New System.Windows.Forms.TextBox
        Me.txtOrigUser = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnSaveAssign = New System.Windows.Forms.Button
        Me.tDocID = New System.Windows.Forms.TextBox
        Me.txtAssUser = New System.Windows.Forms.TextBox
        Me.txtTask = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.grpDoc = New System.Windows.Forms.GroupBox
        Me.btnAssign = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lstProp = New System.Windows.Forms.ListView
        Me.type = New System.Windows.Forms.ColumnHeader
        Me.value = New System.Windows.Forms.ColumnHeader
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.txtDocType = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDocID = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtType = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.grpRule.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpAssign.SuspendLayout()
        Me.grpDoc.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpRule
        '
        Me.grpRule.Controls.Add(Me.btnAddVal)
        Me.grpRule.Controls.Add(Me.txtval)
        Me.grpRule.Controls.Add(Me.cbopar)
        Me.grpRule.Controls.Add(Me.cbolop)
        Me.grpRule.Controls.Add(Me.txtcondition)
        Me.grpRule.Controls.Add(Me.cborop)
        Me.grpRule.Controls.Add(Me.cboType)
        Me.grpRule.Controls.Add(Me.GroupBox1)
        Me.grpRule.Controls.Add(Me.btnParse)
        Me.grpRule.Controls.Add(Me.lstpreced)
        Me.grpRule.Controls.Add(Me.Label14)
        Me.grpRule.Controls.Add(Me.Label17)
        Me.grpRule.Controls.Add(Me.btnSaveRule)
        Me.grpRule.Controls.Add(Me.txtRuleName)
        Me.grpRule.Controls.Add(Me.Label21)
        Me.grpRule.Enabled = False
        Me.grpRule.Location = New System.Drawing.Point(722, 12)
        Me.grpRule.Name = "grpRule"
        Me.grpRule.Size = New System.Drawing.Size(376, 452)
        Me.grpRule.TabIndex = 21
        Me.grpRule.TabStop = False
        Me.grpRule.Text = "RULE"
        '
        'btnAddVal
        '
        Me.btnAddVal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAddVal.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddVal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddVal.Location = New System.Drawing.Point(316, 97)
        Me.btnAddVal.Name = "btnAddVal"
        Me.btnAddVal.Size = New System.Drawing.Size(46, 20)
        Me.btnAddVal.TabIndex = 68
        Me.btnAddVal.Text = "Add"
        Me.btnAddVal.UseVisualStyleBackColor = False
        '
        'txtval
        '
        Me.txtval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtval.ForeColor = System.Drawing.Color.Gray
        Me.txtval.Location = New System.Drawing.Point(121, 97)
        Me.txtval.Name = "txtval"
        Me.txtval.Size = New System.Drawing.Size(197, 20)
        Me.txtval.TabIndex = 67
        Me.txtval.Text = "               Type your value here"
        '
        'cbopar
        '
        Me.cbopar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbopar.FormattingEnabled = True
        Me.cbopar.Location = New System.Drawing.Point(227, 124)
        Me.cbopar.Name = "cbopar"
        Me.cbopar.Size = New System.Drawing.Size(102, 21)
        Me.cbopar.TabIndex = 66
        Me.cbopar.Text = "PARENTHESIS"
        '
        'cbolop
        '
        Me.cbolop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbolop.FormattingEnabled = True
        Me.cbolop.Location = New System.Drawing.Point(123, 124)
        Me.cbolop.Name = "cbolop"
        Me.cbolop.Size = New System.Drawing.Size(98, 21)
        Me.cbolop.TabIndex = 65
        Me.cbolop.Text = "--CONNECTOR--"
        '
        'txtcondition
        '
        Me.txtcondition.BackColor = System.Drawing.Color.FloralWhite
        Me.txtcondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtcondition.Location = New System.Drawing.Point(44, 154)
        Me.txtcondition.Name = "txtcondition"
        Me.txtcondition.Size = New System.Drawing.Size(318, 20)
        Me.txtcondition.TabIndex = 64
        '
        'cborop
        '
        Me.cborop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cborop.FormattingEnabled = True
        Me.cborop.Location = New System.Drawing.Point(245, 71)
        Me.cborop.Name = "cborop"
        Me.cborop.Size = New System.Drawing.Size(84, 21)
        Me.cborop.TabIndex = 62
        Me.cborop.Text = "OPERATOR"
        '
        'cboType
        '
        Me.cboType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cboType.FormattingEnabled = True
        Me.cboType.Location = New System.Drawing.Point(123, 70)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(116, 21)
        Me.cboType.TabIndex = 61
        Me.cboType.Text = "---------- TYPE ----------"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtElseAction)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.txtAction)
        Me.GroupBox1.Controls.Add(Me.txtErrorMsg)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(24, 273)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(338, 144)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "OUTPUT FROM PARSER"
        '
        'txtElseAction
        '
        Me.txtElseAction.Enabled = False
        Me.txtElseAction.Location = New System.Drawing.Point(106, 94)
        Me.txtElseAction.Name = "txtElseAction"
        Me.txtElseAction.Size = New System.Drawing.Size(218, 20)
        Me.txtElseAction.TabIndex = 61
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Maroon
        Me.Label15.Location = New System.Drawing.Point(25, 94)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 14)
        Me.Label15.TabIndex = 60
        Me.Label15.Text = "Else Action"
        '
        'txtAction
        '
        Me.txtAction.Enabled = False
        Me.txtAction.Location = New System.Drawing.Point(106, 34)
        Me.txtAction.Name = "txtAction"
        Me.txtAction.Size = New System.Drawing.Size(218, 20)
        Me.txtAction.TabIndex = 59
        '
        'txtErrorMsg
        '
        Me.txtErrorMsg.Location = New System.Drawing.Point(106, 66)
        Me.txtErrorMsg.Name = "txtErrorMsg"
        Me.txtErrorMsg.Size = New System.Drawing.Size(218, 20)
        Me.txtErrorMsg.TabIndex = 58
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Maroon
        Me.Label19.Location = New System.Drawing.Point(55, 36)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(45, 14)
        Me.Label19.TabIndex = 57
        Me.Label19.Text = "Action"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Maroon
        Me.Label20.Location = New System.Drawing.Point(2, 66)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(98, 14)
        Me.Label20.TabIndex = 56
        Me.Label20.Text = "Error Message"
        '
        'btnParse
        '
        Me.btnParse.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnParse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnParse.ForeColor = System.Drawing.Color.Maroon
        Me.btnParse.Location = New System.Drawing.Point(130, 232)
        Me.btnParse.Name = "btnParse"
        Me.btnParse.Size = New System.Drawing.Size(91, 23)
        Me.btnParse.TabIndex = 59
        Me.btnParse.Text = "PARSE RULE"
        Me.btnParse.UseVisualStyleBackColor = False
        '
        'lstpreced
        '
        Me.lstpreced.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lstpreced.FormattingEnabled = True
        Me.lstpreced.Items.AddRange(New Object() {"0 - Same Precedence", "1 - Default Precedence", "2 - Use Nested Parentheses and Default Precedence", "3 - Use Nested Parentheses and Same Precedence"})
        Me.lstpreced.Location = New System.Drawing.Point(128, 191)
        Me.lstpreced.Name = "lstpreced"
        Me.lstpreced.Size = New System.Drawing.Size(201, 21)
        Me.lstpreced.TabIndex = 58
        Me.lstpreced.Text = "SELECT PRECEDENCE FOR RULE"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(42, 71)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(67, 14)
        Me.Label14.TabIndex = 56
        Me.Label14.Text = "Condition"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Maroon
        Me.Label17.Location = New System.Drawing.Point(42, 193)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 14)
        Me.Label17.TabIndex = 50
        Me.Label17.Text = "Precedence"
        '
        'btnSaveRule
        '
        Me.btnSaveRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSaveRule.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveRule.Location = New System.Drawing.Point(128, 423)
        Me.btnSaveRule.Name = "btnSaveRule"
        Me.btnSaveRule.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveRule.TabIndex = 48
        Me.btnSaveRule.Text = "SAVE"
        Me.btnSaveRule.UseVisualStyleBackColor = False
        '
        'txtRuleName
        '
        Me.txtRuleName.Location = New System.Drawing.Point(121, 39)
        Me.txtRuleName.Name = "txtRuleName"
        Me.txtRuleName.Size = New System.Drawing.Size(218, 20)
        Me.txtRuleName.TabIndex = 45
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Maroon
        Me.Label21.Location = New System.Drawing.Point(41, 41)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(74, 14)
        Me.Label21.TabIndex = 42
        Me.Label21.Text = "Rule Name"
        '
        'grpAssign
        '
        Me.grpAssign.Controls.Add(Me.btnCreateRule)
        Me.grpAssign.Controls.Add(Me.txtRequest)
        Me.grpAssign.Controls.Add(Me.Label9)
        Me.grpAssign.Controls.Add(Me.tDocType)
        Me.grpAssign.Controls.Add(Me.Label5)
        Me.grpAssign.Controls.Add(Me.txtPTask)
        Me.grpAssign.Controls.Add(Me.txtState)
        Me.grpAssign.Controls.Add(Me.txtOrigUser)
        Me.grpAssign.Controls.Add(Me.Label6)
        Me.grpAssign.Controls.Add(Me.Label7)
        Me.grpAssign.Controls.Add(Me.Label8)
        Me.grpAssign.Controls.Add(Me.btnSaveAssign)
        Me.grpAssign.Controls.Add(Me.tDocID)
        Me.grpAssign.Controls.Add(Me.txtAssUser)
        Me.grpAssign.Controls.Add(Me.txtTask)
        Me.grpAssign.Controls.Add(Me.Label11)
        Me.grpAssign.Controls.Add(Me.Label12)
        Me.grpAssign.Controls.Add(Me.Label13)
        Me.grpAssign.Enabled = False
        Me.grpAssign.Location = New System.Drawing.Point(338, 12)
        Me.grpAssign.Name = "grpAssign"
        Me.grpAssign.Size = New System.Drawing.Size(378, 402)
        Me.grpAssign.TabIndex = 20
        Me.grpAssign.TabStop = False
        Me.grpAssign.Text = "ASSIGNMENT"
        '
        'btnCreateRule
        '
        Me.btnCreateRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCreateRule.Enabled = False
        Me.btnCreateRule.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCreateRule.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnCreateRule.Location = New System.Drawing.Point(211, 301)
        Me.btnCreateRule.Name = "btnCreateRule"
        Me.btnCreateRule.Size = New System.Drawing.Size(91, 23)
        Me.btnCreateRule.TabIndex = 44
        Me.btnCreateRule.Text = "CREATE RULE"
        Me.btnCreateRule.UseVisualStyleBackColor = False
        '
        'txtRequest
        '
        Me.txtRequest.Location = New System.Drawing.Point(121, 241)
        Me.txtRequest.Name = "txtRequest"
        Me.txtRequest.Size = New System.Drawing.Size(218, 20)
        Me.txtRequest.TabIndex = 43
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Maroon
        Me.Label9.Location = New System.Drawing.Point(50, 241)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 14)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Request"
        '
        'tDocType
        '
        Me.tDocType.Enabled = False
        Me.tDocType.Location = New System.Drawing.Point(121, 97)
        Me.tDocType.Name = "tDocType"
        Me.tDocType.Size = New System.Drawing.Size(218, 20)
        Me.tDocType.TabIndex = 41
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Maroon
        Me.Label5.Location = New System.Drawing.Point(52, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 14)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "DocType"
        '
        'txtPTask
        '
        Me.txtPTask.Location = New System.Drawing.Point(121, 180)
        Me.txtPTask.Name = "txtPTask"
        Me.txtPTask.Size = New System.Drawing.Size(218, 20)
        Me.txtPTask.TabIndex = 39
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(121, 209)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(218, 20)
        Me.txtState.TabIndex = 38
        '
        'txtOrigUser
        '
        Me.txtOrigUser.Enabled = False
        Me.txtOrigUser.Location = New System.Drawing.Point(121, 152)
        Me.txtOrigUser.Name = "txtOrigUser"
        Me.txtOrigUser.Size = New System.Drawing.Size(218, 20)
        Me.txtOrigUser.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(30, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 14)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Parent Task"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(68, 209)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 14)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "State"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Maroon
        Me.Label8.Location = New System.Drawing.Point(6, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 14)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Originating User"
        '
        'btnSaveAssign
        '
        Me.btnSaveAssign.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSaveAssign.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveAssign.Location = New System.Drawing.Point(121, 301)
        Me.btnSaveAssign.Name = "btnSaveAssign"
        Me.btnSaveAssign.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveAssign.TabIndex = 32
        Me.btnSaveAssign.Text = "SAVE"
        Me.btnSaveAssign.UseVisualStyleBackColor = False
        '
        'tDocID
        '
        Me.tDocID.Enabled = False
        Me.tDocID.Location = New System.Drawing.Point(121, 71)
        Me.tDocID.Name = "tDocID"
        Me.tDocID.Size = New System.Drawing.Size(218, 20)
        Me.tDocID.TabIndex = 31
        '
        'txtAssUser
        '
        Me.txtAssUser.Location = New System.Drawing.Point(121, 125)
        Me.txtAssUser.Name = "txtAssUser"
        Me.txtAssUser.Size = New System.Drawing.Size(218, 20)
        Me.txtAssUser.TabIndex = 30
        '
        'txtTask
        '
        Me.txtTask.Location = New System.Drawing.Point(121, 43)
        Me.txtTask.Name = "txtTask"
        Me.txtTask.Size = New System.Drawing.Size(218, 20)
        Me.txtTask.TabIndex = 29
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Maroon
        Me.Label11.Location = New System.Drawing.Point(68, 73)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 14)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "DocID"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Maroon
        Me.Label12.Location = New System.Drawing.Point(22, 127)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 14)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Assigned User"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Maroon
        Me.Label13.Location = New System.Drawing.Point(76, 45)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(36, 14)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Task"
        '
        'grpDoc
        '
        Me.grpDoc.Controls.Add(Me.btnAssign)
        Me.grpDoc.Controls.Add(Me.btnSave)
        Me.grpDoc.Controls.Add(Me.lstProp)
        Me.grpDoc.Controls.Add(Me.Label22)
        Me.grpDoc.Controls.Add(Me.Label18)
        Me.grpDoc.Controls.Add(Me.txtValue)
        Me.grpDoc.Controls.Add(Me.txtDocType)
        Me.grpDoc.Controls.Add(Me.Label4)
        Me.grpDoc.Controls.Add(Me.txtDocID)
        Me.grpDoc.Controls.Add(Me.Label10)
        Me.grpDoc.Controls.Add(Me.txtType)
        Me.grpDoc.Controls.Add(Me.btnAdd)
        Me.grpDoc.Location = New System.Drawing.Point(10, 12)
        Me.grpDoc.Name = "grpDoc"
        Me.grpDoc.Size = New System.Drawing.Size(322, 402)
        Me.grpDoc.TabIndex = 19
        Me.grpDoc.TabStop = False
        Me.grpDoc.Text = "DOCUMENT"
        '
        'btnAssign
        '
        Me.btnAssign.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAssign.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAssign.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAssign.Location = New System.Drawing.Point(171, 364)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(75, 23)
        Me.btnAssign.TabIndex = 53
        Me.btnAssign.Text = "ASSIGN"
        Me.btnAssign.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Location = New System.Drawing.Point(78, 364)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 52
        Me.btnSave.Text = "SAVE"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lstProp
        '
        Me.lstProp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.type, Me.value})
        Me.lstProp.FullRowSelect = True
        Me.lstProp.GridLines = True
        Me.lstProp.Location = New System.Drawing.Point(7, 135)
        Me.lstProp.Name = "lstProp"
        Me.lstProp.Size = New System.Drawing.Size(309, 150)
        Me.lstProp.TabIndex = 51
        Me.lstProp.UseCompatibleStateImageBehavior = False
        Me.lstProp.View = System.Windows.Forms.View.Details
        '
        'type
        '
        Me.type.Text = "TYPE (PROPERTY)"
        Me.type.Width = 162
        '
        'value
        '
        Me.value.Text = "VALUE"
        Me.value.Width = 232
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(228, 32)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(42, 14)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Value"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(75, 32)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(37, 14)
        Me.Label18.TabIndex = 49
        Me.Label18.Text = "Type"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(206, 57)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(100, 20)
        Me.txtValue.TabIndex = 46
        '
        'txtDocType
        '
        Me.txtDocType.Location = New System.Drawing.Point(78, 327)
        Me.txtDocType.Name = "txtDocType"
        Me.txtDocType.Size = New System.Drawing.Size(228, 20)
        Me.txtDocType.TabIndex = 45
        Me.txtDocType.Text = "CAM"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(19, 329)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 14)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "DocType"
        '
        'txtDocID
        '
        Me.txtDocID.Location = New System.Drawing.Point(78, 301)
        Me.txtDocID.Name = "txtDocID"
        Me.txtDocID.Size = New System.Drawing.Size(228, 20)
        Me.txtDocID.TabIndex = 43
        Me.txtDocID.Text = "101"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Maroon
        Me.Label10.Location = New System.Drawing.Point(35, 303)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 14)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "DocID"
        '
        'txtType
        '
        Me.txtType.Location = New System.Drawing.Point(13, 57)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(187, 20)
        Me.txtType.TabIndex = 11
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(140, 94)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(41, 23)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "ADD"
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'MainV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1103, 471)
        Me.Controls.Add(Me.grpRule)
        Me.Controls.Add(Me.grpAssign)
        Me.Controls.Add(Me.grpDoc)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "MainV2"
        Me.Text = "STERLING WORKFLOW"
        Me.grpRule.ResumeLayout(False)
        Me.grpRule.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpAssign.ResumeLayout(False)
        Me.grpAssign.PerformLayout()
        Me.grpDoc.ResumeLayout(False)
        Me.grpDoc.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpRule As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtElseAction As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtAction As System.Windows.Forms.TextBox
    Friend WithEvents txtErrorMsg As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnParse As System.Windows.Forms.Button
    Friend WithEvents lstpreced As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents btnSaveRule As System.Windows.Forms.Button
    Friend WithEvents txtRuleName As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grpAssign As System.Windows.Forms.GroupBox
    Friend WithEvents btnCreateRule As System.Windows.Forms.Button
    Friend WithEvents txtRequest As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tDocType As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPTask As System.Windows.Forms.TextBox
    Friend WithEvents txtState As System.Windows.Forms.TextBox
    Friend WithEvents txtOrigUser As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSaveAssign As System.Windows.Forms.Button
    Friend WithEvents tDocID As System.Windows.Forms.TextBox
    Friend WithEvents txtAssUser As System.Windows.Forms.TextBox
    Friend WithEvents txtTask As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grpDoc As System.Windows.Forms.GroupBox
    Friend WithEvents btnAssign As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lstProp As System.Windows.Forms.ListView
    Friend WithEvents type As System.Windows.Forms.ColumnHeader
    Friend WithEvents value As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtDocType As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDocID As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents cborop As System.Windows.Forms.ComboBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents cbopar As System.Windows.Forms.ComboBox
    Friend WithEvents cbolop As System.Windows.Forms.ComboBox
    Friend WithEvents txtcondition As System.Windows.Forms.TextBox
    Friend WithEvents txtval As System.Windows.Forms.TextBox
    Friend WithEvents btnAddVal As System.Windows.Forms.Button
End Class
