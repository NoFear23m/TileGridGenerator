Imports FlickrNet

Public Class AuthWindow

    Private requestToken As OAuthRequestToken

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim f As Flickr = FlickrAuth.GetInstance()
        Try
            Dim accessToken = f.OAuthGetAccessToken(requestToken, AutCode.Text)
            FlickrAuth.OAuthToken = accessToken
            txtResult.Text = ("Successfully authenticated as " + accessToken.FullName)
        Catch ex As FlickrApiException
            MessageBox.Show(("Failed to get access token. Error message: " + ex.Message))
        End Try
    End Sub

    Private Sub AuthWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim f As Flickr = FlickrAuth.GetInstance()
        requestToken = f.OAuthGetRequestToken("oob")
        Dim url = f.OAuthCalculateAuthorizationUrl(requestToken.Token, AuthLevel.Write)

        Process.Start(url)
    End Sub

    Private Sub TxtAllOk_Click(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = FlickrAuth.OAuthToken IsNot Nothing
    End Sub
End Class
