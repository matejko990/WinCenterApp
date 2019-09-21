Imports System.IO
Imports System.Collections.Specialized
Imports System.Text.RegularExpressions
Imports System.Windows.Threading
Imports System.ComponentModel

Class MainWindow

    ' Global variable
    Public location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    Public ElixirFile_Radio_Value As String
    Public In_Out As String
    Public TDate As DateTime = DateTime.Now
    Public Year As String = DatePart("yyyy", TDate)

    'Dim LoginScreen As New LoginScreen

    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()

    ' Global property
    Public Property ScoreValue_Year As String
    Public Property ScoreValue_Month As String
    Public Property ScoreValue_Day As String
    Public Property ScoreValue_Nr_sesji As String
    Public Property ScoreValue_Nr_pliku As String

    'Public WindowsMain As Boolean

    '' WithEvents
    'Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) 'Handles MyBase.Loaded

        UserNameLabel.Content = "Zalogowany użytkownik: " & Environment.UserName & " :D"

        Main_Home.Visibility = Visibility.Visible

        '' Time in main menu
        'TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        'TimerRefreshTime.Start()

        Year = Format(TDate, "yy")
        YearElixir.Text = Year

    End Sub

    'Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick

    '    ' Time content in label
    '    lblTime.Content = Now.ToString("HH:mm:ss")

    'End Sub

    'Private Sub ToggleButton_MouseLeftButtonDown(sender As Object, e As RoutedEventArgs) Handles RangeWindow.MouseLeftButtonDown

    '    ' Calendar visibility for MouseLeftButtonDown
    '    Range_Calendar.Visibility = Visibility.Collapsed

    'End Sub

    'Private Sub ToggleButton_MouseLeave(sender As Object, e As MouseEventArgs) Handles Range_Calendar.MouseLeave

    '    ' Calendar visibility for MouseLeave
    '    Range_Calendar.Visibility = Visibility.Collapsed

    'End Sub

    'Private Sub ToggleButton_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles btnFirst.MouseDoubleClick

    '    ' Calendar visibility for MouseDoubleClick
    '    Range_Calendar.Visibility = Visibility.Visible

    'End Sub

    Private Sub Chngpass(sender As Object, e As RoutedEventArgs)
        Dim ChangePasswordByUser = New ChangePasswordByUser
        ChangePasswordByUser.Show()

    End Sub

    Private Sub Lgout(sender As Object, e As RoutedEventArgs)

        ' Close current window
        'Dim ChangePasswordByUser = New ChangePasswordByUser
        'ChangePasswordByUser.Close()
        TxtString_msginformation = "Zostaniesz wylogowan-y/-a!"

        MsgInformation.Show()

        TxtString_msginformation = Nothing

        Me.Close()
        Dim LoginScreen As New LoginScreen
        LoginScreen.Show()

    End Sub

    Private Sub SearchElixir_Click(sender As Object, e As RoutedEventArgs)

        Try

            'Count()

            ' Restore BorderBrush
            YearElixir.BorderBrush = Brushes.White
            MonthElixir_From.BorderBrush = Brushes.White
            MonthElixir_To.BorderBrush = Brushes.White
            DayFromElixir.BorderBrush = Brushes.White
            DayToElixir.BorderBrush = Brushes.White
            SearchElixir_I.BorderBrush = Brushes.White
            SearchElixir_II.BorderBrush = Brushes.White

            ' Clear score
            ScoreElixir.Clear()

            ' Variable for FilePath
            Dim filePath As String

            ' Variable for UserName
            Dim UserNameLog As String = Environment.UserName

            ' Location for LocalPath
            location = New Uri(location).LocalPath & "\"

            ' Check file exists
            If System.IO.File.Exists(location & "wynik_" & UserNameLog & ".txt") Then
                System.IO.File.Delete(location & "wynik_" & UserNameLog & ".txt")
            End If

            ' Create file with search score
            filePath = String.Format(location & "wynik_" & UserNameLog & ".txt", DateTime.Today.ToString("dd-MMM-yyyy"))

            'Dim txt_I As String = SearchElixir_I.Text
            'Dim txt_II As String = SearchElixir_II.Text
            'Dim Badwords() As String = {" "}

            'If Badwords.Any(Function(b) txt_I.ToLower().Contains(b.ToLower())) And SearchElixir_I.Text <> "np. numer rachunku" Then
            '    'Whoa.. txt contains some bad vibrations..
            '    MessageBox.Show("Usuń spacje w polu I! Bo nie znajdę nic! :P")

            '    SearchElixir_I.BorderBrush = Brushes.Red

            '    Exit Sub

            'ElseIf Badwords.Any(Function(b) txt_II.ToLower().Contains(b.ToLower())) And SearchElixir_II.Text <> "np. kwota (opcjonalnie)" Then
            '    'Whoa.. txt contains some bad vibrations..
            '    MessageBox.Show("Usuń spacje w polu II! Bo nie znajdę nic! :P")

            '    SearchElixir_II.BorderBrush = Brushes.Red

            '    Exit Sub

            'End If

            If ChckB_Elixir.IsChecked Then

                If SearchElixir_I.Text = "" And SearchElixir_II.Text = "np._kwota_(opcjonalnie)" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu I! :P")

                    SearchElixir_I.Text = "np._numer_rachunku"

                    SearchElixir_I.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf SearchElixir_II.Text = "" And SearchElixir_I.Text = "np._numer_rachunku" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu II! :P")

                    SearchElixir_II.Text = "np._kwota_(opcjonalnie)"

                    SearchElixir_II.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf SearchElixir_I.Text = "" And SearchElixir_II.Text = "" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu I lub II! :P")

                    SearchElixir_I.Text = "np._numer_rachunku"
                    SearchElixir_II.Text = "np._kwota_(opcjonalnie)"

                    SearchElixir_I.BorderBrush = Brushes.Red
                    SearchElixir_II.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf SearchElixir_I.Text = "np._numer_rachunku" And SearchElixir_II.Text = "np._kwota_(opcjonalnie)" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu I lub II! :P")

                    SearchElixir_I.BorderBrush = Brushes.Red
                    SearchElixir_II.BorderBrush = Brushes.Red

                    Exit Sub

                End If

            Else

                If SearchElixir_I.Text = "" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu I! :P")

                    SearchElixir_I.Text = "np._numer_rachunku"

                    SearchElixir_I.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf SearchElixir_I.Text = "np._numer_rachunku" Then

                    MessageBox.Show("Wprowadź jakąś frazę w polu I! :P")

                    SearchElixir_I.BorderBrush = Brushes.Red

                    Exit Sub

                End If

            End If

            If ChckB.IsChecked Then

                If MonthElixir_From.Text = "" Then 'Or MonthElixir_To.Text = "" Or DayFromElixir.Text = "" Or DayToElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_From.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf MonthElixir_To.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_To.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayFromElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayToElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                End If

            Else

                If MonthElixir_To.Text = "" Then 'Or DayFromElixir.Text = "" Or DayToElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_To.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayFromElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayToElixir.Text = "" Then

                    MessageBox.Show("Popraw datę! :P")

                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                End If

            End If

            Dim MonthF As Integer
            Dim MonthT As Integer = Integer.Parse(MonthElixir_To.Text)
            Dim DayF As Integer = Integer.Parse(DayFromElixir.Text)
            Dim DayT As Integer = Integer.Parse(DayToElixir.Text)

            ' Parse String to Integer
            If ChckB.IsChecked Then

                MonthF = Integer.Parse(MonthElixir_From.Text)

                If MonthF > MonthT Then 'Or MonthF = 0 Or MonthT = 0 Or DayF = 0 Or DayT = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_From.BorderBrush = Brushes.Red
                    MonthElixir_To.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf MonthF = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_From.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf MonthT = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_To.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayF = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayT = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf MonthF = MonthT And DayF > DayT Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red
                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                    'ElseIf DayF > DayT Then

                    '    MessageBox.Show("Popraw datę! :P")

                    '    DayFromElixir.BorderBrush = Brushes.Red
                    '    DayToElixir.BorderBrush = Brushes.Red

                    '    Exit Sub

                End If

            Else

                If MonthT = 0 Then 'Or DayF > DayT Or DayF = 0 Or DayT = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    MonthElixir_To.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayF > DayT Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red
                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayF = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    DayFromElixir.BorderBrush = Brushes.Red

                    Exit Sub

                ElseIf DayT = 0 Then

                    MessageBox.Show("Popraw datę! :P")

                    DayToElixir.BorderBrush = Brushes.Red

                    Exit Sub

                End If

            End If

            Dim z As Integer

            If MonthF = 0 Then

                MonthF = MonthT
                z = 0

            Else

                z = MonthT - MonthF

            End If

            ' Variable for ElixirCatalog
            Dim ElixirCatalog As String

            ' Variable for list collection
            Dim ListsElixirFiles As New StringCollection

            ' Check condition that search are you around only two month
            If z > 1 Then

                MessageBox.Show("Popraw miesiące! Możesz szukać tylko na pzełomie dwóch miesięcy! :P")

                MonthElixir_To.BorderBrush = Brushes.Red

                Exit Sub

            End If

            For a = 0 To z

                ' Variable for date condition in range from 1 - 9
                'Dim OneToNine As Regex = New Regex("^[1-9]$")
                Dim Month_i As String = MonthF + a

                If Month_i >= 10 Then

                    ElixirCatalog = "X:\Archiwum\ELIXIR\" & In_Out & "\20" & YearElixir.Text & "\" & Month_i & "\" & ElixirFile_Radio_Value & "\"

                Else

                    ElixirCatalog = "X:\Archiwum\ELIXIR\" & In_Out & "\20" & YearElixir.Text & "\" & "0" & Month_i & "\" & ElixirFile_Radio_Value & "\"

                End If

                Me.Cursor = Cursors.Wait

                Dim ElixirFilesCatalog = My.Computer.FileSystem.GetFiles(ElixirCatalog)

                ' Check all file in ElixirFilesCatalog
                For Each fileLocation In ElixirFilesCatalog

                    If fileLocation = ElixirCatalog & "wynik" Then

                        ' Check file exists
                        If System.IO.File.Exists(ElixirCatalog & "wynik") Then
                            System.IO.File.Delete(ElixirCatalog & "wynik")
                        End If

                        ' Close searching
                        Exit For

                    End If

                    ' Regex function
                    Dim mc = Regex.Matches(fileLocation, "\\(?<plik>[A-Z,a-z]{2,5}[\d]{2}(?<miesiac>[\d]{2})(?<dzien>[\d]{2})[\d]{2,3}\.[A-Z,a-z]{3})$")
                    ' nie wiem czy sens jest dodawać nazwy grupy, skoro i tak ich nie używamy, zamiast tego mamy ...Groups.Item(1).Value, gdzie Item(1) to grupa pierwsza, druga, itd.

                    ' Check hit count > 0
                    If mc.Count > 0 Then 'Or mc_1.Count > 0 Then

                        ' Variable for second hit regex 
                        Dim ScoreValueElixir_Day = mc.Item(0).Groups.Item(3).Value
                        Dim ScoreValueElixir_Month = mc.Item(0).Groups.Item(2).Value

                        Dim ScoreDay As Integer = Integer.Parse(ScoreValueElixir_Day)
                        Dim ScoreMonth As Integer = Integer.Parse(ScoreValueElixir_Month)

                        ' Check date condition - TextBox validate from 1 to 9 add "0"
                        'If MonthElixir.Text = OneToNine.IsMatch(MonthElixir.Text) Then

                        ' Add "0" to TextBox before count
                        Dim MonthF_T As String = MonthF
                        Dim MonthT_T As String = MonthT
                        Dim DayF_T As String = DayF
                        Dim DayT_T As String = DayT

                        If ChckB.IsChecked Then

                            ' Check date condition for two month's
                            If MonthF_T = ScoreMonth And ScoreMonth < MonthT_T And ScoreDay >= DayF_T Then 'And ScoreDay <= DayF_T_1 Then

                                ' Add file to lists collection
                                ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)
                                'ListsElixirFiles.Add(mc_1.Item(0).Groups.Item(1).Value)

                            ElseIf MonthF_T < ScoreMonth And ScoreMonth < MonthT_T Then 'And ScoreDay >= DayF_T And ScoreDay <= DayF_T_1 Then

                                ' Add file to lists collection
                                ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)
                                'ListsElixirFiles.Add(mc_1.Item(0).Groups.Item(1).Value)

                            ElseIf MonthT_T = ScoreMonth And ScoreMonth > MonthF_T And ScoreDay <= DayT_T Then 'And ScoreDay <= DayT_T_1 Then

                                ' Add file to lists collection
                                ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)
                                'ListsElixirFiles.Add(mc_1.Item(0).Groups.Item(1).Value)     

                            ElseIf ScoreMonth = MonthF_T And ScoreMonth = MonthT_T And ScoreDay >= DayF_T And ScoreDay <= DayT_T Then

                                ' Add file to lists collection
                                ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)
                                'ListsElixirFiles.Add(mc_1.Item(0).Groups.Item(1).Value)

                            End If

                            'ElseIf ScoreMonth = Month_i And ScoreDay >= DayF_T And ScoreDay <= DayF_T_1 Then

                        Else

                            ' Check date condition for one month
                            If ScoreMonth = MonthT_T And ScoreDay >= DayF_T And ScoreDay <= DayT_T Then

                                ' Add file to lists collection
                                ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)
                                'ListsElixirFiles.Add(mc_1.Item(0).Groups.Item(1).Value)

                            End If

                        End If

                    End If

                    'End If

                    ' Next file
                Next fileLocation

                ' Check all file from lists collection
                For Each k In ListsElixirFiles

                    ' Declare variable for ReadAllLines from one file in lists cllection 
