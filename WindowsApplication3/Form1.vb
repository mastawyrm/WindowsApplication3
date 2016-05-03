Public Class Form1
    Private Sub browse_Click(sender As Object, e As EventArgs) Handles browse.Click
        Dim fileDlog As New OpenFileDialog()
        fileDlog.InitialDirectory = "%userprofile%\documents"
        fileDlog.Filter = "All Files (*.*)|*.*" & "|Excel Files (*.xlsx)|*.xlsx" &
            "|CSV Files (*.csv)|*.csv"
        fileDlog.FilterIndex = 1
        fileDlog.RestoreDirectory = True
        fileDlog.ShowDialog()
        fileBox.Text = fileDlog.FileName
    End Sub

    Private Sub search_Click(sender As Object, e As EventArgs) Handles search.Click
        Dim contents As String
        contents = loadFile(fileBox.Text)
        MessageBox.Show(contents)
    End Sub
End Class
