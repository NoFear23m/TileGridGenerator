Imports TileGridGenerator.ViewModel.Services

Namespace Services
    Public Class StartProcessService
        Implements IStartProcessService

        Public Sub StartProcess(filename As String) Implements IStartProcessService.StartProcess
            Process.Start(filename)
        End Sub
    End Class
End Namespace
