<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.srcBox = New System.Windows.Forms.TextBox()
        Me.dstBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fileBox = New System.Windows.Forms.TextBox()
        Me.browse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.search = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'srcBox
        '
        Me.srcBox.Location = New System.Drawing.Point(64, 70)
        Me.srcBox.Name = "srcBox"
        Me.srcBox.Size = New System.Drawing.Size(100, 22)
        Me.srcBox.TabIndex = 0
        '
        'dstBox
        '
        Me.dstBox.Location = New System.Drawing.Point(261, 70)
        Me.dstBox.Name = "dstBox"
        Me.dstBox.Size = New System.Drawing.Size(100, 22)
        Me.dstBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(184, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Destination:"
        '
        'fileBox
        '
        Me.fileBox.Location = New System.Drawing.Point(14, 28)
        Me.fileBox.Name = "fileBox"
        Me.fileBox.Size = New System.Drawing.Size(266, 22)
        Me.fileBox.TabIndex = 4
        Me.fileBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'browse
        '
        Me.browse.Location = New System.Drawing.Point(286, 27)
        Me.browse.Name = "browse"
        Me.browse.Size = New System.Drawing.Size(75, 23)
        Me.browse.TabIndex = 5
        Me.browse.Text = "Browse:"
        Me.browse.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "File full of data:"
        '
        'search
        '
        Me.search.Location = New System.Drawing.Point(286, 115)
        Me.search.Name = "search"
        Me.search.Size = New System.Drawing.Size(75, 23)
        Me.search.TabIndex = 7
        Me.search.Text = "Search"
        Me.search.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 150)
        Me.Controls.Add(Me.search)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.browse)
        Me.Controls.Add(Me.fileBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dstBox)
        Me.Controls.Add(Me.srcBox)
        Me.MinimumSize = New System.Drawing.Size(405, 45)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents srcBox As TextBox
    Friend WithEvents dstBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents fileBox As TextBox
    Friend WithEvents browse As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents search As Button
End Class
