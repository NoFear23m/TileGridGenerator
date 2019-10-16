Namespace ViewModel.Services
    Public Interface ISaveFileDialogService
        Function ShowDialog(title As String, Optional initialDir As String = "C:\", Optional filter As String = "All files (*.*)|*.*", Optional filterindex As Integer = 1, Optional restoreDir As Boolean = True) As Boolean
        Property FileName As String

    End Interface
End Namespace