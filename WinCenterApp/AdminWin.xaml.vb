Imports System.Data
'Imports System.Data.SqlClient

Public Class AdminWin

    'Other variable
    Public UserNameLog As String = Environment.UserName

    'Shared variable's for comunication
    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()

    'Timer Events
    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        ' Label content - UserName
        UserNameLabel.Content = "Zalogowany użytkownik: " & Environment.UserName & " :D"

        ' Timer starts
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()

    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick

        ' Label content - Acctually time
        lblTime.Content = Now.ToString("HH:mm:ss")

    End Sub

    Private Sub Chngpass(sender As Object, e As RoutedEventArgs)

        ' Open the window "ChangePasswordByUser"
        Dim ChangePasswordByUser = New ChangePasswordByUser
        ChangePasswordByUser.Show()

    End Sub

    Private Sub Lgout(sender As Object, e As RoutedEventArgs)

        ' Message about Logout
        'MsgBox("You are Now Logged Out", MsgBoxStyle.Information, "Login")

        TxtString_msginformation = "Zostaniesz wylogowan-y/-a!"

        MsgInformation.Show()

        TxtString_msginformation = Nothing

        ' Close current window
        Me.Close()

        ' Variable and call to the login window
        Dim LoginScreen = New LoginScreen()

        ' Open the window "LoginScreen"
        LoginScreen.Show()

    End Sub


End Class
