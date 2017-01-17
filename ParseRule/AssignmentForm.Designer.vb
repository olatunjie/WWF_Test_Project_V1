<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AssignmentForm
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
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.tbDocID = New System.Windows.Forms.TextBox
        Me.tbAssUser = New System.Windows.Forms.TextBox
        Me.tbTask = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.tbParentTask = New System.Windows.Forms.TextBox
        Me.tbState = New System.Windows.Forms.TextBox
        Me.tbOrigUser = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.tbDocType = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbRequest = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(123, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Assignment"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(126, 250)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save Properties"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'tbDocID
        '
        Me.tbDocID.Location = New System.Drawing.Point(92, 53)
        Me.tbDocID.Name = "tbDocID"
        Me.tbDocID.Size = New System.Drawing.Size(218, 20)
        Me.tbDocID.TabIndex = 13
        '
        'tbAssUser
        '
        Me.tbAssUser.Location = New System.Drawing.Point(92, 107)
        Me.tbAssUser.Name = "tbAssUser"
        Me.tbAssUser.Size = New System.Drawing.Size(218, 20)
        Me.tbAssUser.TabIndex = 12
        '
        'tbTask
        '
        Me.tbTask.Location = New System.Drawing.Point(92, 25)
        Me.tbTask.Name = "tbTask"
        Me.tbTask.Size = New System.Drawing.Size(218, 20)
        Me.tbTask.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "DocID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "AssignedUser"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Task"
        '
        'tbParentTask
        '
        Me.tbParentTask.Location = New System.Drawing.Point(92, 162)
        Me.tbParentTask.Name = "tbParentTask"
        Me.tbParentTask.Size = New System.Drawing.Size(218, 20)
        Me.tbParentTask.TabIndex = 21
        '
        'tbState
        '
        Me.tbState.Location = New System.Drawing.Point(92, 191)
        Me.tbState.Name = "tbState"
        Me.tbState.Size = New System.Drawing.Size(218, 20)
        Me.tbState.TabIndex = 20
        '
        'tbOrigUser
        '
        Me.tbOrigUser.Location = New System.Drawing.Point(92, 134)
        Me.tbOrigUser.Name = "tbOrigUser"
        Me.tbOrigUser.Size = New System.Drawing.Size(218, 20)
        Me.tbOrigUser.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Parent Task"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "State"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 138)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Originating User"
        '
        'tbDocType
        '
        Me.tbDocType.Location = New System.Drawing.Point(92, 79)
        Me.tbDocType.Name = "tbDocType"
        Me.tbDocType.Size = New System.Drawing.Size(218, 20)
        Me.tbDocType.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "DocType"
        '
        'tbRequest
        '
        Me.tbRequest.Location = New System.Drawing.Point(92, 223)
        Me.tbRequest.Name = "tbRequest"
        Me.tbRequest.Size = New System.Drawing.Size(218, 20)
        Me.tbRequest.TabIndex = 25
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 226)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Request"
        '
        'AssignmentForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 285)
        Me.Controls.Add(Me.tbRequest)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tbDocType)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbParentTask)
        Me.Controls.Add(Me.tbState)
        Me.Controls.Add(Me.tbOrigUser)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.tbDocID)
        Me.Controls.Add(Me.tbAssUser)
        Me.Controls.Add(Me.tbTask)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AssignmentForm"
        Me.Text = "AddAssignment"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents tbDocID As System.Windows.Forms.TextBox
    Friend WithEvents tbAssUser As System.Windows.Forms.TextBox
    Friend WithEvents tbTask As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbParentTask As System.Windows.Forms.TextBox
    Friend WithEvents tbState As System.Windows.Forms.TextBox
    Friend WithEvents tbOrigUser As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tbDocType As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbRequest As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
