Imports System.Collections.ObjectModel
Imports TileGridGenerator.ViewModel
Imports FlickrNet

Namespace ViewModel
    Public Class AlbumViewModel
        Inherits ViewModelBase

        Private ReadOnly _model As Photoset

        Public Sub New()
            Me.New(New Photoset)
        End Sub

        Public Sub New(flickrModel As Photoset)
            _model = flickrModel
            Photos = New ObservableCollection(Of Photo)
        End Sub

        Public ReadOnly Property PhotoSetId As String
            Get
                Return _model.PhotosetId
            End Get
        End Property

        Public ReadOnly Property Title As String
            Get
                Return _model.Title
            End Get
        End Property

        Public ReadOnly Property ThumbnailUrl As String
            Get
                Return _model.PhotosetThumbnailUrl
            End Get
        End Property


        Public ReadOnly Property SquareThumbnailUrl As String
            Get
                Return _model.PhotosetSquareThumbnailUrl
            End Get
        End Property


        Friend Function LoadPhotos() As Boolean
            Dim f As Flickr = FlickrAuth.GetAuthInstance()

            f.PhotosetsGetPhotosAsync(PhotoSetId, AddressOf AddPhotosToList)
            Return True

        End Function
        Private Sub AddPhotosToList(ByVal obj As FlickrResult(Of PhotosetPhotoCollection))
            For Each p In obj.Result
                Photos.Add(p)
            Next
        End Sub

        Public Property Photos As ObservableCollection(Of FlickrNet.Photo)

    End Class
End Namespace
