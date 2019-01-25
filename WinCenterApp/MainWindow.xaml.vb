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

        Dashboards0.Visibility = Visibility.Visible

    End Sub

    ''This one I would not take'
    'Private Sub Label_MouseLeave(sender As Object, e As MouseEventArgs) Handles Click1.MouseLeave

    '    Dashboards0.Visibility = Visibility.Collapsed

    'End Sub

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

    'Private Sub Label_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Click1.MouseDoubleClick

    '    Dashboards0.Visibility = Visibility.Visible

    'End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles("X:\Archiwum\ELIXIR\OUT\2018\12\SW\", "*.bak").
               Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        'Dim newdir As Object = files

        For j = 0 To files.Count - 1

            Dim value As String = String.Join("", files(j)) & ".bak"

            Dim lines = System.IO.File.ReadAllLines("X:\Archiwum\ELIXIR\OUT\2018\12\SW\" & value, System.Text.Encoding.Default)
            For i = 0 To lines.Length - 1
                If lines(i).Contains(Search.Text) Then
                    wynik.Text = lines(i)

                Else

                    'wynik.Text = ""

                End If
            Next
            System.IO.File.WriteAllLines("X:\Archiwum\ELIXIR\OUT\2018\12\SW\" & value, lines)

        Next

    End Sub

End Class
