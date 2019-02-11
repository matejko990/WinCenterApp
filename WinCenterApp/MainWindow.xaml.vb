Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Threading

Class MainWindow

    Public location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)

    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()
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

    Private Sub Search_Click(sender As Object, e As RoutedEventArgs)

        Dim files As String = "X:\Archiwum\ELIXIR\OUT\20" & Year.Text & "\" & Month.Text & "\SW\"
        Dim Day As String = Convert.ToInt16(DayFrom.Text)
        Dim sDay As String

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            System.IO.File.Delete(location & "wynik.txt")
        End If

        Dim filePath As String = String.Format(location & "wynik.txt", DateTime.Today.ToString("dd-MMM-yyyy"))

        Dim z As Integer
        z = DayTo.Text - DayFrom.Text + 1

        wynik.Clear()

        Dim j As Integer = 0

        Do

            Dim a As String
            Dim b As String

            j = j + 1

            sDay = Day + j - 1

            If sDay.Length < 2 Then


                a = "SW" & Year.Text & Month.Text & "0" + sDay & "001"

            Else

                'sDay = Day + j - 1

                a = "SW" & Year.Text & Month.Text & sDay & "001"

            End If

            Dim sw As String = a & ".bak"

            If (System.IO.File.Exists(files + sw)) Then 'warunek Then istnienia pliku SW

                Dim lines = System.IO.File.ReadAllLines(files & sw, System.Text.Encoding.Default)
                For i = 0 To lines.Length - 1
                    Dim c As Integer = lines.Length
                    If lines(i).Contains(Search.Text) Then
                        If wynik.Text <> "" Then
                            wynik.Text = wynik.Text + lines(i) + vbCrLf

                            Using writer As New StreamWriter(filePath, True)
                                If File.Exists(filePath) Then
                                    writer.WriteLine(lines(i))
                                End If
                            End Using

                        Else

                            wynik.Text = lines(i) + vbCrLf

                            Using writer As New StreamWriter(filePath, True)
                                If File.Exists(filePath) Then
                                    writer.WriteLine(lines(i))
                                End If
                            End Using

                        End If

                    Else

                        'wynik.Text = ""

                    End If
                Next

            End If

            For x As Integer = 1 To 9

                If sDay.Length < 2 Then

                    b = "SWELX" & Year.Text & Month.Text & "0" + sDay & "0" & x

                Else

                    b = "SWELX" & Year.Text & Month.Text & sDay & "0" & x

                End If

                Dim swelx As String = b & ".bak"

                If (System.IO.File.Exists(files + swelx)) Then 'warunek Then istnienia pliku SW

                    Dim lines = System.IO.File.ReadAllLines(files & swelx, System.Text.Encoding.Default)
                    For k = 0 To lines.Length - 1
                        If lines(k).Contains(Search.Text) Then
                            If wynik.Text <> "" Then
                                wynik.Text = wynik.Text + lines(k) + vbCrLf

                                Using writer As New StreamWriter(filePath, True)
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(lines(k))
                                    End If
                                End Using

                            Else

                                wynik.Text = lines(k) + vbCrLf

                                Using writer As New StreamWriter(filePath, True)
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(lines(k))
                                    End If
                                End Using

                            End If

                        Else

                            'wynik.Text = ""

                        End If


                    Next

                End If

            Next x

            If j = z Then

                Exit Sub

            End If

        Loop

    End Sub

    'Private Function FindFiles() As List(Of String)

    '    Dim allFilesFound As New List(Of String)

    '    Dim dirSearch = "X:\Archiwum\ELIXIR\OUT\20" & Year.Text & "\" & Month.Text & "\SW\"

    '    Dim file_name As New List(Of String) From {"SW" & Year.Text & Month.Text & DayFrom.Text & "001.bak", "SW" & Year.Text & Month.Text & DayTo.Text & "001.bak"}

    '    For Each file In file_name
    '        allFilesFound.AddRange(New DirectoryInfo(dirSearch).EnumerateFiles(file, SearchOption.AllDirectories).[Select](Function(d) d.FullName).ToList())
    '    Next

    '    Return allFilesFound

    'End Function

    Private Sub Search_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Search.MouseDoubleClick

        Search.Clear()

    End Sub

    Private Sub ShowWynik_Click(sender As Object, e As RoutedEventArgs)

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            Process.Start(location & "wynik.txt")
        End If

    End Sub
End Class
