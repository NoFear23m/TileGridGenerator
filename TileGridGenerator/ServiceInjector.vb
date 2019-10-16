Imports TileGridGenerator.Services
Imports TileGridGenerator.ViewModel.Services

Public NotInheritable Class ServiceInjector
    Private Sub New()
    End Sub

    Public Shared Sub InjectServices()
        ServiceContainer.Instance.AddService(Of IMessageboxService)(New MessageboxService())
        ServiceContainer.Instance.AddService(Of IDialogWindowService)(New DialogWindowService())
        ServiceContainer.Instance.AddService(Of IWaitingCursorService)(New WaitingCursorService())
        ServiceContainer.Instance.AddService(Of IOpenFileDialogService)(New OpenFileDialogService())
        ServiceContainer.Instance.AddService(Of IWindowService)(New WindowService())
        ServiceContainer.Instance.AddService(Of ISaveFileDialogService)(New SaveFileDialogService())
        ServiceContainer.Instance.AddService(Of IStartProcessService)(New StartProcessService())
    End Sub
End Class