<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.srcBox = New System.Windows.Forms.TextBox()
        Me.dstBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fileBox = New System.Windows.Forms.TextBox()
        Me.browse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.search = New System.Windows.Forms.Button()
        Me.DumpTab = New System.Windows.Forms.TabControl()
        Me.IPlistTab = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.IPlistBox = New System.Windows.Forms.TextBox()
        Me.SRCandDSTtab = New System.Windows.Forms.TabPage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ANYcheck = New System.Windows.Forms.CheckBox()
        Me.typeBox = New System.Windows.Forms.ComboBox()
        Me.XLcheck = New System.Windows.Forms.CheckBox()
        Me.DumpTab.SuspendLayout()
        Me.IPlistTab.SuspendLayout()
        Me.SRCandDSTtab.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'srcBox
        '
        Me.srcBox.Location = New System.Drawing.Point(95, 9)
        Me.srcBox.Name = "srcBox"
        Me.srcBox.Size = New System.Drawing.Size(100, 22)
        Me.srcBox.TabIndex = 0
        '
        'dstBox
        '
        Me.dstBox.Location = New System.Drawing.Point(95, 40)
        Me.dstBox.Name = "dstBox"
        Me.dstBox.Size = New System.Drawing.Size(100, 22)
        Me.dstBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Destination:"
        '
        'fileBox
        '
        Me.fileBox.Location = New System.Drawing.Point(14, 28)
        Me.fileBox.Name = "fileBox"
        Me.fileBox.Size = New System.Drawing.Size(347, 22)
        Me.fileBox.TabIndex = 4
        '
        'browse
        '
        Me.browse.Location = New System.Drawing.Point(293, 56)
        Me.browse.Name = "browse"
        Me.browse.Size = New System.Drawing.Size(68, 23)
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
        Me.search.Location = New System.Drawing.Point(288, 295)
        Me.search.Name = "search"
        Me.search.Size = New System.Drawing.Size(75, 23)
        Me.search.TabIndex = 7
        Me.search.Text = "Search"
        Me.search.UseVisualStyleBackColor = True
        '
        'DumpTab
        '
        Me.DumpTab.Controls.Add(Me.IPlistTab)
        Me.DumpTab.Controls.Add(Me.SRCandDSTtab)
        Me.DumpTab.Controls.Add(Me.TabPage1)
        Me.DumpTab.Location = New System.Drawing.Point(14, 85)
        Me.DumpTab.Name = "DumpTab"
        Me.DumpTab.SelectedIndex = 0
        Me.DumpTab.Size = New System.Drawing.Size(349, 204)
        Me.DumpTab.TabIndex = 11
        '
        'IPlistTab
        '
        Me.IPlistTab.Controls.Add(Me.Label5)
        Me.IPlistTab.Controls.Add(Me.IPlistBox)
        Me.IPlistTab.Location = New System.Drawing.Point(4, 25)
        Me.IPlistTab.Name = "IPlistTab"
        Me.IPlistTab.Padding = New System.Windows.Forms.Padding(3)
        Me.IPlistTab.Size = New System.Drawing.Size(341, 175)
        Me.IPlistTab.TabIndex = 0
        Me.IPlistTab.Text = "IPlist"
        Me.IPlistTab.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 17)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Enter IPs to search:"
        '
        'IPlistBox
        '
        Me.IPlistBox.Location = New System.Drawing.Point(6, 26)
        Me.IPlistBox.Multiline = True
        Me.IPlistBox.Name = "IPlistBox"
        Me.IPlistBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.IPlistBox.Size = New System.Drawing.Size(154, 142)
        Me.IPlistBox.TabIndex = 0
        '
        'SRCandDSTtab
        '
        Me.SRCandDSTtab.Controls.Add(Me.Label1)
        Me.SRCandDSTtab.Controls.Add(Me.srcBox)
        Me.SRCandDSTtab.Controls.Add(Me.Label2)
        Me.SRCandDSTtab.Controls.Add(Me.dstBox)
        Me.SRCandDSTtab.Location = New System.Drawing.Point(4, 25)
        Me.SRCandDSTtab.Name = "SRCandDSTtab"
        Me.SRCandDSTtab.Padding = New System.Windows.Forms.Padding(3)
        Me.SRCandDSTtab.Size = New System.Drawing.Size(341, 175)
        Me.SRCandDSTtab.TabIndex = 1
        Me.SRCandDSTtab.Text = "SRCandDST"
        Me.SRCandDSTtab.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(341, 175)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Dump"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(81, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(181, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Output all rules and objects"
        '
        'ANYcheck
        '
        Me.ANYcheck.AutoSize = True
        Me.ANYcheck.Location = New System.Drawing.Point(140, 297)
        Me.ANYcheck.Name = "ANYcheck"
        Me.ANYcheck.Size = New System.Drawing.Size(142, 21)
        Me.ANYcheck.TabIndex = 15
        Me.ANYcheck.Text = "Include ANY rules"
        Me.ANYcheck.UseVisualStyleBackColor = True
        '
        'typeBox
        '
        Me.typeBox.DisplayMember = "(none)"
        Me.typeBox.FormattingEnabled = True
        Me.typeBox.Items.AddRange(New Object() {"Fortinet Backup configs", "Joe's Excel Dumps"})
        Me.typeBox.Location = New System.Drawing.Point(16, 55)
        Me.typeBox.Name = "typeBox"
        Me.typeBox.Size = New System.Drawing.Size(264, 24)
        Me.typeBox.TabIndex = 18
        '
        'XLcheck
        '
        Me.XLcheck.AutoSize = True
        Me.XLcheck.Location = New System.Drawing.Point(14, 297)
        Me.XLcheck.Name = "XLcheck"
        Me.XLcheck.Size = New System.Drawing.Size(126, 21)
        Me.XLcheck.TabIndex = 19
        Me.XLcheck.Text = "Output to Excel"
        Me.XLcheck.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(387, 338)
        Me.Controls.Add(Me.XLcheck)
        Me.Controls.Add(Me.typeBox)
        Me.Controls.Add(Me.ANYcheck)
        Me.Controls.Add(Me.DumpTab)
        Me.Controls.Add(Me.search)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.browse)
        Me.Controls.Add(Me.fileBox)
        Me.MinimumSize = New System.Drawing.Size(405, 383)
        Me.Name = "Form1"
        Me.Text = "Rule Search"
        Me.DumpTab.ResumeLayout(False)
        Me.IPlistTab.ResumeLayout(False)
        Me.IPlistTab.PerformLayout()
        Me.SRCandDSTtab.ResumeLayout(False)
        Me.SRCandDSTtab.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
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
    Friend WithEvents DumpTab As TabControl
    Friend WithEvents IPlistTab As TabPage
    Friend WithEvents SRCandDSTtab As TabPage
    Friend WithEvents IPlistBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ANYcheck As CheckBox
    Friend WithEvents typeBox As ComboBox
    Friend WithEvents XLcheck As CheckBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label4 As Label
End Class
