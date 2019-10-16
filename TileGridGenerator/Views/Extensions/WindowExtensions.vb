Namespace View.Extensions
    Public Class WindowExtension



        Public Shared Function GetMaxWidth(obj As DependencyObject) As Nullable(Of Double)
            Return DirectCast(obj.GetValue(MaxWidthProperty), Nullable(Of Double))
        End Function
        Public Shared Sub SetMaxWidth(obj As DependencyObject, value As Nullable(Of Double))
            obj.SetValue(MaxWidthProperty, value)
        End Sub
        Public Shared ReadOnly MaxWidthProperty As DependencyProperty = DependencyProperty.RegisterAttached("MaxWidth",
                                                                                                                    GetType(Nullable(Of Double)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.MaxWidth on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).MaxWidth = CDbl(GetMaxWidth(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})


        Public Shared Function GetMaxHeight(obj As DependencyObject) As Nullable(Of Double)
            Return DirectCast(obj.GetValue(MaxHeightProperty), Nullable(Of Double))
        End Function
        Public Shared Sub SetMaxHeight(obj As DependencyObject, value As Nullable(Of Double))
            obj.SetValue(MaxHeightProperty, value)
        End Sub
        Public Shared ReadOnly MaxHeightProperty As DependencyProperty = DependencyProperty.RegisterAttached("MaxHeight",
                                                                                                                    GetType(Nullable(Of Double)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.MaxHeight on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).MaxHeight = CDbl(GetMaxHeight(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})

        Public Shared Function GetMinWidth(obj As DependencyObject) As Nullable(Of Double)
            Return DirectCast(obj.GetValue(MinWidthProperty), Nullable(Of Double))
        End Function
        Public Shared Sub SetMinWidth(obj As DependencyObject, value As Nullable(Of Double))
            obj.SetValue(MinWidthProperty, value)
        End Sub
        Public Shared ReadOnly MinWidthProperty As DependencyProperty = DependencyProperty.RegisterAttached("MinWidth",
                                                                                                                    GetType(Nullable(Of Double)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.MinWidth on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).MinWidth = CDbl(GetMinWidth(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})



        Public Shared Function GetMinHeight(obj As DependencyObject) As Nullable(Of Double)
            Return DirectCast(obj.GetValue(MinHeightProperty), Nullable(Of Double))
        End Function
        Public Shared Sub SetMinHeight(obj As DependencyObject, value As Nullable(Of Double))
            obj.SetValue(MinHeightProperty, value)
        End Sub
        Public Shared ReadOnly MinHeightProperty As DependencyProperty = DependencyProperty.RegisterAttached("MinHeight",
                                                                                                                    GetType(Nullable(Of Double)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.MinHeight on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).MinHeight = CDbl(GetMinHeight(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})


        Public Shared Function GetSizeToContent(obj As DependencyObject) As Nullable(Of SizeToContent)
            Return DirectCast(obj.GetValue(SizeToContentProperty), Nullable(Of SizeToContent))
        End Function
        Public Shared Sub SetSizeToContent(obj As DependencyObject, value As Nullable(Of SizeToContent))
            obj.SetValue(SizeToContentProperty, value)
        End Sub
        Public Shared ReadOnly SizeToContentProperty As DependencyProperty = DependencyProperty.RegisterAttached("SizeToContent",
                                                                                                                    GetType(Nullable(Of SizeToContent)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.SizeToContent on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).SizeToContent = CType(GetSizeToContent(ctl), SizeToContent)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})


        Public Shared Function GetWindowStartupLocation(obj As DependencyObject) As Nullable(Of WindowStartupLocation)
            Return DirectCast(obj.GetValue(WindowStartupLocationProperty), Nullable(Of WindowStartupLocation))
        End Function
        Public Shared Sub SetWindowStartupLocation(obj As DependencyObject, value As Nullable(Of WindowStartupLocation))
            obj.SetValue(WindowStartupLocationProperty, value)
        End Sub
        Public Shared ReadOnly WindowStartupLocationProperty As DependencyProperty = DependencyProperty.RegisterAttached("WindowStartupLocation",
                                                                                                                    GetType(Nullable(Of WindowStartupLocation)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.WindowStartupLocation on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).WindowStartupLocation = CType(GetWindowStartupLocation(ctl), WindowStartupLocation)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})



        Public Shared Function GetResizeMode(obj As DependencyObject) As Nullable(Of ResizeMode)
            Return DirectCast(obj.GetValue(ResizeModeProperty), Nullable(Of ResizeMode))
        End Function
        Public Shared Sub SetResizeMode(obj As DependencyObject, value As Nullable(Of ResizeMode))
            obj.SetValue(ResizeModeProperty, value)
        End Sub
        Public Shared ReadOnly ResizeModeProperty As DependencyProperty = DependencyProperty.RegisterAttached("ResizeMode",
                                                                                                                    GetType(Nullable(Of ResizeMode)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.ResizeMode on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then Window.GetWindow(ctl).ResizeMode = CType(GetResizeMode(ctl), ResizeMode)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})






        Public Shared Function GetWindowStyle(obj As DependencyObject) As Nullable(Of WindowStyle)
            Return DirectCast(obj.GetValue(WindowStyleProperty), Nullable(Of WindowStyle))
        End Function
        Public Shared Sub SetWindowStyle(obj As DependencyObject, value As Nullable(Of WindowStyle))
            obj.SetValue(WindowStyleProperty, value)
        End Sub
        Public Shared ReadOnly WindowStyleProperty As DependencyProperty = DependencyProperty.RegisterAttached("WindowStyle",
                                                                                                                    GetType(Nullable(Of WindowStyle)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.WindowStyle on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then DirectCast(Window.GetWindow(ctl), SPSWindow).WindowStyle = CType(GetWindowStyle(ctl), WindowStyle)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})



        Public Shared Function GetCanCloseWithEsc(obj As DependencyObject) As Nullable(Of Boolean)
            Return DirectCast(obj.GetValue(CanCloseWithEscProperty), Nullable(Of Boolean))
        End Function
        Public Shared Sub SetCanCloseWithEsc(obj As DependencyObject, value As Nullable(Of Boolean))
            obj.SetValue(CanCloseWithEscProperty, value)
        End Sub
        Public Shared ReadOnly CanCloseWithEscProperty As DependencyProperty = DependencyProperty.RegisterAttached("CanCloseWithEsc",
                                                                                                                    GetType(Nullable(Of Boolean)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.CanCloseWithESC on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then DirectCast(Window.GetWindow(ctl), SPSWindow).CanCloseWithEsc = CBool(GetCanCloseWithEsc(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})

        Public Shared Function GetIcon(obj As DependencyObject) As ImageSource
            Return DirectCast(obj.GetValue(IconProperty), ImageSource)
        End Function
        Public Shared Sub SetIcon(obj As DependencyObject, value As ImageSource)
            obj.SetValue(IconProperty, value)
        End Sub
        Public Shared ReadOnly IconProperty As DependencyProperty = DependencyProperty.RegisterAttached("Icon",
                                                                                                                    GetType(ImageSource),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.Icon on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then DirectCast(Window.GetWindow(ctl), SPSWindow).Icon = GetIcon(ctl)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})



        Public Shared Function GetShowInTaskbar(obj As DependencyObject) As Nullable(Of Boolean)
            Return DirectCast(obj.GetValue(ShowInTaskbarProperty), Nullable(Of Boolean))
        End Function
        Public Shared Sub SetShowInTaskbar(obj As DependencyObject, value As Nullable(Of Boolean))
            obj.SetValue(ShowInTaskbarProperty, value)
        End Sub
        Public Shared ReadOnly ShowInTaskbarProperty As DependencyProperty = DependencyProperty.RegisterAttached("ShowInTaskbar",
                                                                                                                    GetType(Nullable(Of Boolean)),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.ShowInTaskbar on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then DirectCast(Window.GetWindow(ctl), SPSWindow).ShowInTaskbar = CBool(GetShowInTaskbar(ctl))
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})


        Public Shared Function GetWindowTitle(obj As DependencyObject) As String
            Return DirectCast(obj.GetValue(WindowTitleProperty), String)
        End Function
        Public Shared Sub SetWindowTitle(obj As DependencyObject, value As String)
            obj.SetValue(WindowTitleProperty, value)
        End Sub
        Public Shared ReadOnly WindowTitleProperty As DependencyProperty = DependencyProperty.RegisterAttached("WindowTitle",
                                                                                                                    GetType(String),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.WindowTitle on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then DirectCast(Window.GetWindow(ctl), SPSWindow).Title = GetWindowTitle(ctl)
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})


        Public Shared Function GetWindowState(obj As DependencyObject) As WindowState
            Return DirectCast(obj.GetValue(WindowStateProperty), WindowState)
        End Function
        Public Shared Sub SetWindowState(obj As DependencyObject, value As WindowState)
            obj.SetValue(WindowStateProperty, value)
        End Sub
        Public Shared ReadOnly WindowStateProperty As DependencyProperty = DependencyProperty.RegisterAttached("WindowState",
                                                                                                                    GetType(WindowState),
                                                                                                                    GetType(WindowExtension),
                                                                                                                    New UIPropertyMetadata() With {
                                                                                                                    .PropertyChangedCallback = Sub(obj, e)
                                                                                                                                                   Dim ctl = TryCast(obj, Control)
                                                                                                                                                   If ctl Is Nothing Then
                                                                                                                                                       Throw New InvalidOperationException("Can only use SPSWindowHelper.WindowState on a control")
                                                                                                                                                   End If
                                                                                                                                                   AddHandler ctl.Loaded, Sub(sender, e2)
                                                                                                                                                                              If Window.GetWindow(ctl) IsNot Nothing Then
                                                                                                                                                                                  Dim win = DirectCast(Window.GetWindow(ctl), SPSWindow)
                                                                                                                                                                                  If CInt(GetWindowState(ctl)) = 2 Then
                                                                                                                                                                                      win.SetwinState(True)
                                                                                                                                                                                  Else
                                                                                                                                                                                      win.SetwinState(False)
                                                                                                                                                                                  End If

                                                                                                                                                                              End If
                                                                                                                                                                          End Sub

                                                                                                                                               End Sub})

    End Class
End Namespace
