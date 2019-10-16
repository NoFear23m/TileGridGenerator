Imports TileGridGenerator.ViewModel.Services

Namespace Services
    Public Class SaveFileDialogService
        Implements ISaveFileDialogService

        Public Property FileName As String Implements ISaveFileDialogService.FileName


        Public Function ShowDialog(title As String, Optional initialDir As String = "C:\", Optional filter As String = "All files (*.*)|*.*", Optional filterindex As Integer = 1, Optional restoreDir As Boolean = True) As Boolean Implements ISaveFileDialogService.ShowDialog
            Dim ofd As New Forms.SaveFileDialog
            ofd.Filter = filter
            ofd.FilterIndex = filterindex
            ofd.InitialDirectory = initialDir
            ofd.RestoreDirectory = restoreDir
            ofd.Title = title
            Dim ret As Boolean? = ofd.ShowDialog()
            FileName = ofd.FileName
            Return ret.HasValue AndAlso ret.Value AndAlso Not String.IsNullOrEmpty(FileName)

        End Function
    End Class
End Namespace
