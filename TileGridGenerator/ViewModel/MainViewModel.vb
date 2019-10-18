Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Text
Imports TileGridGenerator.ViewModel.Command
Imports TileGridGenerator.ViewModel.Services

Namespace ViewModel
    Public Class MainViewModel
        Inherits ViewModelBase


        Public Sub New()
            OpenedFiles = New ObservableCollection(Of TileGridFileViewModel)

            LastFiles = New List(Of String)
            LastFiles = InstanceHolder.Instance.Settings.LastUsedFiles
        End Sub


        Private _openedFiles As ObservableCollection(Of TileGridFileViewModel)
        Public Property OpenedFiles As ObservableCollection(Of TileGridFileViewModel)
            Get
                Return _openedFiles
            End Get
            Set(ByVal value As ObservableCollection(Of TileGridFileViewModel))
                _openedFiles = value
                RaisePropertyChanged()
            End Set
        End Property


        Private _lastFiles As List(Of String)
        Public Property LastFiles As List(Of String)
            Get
                Return _lastFiles
            End Get
            Set(ByVal value As List(Of String))
                _lastFiles = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _selectedFile As TileGridFileViewModel
        Public Property SelectedFile As TileGridFileViewModel
            Get
                Return _selectedFile
            End Get
            Set(ByVal value As TileGridFileViewModel)
                _selectedFile = value
                RaisePropertyChanged()
                RefreshEditor(value)
            End Set
        End Property


        Private _currentEditorText As String
        Public Property CurrentEditorText As String
            Get
                Return _currentEditorText
            End Get
            Set(ByVal value As String)
                _currentEditorText = value
                RaisePropertyChanged()
            End Set
        End Property


        Private _newFileCommand As ICommand
        Public ReadOnly Property NewFileCommand As ICommand
            Get
                If _newFileCommand Is Nothing Then _
                    _newFileCommand = New RelayCommand(AddressOf NewFileCommand_Execute)
                Return _newFileCommand
            End Get
        End Property
        Private Sub NewFileCommand_Execute(ByVal obj As Object)
            Dim p2 As New TileGridFileViewModel()
            OpenedFiles.Add(p2)

            AddHandler p2.WillClose, AddressOf CloseTab
            AddHandler p2.ValueChanged, AddressOf RefreshEditor
            If OpenedFiles.Count > 0 Then SelectedFile = OpenedFiles.LastOrDefault
        End Sub
        Private Sub RefreshEditor(ByVal obj As TileGridFileViewModel)
            Debug.WriteLine("Refresh")
            If SelectedFile Is Nothing Then
                CurrentEditorText = "Kein Parameter definiert. Bitte lade eine Datei oder erstelle eine neue und füge Fotos von einem FlickR Album hinzu."
                Exit Sub
            End If
            Dim cssTemplateText = IO.File.ReadAllText($"{My.Application.Info.DirectoryPath}\Templates\tileStyle_Template.css")

            'Alle Platzhalten im CSS Text mit Werten ersetzen
            Dim cleanedCssText As String = ReplaceCssTemplatePlaceholder(cssTemplateText)
            Dim htmlText As String = GenerateHtmlText()

            CurrentEditorText = $"<Style>{Environment.NewLine}{cleanedCssText}</Style>{Environment.NewLine}{Environment.NewLine}{htmlText}"
        End Sub



        Private _loadFileCommand As ICommand
        Public ReadOnly Property LoadFileCommand As ICommand
            Get
                If _loadFileCommand Is Nothing Then _
                    _loadFileCommand = New RelayCommand(AddressOf LoadFileCommand_Execute)
                Return _loadFileCommand
            End Get
        End Property
        Private Sub LoadFileCommand_Execute(ByVal obj As Object)
            Dim ofdServ = ServiceContainer.GetService(Of IOpenFileDialogService)

            If ofdServ.ShowDialog("TileGridGenerator-Datei laden", filter:="TileGridGenerator Dateien (*.tgf)|*.tgf|Alle Dateien (*.*)|*.*", restoreDir:=True, multiSelect:=True) Then
                For Each f In ofdServ.OpenedFiles
                    LoadFile(f)
                Next

            End If

        End Sub

        Private _saveSelectedFileCommand As ICommand
        Public ReadOnly Property SaveSelectedFileCommand As ICommand
            Get
                If _saveSelectedFileCommand Is Nothing Then _
                    _saveSelectedFileCommand = New RelayCommand(AddressOf SaveSelectedFileCommand_Execute, AddressOf SaveSelectedFileCommand_CanExecute)
                Return _saveSelectedFileCommand
            End Get
        End Property
        Private Function SaveSelectedFileCommand_CanExecute(ByVal obj As Object) As Boolean
            Return SelectedFile IsNot Nothing AndAlso SelectedFile.IsEdited
        End Function
        Private Sub SaveSelectedFileCommand_Execute(ByVal obj As Object)
            SelectedFile.Save()
        End Sub


        Private _saveAllFilesCommand As ICommand
        Public ReadOnly Property SaveAllFilesCommand As ICommand
            Get
                If _saveAllFilesCommand Is Nothing Then _
                    _saveAllFilesCommand = New RelayCommand(AddressOf SaveAllFilesCommand_Execute, AddressOf SaveAllFilesCommand_CanExecute)
                Return _saveAllFilesCommand
            End Get
        End Property
        Private Function SaveAllFilesCommand_CanExecute(ByVal obj As Object) As Boolean
            Return OpenedFiles IsNot Nothing AndAlso OpenedFiles.Count > 0
        End Function
        Private Sub SaveAllFilesCommand_Execute(ByVal obj As Object)
            For Each f In OpenedFiles
                f.Save()
            Next
        End Sub

        Private _openRecentFileCommand As ICommand
        Public ReadOnly Property OpenRecentFileCommand As ICommand
            Get
                If _openRecentFileCommand Is Nothing Then _
                    _openRecentFileCommand = New RelayCommand(AddressOf OpenRecentFileCommand_Execute)
                Return _openRecentFileCommand
            End Get
        End Property
        Private Sub OpenRecentFileCommand_Execute(ByVal obj As Object)
            If obj IsNot Nothing Then
                LoadFile(obj.ToString)
            End If
        End Sub

        Friend Sub LoadFile(path As String)
            Using objStreamReader As New StreamReader(path)
                Dim p2 As New TileGridFileViewModel
                Dim x As New Xml.Serialization.XmlSerializer(GetType(TileGridFileViewModel))
                p2 = x.Deserialize(objStreamReader)
                objStreamReader.Close()
                p2.Filepath = path
                If p2.PhotoItems IsNot Nothing Then
                    For Each item In p2.PhotoItems
                        AddHandler item.ValueChanged, Sub() RefreshEditor(Nothing)
                    Next
                End If

                OpenedFiles.Add(p2)
                p2.IsSaved = True
                AddHandler p2.WillClose, AddressOf CloseTab
                AddHandler p2.ValueChanged, AddressOf RefreshEditor
            End Using
            If IO.File.Exists(path) Then
                InstanceHolder.Instance.Settings.AddLastUsedFile(path)
                InstanceHolder.Instance.Settings.Save()
                LastFiles = InstanceHolder.Instance.Settings.LastUsedFiles
            End If
            If OpenedFiles.Count > 0 Then SelectedFile = OpenedFiles.LastOrDefault
        End Sub


        Private Sub CloseTab(tab As TileGridFileViewModel)
            RemoveHandler tab.WillClose, AddressOf CloseTab
            RemoveHandler tab.ValueChanged, AddressOf RefreshEditor
            OpenedFiles.Remove(tab)
            tab.Dispose()
            If SelectedFile Is Nothing Then SelectedFile = OpenedFiles.FirstOrDefault
        End Sub

        Private Function ReplaceCssTemplatePlaceholder(ByVal cssTemplateText As String) As String
            cssTemplateText = cssTemplateText.Replace("$BorderWidth$", SelectedFile.BorderWidth)
            cssTemplateText = cssTemplateText.Replace("$BorderColor$", System.Drawing.ColorTranslator.ToHtml(GetColorValue(New SolidColorBrush(ColorConverter.ConvertFromString(SelectedFile.BorderColor)))))
            cssTemplateText = cssTemplateText.Replace("$Background$", System.Drawing.ColorTranslator.ToHtml(GetColorValue(New SolidColorBrush(ColorConverter.ConvertFromString(SelectedFile.Background)))))
            cssTemplateText = cssTemplateText.Replace("$AnimationTransform$", SelectedFile.ZoomAnimationDuration)
            cssTemplateText = cssTemplateText.Replace("$ZoomHover$", Replace(SelectedFile.ZoomArea.ToString, ",", "."))
            cssTemplateText = cssTemplateText.Replace("$TitleFontSize$", SelectedFile.TitleFontSize)
            cssTemplateText = cssTemplateText.Replace("$DescriptionFontsize$", SelectedFile.DescriptionFontSize)
            cssTemplateText = cssTemplateText.Replace("$FontFamily$", SelectedFile.FontFamily)
            cssTemplateText = cssTemplateText.Replace("$FontWeightTitle$", If(SelectedFile.TitleBold, "bold", "normal"))
            cssTemplateText = cssTemplateText.Replace("$FontWeightDescription$", If(SelectedFile.DescriptionBold, "bold", "normal"))

            Return cssTemplateText
        End Function

        Private _terminateAppCommand As ICommand
        Public ReadOnly Property TerminateAppCommand As ICommand
            Get
                If _terminateAppCommand Is Nothing Then _
                    _terminateAppCommand = New RelayCommand(Sub() My.Application.Shutdown())
                Return _terminateAppCommand
            End Get
        End Property

        Private Function GetColorValue(br As SolidColorBrush) As System.Drawing.Color
            Return System.Drawing.Color.FromArgb(br.Color.A, br.Color.R, br.Color.G, br.Color.B)
        End Function

        Private Function GenerateHtmlText() As String
            Dim htmlStringBuilder As New StringBuilder
            For r As Integer = 0 To 0 ' SelectedFile.PhotoItems.Count Mod 4
                htmlStringBuilder.AppendFormat("<div class=""{0}"">", "tileRow") : IndentCursor(htmlStringBuilder, True, 2)

                For c As Integer = 0 To SelectedFile.Columns - 1
                    htmlStringBuilder.AppendFormat("<div class=""{0}"">", "tileColumn") : IndentCursor(htmlStringBuilder, True, 4)

                    Dim cRow = r : Dim cColumn = c
                    Dim elementsToInsert = SelectedFile.PhotoItems.Where(Function(p) p.Column - 1 = cColumn).ToList
                    For Each e In elementsToInsert

                        If e.UseZoomingEffect Then htmlStringBuilder.AppendFormat("<div class=""{0}"">", "tileZoom") : IndentCursor(htmlStringBuilder, True, 6)
                        htmlStringBuilder.AppendFormat("<div class=""{0}"">", "tileContainer") : IndentCursor(htmlStringBuilder, True, 8)
                        htmlStringBuilder.AppendFormat("<img src=""{0}""/>", e.SmallPhotoUrl) : IndentCursor(htmlStringBuilder, True, 10)

                        If Not String.IsNullOrEmpty(e.TitleText) Or Not String.IsNullOrEmpty(e.DescriptionText) Then
                            htmlStringBuilder.AppendFormat("<div class=""{0}"">", GetTextPositionTagBySelectedIndex(e.TextPosition)) : IndentCursor(htmlStringBuilder, True, 10)

                            If Not String.IsNullOrEmpty(e.TitleText) Then
                                htmlStringBuilder.AppendFormat("<div class=""{0}"">", "normalFont")
                                htmlStringBuilder.Append(e.TitleText)
                                htmlStringBuilder.Append("</div>")

                            End If

                            If Not String.IsNullOrEmpty(e.DescriptionText) Then
                                htmlStringBuilder.AppendFormat("<div class=""{0}"">", "minifont")
                                htmlStringBuilder.Append(e.DescriptionText)
                                htmlStringBuilder.Append("</div>")

                            End If

                            IndentCursor(htmlStringBuilder, True, 10)
                            htmlStringBuilder.Append("</div>")
                        End If

                        IndentCursor(htmlStringBuilder, True, 8)
                        htmlStringBuilder.Append("</div>") : IndentCursor(htmlStringBuilder, True, 6)
                        If e.UseZoomingEffect Then htmlStringBuilder.Append("</div>")

                    Next

                    IndentCursor(htmlStringBuilder, True, 4)
                    htmlStringBuilder.Append("</div>")
                Next
                IndentCursor(htmlStringBuilder, True, 2)
                htmlStringBuilder.Append("</div>")
            Next
            Return htmlStringBuilder.ToString
        End Function

        Private Function GetTextPositionTagBySelectedIndex(ByVal textPosition As Integer) As String
            Select Case textPosition
                Case 0
                    Return "top-left"
                Case 1
                    Return "top-right"
                Case 2
                    Return "bottom-left"
                Case 3
                    Return "bottom-right"
                Case 4
                    Return "centered"
                Case Else
                    Return "centered"
            End Select
        End Function
        Private Sub IndentCursor(ByRef sb As StringBuilder, ByVal newLineBeforeIndent As Boolean, ByVal indentCharCount As Integer)
            If newLineBeforeIndent Then sb.Append(Environment.NewLine)
            For i As Integer = 0 To indentCharCount - 1
                sb.Append(" ")
            Next
        End Sub
    End Class
End Namespace
