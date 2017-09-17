<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.tblouter = New System.Windows.Forms.TableLayoutPanel()
        Me.trainingButton = New System.Windows.Forms.Button()
        Me.choosenFile = New System.Windows.Forms.Label()
        Me.tbInfo = New System.Windows.Forms.TextBox()
        Me.ofDialog = New System.Windows.Forms.OpenFileDialog()
        Me.tblouter.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblouter
        '
        Me.tblouter.ColumnCount = 2
        Me.tblouter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.tblouter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblouter.Controls.Add(Me.trainingButton, 0, 0)
        Me.tblouter.Controls.Add(Me.choosenFile, 1, 0)
        Me.tblouter.Controls.Add(Me.tbInfo, 0, 1)
        Me.tblouter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblouter.Location = New System.Drawing.Point(0, 0)
        Me.tblouter.Name = "tblouter"
        Me.tblouter.RowCount = 2
        Me.tblouter.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblouter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblouter.Size = New System.Drawing.Size(867, 497)
        Me.tblouter.TabIndex = 0
        '
        'trainingButton
        '
        Me.trainingButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trainingButton.AutoSize = True
        Me.trainingButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.trainingButton.Font = New System.Drawing.Font("Old English Text MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trainingButton.Location = New System.Drawing.Point(3, 3)
        Me.trainingButton.Name = "trainingButton"
        Me.trainingButton.Size = New System.Drawing.Size(100, 30)
        Me.trainingButton.TabIndex = 0
        Me.trainingButton.Text = "Open Image"
        Me.trainingButton.UseVisualStyleBackColor = True
        '
        'choosenFile
        '
        Me.choosenFile.AutoSize = True
        Me.choosenFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.choosenFile.Font = New System.Drawing.Font("Old English Text MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.choosenFile.Location = New System.Drawing.Point(109, 0)
        Me.choosenFile.Name = "choosenFile"
        Me.choosenFile.Size = New System.Drawing.Size(755, 36)
        Me.choosenFile.TabIndex = 1
        Me.choosenFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbInfo
        '
        Me.tblouter.SetColumnSpan(Me.tbInfo, 2)
        Me.tbInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbInfo.Font = New System.Drawing.Font("Old English Text MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbInfo.Location = New System.Drawing.Point(3, 39)
        Me.tbInfo.Multiline = True
        Me.tbInfo.Name = "tbInfo"
        Me.tbInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.tbInfo.Size = New System.Drawing.Size(861, 455)
        Me.tbInfo.TabIndex = 2
        Me.tbInfo.WordWrap = False
        '
        'ofDialog
        '
        Me.ofDialog.FileName = "OpenFileDialog1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(867, 497)
        Me.Controls.Add(Me.tblouter)
        Me.Name = "frmMain"
        Me.Text = "Form1"
        Me.tblouter.ResumeLayout(False)
        Me.tblouter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tblouter As TableLayoutPanel
    Friend WithEvents trainingButton As Button
    Friend WithEvents choosenFile As Label
    Friend WithEvents tbInfo As TextBox
    Friend WithEvents ofDialog As OpenFileDialog
End Class