#Disable Warning BC42104 ' Zmienna została użyta przed przypisaniem do niej wartości
                    Dim lines = System.IO.File.ReadAllLines(ElixirCatalog & k, System.Text.Encoding.Default)
#Enable Warning BC42104 ' Zmienna została użyta przed przypisaniem do niej wartości

                    ' Check all lines from file
                    For i = 0 To lines.Length - 1

                        ' Variable for all lines count
                        Dim linesCount As Integer = lines.Length

                        ' Check second condition contains something
                        If SearchElixir_II.Text = "np._kwota_(opcjonalnie)" Then
                            SearchElixir_II.Clear() ' clear variable
                        ElseIf SearchElixir_I.Text = "np._numer_rachunku" Then
                            SearchElixir_I.Clear() ' clear variable
                        End If

                        ' Check contains lines in file: if yes
                        If lines(i).Contains(SearchElixir_I.Text) And lines(i).Contains(SearchElixir_II.Text) Then

                            ' Check ScoreElixir.Text is empty
                            If ScoreElixir.Text <> "" Then

                                ' Adds find lines to ScoreElixir.Text
                                ScoreElixir.Text = ScoreElixir.Text + k & "#" & i + 1 & "#" & lines(i) + vbCrLf

                                ' Adds find lines to filePath: wynik.txt
