<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParser
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
        Me.txtRule = New System.Windows.Forms.TextBox
        Me.btnParse = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.ListBox3 = New System.Windows.Forms.ListBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtcondition = New System.Windows.Forms.TextBox
        Me.btnlogictest = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.ListBox4 = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lstpreced = New System.Windows.Forms.ComboBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtRule
        '
        Me.txtRule.Location = New System.Drawing.Point(76, 25)
        Me.txtRule.Name = "txtRule"
        Me.txtRule.Size = New System.Drawing.Size(502, 20)
        Me.txtRule.TabIndex = 0
        '
        'btnParse
        '
        Me.btnParse.Location = New System.Drawing.Point(12, 227)
        Me.btnParse.Name = "btnParse"
        Me.btnParse.Size = New System.Drawing.Size(65, 22)
        Me.btnParse.TabIndex = 1
        Me.btnParse.Text = "PARSE"
        Me.btnParse.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Rule"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.WWF_Test_Project_V1.My.Resources.Resources.LOGO2
        Me.PictureBox1.Location = New System.Drawing.Point(12, 266)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(115, 47)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 86)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(148, 134)
        Me.ListBox1.TabIndex = 6
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(320, 86)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(101, 134)
        Me.ListBox2.TabIndex = 7
        '
        'ListBox3
        '
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.Location = New System.Drawing.Point(166, 86)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(148, 134)
        Me.ListBox3.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(267, 232)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "="
        '
        'txtcondition
        '
        Me.txtcondition.Enabled = False
        Me.txtcondition.Location = New System.Drawing.Point(286, 229)
        Me.txtcondition.Name = "txtcondition"
        Me.txtcondition.Size = New System.Drawing.Size(79, 20)
        Me.txtcondition.TabIndex = 10
        '
        'btnlogictest
        '
        Me.btnlogictest.Location = New System.Drawing.Point(83, 226)
        Me.btnlogictest.Name = "btnlogictest"
        Me.btnlogictest.Size = New System.Drawing.Size(97, 23)
        Me.btnlogictest.TabIndex = 11
        Me.btnlogictest.Text = "LOGICAL TEST"
        Me.btnlogictest.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(186, 227)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 22)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Check"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox4
        '
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.Location = New System.Drawing.Point(431, 87)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(146, 134)
        Me.ListBox4.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "PRECEDENCE"
        '
        'lstpreced
        '
        Me.lstpreced.FormattingEnabled = True
        Me.lstpreced.Items.AddRange(New Object() {"0 - Same Precedence", "1 - Default Precedence", "2 - Use Nested Parentheses and Default Precedence", "3 - Use Nested Parentheses and Same Precedence"})
        Me.lstpreced.Location = New System.Drawing.Point(113, 59)
        Me.lstpreced.Name = "lstpreced"
        Me.lstpreced.Size = New System.Drawing.Size(201, 21)
        Me.lstpreced.TabIndex = 15
        Me.lstpreced.Text = "SELECT PRECEDENCE FOR RULE"
        '
        'frmParser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(588, 316)
        Me.Controls.Add(Me.lstpreced)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnlogictest)
        Me.Controls.Add(Me.txtcondition)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnParse)
        Me.Controls.Add(Me.txtRule)
        Me.MaximizeBox = False
        Me.Name = "frmParser"
        Me.Text = "PARSER"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRule As System.Windows.Forms.TextBox
    Friend WithEvents btnParse As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcondition As System.Windows.Forms.TextBox
    Friend WithEvents btnlogictest As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox4 As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lstpreced As System.Windows.Forms.ComboBox

End Class
