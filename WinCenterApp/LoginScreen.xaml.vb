Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Threading
Imports System.ComponentModel

Public Class LoginScreen
    Dim WithEvents BackgroundWorker1 As BackgroundWorker
    Public Shared TxtString As String
    Public Shared username As String

    Dim conn As SqlConnection 'Dim connection As New SqlClient.SqlConnection
    Dim cmd As SqlCommand     'Dim command As New SqlClient.SqlCommand
    'Dim adapter As New SqlClient.SqlDataAdapter
    'Dim dataset As New DataSet
    Dim dr As SqlDataReader
    Dim X As Integer = 0
    Dim MainWindow = New MainWindow()
    Dim MsgCritical = New MsgCritical() 'create construktor
    Dim MsgCheck = New MsgCheck()
    Dim ProgresBarAnimation = New ProgresBarAnimation()
    Dim inprocent As Integer = 1
    Dim AdminWin = New AdminWin()
    'Dim TypeUser As String
    'Dim Type As String

    'Public UserInput As String
    'Dim DispatcherTimer_Tick = New DispatcherTimer_Tick()
    'Dim ProgresBarAnimation = New ProgresBarAnimation()

    Sub Connectdb()


        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"
        'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
               Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        'Dim newdir As Object = files

        Dim value As String = String.Join("", files) & ".mdf"

        conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30") 'connection.ConnectionString = ("Data Source=MYKDONALD-PC\SQLEXPRESS;Initial Catalog=Data.mdf;Integrated Security=True")
        conn.Open()

    End Sub

    Private Sub BtnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSubmit.Click

        Call Connectdb()

        'TypeUser = ChoiseTypeUser.Text

        'If TypeUser = "Admin" Then
        '    Type = "Admin"
        'ElseIf TypeUser = "User" Then
        '    Type = "User"
        'End If

        cmd = New SqlCommand("SELECT * FROM [User] WHERE Username COLLATE Latin1_General_CS_AS ='" & txtUsername.Text & "'AND Password COLLATE Latin1_General_CS_AS =CONVERT(VARCHAR(50),HashBytes('MD5','" & txtPassword.Password & "'),2) AND TypeUser COLLATE Latin1_General_CS_AS ='" & ChoiseTypeUser.Text & "'", conn)
        'command.CommandText = "SELECT COUNT (*) FROM [User] WHERE Username= '" & txtUsername.Text & "'AND Password='" & txtPassword.Password & "';"
        'connection.Open()

        dr = cmd.ExecuteReader
        dr.Read()

        'command.Connection = connection

        'adapter.SelectCommand = command

        'adapter.Fill(dataset, "0")

        'Dim count = dataset.Tables(0).Rows.Count

        If dr.HasRows Then 'If count > 0 Then

            'Me.BackgroundWorker1 = New BackgroundWorker
            ''Enable progress reporting
            'BackgroundWorker1.WorkerReportsProgress = True
            ''Set the progress state as "normal"
            'TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Normal
            ''Start the work 
            'BackgroundWorker1.RunWorkerAsync()
            ''DoWork Event occurs
            ''Now control will goes to worker_DoWork Sub because it handles the DoWork Event

            'If dr.Item("Password") = "" Then '<> txtPassword.Password Then

            '    MsgBox("Incorrect login please check Username Or Password!", MsgBoxStyle.Critical, "Error")

            'Exit Sub

            'Else

            'If MsgCheck Is Nothing Then
            Ps.Foreground = Brushes.Green
            Us.Foreground = Brushes.Green

            'Dim dt As DispatcherTimer = New DispatcherTimer()
            'AddHandler dt.Tick, AddressOf DispatcherTimer_Tick
            'dt.Interval = New TimeSpan(0, 0, 0, 0, 5)
            'dt.Start()

            'Call DispatcherTimer_Tick()

            'Dim progbar As New ProgressBar()

            'progbar.Background = Brushes.Gray

            'progbar.Foreground = Brushes.Green

            'progbar.Width = 150

            'progbar.Height = 15

            'Dim duration As New Duration(TimeSpan.FromMilliseconds(2000))

            'Dim doubleanimation As New Animation.DoubleAnimation(100.0, duration)

            'doubleanimation.RepeatBehavior = New Animation.RepeatBehavior(1)

            'progbar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation)

            'sbar.Items.Add(progbar)
            'System.Threading.Thread.Sleep(New TimeSpan(0, 0, 3))
            If ChoiseTypeUser.Text = "User" Then
                'Call Load()
                Me.Hide()
                ProgresBarAnimation.Show()
                Call Load()
                'MsgCheck.Show()
                Call Main()

            Else

                Me.Hide()
                ProgresBarAnimation.Show()
                Call Load()
                'MsgCheck.Show()
                AdminWin.Show()

            End If

            'Do While ProgressBar1.Value < 100
            '    While ProgressBar1.Value < 100
            '        ProgressBar1.Value += 1    ' Add 1 to the current value
            '    End While
            '    If ProgressBar1.Value = 100 Then

            'Me.Close() 

            'ProgresBarAnimation.Show()
            'MsgCheck.Show() 'MsgBox("You are Now Logged In", MsgBoxStyle.Information, "Login")

            'Threading.Thread.Sleep(2000)

            'Call Main()
            'Me.Hide()
            '        End If
            '        'End If
            '    Loop
            'Else

            'Char.IsLetter(txtUsername.Text) 'tylko litery
            'If Char.IsLetter(txtUsername.Text) = False Then
            '    txtUsername.Clear()
            '    txtUsername.Focus()
            'End If
        Else

                If txtUsername.Text = "" And txtPassword.Password = "" Then

                TxtString = "Pole 'Username' oraz 'Password' jest puste!" & vbNewLine & "Wprowadź nazwę użykownika oraz hasło!"

                Us.Foreground = Brushes.Red
                Ps.Foreground = Brushes.Red

                MsgCritical.Show()
                txtUsername.Focus()

                Exit Sub

            Else

                If txtUsername.Text = "" Then

                    TxtString = "Pole 'Username' jest puste!" & vbNewLine & "Wprowadź nazwę użykownika!"

                    Us.Foreground = Brushes.Red
                    Ps.Foreground = Brushes.White

                    MsgCritical.Show()
                    'txtPassword.Clear()
                    txtUsername.Focus()


                    Exit Sub

                Else

                    'Funkcja regex walidacja znaków pola Username pod kątem: małe litery - 2 szt. oraz cyfry - 5 szt.
                    If System.Text.RegularExpressions.Regex.IsMatch(txtUsername.Text, "([a-z]{2}[\d]{5})") Then

                        txtUsername.IsEnabled = False 'zablokowanie pola Username - poprawna walidacja loginu!

                        If txtPassword.Password = "" Then

                            TxtString = "Pole 'Password' jest puste!" & vbNewLine & "Wprowadź hasło!"

                            Ps.Foreground = Brushes.Red
                            Us.Foreground = Brushes.White

                            MsgCritical.Show()
                            'txtUsername.Clear()
                            txtPassword.Focus()

                            Exit Sub

                        Else

                            TxtString = "Nieprawidłowy login lub hasło!" & vbNewLine & "Wprowadź poprawne dane logowania!"

                            Us.Foreground = Brushes.Red
                            Ps.Foreground = Brushes.Red

                            MsgCritical.Show() 'MsgBox("Incorrect login please check Username Or Password!", MsgBoxStyle.Critical, "Error")
                            txtUsername.Clear()
                            txtPassword.Clear()
                            txtUsername.IsEnabled = True 'odblokowanie pola Username - niepoprawne walidacja loginu i hasła - kolejna z 3 prób!

                        End If

                    Else

                        TxtString = "Nieprawidłowy login!" & vbNewLine & "Wprowadź login w formacie: piXXXXX!"

                        Us.Foreground = Brushes.Yellow
                        Ps.Foreground = Brushes.White

                        MsgCritical.Show()
                        txtUsername.Clear()
                        txtPassword.Clear()
                        txtUsername.Focus()

                    End If

                End If

            End If

            'warunek 3 prób logowania i zakończenie programu
            X = X + 1
            If X >= 3 Then

                'timeout 1s  
                Threading.Thread.Sleep(1000)

                'nadanie zmiennej globalnej wartości string
                TxtString = "Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!"

                MsgCritical.Show() 'MsgBox("Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!", MsgBoxStyle.Critical, "Error")

                'zakończenie działania programu
                End

            End If

        End If


    End Sub

    Sub Main() 'wyświetla główne okna obsługi i ukrywa ekran logowania

        Me.Hide()
        MainWindow.Show()

        username = txtUsername.Text

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click 'obsługa button'a

        End

    End Sub

    Private Sub TxtUsername_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles txtUsername.KeyDown 'obsługa keypress klawitury dla pola Username

        If e.Key = Key.Return Then 'Key.enter, Key.return - obsługa tożsama
            txtPassword.Focus() 'zaznacz/przejdź do pola Password
            e.Handled = True
            'txtUsername.IsEnabled = False
        End If

    End Sub

    Private Sub TxtPassword_KeyDown(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles txtPassword.KeyDown 'obsługa keypress klawitury dla pola Password

        If e.Key = Key.Enter Then 'naciśnięcie Enter
            Call BtnSubmit_Click(sender, e)
            e.Handled = True
            'txtPassword.IsEnabled = False
        End If

    End Sub

    Private Sub Window_KeyDown_1(sender As Object, e As KeyEventArgs) 'obsługa keypress klawitury dla okna głównego

        If e.Key = Key.Escape Then 'naciśniecie Esc
            Call BtnCancel_Click(sender, e)
            e.Handled = True
        End If

    End Sub

    Private Sub TxtUsername_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtUsername.GotFocus 'to co się dzieje runtime
        'txtUsername.IsEnabled = False 'wyłączenie pola tekstowego
        txtUsername.Foreground = Brushes.WhiteSmoke 'kolor tekstu pisanego
        txtPassword.Foreground = Brushes.WhiteSmoke 'kolor tekstu pisanego

    End Sub

    'Private Sub BtnSubmit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

    '    'ProgressBar1.Items.Clear()

    '    'Dim lbl As New Label()

    '    'lbl.Background = New LinearGradientBrush(Colors.Pink, Colors.Red, 90)

    '    'lbl.Content = "Progress"

    '    'sbar.Items.Add(lbl)

    '    Dim progbar As New ProgressBar()

    '    progbar.Background = Brushes.Gray

    '    progbar.Foreground = Brushes.Green

    '    progbar.Width = 150

    '    progbar.Height = 15

    '    Dim duration As New Duration(TimeSpan.FromMilliseconds(2000))

    '    Dim doubleanimation As New Animation.DoubleAnimation(100.0, duration)

    '    doubleanimation.RepeatBehavior = New Animation.RepeatBehavior(1)

    '    progbar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation)

    '    sbar.Items.Add(progbar)

    'End Sub

    Sub Load()
        Cursor = Cursors.Wait

        System.Threading.Thread.Sleep(New TimeSpan(0, 0, 0, 0, 500))

        Cursor = Cursors.Arrow
    End Sub

    'Private Sub LReg_Click(sender As Object, e As RoutedEventArgs) Handles LReg.Click
    '    Dim AdminWin = New AdminWin()
    '    AdminWin.Show()
    'End Sub

    'Public Sub DispatcherTimer_Tick()
    '    Dim DelayTime As TimeSpan
    '    ProgressBar1.Value = inprocent
    '    procento.Content = "Postęp..." & inprocent & "%"
    '    inprocent = inprocent + 1
    '    Dim NewTime As DateTime = DateTime.Now.AddSeconds(2)
    '    Do While inprocent <> 100
    '        DelayTime = NewTime.Subtract(DateTime.Now)
    '        Threading.Thread.Sleep(50) ' simulate work
    '        If ProgressBar1 IsNot Nothing Then
    '            ProgressBar1.Value = 100
    '        End If
    '        inprocent += 1


    '        Delay(1)
    '        Dim duration As New Duration(TimeSpan.FromSeconds(10))
    '        Dim doubleanimation As New Animation.DoubleAnimation(100.0, duration)
    '        ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, doubleanimation)
    '    Loop

    '    If inprocent >= 100 Then

    '        inprocent = 100

    '        ProgressBar1.Value = 100
    '        procento.Content = "Postęp..." & ProgressBar1.Value & "%"

    '        Return True

    '    End If

    '    Return True

    'End Sub

    'Private Sub worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
    '    For i As Integer = 0 To 99 'Step 10
    '        System.Threading.Thread.Sleep(5)
    '        'Raises the ProgressChanged event passing the value
    '        CType(sender, System.ComponentModel.BackgroundWorker).ReportProgress(i)
    '        'Now control will goes to worker_ProgressChanged Sub because it handles the ProgressChanged Event
    '    Next i
    'End Sub

    'Private Sub worker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
    '    'Increment the value on progress bars in window
    '    ProgressBar1.Value = e.ProgressPercentage
    '    'Increment the value on progress bars in Taskbar
    '    procento.Content = inprocent & "%"
    '    inprocent = inprocent + 1
    '    TaskbarItemInfo.ProgressValue = CDbl(e.ProgressPercentage) / 100
    'End Sub

    '' Work completed
    'Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
    '    ProgressBar1.Value = 100
    '    TaskbarItemInfo.ProgressValue = 1.0
    '    'Set the progress state as "indeterminate"
    '    TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Indeterminate
    '    'display a message box and keep the result in variable result
    '    'Dim result = MessageBox.Show("The progress completed. Would you like to exit now?", "Message - Progress Completed", MessageBoxButton.YesNo)
    '    ''if result is Yes - Close the application
    '    'If result = MessageBoxResult.Yes Then End
    '    Call Load()
    '    MsgCheck.Show()
    '    Call Main()

    'End Sub

    ' This function removes invalid filename characters from a user input string.

    'Private Function RemoveInvalidFileNameChars(UserInput As String) As String

    '    For Each invalidChar In IO.Path.GetInvalidFileNameChars
    '        UserInput = UserInput.Replace(invalidChar, "")
    '    Next
    '    Return UserInput
    'End Function

    'Function IsValidFileNameOrPath(ByVal name As String) As Boolean
    '    ' Determines if the name is Nothing.
    '    If name Is Nothing Then
    '        Return False
    '    End If

    '    ' Determines if there are bad characters in the name.
    '    For Each badChar As Char In System.IO.Path.GetInvalidPathChars
    '        If InStr(name, badChar) > 0 Then
    '            Return False
    '        End If
    '    Next

    '    ' The name passes basic validation.
    '    Return True
    'End Function

End Class