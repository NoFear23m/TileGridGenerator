
Imports System.IO
Imports System.Xml.Serialization

<Serializable()>
Public Class SettingManager

    Dim settFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\TileGridGenerator\"
    Dim settFileName = "settings.xml"

    Public Sub New()
        Me.API_Key = "a912cd46b02b7f6cecd9c1b5c6957e46"
    End Sub

    Public Sub InitSettings()
        If Not IO.Directory.Exists($"{settFolderPath}") Then
            IO.Directory.CreateDirectory($"{settFolderPath}")
            If Not IO.File.Exists($"{settFolderPath}{settFileName}") Then Save()
        End If

        Using objStreamReader As New StreamReader($"{settFolderPath}{settFileName}")
            Dim p2 As New SettingManager
            Dim x As New Xml.Serialization.XmlSerializer(GetType(SettingManager))
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Me.LastUsedFiles = p2.LastUsedFiles
            Me.OAuthToken = p2.OAuthToken
            Me.API_Key = p2.API_Key
        End Using
        Save()
    End Sub

    Public Property API_Key As String
    Public Property OAuthToken As FlickrNet.OAuthAccessToken
    Public Property LastUsedFiles As List(Of String)

    Public Sub AddLastUsedFile(filePath As String)


        If LastUsedFiles Is Nothing Then
            LastUsedFiles = New List(Of String)
            LastUsedFiles.Add(filePath)
        Else
            If LastUsedFiles.Any(Function(x) x = filePath) Then
                LastUsedFiles.Remove(filePath)
                LastUsedFiles.Add(filePath)
                Do Until LastUsedFiles.Count <= 5
                    LastUsedFiles.RemoveAt(0)
                Loop
            End If
        End If
        Save()
    End Sub

    Public Sub Save()
        Dim xmlSerializer As New XmlSerializer(Me.GetType)
        Using string_writer As New StreamWriter($"{settFolderPath}{settFileName}")
            xmlSerializer.Serialize(string_writer, Me)
            string_writer.Close()
        End Using

    End Sub


End Class
