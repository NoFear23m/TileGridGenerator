Imports ICSharpCode.AvalonEdit

Namespace View.Extensions
    Public Class AvalonExtension
        Inherits DependencyObject
        Public Shared Function GetAvalonText(ByVal element As DependencyObject) As String
            If element Is Nothing Then
                Throw New ArgumentNullException("element")
            End If
            Return CType(element.GetValue(AvalonTextProperty), String)
        End Function
        Public Shared Sub SetAvalonText(ByVal element As DependencyObject, ByVal value As String)
            If element Is Nothing Then
                Throw New ArgumentNullException("element")
            End If
            element.SetValue(AvalonTextProperty, value)
        End Sub
        Public Shared ReadOnly AvalonTextProperty As _
                               DependencyProperty = DependencyProperty.RegisterAttached("AvalonText",
                               GetType(String), GetType(AvalonExtension),
                               New PropertyMetadata("", AddressOf AvalonTextProprty_Changed))
        Private Shared Sub AvalonTextProprty_Changed(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim textEditor As TextEditor = CType(d, TextEditor)
            'RemoveHandler textEditor.TextChanged, AddressOf AvalonText_Changed
            'AddHandler textEditor.TextChanged, AddressOf AvalonText_Changed
            If e.NewValue IsNot Nothing Then
                textEditor.Document.Text = e.NewValue.ToString
            Else
                textEditor.Document.Text = ""
            End If

        End Sub
        Private Shared Sub AvalonText_Changed(ByVal sender As Object, ByVal e As EventArgs)
            Dim av = DirectCast(sender, TextEditor)
            av.SetValue(AvalonTextProperty, av.Document.Text)
        End Sub

    End Class
End Namespace
