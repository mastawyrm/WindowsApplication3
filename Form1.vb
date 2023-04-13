Public Class Form1
    Public fType As String = "fortiConf"
    Public vdoms As New List(Of String)

    Private Sub browse_Click(sender As Object, e As EventArgs) Handles browse.Click
        Dim fileDlog As New OpenFileDialog()
        fileDlog.InitialDirectory = "%userprofile%\documents"
        If typeBox.SelectedIndex = 0 Then
            fileDlog.Filter = "Backup Files (*.conf)|*.conf"
            fileDlog.InitialDirectory = "%userprofile%\documents"
        End If
        If typeBox.SelectedIndex = 1 Then
            fileDlog.Filter = "Show Config (Netscreen)|*"
            fileDlog.InitialDirectory = "%userprofile%\documents"
        End If
        If typeBox.SelectedIndex = 2 Then
            fileDlog.Filter = "Excel Files (*.xlsx)|*.xlsx"
            fileDlog.InitialDirectory = "%userprofile%\documents"
        End If
        fileDlog.FilterIndex = 0
        fileDlog.RestoreDirectory = False
        fileDlog.ShowDialog()
        fileBox.Text = fileDlog.FileName
        If typeBox.SelectedIndex = 0 Then
            vdomBox.Items.Clear()
            If My.Computer.FileSystem.FileExists(fileBox.Text) Then
                vdoms = vdomLoad(fileBox.Text)
                For Each vdom In vdoms
                    vdomBox.Items.Add(vdom)
                Next
                If vdomBox.Items.Count = 0 Then
                    vdomBox.Items.Add("")
                Else
                    vdomBox.Items.Add("All")
                End If

                vdomBox.SelectedIndex = 0

                End If
        End If
    End Sub

    Private Sub typeBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles typeBox.SelectedIndexChanged
        Select Case typeBox.SelectedIndex
            Case 0
                fType = "fortiConf"
            Case 1
                fType = "ssgConf"
            Case 2
                fType = "excel"
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        typeBox.SelectedIndex = 0
        cidrDrop.SelectedIndex = 32
    End Sub

    Private Sub IPlistBox_Leave(sender As Object, e As EventArgs) Handles IPlistBox.Leave
        Dim index = 0
        Dim blanks = 0
        Dim lines = IPlistBox.Lines
        IPlistBox.Text = Nothing
        For Each IP In lines
            If IP IsNot "" And IPlistBox.Lines.Contains(IP) = False Then
                If IPlistBox.Text IsNot "" Then IPlistBox.Text += vbCrLf
                IPlistBox.Text += IP
            End If
        Next
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        Dim loadedFile As String = Nothing
        If fileBox.Text Like "*.xlsx" Then fType = "excel"
        If fileBox.Text Like "*.csv" Then fType = "csv"
        If My.Computer.FileSystem.FileExists(fileBox.Text) Then
            test = vdomBox.Items(0)
            If vdomBox.Items(0) IsNot "" And vdomBox.SelectedIndex = (vdomBox.Items.Count - 1) Then
                For Each vdom In vdoms
                    loadedFile = loadFile(fileBox.Text, fType, vdom, vdoms)
                    If loadedFile IsNot Nothing Then
                        If IPlistTab.Visible Then
                            If allPortsBox.Checked Then
                                listSearch(IPlistBox.Lines.Reverse.ToArray, ANYcheck.Checked, subnetCheck.Checked, parentGroupCheck.Checked, noResultBox.Checked, grpExpandChk.Checked, resolve_check.Checked,, vdom)
                            Else
                                listSearch(IPlistBox.Lines.Reverse.ToArray, ANYcheck.Checked, subnetCheck.Checked, parentGroupCheck.Checked, noResultBox.Checked, grpExpandChk.Checked, resolve_check.Checked, portBox.Lines, vdom)
                            End If
                        End If

                        If SRCandDSTtab.Visible Then SRC_DSTsearch(srcBox.Text, dstBox.Text, ANYcheck.Checked, subnetCheck.Checked, grpExpandChk.Checked, resolve_check.Checked, vdom)
                        If DumpTab.Visible Then ruleDump(ANYcheck.Checked, resolve_check.Checked, vdom)
                    End If
                Next
            Else
                loadedFile = loadFile(fileBox.Text, fType, vdomBox.SelectedItem, vdoms)
                If loadedFile IsNot Nothing Then
                    If IPlistTab.Visible Then
                        If allPortsBox.Checked Then
                            listSearch(IPlistBox.Lines.Reverse.ToArray, ANYcheck.Checked, subnetCheck.Checked, parentGroupCheck.Checked, noResultBox.Checked, grpExpandChk.Checked, resolve_check.Checked)
                        Else
                            listSearch(IPlistBox.Lines.Reverse.ToArray, ANYcheck.Checked, subnetCheck.Checked, parentGroupCheck.Checked, noResultBox.Checked, grpExpandChk.Checked, resolve_check.Checked, portBox.Lines)
                        End If
                    End If

                    If SRCandDSTtab.Visible Then SRC_DSTsearch(srcBox.Text, dstBox.Text, ANYcheck.Checked, subnetCheck.Checked, grpExpandChk.Checked, resolve_check.Checked)
                    If DumpTab.Visible Then ruleDump(ANYcheck.Checked, resolve_check.Checked)
                End If
            End If
        Else
            MessageBox.Show("File not found.")
            loadedFile = Nothing
        End If


    End Sub

    Private Sub srcBox_Leave(sender As Object, e As EventArgs) Handles srcBox.Leave
        srcBox.Text = extractValidIP(srcBox.Text)
    End Sub
    Private Sub dstBox_Leave(sender As Object, e As EventArgs) Handles dstBox.Leave
        dstBox.Text = extractValidIP(dstBox.Text)
    End Sub

    Private Sub AllBox_CheckedChanged(sender As Object, e As EventArgs) Handles allPortsBox.CheckedChanged
        If allPortsBox.Checked Then
            portBox.Enabled = False
        ElseIf Not allPortsBox.Checked Then
            portBox.Enabled = True
        End If
    End Sub

    Private Sub IPaddButton_Click(sender As Object, e As EventArgs) Handles IPaddButton.Click
        Dim output As String = extractValidIP(IPentry.Text)
        If output IsNot Nothing Then
            output += "_" + cidrDrop.Text
            If IPlistBox.Text IsNot "" Then IPlistBox.AppendText(vbNewLine)
            IPlistBox.AppendText(output)
        End If
    End Sub

    Private Sub IPlistBox_mouseClick(sender As Object, e As MouseEventArgs) Handles IPlistBox.Click
        Dim charIndex = IPlistBox.GetCharIndexFromPosition(e.Location)
        Dim line = IPlistBox.GetLineFromCharIndex(charIndex)
        If IPlistBox.Lines.Count > line Then
            IPlistBox.SelectionStart = IPlistBox.GetFirstCharIndexOfCurrentLine
            IPlistBox.SelectionLength = IPlistBox.Lines(line).Length
        End If
    End Sub

    Private Sub IPentry_KeyDown(sender As Object, e As KeyEventArgs) Handles IPentry.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim output As String = extractValidIP(IPentry.Text)
            If output IsNot Nothing Then
                output += "_" + cidrDrop.Text
                If IPlistBox.Text IsNot "" Then IPlistBox.AppendText(vbNewLine)
                IPlistBox.AppendText(output)
            End If
            IPentry.SelectAll()
        End If
    End Sub

    Private Sub IPlistBox_KeyDown(sender As Object, e As KeyEventArgs) Handles IPlistBox.KeyDown
        If e.KeyCode = Keys.Delete Then
            IPlistBox.SelectedText = ""
        End If
    End Sub

    Private Sub srcBox_KeyDown(sender As Object, e As KeyEventArgs) Handles srcBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            search.PerformClick()
        End If
    End Sub

    Private Sub dstBox_KeyDown(sender As Object, e As KeyEventArgs) Handles dstBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            search.PerformClick()
        End If
    End Sub

    Private Sub SwapAdd_Click(sender As Object, e As EventArgs) Handles SwapAdd.Click
        Dim temp As String = srcBox.Text
        srcBox.Text = dstBox.Text
        dstBox.Text = temp
    End Sub

    Private Sub cidrDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cidrDrop.SelectedIndexChanged

    End Sub

End Class
