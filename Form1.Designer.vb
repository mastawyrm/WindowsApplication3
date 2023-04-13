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
        Me.components = New System.ComponentModel.Container()
        Me.srcBox = New System.Windows.Forms.TextBox()
        Me.dstBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fileBox = New System.Windows.Forms.TextBox()
        Me.browse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.search = New System.Windows.Forms.Button()
        Me.Tabs = New System.Windows.Forms.TabControl()
        Me.IPlistTab = New System.Windows.Forms.TabPage()
        Me.IPentry = New System.Windows.Forms.TextBox()
        Me.cidrDrop = New System.Windows.Forms.ComboBox()
        Me.IPaddButton = New System.Windows.Forms.Button()
        Me.allPortsBox = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.portBox = New System.Windows.Forms.TextBox()
        Me.IPlistBox = New System.Windows.Forms.TextBox()
        Me.SRCandDSTtab = New System.Windows.Forms.TabPage()
        Me.SwapAdd = New System.Windows.Forms.Button()
        Me.DumpTab = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.parentGroupCheck = New System.Windows.Forms.CheckBox()
        Me.ANYcheck = New System.Windows.Forms.CheckBox()
        Me.typeBox = New System.Windows.Forms.ComboBox()
        Me.resolve_check = New System.Windows.Forms.CheckBox()
        Me.subnetCheck = New System.Windows.Forms.CheckBox()
        Me.grpExpandChk = New System.Windows.Forms.CheckBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.vdomBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.noResultBox = New System.Windows.Forms.CheckBox()
        Me.Tabs.SuspendLayout()
        Me.IPlistTab.SuspendLayout()
        Me.SRCandDSTtab.SuspendLayout()
        Me.DumpTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'srcBox
        '
        Me.srcBox.Location = New System.Drawing.Point(71, 7)
        Me.srcBox.Margin = New System.Windows.Forms.Padding(2)
        Me.srcBox.Name = "srcBox"
        Me.srcBox.Size = New System.Drawing.Size(95, 20)
        Me.srcBox.TabIndex = 0
        '
        'dstBox
        '
        Me.dstBox.Location = New System.Drawing.Point(71, 32)
        Me.dstBox.Margin = New System.Windows.Forms.Padding(2)
        Me.dstBox.Name = "dstBox"
        Me.dstBox.Size = New System.Drawing.Size(95, 20)
        Me.dstBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Source:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Destination:"
        '
        'fileBox
        '
        Me.fileBox.Location = New System.Drawing.Point(59, 36)
        Me.fileBox.Margin = New System.Windows.Forms.Padding(2)
        Me.fileBox.Name = "fileBox"
        Me.fileBox.Size = New System.Drawing.Size(167, 20)
        Me.fileBox.TabIndex = 4
        '
        'browse
        '
        Me.browse.Location = New System.Drawing.Point(229, 37)
        Me.browse.Margin = New System.Windows.Forms.Padding(2)
        Me.browse.Name = "browse"
        Me.browse.Size = New System.Drawing.Size(51, 19)
        Me.browse.TabIndex = 5
        Me.browse.Text = "Browse:"
        Me.browse.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 14)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "File type:"
        '
        'search
        '
        Me.search.Location = New System.Drawing.Point(218, 332)
        Me.search.Margin = New System.Windows.Forms.Padding(2)
        Me.search.Name = "search"
        Me.search.Size = New System.Drawing.Size(56, 19)
        Me.search.TabIndex = 7
        Me.search.Text = "Search"
        Me.search.UseVisualStyleBackColor = True
        '
        'Tabs
        '
        Me.Tabs.Controls.Add(Me.IPlistTab)
        Me.Tabs.Controls.Add(Me.SRCandDSTtab)
        Me.Tabs.Controls.Add(Me.DumpTab)
        Me.Tabs.Location = New System.Drawing.Point(10, 87)
        Me.Tabs.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(271, 178)
        Me.Tabs.TabIndex = 11
        '
        'IPlistTab
        '
        Me.IPlistTab.Controls.Add(Me.IPentry)
        Me.IPlistTab.Controls.Add(Me.cidrDrop)
        Me.IPlistTab.Controls.Add(Me.IPaddButton)
        Me.IPlistTab.Controls.Add(Me.allPortsBox)
        Me.IPlistTab.Controls.Add(Me.Label6)
        Me.IPlistTab.Controls.Add(Me.portBox)
        Me.IPlistTab.Controls.Add(Me.IPlistBox)
        Me.IPlistTab.Location = New System.Drawing.Point(4, 22)
        Me.IPlistTab.Margin = New System.Windows.Forms.Padding(2)
        Me.IPlistTab.Name = "IPlistTab"
        Me.IPlistTab.Padding = New System.Windows.Forms.Padding(2)
        Me.IPlistTab.Size = New System.Drawing.Size(263, 152)
        Me.IPlistTab.TabIndex = 0
        Me.IPlistTab.Text = "IPlist"
        Me.IPlistTab.UseVisualStyleBackColor = True
        '
        'IPentry
        '
        Me.IPentry.Location = New System.Drawing.Point(5, 6)
        Me.IPentry.Margin = New System.Windows.Forms.Padding(2)
        Me.IPentry.Name = "IPentry"
        Me.IPentry.Size = New System.Drawing.Size(90, 20)
        Me.IPentry.TabIndex = 7
        '
        'cidrDrop
        '
        Me.cidrDrop.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.cidrDrop.DropDownHeight = 110
        Me.cidrDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cidrDrop.DropDownWidth = 42
        Me.cidrDrop.FormattingEnabled = True
        Me.cidrDrop.IntegralHeight = False
        Me.cidrDrop.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32"})
        Me.cidrDrop.Location = New System.Drawing.Point(99, 6)
        Me.cidrDrop.Margin = New System.Windows.Forms.Padding(2)
        Me.cidrDrop.Name = "cidrDrop"
        Me.cidrDrop.Size = New System.Drawing.Size(46, 21)
        Me.cidrDrop.TabIndex = 6
        '
        'IPaddButton
        '
        Me.IPaddButton.Location = New System.Drawing.Point(149, 5)
        Me.IPaddButton.Margin = New System.Windows.Forms.Padding(2)
        Me.IPaddButton.Name = "IPaddButton"
        Me.IPaddButton.Size = New System.Drawing.Size(17, 21)
        Me.IPaddButton.TabIndex = 5
        Me.IPaddButton.Text = "+"
        Me.IPaddButton.UseVisualStyleBackColor = True
        '
        'allPortsBox
        '
        Me.allPortsBox.AutoSize = True
        Me.allPortsBox.Checked = True
        Me.allPortsBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.allPortsBox.Location = New System.Drawing.Point(229, 3)
        Me.allPortsBox.Margin = New System.Windows.Forms.Padding(2)
        Me.allPortsBox.Name = "allPortsBox"
        Me.allPortsBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.allPortsBox.Size = New System.Drawing.Size(37, 17)
        Me.allPortsBox.TabIndex = 4
        Me.allPortsBox.Text = "All"
        Me.allPortsBox.UseVisualStyleBackColor = True
        Me.allPortsBox.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(190, 4)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Ports:"
        '
        'portBox
        '
        Me.portBox.Location = New System.Drawing.Point(149, 37)
        Me.portBox.Margin = New System.Windows.Forms.Padding(2)
        Me.portBox.Multiline = True
        Me.portBox.Name = "portBox"
        Me.portBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.portBox.Size = New System.Drawing.Size(114, 116)
        Me.portBox.TabIndex = 2
        '
        'IPlistBox
        '
        Me.IPlistBox.BackColor = System.Drawing.SystemColors.HighlightText
        Me.IPlistBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.IPlistBox.HideSelection = False
        Me.IPlistBox.Location = New System.Drawing.Point(4, 34)
        Me.IPlistBox.Margin = New System.Windows.Forms.Padding(2)
        Me.IPlistBox.Multiline = True
        Me.IPlistBox.Name = "IPlistBox"
        Me.IPlistBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.IPlistBox.Size = New System.Drawing.Size(128, 116)
        Me.IPlistBox.TabIndex = 0
        Me.IPlistBox.WordWrap = False
        '
        'SRCandDSTtab
        '
        Me.SRCandDSTtab.Controls.Add(Me.SwapAdd)
        Me.SRCandDSTtab.Controls.Add(Me.Label1)
        Me.SRCandDSTtab.Controls.Add(Me.srcBox)
        Me.SRCandDSTtab.Controls.Add(Me.Label2)
        Me.SRCandDSTtab.Controls.Add(Me.dstBox)
        Me.SRCandDSTtab.Location = New System.Drawing.Point(4, 22)
        Me.SRCandDSTtab.Margin = New System.Windows.Forms.Padding(2)
        Me.SRCandDSTtab.Name = "SRCandDSTtab"
        Me.SRCandDSTtab.Padding = New System.Windows.Forms.Padding(2)
        Me.SRCandDSTtab.Size = New System.Drawing.Size(263, 152)
        Me.SRCandDSTtab.TabIndex = 1
        Me.SRCandDSTtab.Text = "SRCandDST"
        Me.SRCandDSTtab.UseVisualStyleBackColor = True
        '
        'SwapAdd
        '
        Me.SwapAdd.Font = New System.Drawing.Font("Onyx", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SwapAdd.Location = New System.Drawing.Point(168, 10)
        Me.SwapAdd.Margin = New System.Windows.Forms.Padding(0)
        Me.SwapAdd.Name = "SwapAdd"
        Me.SwapAdd.Size = New System.Drawing.Size(29, 38)
        Me.SwapAdd.TabIndex = 4
        Me.SwapAdd.Text = "↑↓"
        Me.SwapAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SwapAdd.UseVisualStyleBackColor = True
        '
        'DumpTab
        '
        Me.DumpTab.Controls.Add(Me.Label4)
        Me.DumpTab.Location = New System.Drawing.Point(4, 22)
        Me.DumpTab.Margin = New System.Windows.Forms.Padding(2)
        Me.DumpTab.Name = "DumpTab"
        Me.DumpTab.Padding = New System.Windows.Forms.Padding(2)
        Me.DumpTab.Size = New System.Drawing.Size(263, 152)
        Me.DumpTab.TabIndex = 2
        Me.DumpTab.Text = "Dump"
        Me.DumpTab.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(61, 55)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Output all rules and objects"
        '
        'parentGroupCheck
        '
        Me.parentGroupCheck.AutoSize = True
        Me.parentGroupCheck.Location = New System.Drawing.Point(9, 292)
        Me.parentGroupCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.parentGroupCheck.Name = "parentGroupCheck"
        Me.parentGroupCheck.Size = New System.Drawing.Size(132, 17)
        Me.parentGroupCheck.TabIndex = 16
        Me.parentGroupCheck.Text = "Include Parent Groups"
        Me.parentGroupCheck.UseVisualStyleBackColor = True
        '
        'ANYcheck
        '
        Me.ANYcheck.AutoSize = True
        Me.ANYcheck.Location = New System.Drawing.Point(9, 311)
        Me.ANYcheck.Margin = New System.Windows.Forms.Padding(2)
        Me.ANYcheck.Name = "ANYcheck"
        Me.ANYcheck.Size = New System.Drawing.Size(111, 17)
        Me.ANYcheck.TabIndex = 15
        Me.ANYcheck.Text = "Include ANY rules"
        Me.ANYcheck.UseVisualStyleBackColor = True
        '
        'typeBox
        '
        Me.typeBox.DisplayMember = "(none)"
        Me.typeBox.FormattingEnabled = True
        Me.typeBox.Items.AddRange(New Object() {"Fortinet backup configs", "NSM export configs", "Joe's Excel Dumps"})
        Me.typeBox.Location = New System.Drawing.Point(59, 11)
        Me.typeBox.Margin = New System.Windows.Forms.Padding(2)
        Me.typeBox.Name = "typeBox"
        Me.typeBox.Size = New System.Drawing.Size(222, 21)
        Me.typeBox.TabIndex = 18
        '
        'resolve_check
        '
        Me.resolve_check.AutoSize = True
        Me.resolve_check.Location = New System.Drawing.Point(187, 310)
        Me.resolve_check.Margin = New System.Windows.Forms.Padding(2)
        Me.resolve_check.Name = "resolve_check"
        Me.resolve_check.Size = New System.Drawing.Size(83, 17)
        Me.resolve_check.TabIndex = 19
        Me.resolve_check.Text = "Resolve IPs"
        Me.resolve_check.UseVisualStyleBackColor = True
        '
        'subnetCheck
        '
        Me.subnetCheck.AutoSize = True
        Me.subnetCheck.Location = New System.Drawing.Point(9, 270)
        Me.subnetCheck.Margin = New System.Windows.Forms.Padding(2)
        Me.subnetCheck.Name = "subnetCheck"
        Me.subnetCheck.Size = New System.Drawing.Size(103, 17)
        Me.subnetCheck.TabIndex = 20
        Me.subnetCheck.Text = "Include Subnets"
        Me.subnetCheck.UseVisualStyleBackColor = True
        '
        'grpExpandChk
        '
        Me.grpExpandChk.AutoSize = True
        Me.grpExpandChk.Location = New System.Drawing.Point(187, 292)
        Me.grpExpandChk.Margin = New System.Windows.Forms.Padding(2)
        Me.grpExpandChk.Name = "grpExpandChk"
        Me.grpExpandChk.Size = New System.Drawing.Size(110, 17)
        Me.grpExpandChk.TabIndex = 21
        Me.grpExpandChk.Text = "Expand all groups"
        Me.grpExpandChk.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'vdomBox
        '
        Me.vdomBox.FormattingEnabled = True
        Me.vdomBox.Location = New System.Drawing.Point(59, 61)
        Me.vdomBox.Name = "vdomBox"
        Me.vdomBox.Size = New System.Drawing.Size(222, 21)
        Me.vdomBox.TabIndex = 23
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(21, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Vdom:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(2, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Config file:"
        '
        'noResultBox
        '
        Me.noResultBox.AutoSize = True
        Me.noResultBox.Location = New System.Drawing.Point(187, 270)
        Me.noResultBox.Name = "noResultBox"
        Me.noResultBox.Size = New System.Drawing.Size(95, 17)
        Me.noResultBox.TabIndex = 26
        Me.noResultBox.Text = "Show 0 results"
        Me.noResultBox.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 361)
        Me.Controls.Add(Me.noResultBox)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.vdomBox)
        Me.Controls.Add(Me.grpExpandChk)
        Me.Controls.Add(Me.subnetCheck)
        Me.Controls.Add(Me.parentGroupCheck)
        Me.Controls.Add(Me.resolve_check)
        Me.Controls.Add(Me.typeBox)
        Me.Controls.Add(Me.Tabs)
        Me.Controls.Add(Me.ANYcheck)
        Me.Controls.Add(Me.search)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.browse)
        Me.Controls.Add(Me.fileBox)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(308, 318)
        Me.Name = "Form1"
        Me.Text = "Rule Search"
        Me.Tabs.ResumeLayout(False)
        Me.IPlistTab.ResumeLayout(False)
        Me.IPlistTab.PerformLayout()
        Me.SRCandDSTtab.ResumeLayout(False)
        Me.SRCandDSTtab.PerformLayout()
        Me.DumpTab.ResumeLayout(False)
        Me.DumpTab.PerformLayout()
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
    Friend WithEvents Tabs As TabControl
    Friend WithEvents IPlistTab As TabPage
    Friend WithEvents SRCandDSTtab As TabPage
    Friend WithEvents IPlistBox As TextBox
    Friend WithEvents ANYcheck As CheckBox
    Friend WithEvents typeBox As ComboBox
    Friend WithEvents resolve_check As CheckBox
    Friend WithEvents DumpTab As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents portBox As TextBox
    Friend WithEvents allPortsBox As CheckBox
    Friend WithEvents parentGroupCheck As CheckBox
    Friend WithEvents subnetCheck As CheckBox
    Friend WithEvents IPaddButton As Button
    Friend WithEvents cidrDrop As ComboBox
    Friend WithEvents IPentry As TextBox
    Friend WithEvents grpExpandChk As CheckBox
    Friend WithEvents SwapAdd As Button
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents vdomBox As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents noResultBox As CheckBox
End Class
