Imports System.Collections.ObjectModel
Imports FlickrNet
Imports TileGridGenerator.ViewModel.Command
Imports TileGridGenerator.ViewModel.Services

Namespace ViewModel
    Public Class AlbumSelectorViewModel
        Inherits ViewModelBase


        Public Sub New()
            AlbumList = New ObservableCollection(Of AlbumViewModel)
            GetAlbumList()
        End Sub


        Friend Sub GetAlbumList()
            Dim f As Flickr = FlickrAuth.GetAuthInstance()

            f.PhotosetsGetListAsync(AddressOf AddAlbumToList)
        End Sub
        Private Sub AddAlbumToList(ByVal obj As FlickrResult(Of PhotosetCollection))
            For Each a In obj.Result
                AlbumList.Add(New AlbumViewModel(a))
            Next
        End Sub

        Public ReadOnly Property AlbumList As ObservableCollection(Of AlbumViewModel)


        Dim _selectedAlbum As AlbumViewModel
        Property SelectedAlbum As AlbumViewModel
            Get
                Return _selectedAlbum
            End Get
            Set
                _selectedAlbum = Value
                If SelectedAlbum IsNot Nothing Then _selectedAlbum.LoadPhotos()
                RaisePropertyChanged()
            End Set
        End Property


        Private _abortCommand As ICommand
        Public ReadOnly Property AbortCommand As ICommand
            Get
                If _abortCommand Is Nothing Then _
                    _abortCommand = New RelayCommand(AddressOf AbortCommand_Execute)
                Return _abortCommand
            End Get
        End Property
        Private Sub AbortCommand_Execute(ByVal obj As Object)
            SelectedAlbum = Nothing
            Dim diaServ = ServiceContainer.GetService(Of IDialogWindowService)
            If diaServ IsNot Nothing Then diaServ.CloseDialog(Me)
        End Sub

        Private _acceptCommand As ICommand
        Public ReadOnly Property AcceptCommand As ICommand
            Get
                If _acceptCommand Is Nothing Then _
                    _acceptCommand = New RelayCommand(AddressOf AcceptCommand_Execute, AddressOf AcceptCommand_CanExecute)
                Return _acceptCommand
            End Get
        End Property
        Private Function AcceptCommand_CanExecute(ByVal obj As Object) As Boolean
            Return SelectedAlbum IsNot Nothing
        End Function
        Private Sub AcceptCommand_Execute(ByVal obj As Object)
            Dim diaServ = ServiceContainer.GetService(Of IDialogWindowService)
            If diaServ IsNot Nothing Then diaServ.CloseDialog(Me)
        End Sub


    End Class

End Namespace
