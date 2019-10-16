Imports TileGridGenerator.ViewModel.Services

Namespace Services
    Public Class WindowService
        Implements IWindowService

        Private _win As SPSWindow

        Public Function Openwindow(windowname As String, datacontext As Object, owner As Object, Optional topMost As Boolean = False, Optional showInTaskbar As Boolean = True) As Boolean Implements IWindowService.OpenWindow
            Dim spswin As New SPSWindow(windowname, datacontext, SizeToContent.Manual, WindowStartupLocation.CenterOwner) With {
                .Topmost = topMost,
                .ShowInTaskbar = showInTaskbar
            }
            spswin.Owner = spswin.FindOwnerWindow(owner)
            spswin.AsSpsModalDialog = False

            Application.Current.Dispatcher.Invoke(Sub() spswin.Show())
            _win = spswin
            Return True
        End Function



        Public Sub Closewindow(vm As Object) Implements IWindowService.CloseWindow
            Dim owner As Window = Application.Current.Windows.Cast(Of Window)().SingleOrDefault(Function(x) x.DataContext IsNot Nothing AndAlso x.DataContext.GetType Is vm.GetType)

            If owner Is Nothing Then
                For Each window As Window In (From win In Application.Current.Windows()).ToList
                    If window.DataContext.GetType Is vm.GetType Then
                        window.Close()
                    End If
                Next
            Else
                owner.Close()
            End If
        End Sub

        Public Sub CloseWindow() Implements IWindowService.CloseWindow
            If _win IsNot Nothing Then
                _win.Close()
            Else
                Throw New Exception("Win was nothing!!!")
            End If
        End Sub
    End Class
End Namespace
