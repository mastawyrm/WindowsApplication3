Public Class Form1
    Public fType As String = Nothing

    Private Sub browse_Click(sender As Object, e As EventArgs) Handles browse.Click
        Dim fileDlog As New OpenFileDialog()
        fileDlog.InitialDirectory = "%userprofile%\documents"
        If typeBox.SelectedIndex = 0 Then
            fileDlog.Filter = "Backup Files (*.conf)|*.conf"
            fileDlog.InitialDirectory = "\\msnaf01c\Firewall System Management"
        End If
        If typeBox.SelectedIndex = 1 Then
            fileDlog.Filter = "Excel Files (*.xlsx)|*.xlsx"
            fileDlog.InitialDirectory = "%userprofile%\documents"
        End If
        fileDlog.FilterIndex = 0
        fileDlog.RestoreDirectory = True
        fileDlog.ShowDialog()
        fileBox.Text = fileDlog.FileName
        If fileDlog.FileName Like "*.xlsx" Then fType = "excel"
        If fileDlog.FileName Like "*.conf" Then fType = "fortiConf"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        typeBox.SelectedIndex = 0
    End Sub

    Private Sub IPlistBox_Leave(sender As Object, e As EventArgs) Handles IPlistBox.Leave
        Dim index = 0
        Dim blanks = 0
        Dim lines = IPlistBox.Lines
        IPlistBox.Text = Nothing
        For Each IP In lines
            If extractValidIP(IP) IsNot Nothing Then
                If IPlistBox.Text IsNot "" Then
                    IPlistBox.Text = IPlistBox.Text & vbCrLf
                End If
                IPlistBox.Text = IPlistBox.Text & extractValidIP(IP)
            End If
        Next
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        Dim loadedFile As String = Nothing
        If fileBox.Text Like "*.xlsx" Then fType = "excel"
        If fileBox.Text Like "*.csv" Then fType = "csv"
        If My.Computer.FileSystem.FileExists(fileBox.Text) Then
            loadedFile = loadFile(fileBox.Text, fType)
        Else
            MessageBox.Show("File not found.")
            loadedFile = Nothing
        End If

        If loadedFile IsNot Nothing Then
            If IPlistTab.Visible Then listSearch(IPlistBox.Lines, ANYcheck.Checked)
            If SRCandDSTtab.Visible Then SRC_DSTsearch(srcBox.Text, dstBox.Text, ANYcheck.Checked)
            If DumpTab.Visible Then ruleDump(ANYcheck.Checked)
        End If
    End Sub

    Private Sub outBrowse_Click(sender As Object, e As EventArgs)
        Dim saveDlog As New SaveFileDialog
        saveDlog.InitialDirectory = "%userprofile%\documents"
        saveDlog.Filter = "Excel|*.xlsx"
    End Sub

    Private Sub srcBox_Leave(sender As Object, e As EventArgs) Handles srcBox.Leave
        srcBox.Text = extractValidIP(srcBox.Text)
    End Sub
    Private Sub dstBox_Leave(sender As Object, e As EventArgs) Handles dstBox.Leave
        dstBox.Text = extractValidIP(dstBox.Text)
    End Sub

End Class
