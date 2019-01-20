Imports System.Windows.Threading

Class MainWindow

    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        timerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        timerRefreshTime.Start()
    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick
        lblTime.Content = Now.ToString("HH:mm:ss")
    End Sub

    'Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
    'btnFirst.Visibility = Visibility.Visible
    'btnFirst.Focus()
    'End Sub

    'Private Sub ToggleButton1_MouseLeave(sender As Object, e As MouseEventArgs) Handles btnFirst.MouseLeave

    'range_calendar.Visibility = Visibility.Collapsed

    'End Sub

    Private Sub ToggleButton_MouseLeftButtonDown(sender As Object, e As RoutedEventArgs) Handles RangeWindow.MouseLeftButtonDown

        Range_Calendar.Visibility = Visibility.Collapsed

    End Sub

    'This one I would not take'
    Private Sub ToggleButton_MouseLeave(sender As Object, e As MouseEventArgs) Handles Range_Calendar.MouseLeave

        Range_Calendar.Visibility = Visibility.Collapsed

    End Sub

    'Private Sub ToggleButton_MouseMove(sender As Object, e As MouseEventArgs) Handles btnFirst.MouseMove

    'range_calendar.Visibility = Visibility.Visible

    'End Sub

    Private Sub ToggleButton_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles btnFirst.MouseDoubleClick

        Range_Calendar.Visibility = Visibility.Visible

    End Sub

    Private Sub Label_MouseMove(sender As Object, e As MouseEventArgs) Handles Click1.MouseMove

        Dashboards.Visibility = Visibility.Visible

    End Sub

    'This one I would not take'
    Private Sub Label_MouseLeave(sender As Object, e As MouseEventArgs) Handles Click1.MouseLeave

        Dashboards.Visibility = Visibility.Collapsed

    End Sub

    Private Sub Lgout(sender As Object, e As RoutedEventArgs)

        MsgBox("You are Now Logged Out", MsgBoxStyle.Information, "Login")

        Me.Close()

        Dim LoginScreen = New LoginScreen()

        LoginScreen.Show()

    End Sub

    Private Sub Chngpass(sender As Object, e As RoutedEventArgs)

        Dim ChangePasswordByUser = New ChangePasswordByUser

        ChangePasswordByUser.Show()

    End Sub

End Class
