Namespace ViewModel.Services
    Public Interface IMessageboxService

        Function Show(message As String, title As String, buttons As EnuMessageBoxButton, image As EnuMessageBoxImage) As EnuMessageBoxResult

        Function Show(message As String, buttons As EnuMessageBoxButton, image As EnuMessageBoxImage) As EnuMessageBoxResult

        Function Show(message As String, image As EnuMessageBoxImage) As EnuMessageBoxResult

        Function Show(message As String, title As String, image As EnuMessageBoxImage) As EnuMessageBoxResult

        Function Show(message As String) As EnuMessageBoxResult

    End Interface

    Public Enum EnuMessageBoxResult
        None = 0
        Ok = 1
        Cancel = 2
        Yes = 6
        No = 7
    End Enum

    Public Enum EnuMessageBoxButton
        Ok = 0
        OkCancel = 1
        YesNoCancel = 3
        YesNo = 4
    End Enum

    Public Enum EnuMessageBoxImage
        None = 0
        [Error] = 16
        Question = 32
        Warning = 48
        Information = 64
    End Enum

End Namespace