#Disable Warning BC42104 ' Zmienna została użyta przed przypisaniem do niej wartości
                                Using writer As New StreamWriter(filePath, True)
#Enable Warning BC42104 ' Zmienna została użyta przed przypisaniem do niej wartości
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(k & "#" & i + 1 & "#" & lines(i))
                                    End If
                                End Using

                                'If... is not empty
                            Else

                                ScoreElixir.Text = k & "#" & i + 1 & "#" & lines(i) + vbCrLf

                                Using writer As New StreamWriter(filePath, True)
                                    If File.Exists(filePath) Then
                                        writer.WriteLine(k & "#" & i + 1 & "#" & lines(i))
                                    End If
                                End Using

                            End If

                            ' if... no
                        Else

                            'ScoreElixir.Text = "Nie znaleziono żadnego rekordu!"

                        End If

                        ' Next lines(i)
                    Next i

                    ' Next file "k" from list collection
                Next k

                ListsElixirFiles.Clear()

                'Next catalog
            Next a


        Catch ex As Exception

            'Throw ex
            MsgBox("Error as: " & vbCrLf & ex.ToString)

        Finally

            If ScoreElixir.Text = "" Then

                ScoreElixir.Text = "Nie znaleziono żadnego rekordu!"

            End If

            ' This line executes whether or not the exception occurs.
            Me.Cursor = Cursors.Hand

        End Try

        ' Check second condition contains something
        If SearchElixir_II.Text = "" Then
            SearchElixir_II.Text = "np._kwota_(opcjonalnie)" ' restore variable
        ElseIf SearchElixir_I.Text = "" Then
            SearchElixir_I.Text = "np._numer_rachunku" ' restore variable
        End If

        ProgressBar1.Value = 100
        procento.Content = 100 & "%"

        Dim response = MsgBox("Wyszukiwanie zakończone!", MsgBoxStyle.YesNo, "Uwaga!")

        If response = MsgBoxResult.Yes = True Then

            Dim UserNameLog As String = Environment.UserName

            ' Location for LocalPath
            location = New Uri(location).LocalPath & "\"

            ' Check file wynik.txt exists and open file txt
            If System.IO.File.Exists(location & "wynik_" & UserNameLog & ".txt") Then
                Process.Start(location & "wynik_" & UserNameLog & ".txt")
            End If

            ProgressBar1.Value = Nothing
            procento.Content = Nothing

        Else

            ProgressBar1.Value = Nothing
            procento.Content = Nothing

        End If

    End Sub

    'Public Event ProgressBarEvent(ByVal [num] As Integer)

    'Public Sub Count()
    '    For i = 0 To 100
    '        RaiseEvent ProgressBarEvent(i)
    '    Next

    'End Sub

    'Public Sub ProgressBar(ByVal [mynum] As Integer) Handles Me.ProgressBarEvent
    '    Me.ProgressBar1.Maximum = 100
    '    Me.ProgressBar1.Minimum = 0
    '    Me.ProgressBar1.Value = mynum
    '    Console.WriteLine(Me.ProgressBar1.Value)
    '    'Me.ProgressNum.Text = Me.ProgressBar1.Value
    'End Sub

    Private Sub ShowWynikElixir_Click(sender As Object, e As RoutedEventArgs)

        Dim UserNameLog As String = Environment.UserName

        ' Location for LocalPath
        location = New Uri(location).LocalPath & "\"

        ' Check file wynik.txt exists and open file txt
        If System.IO.File.Exists(location & "wynik_" & UserNameLog & ".txt") Then
            Process.Start(location & "wynik_" & UserNameLog & ".txt")
        End If

    End Sub

    Private Sub ClearElixir_Click(sender As Object, e As RoutedEventArgs)

        ' Clear field and restore standard settings
        MonthElixir_From.Clear()
        MonthElixir_To.Clear()
        DayFromElixir.Clear()
        DayToElixir.Clear()
        SearchElixir_I.Clear()
        SearchElixir_I.Text = "np._numer_rachunku"
        SearchElixir_II.Text = "np._kwota_(opcjonalnie)"
        ScoreElixir.Clear()
        ScoreElixir.Text = "Okno wyniku"
        Search_ELX.Clear()
        Search_ELX.Text = "ELX#"

        ' Restore Year TextBox
        Year = Format(TDate, "yy")
        YearElixir.Text = Year

        ' Restore BorderBrush
        YearElixir.BorderBrush = Brushes.White
        MonthElixir_From.BorderBrush = Brushes.White
        MonthElixir_To.BorderBrush = Brushes.White
        DayFromElixir.BorderBrush = Brushes.White
        DayToElixir.BorderBrush = Brushes.White
        SearchElixir_I.BorderBrush = Brushes.White
        SearchElixir_II.BorderBrush = Brushes.White

        ProgressBar1.Value = Nothing
        procento.Content = Nothing

    End Sub

    Private Sub Elixir_Finder_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Elixir_Finder.MouseDoubleClick

        ' ElixirFinder visibility for MouseDoubleClick
        ElixirFinder.Visibility = Visibility.Visible
        ' Home visibility for MouseDoubleClick
        Main_Home.Visibility = Visibility.Collapsed

    End Sub

    'Private Sub SearchElixir_I_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles SearchElixir_I.MouseDoubleClick

    '    ' Clear field for SearchElixir when MouseDoubleClick
    '    SearchElixir_I.Clear()

    'End Sub

    'Private Sub SearchElixir_II_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles SearchElixir_II.MouseDoubleClick

    '    SearchElixir_II.Clear()

    'End Sub

    Private Sub RadioEP_Checked(sender As Object, e As RoutedEventArgs)

        If EP_Radio.IsChecked = True Then

            In_Out = "IN"
            ElixirFile_Radio_Value = "EP"
            EP_Radio.Foreground = Brushes.GreenYellow
            EP_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioPP_Checked(sender As Object, e As RoutedEventArgs)

        If PP_Radio.IsChecked = True Then

            In_Out = "IN"
            ElixirFile_Radio_Value = "PP"
            PP_Radio.Foreground = Brushes.GreenYellow
            PP_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioOP_Checked(sender As Object, e As RoutedEventArgs)

        If OP_Radio.IsChecked = True Then

            In_Out = "IN"
            ElixirFile_Radio_Value = "OP"
            OP_Radio.Foreground = Brushes.GreenYellow
            OP_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioDP_Checked(sender As Object, e As RoutedEventArgs)

        If DP_Radio.IsChecked = True Then

            In_Out = "IN"
            ElixirFile_Radio_Value = "DP"
            DP_Radio.Foreground = Brushes.GreenYellow
            DP_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioIU_Checked(sender As Object, e As RoutedEventArgs)

        If IU_Radio.IsChecked = True Then

            In_Out = "IN"
            ElixirFile_Radio_Value = "IU"
            IU_Radio.Foreground = Brushes.GreenYellow
            IU_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioEW_Checked(sender As Object, e As RoutedEventArgs)

        If EW_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "EW"
            EW_Radio.Foreground = Brushes.GreenYellow
            EW_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioSW_Checked(sender As Object, e As RoutedEventArgs)

        If SW_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "SW"
            SW_Radio.Foreground = Brushes.GreenYellow
            SW_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioPW_Checked(sender As Object, e As RoutedEventArgs)

        If PW_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "PW"
            PW_Radio.Foreground = Brushes.GreenYellow
            PW_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioOW_Checked(sender As Object, e As RoutedEventArgs)

        If OW_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "OW"
            OW_Radio.Foreground = Brushes.GreenYellow
            OW_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioDW_Checked(sender As Object, e As RoutedEventArgs)

        If DW_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "DW"
            DW_Radio.Foreground = Brushes.GreenYellow
            DW_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub RadioWY_Checked(sender As Object, e As RoutedEventArgs)

        If WY_Radio.IsChecked = True Then

            In_Out = "OUT"
            ElixirFile_Radio_Value = "WY"
            WY_Radio.Foreground = Brushes.GreenYellow
            WY_Radio.Background = Brushes.YellowGreen

        End If

    End Sub

    Private Sub EP_Radio_Unchecked(sender As Object, e As RoutedEventArgs) Handles EP_Radio.Unchecked, PP_Radio.Unchecked, OP_Radio.Unchecked, DP_Radio.Unchecked, IU_Radio.Unchecked, EW_Radio.Unchecked, SW_Radio.Unchecked,
        PW_Radio.Unchecked, OW_Radio.Unchecked, DW_Radio.Unchecked, WY_Radio.Unchecked

        EP_Radio.Foreground = Brushes.White
        EP_Radio.Background = Brushes.White

        PP_Radio.Foreground = Brushes.White
        PP_Radio.Background = Brushes.White

        OP_Radio.Foreground = Brushes.White
        OP_Radio.Background = Brushes.White

        DP_Radio.Foreground = Brushes.White
        DP_Radio.Background = Brushes.White

        IU_Radio.Foreground = Brushes.White
        IU_Radio.Background = Brushes.White

        EW_Radio.Foreground = Brushes.White
        EW_Radio.Background = Brushes.White

        SW_Radio.Foreground = Brushes.White
        SW_Radio.Background = Brushes.White

        PW_Radio.Foreground = Brushes.White
        PW_Radio.Background = Brushes.White

        OW_Radio.Foreground = Brushes.White
        OW_Radio.Background = Brushes.White

        DW_Radio.Foreground = Brushes.White
        DW_Radio.Background = Brushes.White

        WY_Radio.Foreground = Brushes.White
        WY_Radio.Background = Brushes.White

    End Sub

    Private Sub SearchElx_Click(sender As Object, e As RoutedEventArgs)

        Try

            ' Restore BorderBrush
            YearElixir.BorderBrush = Brushes.White
            MonthElixir_From.BorderBrush = Brushes.White
            MonthElixir_To.BorderBrush = Brushes.White
            DayFromElixir.BorderBrush = Brushes.White
            DayToElixir.BorderBrush = Brushes.White
            SearchElixir_I.BorderBrush = Brushes.White
            SearchElixir_II.BorderBrush = Brushes.White

            Me.Cursor = Cursors.Wait

            Dim data_Elx As String = Search_ELX.Text
            data_Elx = data_Elx.Replace(" ", String.Empty)

            Dim dc = Regex.Matches(data_Elx, "(?<rok>[\d]{2})(?<miesiac>[\d]{2})(?<dzien>[\d]{2})(?<sesja>[\d]{2})|(\/)|(\\)|(?<nr_pliku>[\d]+$)")
            'nie wiem czy sens jest dodawać nazwy grupy, skoro i tak ich nie używamy, zamiast tego mamy ...Groups.Item(1).Value, gdzie Item(1) to grupa pierwsza, druga, itd.

            If dc.Count > 0 Then

                If dc.Item(0).Groups.Item(3).Value Then

                    ScoreValue_Year = dc.Item(0).Groups.Item(3).Value

                End If

                If dc.Item(0).Groups.Item(4).Value Then

                    ScoreValue_Month = dc.Item(0).Groups.Item(4).Value

                End If

                If dc.Item(0).Groups.Item(5).Value Then

                    ScoreValue_Day = dc.Item(0).Groups.Item(5).Value

                End If

                If dc.Item(0).Groups.Item(6).Value Then

                    ScoreValue_Nr_sesji = dc.Item(0).Groups.Item(6).Value

                End If

                If dc.Item(2).Groups.Item(7).Value Then

                    ScoreValue_Nr_pliku = dc.Item(2).Groups.Item(7).Value

                End If

            End If

            Dim ElixirCatalog As String = "X:\Archiwum\ELIXIR\" & "IN" & "\20" & ScoreValue_Year & "\" & ScoreValue_Month & "\" & "EP" & "\"

            Dim ElixirFilesCatalog = My.Computer.FileSystem.GetFiles(ElixirCatalog)

            Dim UserNameLog As String = Environment.UserName

            location = New Uri(location).LocalPath & "\"

            If System.IO.File.Exists(location & "wynik_" & UserNameLog & ".txt") Then
                System.IO.File.Delete(location & "wynik_" & UserNameLog & ".txt")
            End If

            Dim filePath As String = String.Format(location & "wynik_" & UserNameLog & ".txt", DateTime.Today.ToString("dd-MMM-yyyy"))

            ScoreElixir.Clear() 'clear score

            Dim ListsElixirFiles As New StringCollection

            For Each fileLocation In ElixirFilesCatalog

                If fileLocation = ElixirCatalog & "wynik" Then

                    ' Check file exists
                    If System.IO.File.Exists(ElixirCatalog & "wynik") Then
                        System.IO.File.Delete(ElixirCatalog & "wynik")
                    End If

                    ' Close searching
                    Exit For

                End If

                Dim mc = Regex.Matches(fileLocation, "\\(?<plik>[A-Z,a-z]{2,5}[\d]{2}[\d]{2}(?<dzien>[\d]{2})(?<nr_sesji>[\d]{2,3})\.[A-Z,a-z]{3})$")
                'nie wiem czy sens jest dodawać nazwy grupy, skoro i tak ich nie używamy, zamiast tego mamy ...Groups.Item(1).Value, gdzie Item(1) to grupa pierwsza, druga, itd.

                Dim ScoreValueElixir = mc.Item(0).Groups.Item(2).Value
                Dim ScoreValueNrSesjiPliku = mc.Item(0).Groups.Item(3).Value

                If mc.Count > 0 Then

                    If ScoreValueElixir = ScoreValue_Day And ScoreValueNrSesjiPliku = ScoreValue_Nr_sesji Then

                        ListsElixirFiles.Add(mc.Item(0).Groups.Item(1).Value)

                    End If

                End If

            Next

            For Each k In ListsElixirFiles

                Dim lines = System.IO.File.ReadAllLines(ElixirCatalog & k, System.Text.Encoding.Default)
                Dim i As Integer = ScoreValue_Nr_pliku
                Dim c As Integer = lines.Length
                If c > i - 1 Then
                    If lines(i - 1) <> "" Then
                        If ScoreElixir.Text <> "" Then
                            ScoreElixir.Text = ScoreElixir.Text + k & "#" & i & "#" & lines(i - 1) + vbCrLf

                            Using writer As New StreamWriter(filePath, True)
                                If File.Exists(filePath) Then
                                    writer.WriteLine(k & "#" & i & "#" & lines(i - 1))
                                End If
                            End Using

                        Else

                            ScoreElixir.Text = k & "#" & i & "#" & lines(i - 1) + vbCrLf

                            Using writer As New StreamWriter(filePath, True)
                                If File.Exists(filePath) Then
                                    writer.WriteLine(k & "#" & i & "#" & lines(i - 1))
                                End If
                            End Using

                        End If

                    End If

                Else

                    'ScoreElixir.Text = "Nie znaleziono żadnego rekordu!"

                End If

            Next k

        Catch ex As Exception

            'Throw ex
            MsgBox("Error as: " & vbCrLf & ex.ToString)

        Finally

            If ScoreElixir.Text = "" Then

                ScoreElixir.Text = "Nie znaleziono żadnego rekordu!"

            End If

            ' This line executes whether or not the exception occurs. 
            Me.Cursor = Cursors.Hand

        End Try

        ProgressBar1.Value = 100
        procento.Content = 100 & "%"

        Dim response = MsgBox("Wyszukiwaniwe zakończone!", MsgBoxStyle.YesNo, "Uwaga!")

        If response = MsgBoxResult.Yes Then

            Dim UserNameLog As String = Environment.UserName

            ' Location for LocalPath
            location = New Uri(location).LocalPath & "\"

            ' Check file wynik.txt exists and open file txt
            If System.IO.File.Exists(location & "wynik_" & UserNameLog & ".txt") Then
                Process.Start(location & "wynik_" & UserNameLog & ".txt")
            End If

            ProgressBar1.Value = Nothing
            procento.Content = Nothing

        Else

            ProgressBar1.Value = Nothing
            procento.Content = Nothing

        End If

    End Sub

    Private Sub Search_ELX_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Search_ELX.MouseDoubleClick

        Search_ELX.Clear()

    End Sub

    Private Sub MonthElixir_From_TextChanged(sender As Object, e As TextChangedEventArgs) Handles MonthElixir_From.TextChanged

        MonthElixir_From.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\d]$)|(^1[3-9])|(^2[0-9])|(^3[0-9])|(^4[0-9])|(^5[0-9])|(^6[0-9])|(^7[0-9])|(^8[0-9])|(^9[0-9])")
        MonthElixir_From.Text = digitsOnly.Replace(MonthElixir_From.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub MonthElixir_To_TextChanged(sender As Object, e As TextChangedEventArgs) Handles MonthElixir_To.TextChanged

        MonthElixir_To.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\d]$)|(^1[3-9])|(^2[0-9])|(^3[0-9])|(^4[0-9])|(^5[0-9])|(^6[0-9])|(^7[0-9])|(^8[0-9])|(^9[0-9])")
        MonthElixir_To.Text = digitsOnly.Replace(MonthElixir_To.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub DayFromElixir_TextChanged(sender As Object, e As TextChangedEventArgs) Handles DayFromElixir.TextChanged

        DayFromElixir.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\d]$)|(^3[2-9])|(^4[0-9])|(^5[0-9])|(^6[0-9])|(^7[0-9])|(^8[0-9])|(^9[0-9])")
        DayFromElixir.Text = digitsOnly.Replace(DayFromElixir.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub DayToElixir_TextChanged(sender As Object, e As TextChangedEventArgs) Handles DayToElixir.TextChanged

        DayToElixir.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\d]$)|(^3[2-9])|(^4[0-9])|(^5[0-9])|(^6[0-9])|(^7[0-9])|(^8[0-9])|(^9[0-9])")
        DayToElixir.Text = digitsOnly.Replace(DayToElixir.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub TodayDate_Click(sender As Object, e As RoutedEventArgs)

        'Dim TDate As DateTime = DateTime.Now

        'Dim Year As String = DatePart("yyyy", TDate)

        Year = Format(TDate, "yy")
        YearElixir.Text = Year

        Dim Month As String = DatePart("m", TDate)
        MonthElixir_To.Text = Month

        Dim Day As String = DatePart("d", TDate)

        If ChckB.IsChecked Then

            If Month = 1 Then

                MonthElixir_From.Text = Month
                MonthElixir_To.Text = Month

                If Day <= 5 Then
                    DayFromElixir.Text = 1
                    DayToElixir.Text = Day
                Else
                    DayFromElixir.Text = Day - 5
                    DayToElixir.Text = Day
                End If

            Else

                MonthElixir_From.Text = Month - 1
                MonthElixir_To.Text = Month
                DayFromElixir.Text = 1
                DayToElixir.Text = Day

            End If

        Else

            If Day <= 5 Then
                DayFromElixir.Text = 1
                DayToElixir.Text = Day
            Else
                DayFromElixir.Text = Day - 5
                DayToElixir.Text = Day
            End If

        End If

    End Sub

    Private Sub YearElixir_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles YearElixir.MouseDoubleClick

        YearElixir.Clear()

    End Sub

    Private Sub MonthElixir_From_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles MonthElixir_From.MouseDoubleClick

        MonthElixir_From.Clear()

    End Sub

    Private Sub MonthElixir_To_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles MonthElixir_To.MouseDoubleClick

        MonthElixir_To.Clear()

    End Sub

    Private Sub DayFromElixir_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles DayFromElixir.MouseDoubleClick

        DayFromElixir.Clear()

    End Sub

    Private Sub DayToElixir_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles DayToElixir.MouseDoubleClick

        DayToElixir.Clear()

    End Sub

    Private Sub Chckb_H_Checked(sender As Object, e As RoutedEventArgs)

        If ChckB.IsChecked Then

            MonthElixir_From.Visibility = Visibility.Visible

        End If

    End Sub

    Private Sub ChckB_Unchecked(sender As Object, e As RoutedEventArgs) Handles ChckB.Unchecked

        MonthElixir_From.Visibility = Visibility.Hidden

        MonthElixir_From.Clear()

    End Sub

    Private Sub ChckB_Elixir_Checked(sender As Object, e As RoutedEventArgs)

        If ChckB_Elixir.IsChecked Then

            SearchElixir_II.Visibility = Visibility.Visible

        End If

    End Sub

    Private Sub ChckB_Elixir_Unchecked(sender As Object, e As RoutedEventArgs) Handles ChckB_Elixir.Unchecked

        SearchElixir_II.Visibility = Visibility.Hidden
        SearchElixir_II.Clear()
        SearchElixir_II.Text = "np._kwota_(opcjonalnie)"

    End Sub

    Private Sub SearchElixir_I_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SearchElixir_I.TextChanged

        SearchElixir_I.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\S])")
        SearchElixir_I.Text = digitsOnly.Replace(SearchElixir_I.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub SearchElixir_II_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SearchElixir_II.TextChanged

        SearchElixir_II.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\S])")
        SearchElixir_II.Text = digitsOnly.Replace(SearchElixir_II.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub Search_ELX_TextChanged(sender As Object, e As TextChangedEventArgs) Handles Search_ELX.TextChanged

        Search_ELX.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\S])|([^ELX#\\|\/\d]*$)")
        Search_ELX.Text = digitsOnly.Replace(Search_ELX.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub SearchElixir_I_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles SearchElixir_I.MouseUp

        ' Clear field for SearchElixir when MouseUp
        SearchElixir_I.Clear()

    End Sub

    Private Sub SearchElixir_II_MouseUp(sender As Object, e As MouseButtonEventArgs) Handles SearchElixir_II.MouseUp

        ' Clear field for SearchElixir when MouseUp
        SearchElixir_II.Clear()

    End Sub

    Private Sub Home_Click(sender As Object, e As RoutedEventArgs) Handles Home.Click

        ' ElixirFinder visibility for MouseDoubleClick
        ElixirFinder.Visibility = Visibility.Collapsed
        ' Home visibility for MouseDoubleClick
        Main_Home.Visibility = Visibility.Visible

    End Sub

End Class