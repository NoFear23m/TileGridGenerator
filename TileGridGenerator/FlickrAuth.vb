Imports FlickrNet

Public Class FlickrAuth

    Public Shared ApiKey As String = InstanceHolder.Instance.Settings.API_Key
    Public Shared SharedSecret As String = "236dc18892ef24ca"

    Public Shared Function GetInstance() As Flickr
        Return New Flickr(ApiKey, SharedSecret)
    End Function

    Public Shared Function GetAuthInstance() As Flickr
        Dim f = New Flickr(ApiKey, SharedSecret)
        f.OAuthAccessToken = OAuthToken.Token
        f.OAuthAccessTokenSecret = OAuthToken.TokenSecret
        Return f
    End Function

    Public Shared Property OAuthToken As OAuthAccessToken
        Get
            Return InstanceHolder.Instance.Settings.OAuthToken
        End Get
        Set
            InstanceHolder.Instance.Settings.OAuthToken = Value
            InstanceHolder.Instance.Settings.Save()
        End Set
    End Property
End Class
