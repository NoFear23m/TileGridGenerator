Imports System.Threading
Imports TileGridGenerator.ViewModel.Services

Namespace Services
    Public Class MessageboxService
        Implements IMessageboxService

        Public Function Show(message As String, buttons As EnuMessageBoxButton, image As EnuMessageBoxImage) As EnuMessageBoxResult Implements IMessageboxService.Show
            Return Show(message, My.Application.Info.ProductName, buttons, image)
        End Function

        Public Function Show(message As String, image As EnuMessageBoxImage) As EnuMessageBoxResult Implements IMessageboxService.Show
            Return Show(message, My.Application.Info.ProductName, EnuMessageBoxButton.Ok, image)
        End Function

        Public Function Show(message As String, title As String, image As EnuMessageBoxImage) As EnuMessageBoxResult Implements IMessageboxService.Show
            Return Show(message, title, EnuMessageBoxButton.Ok, image)
        End Function

        Public Function Show(message As String) As EnuMessageBoxResult Implements IMessageboxService.Show
            Return Show(message, My.Application.Info.ProductName, EnuMessageBoxButton.Ok, EnuMessageBoxImage.None)
        End Function

        Public Function Show(text As String, caption As String, buttons As EnuMessageBoxButton, image As EnuMessageBoxImage) As EnuMessageBoxResult Implements IMessageboxService.Show
            Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA)
            Dim wcServ As IWaitingCursorService = ServiceContainer.GetService(Of IWaitingCursorService)
            wcServ.SetWaitingCursor(False)
            Dim msgBoxResult As MessageBoxResult = Application.Current.Dispatcher.Invoke(Function() MessageBox.Show(text, caption, DirectCast(buttons, MessageBoxButton), DirectCast(image, MessageBoxImage)))
            Return CType(msgBoxResult, EnuMessageBoxResult)
        End Function



    End Class
End Namespace
