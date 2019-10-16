Public Class InstanceHolder
    Private Sub New()
        Settings = New SettingManager
        Settings.InitSettings()

    End Sub

    Public Shared ReadOnly Property Instance As InstanceHolder = New InstanceHolder

    Public Property Settings As SettingManager





End Class