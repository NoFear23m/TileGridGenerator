Namespace ViewModel.Services
    Public Interface IOpenFileDialogService
        Property OpenedFiles As List(Of String)

        Function ShowDialog(title As String, Optional initialDir As String = "C:\", Optional filter As String = "All files (*.*)|*.*",
                            Optional filterindex As Integer = 1, Optional restoreDir As Boolean = True, Optional multiSelect As Boolean = False) As EnuOpenFileDialogResult

    End Interface

    Public Enum EnuOpenFileDialogResult
        Ok = 1
        Cancel = 2
    End Enum
End Namespace