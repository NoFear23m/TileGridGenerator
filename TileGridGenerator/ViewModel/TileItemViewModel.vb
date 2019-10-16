
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization
Imports TileGridGenerator.ViewModel.Command
Imports TileGridGenerator.ViewModel.Services

Namespace ViewModel
    <Serializable()>
    Public Class TileItemViewModel
        Inherits ViewModelBase

        Public Event ValueChanged(obj As TileItemViewModel)

        Public Sub New()
            UseZoomingEffect = True
        End Sub

        <NonSerialized()>
        Private _aviablePhotos As ObservableCollection(Of FlickrNet.Photo)
        <XmlIgnore()>
        Public Property AviablePhotos As ObservableCollection(Of FlickrNet.Photo)
            Get
                Return _aviablePhotos
            End Get
            Set(ByVal value As ObservableCollection(Of FlickrNet.Photo))
                _aviablePhotos = value
                If value IsNot Nothing Then CurrentPhoto = AviablePhotos.FirstOrDefault(Function(p) p.SmallUrl = SmallPhotoUrl)
                RaisePropertyChanged()
            End Set
        End Property


        <NonSerialized()>
        Private _currentPhoto As FlickrNet.Photo
        <XmlIgnore()>
        Public Property CurrentPhoto As FlickrNet.Photo
            Get
                Return _currentPhoto
            End Get
            Set(ByVal value As FlickrNet.Photo)
                _currentPhoto = value
                SmallPhotoUrl = _currentPhoto?.SmallUrl
                RaisePropertyChanged()
            End Set
        End Property

        Private _smallPhotoUrl As String
        Public Property SmallPhotoUrl As String
            Get
                Return _smallPhotoUrl
            End Get
            Set(ByVal value As String)
                _smallPhotoUrl = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property

        Private _altText As String
        Public Property AltText As String
            Get
                Return _altText
            End Get
            Set(ByVal value As String)
                _altText = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property


        Private _column As Integer = 1
        Public Property Column As Integer
            Get
                Return _column
            End Get
            Set(ByVal value As Integer)
                _column = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property

        Private _useZoomingEffect As Boolean
        Public Property UseZoomingEffect As Boolean
            Get
                Return _useZoomingEffect
            End Get
            Set(ByVal value As Boolean)
                _useZoomingEffect = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property

        Private _titleText As String
        Public Property TitleText As String
            Get
                Return _titleText
            End Get
            Set(ByVal value As String)
                _titleText = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property

        Public ReadOnly Property PhotoWidth As Double
            Get
                Return CurrentPhoto.SmallWidth
            End Get
        End Property

        Public ReadOnly Property PhotoHeight As Double
            Get
                Return CurrentPhoto.SmallHeight
            End Get
        End Property

        Private _descriptionText As String
        Public Property DescriptionText As String
            Get
                Return _descriptionText
            End Get
            Set(ByVal value As String)
                _descriptionText = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property

        Private _textPosition As Integer = 4
        ''' <summary>
        ''' Position des Textes. 1 = Links oben ; 2 = rechts oben ; 3 = links unten ; 4 = rechts unten ; 5 = mittig
        ''' </summary>
        ''' <returns></returns>
        Public Property TextPosition As Integer
            Get
                Return _textPosition
            End Get
            Set(ByVal value As Integer)
                _textPosition = value
                RaiseEvent ValueChanged(Me)
                RaisePropertyChanged()
            End Set
        End Property


        Private _cropImageCommand As ICommand
        Public ReadOnly Property CropImageCommand As ICommand
            Get
                If _cropImageCommand Is Nothing Then _
                    _cropImageCommand = New RelayCommand(AddressOf CropImageCommand_Execute)
                Return _cropImageCommand
            End Get
        End Property
        Private Sub CropImageCommand_Execute(ByVal obj As Object)
            Dim winServ = ServiceContainer.GetService(Of IDialogWindowService)

        End Sub

    End Class
End Namespace
