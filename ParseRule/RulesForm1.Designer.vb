<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RulesForm1
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
        Me.tbCondition = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbElseAction = New System.Windows.Forms.TextBox
        Me.tbRequest = New System.Windows.Forms.TextBox
        Me.tbPrecedence = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.tbAction = New System.Windows.Forms.TextBox
        Me.tbErrorMsg = New System.Windows.Forms.TextBox
        Me.tbRuleName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'tbCondition
        '
        Me.tbCondition.Location = New System.Drawing.Point(104, 97)
        Me.tbCondition.Name = "tbCondition"
        Me.tbCondition.Size = New System.Drawing.Size(218, 20)
        Me.tbCondition.TabIndex = 41
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Condition"
        '
        'tbElseAction
        '
        Me.tbElseAction.Location = New System.Drawing.Point(104, 180)
        Me.tbElseAction.Name = "tbElseAction"
        Me.tbElseAction.Size = New System.Drawing.Size(218, 20)
        Me.tbElseAction.TabIndex = 39
        '
        'tbRequest
        '
        Me.tbRequest.Location = New System.Drawing.Point(104, 209)
        Me.tbRequest.Name = "tbRequest"
        Me.tbRequest.Size = New System.Drawing.Size(218, 20)
        Me.tbRequest.TabIndex = 38
        '
        'tbPrecedence
        '
        Me.tbPrecedence.Location = New System.Drawing.Point(104, 152)
        Me.tbPrecedence.Name = "tbPrecedence"
        Me.tbPrecedence.Size = New System.Drawing.Size(218, 20)
        Me.tbPrecedence.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Else Action"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 215)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Request"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Precedence"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(134, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Rule"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(104, 254)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 32
        Me.btnSave.Text = "Save Properties"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'tbAction
        '
        Me.tbAction.Location = New System.Drawing.Point(104, 71)
        Me.tbAction.Name = "tbAction"
        Me.tbAction.Size = New System.Drawing.Size(218, 20)
        Me.tbAction.TabIndex = 31
        '
        'tbErrorMsg
        '
        Me.tbErrorMsg.Location = New System.Drawing.Point(104, 125)
        Me.tbErrorMsg.Name = "tbErrorMsg"
        Me.tbErrorMsg.Size = New System.Drawing.Size(218, 20)
        Me.tbErrorMsg.TabIndex = 30
        '
        'tbRuleName
        '
        Me.tbRuleName.Location = New System.Drawing.Point(104, 43)
        Me.tbRuleName.Name = "tbRuleName"
        Me.tbRuleName.Size = New System.Drawing.Size(218, 20)
        Me.tbRuleName.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Action"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Error Message"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Rule Name"
        '
        'RulesForm1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 301)
        Me.Controls.Add(Me.tbCondition)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbElseAction)
        Me.Controls.Add(Me.tbRequest)
        Me.Controls.Add(Me.tbPrecedence)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.tbAction)
        Me.Controls.Add(Me.tbErrorMsg)
        Me.Controls.Add(Me.tbRuleName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "RulesForm1"
        Me.Text = "RulesForm1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbElseAction As System.Windows.Forms.TextBox
    Friend WithEvents tbRequest As System.Windows.Forms.TextBox
    Friend WithEvents tbPrecedence As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents tbAction As System.Windows.Forms.TextBox
    Friend WithEvents tbErrorMsg As System.Windows.Forms.TextBox
    Friend WithEvents tbRuleName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
