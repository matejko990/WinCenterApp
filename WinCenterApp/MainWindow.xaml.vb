Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Threading

Class MainWindow

    Public location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Public EW_SW_EP_PP_Radio_Value As String
    Public EWELX_SWELX_Radio_Value As String
    Public In_Out As String

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

    Private Sub SearchSW_Click(sender As Object, e As RoutedEventArgs)

        Dim files As String = "X:\Archiwum\ELIXIR\OUT\20" & YearSW.Text & "\" & MonthSW.Text & "\SW\"
        Dim Day As String = Convert.ToInt16(DayFromSW.Text)
        Dim sDay As String

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            System.IO.File.Delete(location & "wynik.txt")
        End If

        Dim filePath As String = String.Format(location & "wynik.txt", DateTime.Today.ToString("dd-MMM-yyyy"))

        Dim z As Integer
        z = DayToSW.Text - DayFromSW.Text + 1

        ScoreSW.Clear()

        Dim j As Integer = 0

        Do

            Dim a As String
            Dim b As String

            j = j + 1

            sDay = Day + j - 1

            If sDay.Length < 2 Then


                a = "SW" & YearSW.Text & MonthSW.Text & "0" + sDay & "001"

            Else

                'sDay = Day + j - 1

                a = "SW" & YearSW.Text & MonthSW.Text & sDay & "001"

            End If

            Dim sw As String = a & ".bak"

            If (System.IO.File.Exists(files + sw)) Then 'warunek Then istnienia pliku SW

                Dim lines = System.IO.File.ReadAllLines(files & sw, System.Text.Encoding.Default)
                For i = 0 To lines.Length - 1
                    Dim c As Integer = lines.Length
                    If lines(i).Contains(SearchSW.Text) Then
                        If ScoreSW.Text <> "" Then
                            ScoreSW.Text = ScoreSW.Text + lines(i) + vbCrLf

                            Using writer As New StreamWriter(filePath, True)
                                If File.Exists(filePath) Then
                                    writer.WriteLine(lines(i))
                                End If
                            End Using

                        Else

                            ScoreSW.Text = lines(i) + vbCrLf

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

                    b = "SWELX" & YearSW.Text & MonthSW.Text & "0" + sDay & "0" & x

                Else

                    b = "SWELX" & YearSW.Text & MonthSW.Text & sDay & "0" & x

                End If

                Dim swelx As String = b & ".bak"

                If (System.IO.File.Exists(files + swelx)) Then 'warunek Then istnienia pliku SW

                    Dim lines = System.IO.File.ReadAllLines(files & swelx, System.Text.Encoding.Default)
                    For k = 0 To lines.Length - 1
                        If lines(k).Contains(SearchSW.Text) Then
                            If ScoreSW.Text <> "" Then
                                ScoreSW.Text = ScoreSW.Text + lines(k) + vbCrLf

                                Using writer As New StreamWriter(filePath, True)
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(lines(k))
                                    End If
                                End Using

                            Else

                                ScoreSW.Text = lines(k) + vbCrLf

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

    Private Sub Search_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles SearchSW.MouseDoubleClick

        SearchSW.Clear()

    End Sub

    Private Sub ShowWynikSW_Click(sender As Object, e As RoutedEventArgs)

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            Process.Start(location & "wynik.txt")
        End If

    End Sub

    Private Sub ClearSW_Click(sender As Object, e As RoutedEventArgs)

        MonthSW.Clear()
        DayFromSW.Clear()
        DayToSW.Clear()
        SearchSW.Clear()
        ScoreSW.Clear()
        'Search_ELX.Clear()

    End Sub

    Private Sub Us_Finder_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Us_Finder.MouseDoubleClick

        UsFinder.Visibility = Visibility.Visible
        ElixirFinder.Visibility = Visibility.Collapsed

    End Sub

    Private Sub SearchElixir_Click(sender As Object, e As RoutedEventArgs)

        Dim files As String = "X:\Archiwum\ELIXIR\" & In_Out & "\20" & YearElixir.Text & "\" & MonthElixir.Text & "\" & EW_SW_EP_PP_Radio_Value & "\"
        Dim Day As String = Convert.ToInt16(DayFromElixir.Text)
        Dim sDay As String
        Dim ew As String

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            System.IO.File.Delete(location & "wynik.txt")
        End If

        Dim filePath As String = String.Format(location & "wynik.txt", DateTime.Today.ToString("dd-MMM-yyyy"))

        Dim z As Integer
        z = DayToElixir.Text - DayFromElixir.Text + 1

        ScoreElixir.Clear()

        Dim j As Integer = 0
        Dim m As Integer

        Do

            Dim a As String
            Dim b As String

            j = j + 1

            sDay = Day + j - 1

            If EW_SW_EP_PP_Radio_Value <> "EP" Then

                m = 11

            Else

                m = 3

            End If

            For x As Integer = 1 To m

                If sDay.Length < 2 Then

                    If x <> 10 Or x <> 11 Then

                        If EW_SW_EP_PP_Radio_Value = "EP" Then

                            a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & "0" + sDay & "0" & x

                        Else

                            a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & "0" + sDay & "00" & x

                        End If

                    Else

                        a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & "0" + sDay & "0" & x

                    End If

                Else

                    If x <> 10 Or x <> 11 Then

                        If EW_SW_EP_PP_Radio_Value = "EP" Then

                            a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & sDay & "0" & x

                        Else

                            a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & sDay & "00" & x

                        End If

                    Else

                        a = EW_SW_EP_PP_Radio_Value & YearElixir.Text & MonthElixir.Text & sDay & "0" & x

                    End If

                End If

                If EW_SW_EP_PP_Radio_Value = "EP" Then

                    ew = a & ".TXT"

                Else

                    ew = a & ".bak"

                End If

                If (System.IO.File.Exists(files + ew)) Then 'warunek Then istnienia pliku SW

                    Dim lines = System.IO.File.ReadAllLines(files & ew, System.Text.Encoding.Default)
                    For i = 0 To lines.Length - 1
                        Dim c As Integer = lines.Length
                        If lines(i).Contains(SearchElixir.Text) Then
                            If ScoreElixir.Text <> "" Then
                                ScoreElixir.Text = ScoreElixir.Text + lines(i) + vbCrLf

                                Using writer As New StreamWriter(filePath, True)
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(lines(i))
                                    End If
                                End Using

                            Else

                                ScoreElixir.Text = lines(i) + vbCrLf

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

            Next x

            If EWELX_SWELX_Radio_Value = "EWELX" Or EWELX_SWELX_Radio_Value = "SWELX" Then

                For y As Integer = 1 To 9

                    If sDay.Length < 2 Then

                        b = EWELX_SWELX_Radio_Value & YearElixir.Text & MonthElixir.Text & "0" + sDay & "0" & y

                    Else

                        b = EWELX_SWELX_Radio_Value & YearElixir.Text & MonthElixir.Text & sDay & "0" & y

                    End If

                    Dim ewelx As String = b & ".bak"

                    If (System.IO.File.Exists(files + ewelx)) Then 'warunek Then istnienia pliku SW

                        Dim lines = System.IO.File.ReadAllLines(files & ewelx, System.Text.Encoding.Default)
                        For k = 0 To lines.Length - 1
                            If lines(k).Contains(SearchElixir.Text) Then
                                If ScoreElixir.Text <> "" Then
                                    ScoreElixir.Text = ScoreElixir.Text + lines(k) + vbCrLf

                                    Using writer As New StreamWriter(filePath, True)
                                        If File.Exists(filePath) Then
                                            writer.WriteLine(lines(k))
                                        End If
                                    End Using

                                Else

                                    ScoreElixir.Text = lines(k) + vbCrLf

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

                Next y

            End If

            If j = z Then

                Exit Sub

            End If

        Loop

    End Sub

    Private Sub ShowWynikElixir_Click(sender As Object, e As RoutedEventArgs)

        location = New Uri(location).LocalPath & "\"

        If System.IO.File.Exists(location & "wynik.txt") Then
            Process.Start(location & "wynik.txt")
        End If

    End Sub

    Private Sub ClearElixir_Click(sender As Object, e As RoutedEventArgs)

        MonthElixir.Clear()
        DayFromElixir.Clear()
        DayToElixir.Clear()
        SearchElixir.Clear()
        ScoreElixir.Clear()
        Search_ELX.Clear()

    End Sub

    Private Sub Elixir_Finder_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Elixir_Finder.MouseDoubleClick

        ElixirFinder.Visibility = Visibility.Visible
        UsFinder.Visibility = Visibility.Collapsed

    End Sub

    Private Sub SearchElixir_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles SearchElixir.MouseDoubleClick

        SearchElixir.Clear()

    End Sub

    Private Sub RadioEW_Checked(sender As Object, e As RoutedEventArgs)

        If EW_Radio.IsChecked = True Then

            In_Out = "OUT"
            EW_SW_EP_PP_Radio_Value = "EW"
            EWELX_SWELX_Radio_Value = "EWELX"

        End If

    End Sub

    Private Sub RadioEP_Checked(sender As Object, e As RoutedEventArgs)

        If EP_Radio.IsChecked = True Then

            In_Out = "IN"
            EW_SW_EP_PP_Radio_Value = "EP"
            EWELX_SWELX_Radio_Value = ""

        End If

    End Sub

    'Private Sub RangeWindow_IsHitTestVisibleChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles RangeWindow.

    '    ElixirFinder.Visibility = Visibility.Collapsed
    '    UsFinder.Visibility = Visibility.Collapsed
    '    RangeWindow.Visibility = Visibility.Visible

    'End Sub

End Class
