Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Xml.Serialization
Imports FlickrNet
Imports System.Linq
Imports TileGridGenerator.ViewModel
Imports TileGridGenerator.ViewModel.Command
Imports TileGridGenerator.ViewModel.Services

Namespace ViewModel
    <Serializable()>
    Public Class TileGridFileViewModel
        Inherits ViewModelBase

        Public Event WillClose(tab As TileGridFileViewModel)
        Public Event ValueChanged(obj As TileGridFileViewModel)

        Public Sub New()
            Title = "Unbenannt"
            PhotoItems = New ObservableCollection(Of TileItemViewModel)
        End Sub

        Public Sub New(filePath As String)
            Me.Filepath = filePath
            PhotoItems = New ObservableCollection(Of TileItemViewModel)
            Title = IO.Path.GetFileNameWithoutExtension(filePath)
            IsSaved = True
        End Sub



        Private _title As String
        Public Property Title As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _borderColor As String = "Black"
        Public Property BorderColor As String
            Get
                Return _borderColor
            End Get
            Set(ByVal value As String)
                _borderColor = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property



        Private _background As String = "Black"
        Public Property Background As String
            Get
                Return _background
            End Get
            Set(ByVal value As String)
                _background = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _zoomAnimationDuration As Double = 200
        Public Property ZoomAnimationDuration As Double
            Get
                Return _zoomAnimationDuration
            End Get
            Set(ByVal value As Double)
                _zoomAnimationDuration = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _zoomArea As Double = 1.05
        Public Property ZoomArea As Double
            Get
                Return _zoomArea
            End Get
            Set(ByVal value As Double)
                _zoomArea = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _borderWidth As Integer = 5
        Public Property BorderWidth As Integer
            Get
                Return _borderWidth
            End Get
            Set(ByVal value As Integer)
                _borderWidth = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _titleFontSize As Integer = 12
        Public Property TitleFontSize As Integer
            Get
                Return _titleFontSize
            End Get
            Set(ByVal value As Integer)
                _titleFontSize = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _descriptionFontSize As Integer = 18
        Public Property DescriptionFontSize As Integer
            Get
                Return _descriptionFontSize
            End Get
            Set(ByVal value As Integer)
                _descriptionFontSize = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _fontColor As String = "White"
        Public Property FontColor As String
            Get
                Return _fontColor
            End Get
            Set(ByVal value As String)
                _fontColor = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _fontFamily As String = "Arial"
        Public Property FontFamily As String
            Get
                Return _fontFamily
            End Get
            Set(ByVal value As String)
                _fontFamily = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _titleBold As Boolean
        Public Property TitleBold As Boolean
            Get
                Return _titleBold
            End Get
            Set(ByVal value As Boolean)
                _titleBold = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        Private _descriptionBold As Boolean
        Public Property DescriptionBold As Boolean
            Get
                Return _descriptionBold
            End Get
            Set(ByVal value As Boolean)
                _descriptionBold = value
                RaiseEvent ValueChanged(Me)
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property


        <NonSerialized()>
        Private _filepath As String
        <XmlIgnore()>
        Public Property Filepath As String
            Get
                Return _filepath
            End Get
            Set(ByVal value As String)
                _filepath = value
                RaisePropertyChanged()
            End Set
        End Property

        <NonSerialized()>
        Private _isSaved As Boolean
        <XmlIgnore()>
        Public Property IsSaved As Boolean
            Get
                Return _isSaved
            End Get
            Friend Set(ByVal value As Boolean)
                _isSaved = value
                RaisePropertyChanged()
                RaisePropertyChanged(NameOf(IsEdited))
            End Set
        End Property

        Public ReadOnly Property IsEdited As Boolean
            Get
                Return Not IsSaved
            End Get
        End Property


        Public ReadOnly Property Columns As Integer
            Get
                If PhotoItems.Count = 0 Then Return 1
                Return PhotoItems.Select(Function(s) s.Column).Max
            End Get
        End Property


        Private _lastPhotoSetId As String
        Public Property LastPhotoSetId As String
            Get
                Return _lastPhotoSetId
            End Get
            Set(ByVal value As String)
                _lastPhotoSetId = value
                If value IsNot Nothing Then
                    Dim f As Flickr = FlickrAuth.GetAuthInstance()
                    f.PhotosetsGetPhotosAsync(LastPhotoSetId, AddressOf AddAlbumPhotosToList)
                End If
                IsSaved = False
                RaisePropertyChanged()
                RaisePropertyChanged(NameOf(HasPhotoSetId))
            End Set
        End Property

        <NonSerialized()>
        Private _lastPhotoset As FlickrNet.Photoset
        Public Property LastPhotoset As FlickrNet.Photoset
            Get
                Return _lastPhotoset
            End Get
            Set(ByVal value As FlickrNet.Photoset)
                _lastPhotoset = value
                RaisePropertyChanged(NameOf(HasPhotoSetId))
                RaisePropertyChanged()
            End Set
        End Property

        Public ReadOnly Property HasPhotoSetId As Boolean
            Get
                Return LastPhotoSetId IsNot Nothing
            End Get
        End Property



        <NonSerialized()>
        Private _aviablePhotos As ObservableCollection(Of FlickrNet.Photo)
        <XmlIgnore()>
        Public Property AviablePhotos As ObservableCollection(Of FlickrNet.Photo)
            Get
                Return _aviablePhotos
            End Get
            Set(ByVal value As ObservableCollection(Of FlickrNet.Photo))
                _aviablePhotos = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _photoItems As ObservableCollection(Of TileItemViewModel)
        Public Property PhotoItems As ObservableCollection(Of TileItemViewModel)
            Get
                Return _photoItems
            End Get
            Set(ByVal value As ObservableCollection(Of TileItemViewModel))
                _photoItems = value
                IsSaved = False
                RaisePropertyChanged()
            End Set
        End Property

        <NonSerialized()>
        Private _selectedPhotoItem As TileItemViewModel
        <XmlIgnore()>
        Public Property SelectedPhotoItem As TileItemViewModel
            Get
                Return _selectedPhotoItem
            End Get
            Set(ByVal value As TileItemViewModel)
                _selectedPhotoItem = value
                RaisePropertyChanged()
            End Set
        End Property


        Private _addTileItem As ICommand
        Public ReadOnly Property AddTileItem As ICommand
            Get
                If _addTileItem Is Nothing Then _
                    _addTileItem = New RelayCommand(AddressOf AddTileItem_Execute)
                Return _addTileItem
            End Get
        End Property
        Private Sub AddTileItem_Execute(ByVal obj As Object)
            PhotoItems.Add(New TileItemViewModel With {.CurrentPhoto = AviablePhotos.FirstOrDefault, .AviablePhotos = Me.AviablePhotos})
            AddHandler PhotoItems.Last.ValueChanged, Sub() RaiseEvent ValueChanged(Me)
            RaiseEvent ValueChanged(Me)
        End Sub

        Private _removeTileItem As ICommand
        Public ReadOnly Property RemoveTileItem As ICommand
            Get
                If _removeTileItem Is Nothing Then _
                    _removeTileItem = New RelayCommand(AddressOf RemoveTileItem_Execute, AddressOf RemoveTileItem_CanExecute)
                Return _removeTileItem
            End Get
        End Property
        Private Function RemoveTileItem_CanExecute(ByVal obj As Object) As Boolean
            Return SelectedPhotoItem IsNot Nothing
        End Function
        Private Sub RemoveTileItem_Execute(ByVal obj As Object)
            PhotoItems.Remove(SelectedPhotoItem)
            RaiseEvent ValueChanged(Me)
        End Sub

        Private _searchAlbumCommand As ICommand
        Public ReadOnly Property SearchAlbumCommand As ICommand
            Get
                If _searchAlbumCommand Is Nothing Then _
                    _searchAlbumCommand = New RelayCommand(AddressOf SearchAlbumCommand_Execute)
                Return _searchAlbumCommand
            End Get
        End Property
        Private Sub SearchAlbumCommand_Execute(ByVal obj As Object)
            Dim diaServ = ServiceContainer.GetService(Of IDialogWindowService)
            Dim vm As New AlbumSelectorViewModel()

            diaServ.ShowModalDialog("searchAlbum", vm, Me, True, True)
            If vm.SelectedAlbum IsNot Nothing Then
                LastPhotoSetId = vm.SelectedAlbum.PhotoSetId
            End If
        End Sub
        Private Sub AddAlbumPhotosToList(ByVal obj As FlickrResult(Of PhotosetPhotoCollection))
            AviablePhotos = New ObservableCollection(Of Photo)
            For Each p In obj.Result
                AviablePhotos.Add(p)
            Next
            For Each item In PhotoItems
                item.AviablePhotos = AviablePhotos
            Next
        End Sub

        Private _closeFile As ICommand
        Public ReadOnly Property CloseFile As ICommand
            Get
                If _closeFile Is Nothing Then _
                    _closeFile = New RelayCommand(AddressOf CloseFile_Execute)
                Return _closeFile
            End Get
        End Property
        Private Sub CloseFile_Execute(ByVal obj As Object)
            If Not IsSaved Then
                Dim msgServ = ServiceContainer.GetService(Of IMessageboxService)

                Select Case msgServ.Show($"Datei '{Title}' ist nicht gespeichert. Willst du die Datei speichern?", "Speichern?", EnuMessageBoxButton.YesNoCancel, EnuMessageBoxImage.Question)
                    Case EnuMessageBoxResult.Yes
                        If String.IsNullOrEmpty(Filepath) Then

                            Dim fbd = ServiceContainer.GetService(Of ISaveFileDialogService)


                            If fbd.ShowDialog("Datei speichern", filter:="TileGridGenerator Dateien (*.tgf)|*.tgf|Alle Dateien (*.*)|*.*") Then
                                Filepath = fbd.FileName
                                Save()
                                IsSaved = True
                                RaiseEvent WillClose(Me)
                            Else
                                Exit Sub
                            End If
                        Else
                            Save()
                            IsSaved = True
                            RaiseEvent WillClose(Me)
                        End If
                    Case EnuMessageBoxResult.No
                        RaiseEvent WillClose(Me)
                    Case EnuMessageBoxResult.Cancel

                End Select
            Else
                RaiseEvent WillClose(Me)
            End If
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)

            For Each item In PhotoItems
                item.Dispose()
            Next
            PhotoItems.Clear()
        End Sub


        Friend Sub Save()
            Dim xmlSerializer As New XmlSerializer(Me.GetType)
            Using string_writer As New StreamWriter(Me.Filepath)
                xmlSerializer.Serialize(string_writer, Me)
                string_writer.Close()
                IsSaved = True
            End Using

            InstanceHolder.Instance.Settings.AddLastUsedFile(Filepath)
        End Sub



    End Class
End Namespace
