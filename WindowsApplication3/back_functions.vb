Imports System.IO

Module back_functions
    Public Function loadFile(ByVal fName As String)
        Dim value As String
        value = File.ReadAllText(fName)
        Return value
    End Function
End Module
