
Imports TileGridGenerator.ViewModel.Services
Imports OpenFileDialog = Microsoft.Win32.OpenFileDialog

Namespace Services
    Public Class OpenFileDialogService
        Implements IOpenFileDialogService

        Public Property OpenedFiles As List(Of String) Implements IOpenFileDialogService.OpenedFiles

        Public Function ShowDialog(title As String, Optional initialDir As String = "C:\", Optional filter As String = "All files (*.*)|*.*", Optional filterindex As Integer = 1, Optional restoreDir As Boolean = True, Optional multiSelect As Boolean = False) As EnuOpenFileDialogResult Implements IOpenFileDialogService.ShowDialog
            Dim ofd As New OpenFileDialog With {
                .Filter = filter,
                .FilterIndex = filterindex,
                .InitialDirectory = initialDir,
                .Multiselect = multiSelect,
                .RestoreDirectory = restoreDir,
                .Title = title
            }
            If ofd.ShowDialog Then
                OpenedFiles = ofd.FileNames.ToList
                Return EnuOpenFileDialogResult.Ok
            Else
                OpenedFiles = New List(Of String)
                Return EnuOpenFileDialogResult.Cancel
            End If
        End Function
    End Class
End Namespace
