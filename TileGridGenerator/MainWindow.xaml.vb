Imports FlickrNet
Imports ICSharpCode.AvalonEdit.Highlighting
Imports TileGridGenerator.ViewModel.Services

Class MainWindow
    Inherits MahApps.Metro.Controls.MetroWindow



    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

    End Sub



    Private Sub MainWindow_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        My.Application.Shutdown()
    End Sub

    Private Sub AvalonText_TextChanged(sender As Object, e As EventArgs)
        CefSharp.WebBrowserExtensions.LoadHtml(Me.Browser, DirectCast(sender, ICSharpCode.AvalonEdit.TextEditor).Document.Text, "https://www.birdys-leinentraum.at/")
        'Me.Browser.GetBrowser.MainFrame.LoadStringForUrl(DirectCast(sender, ICSharpCode.AvalonEdit.TextEditor).Document.Text, "localhost")  'NavigateToString(DirectCast(sender, ICSharpCode.AvalonEdit.TextEditor).Document.Text)

    End Sub
End Class
