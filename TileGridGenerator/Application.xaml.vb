Imports System.Windows.Threading
Imports FlickrNet
Imports TileGridGenerator.ViewModel
Imports TileGridGenerator.ViewModel.Services

Class Application



    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        ServiceInjector.InjectServices()
        Dim settMAnager As New SettingManager()
        settMAnager.InitSettings()
        settMAnager = Nothing
        Dim mw As New MainWindow
        If FlickrAuth.OAuthToken Is Nothing OrElse FlickrAuth.OAuthToken.Token Is Nothing Then
            Dim winServ = ServiceContainer.GetService(Of IDialogWindowService)
            winServ.ShowModalDialog("authWindow", New FlickAuthViewModel, Nothing, False, True)
        End If
        If FlickrAuth.OAuthToken IsNot Nothing Then

            mw.Show()
            'Dim mainWinServ = ServiceContainer.GetService(Of IWindowService)
            'mainWinServ.OpenWindow("MainWindow", New MainViewModel, Nothing)
        Else
            My.Application.Shutdown()
        End If
        Registerfile(".tgf", "TileGridGenerator-Datei", My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".exe", My.Application.Info.DirectoryPath & "\icon.ico")
        'MessageBox.Show("Das Programm solle mindestens 1x als Administrator gestartet werden damit die Dateiregistrierungen durchgeführt werden können.")
        'End If

        If e.Args.Count > 0 Then
            Dim v As Array
            Dim mypath As String
            Dim commandlineargs As String = Environment.CommandLine
            v = Split(commandlineargs, """ ")
            mypath = v(1)

            DirectCast(mw.DataContext, MainViewModel).LoadFile(mypath)
        End If
    End Sub

    Private Sub Application_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        MessageBox.Show(e.Exception.Message, "Unbehandelter Fehler. Bitte melde diesen", MessageBoxButton.OK, MessageBoxImage.Error)
    End Sub

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.


    Private Function Registerfile(ByVal endung As String, ByVal namedesdateityps As String, ByVal pfadzuprogramm As String, ByVal pfadzuicon As String) As Boolean
        Try
            Dim objSubKey As Microsoft.Win32.RegistryKey
            Dim objSubKey2 As Microsoft.Win32.RegistryKey
            Dim Wert As String = namedesdateityps
            Dim sKey As String = endung
            Dim sKey2 As String = endung & "\ShellNew"
            Dim sEntry As String = "Content Type"
            Dim sValue As String = "text/plain"
            Dim sEntry2 As String = "PerceivedType"
            Dim sValue2 As String = "text"
            Dim sEntry3 As String = "Nullfile"
            objSubKey = My.Computer.Registry.ClassesRoot.CreateSubKey(sKey)
            objSubKey.SetValue("", Wert)
            objSubKey.SetValue(sEntry, sValue)
            objSubKey.SetValue(sEntry2, sValue2)
            objSubKey2 = My.Computer.Registry.ClassesRoot.CreateSubKey(sKey2)
            objSubKey2.SetValue(sEntry3, "")
            Dim objSubKey3 As Microsoft.Win32.RegistryKey
            Dim sKey3 As String = namedesdateityps
            Dim sKey4 As String = namedesdateityps & "\shell\open\command"
            Dim skey5 As String = namedesdateityps & "\DefaultIcon"
            Dim sValue3 As String = namedesdateityps
            Dim objSubKey5 As Microsoft.Win32.RegistryKey
            Dim sValue5 As String = """" + pfadzuprogramm + """" + " %1"
            Dim sValue6 As String = """" + pfadzuicon + """"
            Dim objSubKey4 As Microsoft.Win32.RegistryKey
            objSubKey3 = My.Computer.Registry.ClassesRoot.CreateSubKey(sKey3)
            objSubKey3.SetValue("", sValue3)
            objSubKey4 = My.Computer.Registry.ClassesRoot.CreateSubKey(sKey4)
            objSubKey4.SetValue("", sValue5)
            objSubKey5 = My.Computer.Registry.ClassesRoot.CreateSubKey(skey5)
            objSubKey5.SetValue("", sValue6)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


End Class
