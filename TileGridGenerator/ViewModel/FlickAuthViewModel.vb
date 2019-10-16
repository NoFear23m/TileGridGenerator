Imports FlickrNet
Imports TileGridGenerator.ViewModel
Imports TileGridGenerator.ViewModel.Command
Imports TileGridGenerator.ViewModel.Services

Namespace ViewModel
    Public Class FlickAuthViewModel
        Inherits ViewModelBase


        Private requestToken As OAuthRequestToken

        Public Sub New()
            Dim f As Flickr = FlickrAuth.GetInstance()
            requestToken = f.OAuthGetRequestToken("oob")
            Dim url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write)

            Process.Start(url)
        End Sub


        Private _authPassCode As String
        Public Property AuthPassCode As String
            Get
                Return _authPassCode
            End Get
            Set(ByVal value As String)
                _authPassCode = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _result As String = "Vorgang noch nicht abgeschlossen..."
        Public Property Result As String
            Get
                Return _result
            End Get
            Set(ByVal value As String)
                _result = value
                RaisePropertyChanged()
            End Set
        End Property



        Private _tryAuthCommand As ICommand
        Public ReadOnly Property TryAuthCommand As ICommand
            Get
                If _tryAuthCommand Is Nothing Then _
                    _tryAuthCommand = New RelayCommand(AddressOf TryAuthCommand_Execute, AddressOf TryAuthCommand_CanExecute)
                Return _tryAuthCommand
            End Get
        End Property

        Private Function TryAuthCommand_CanExecute(ByVal obj As Object) As Boolean
            Return Not String.IsNullOrEmpty(AuthPassCode)
        End Function
        Private Sub TryAuthCommand_Execute(ByVal obj As Object)
            Dim f As Flickr = FlickrAuth.GetInstance()
            Try
                Dim accessToken = f.OAuthGetAccessToken(requestToken, AuthPassCode)
                FlickrAuth.OAuthToken = accessToken
                Result = ("Successfully authenticated as " + accessToken.FullName)
            Catch ex As FlickrApiException
                Result = ex.Message
                MessageBox.Show(("Failed to get access token. Error message: " + ex.Message))
            End Try
        End Sub

        Private _closeWindowCommand As ICommand
        Public ReadOnly Property CloseWindowCommand As ICommand
            Get
                If _closeWindowCommand Is Nothing Then _
                    _closeWindowCommand = New RelayCommand(AddressOf CloseWindowCommand_Execute)
                Return _closeWindowCommand
            End Get
        End Property
        Private Sub CloseWindowCommand_Execute(ByVal obj As Object)
            Dim winServ = ServiceContainer.GetService(Of IDialogWindowService)
            winServ.CloseDialog(Me)
        End Sub


    End Class
End Namespace
