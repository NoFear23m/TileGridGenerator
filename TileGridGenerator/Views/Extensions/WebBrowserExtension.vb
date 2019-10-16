Namespace View.Extensions
    Public Class WebBrowserExtension

        Public Shared BodyProperty As DependencyProperty = DependencyProperty.RegisterAttached("Body", GetType(System.String), GetType(WebBrowserExtension), New PropertyMetadata(AddressOf OnBodyChanged))

        Public Shared Function GetBody(ByVal dependencyObject As DependencyObject) As String
            Return CType(dependencyObject.GetValue(BodyProperty), String)
        End Function

        Public Shared Sub SetBody(ByVal dependencyObject As DependencyObject, ByVal body As String)
            dependencyObject.SetValue(BodyProperty, body)
        End Sub

        Private Shared Sub OnBodyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim webBrowser = CType(d, WebBrowser)
            webBrowser.NavigateToString(CType(e.NewValue, String))
        End Sub
    End Class
End Namespace
