Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class LoginScreen

    'Withevents
    Dim WithEvents BackgroundWorker1 As BackgroundWorker

    'Shared variable for TypeUser
    Public Shared TypeUserName As String

    'Shared variable's for comunication
    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()

    'Connetion variable's
    'Dim conn As SqlConnection
    Dim conn As New SqlClient.SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    'Other variable's
    Dim X As Integer = 0
    Public UserNameLog As String = Environment.UserName
    Dim ProgresBarAnimation = New ProgresBarAnimation()
    Dim inprocent As Integer = 1
    Dim flags As Boolean = False

    'Date variable's
    Dim regDate As DateTime = DateTime.Now
    Public curDate As String = regDate.ToString("yyyy-MM-dd") 'HH:mm:ss")
    Public Shared NewDate As Date = DateTime.Now.AddDays(30)
    Public Shared FDate As String = NewDate.ToString("yyyy-MM-dd")
    Public Shared expDate As String '= regexpDate.ToString("yyyy-MM-dd HH:mm:ss")

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        'Call ForceSingleInstanceApplication()

    End Sub

    'Public Sub ForceSingleInstanceApplication() 'Only run one instance app
    '    'Get a reference to the current process
    '    Dim MyProc As Process = Process.GetCurrentProcess

    '    'Check how many processes have the same name as the current process
    '    If (Process.GetProcessesByName(MyProc.ProcessName).Length > 1) Then
    '        'If there is more than one, it is already running
    '        MsgBox("Application is already running", MsgBoxStyle.Critical, My.Application.Info.Title) 'Reflection.Assembly.GetCallingAssembly().GetName().Name)
    '        ' Terminate this process and give the operating system the specified exit code.
    '        Environment.Exit(-2)
    '        Exit Sub
    '    End If
    'End Sub

    Sub Connectdb() 'Create connection with database

        Try

            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            location = New Uri(location).LocalPath & "\"
            'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

            Dim files As New List(Of String)
            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            Dim value As String = String.Join("", files) & ".mdf"

            'MsgBox(location & value)

            'conn = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Initial Catalog=" & location & value & "; Integrated Security=False; Trusted_Connection=Yes; Connect Timeout=30") 'Integrated Security=True; MultipleActiveResultSets=True;; Persist Security Info=False;
            conn.ConnectionString = ("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Initial Catalog=" & value & ";Connect Timeout=30") 'AttachDbFilename=" & location & value & ;User Id=alioruser;Password=aliorpass

            conn.Open() 'Open connetion

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

    End Sub

    Private Sub BtnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles btnSubmit.Click

        Call Connectdb() 'Open connection with db

        'Sql
        cmd = New SqlCommand("SELECT * FROM [User] WHERE Username COLLATE Latin1_General_CS_AS ='" & txtUsername.Text & "'AND Password COLLATE Latin1_General_CS_AS =CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & txtPassword.Password & "'),2) AND TypeUser COLLATE Latin1_General_CS_AS ='" & ChoiseTypeUser.Text & "'AND IsDelete = '" & flags & "'", conn)

        'Read db
        dr = cmd.ExecuteReader
        dr.Read()

        'Check pass and login
        If dr.HasRows Then

            'If everything all right green label brushes
            Ps.Foreground = Brushes.Green
            Us.Foreground = Brushes.Green

            If ChoiseTypeUser.Text = "Użytkownik" Then

                'USER

                TypeUserName = "Uzytkownik"

                WindowLS.IsEnabled = False 'block main window

                ProgresBarAnimation.Show()

                Call Load() 'loading timeout

                Call Main() 'next procedure

            Else
                'ADMIN

                TypeUserName = "Administrator"

                WindowLS.IsEnabled = False 'block main window

                ProgresBarAnimation.Show()

                Call Load() 'loading timeout

                'ExpiryDate for TypeUser
                expDate = dr.GetValue(6)

                'Close connection
                conn.Close()
                dr.Close()
                SqlConnection.ClearAllPools()

                'Check expirydate with currentdate
                If curDate >= expDate Then

                    Dim ChangePasswordByUser = New ChangePasswordByUser
                    ChangePasswordByUser.Show() 'Window changing password

                Else

                    Me.Close()

                    Dim AdminWin = New AdminWin()
                    AdminWin.Show() 'Admin windows

                End If

            End If

        Else

            'Close connection
            conn.Close()
            dr.Close()
            SqlConnection.ClearAllPools()

            If txtPassword.Password = "" Then

                TxtString_msginformation = "Pole 'hasła:' jest puste!" & vbNewLine & "Wprowadź hasło!"

                'Red label brushes - failed login
                Ps.Foreground = Brushes.Red
                Us.Foreground = Brushes.White

                MsgInformation.Show()

                TxtString_msginformation = Nothing
                txtPassword.Focus()

                Exit Sub

            Else

                TxtString_msginformation = "Nieprawidłowe hasło!" & vbNewLine & "Wprowadź ponownie poprawne hasło logowania!"

                Ps.Foreground = Brushes.Red 'failed login pass

                MsgInformation.Show() 'MsgBox("Incorrect login please check Username Or Password!", MsgBoxStyle.Critical, "Error")

                TxtString_msginformation = Nothing
                txtPassword.Clear()

                CountLog() 'Verification count login

            End If

        End If

    End Sub

    Sub CountLog()

        'Three login attempts
        X = X + 1

        If X >= 3 Then

            TxtString_msgcritical = "Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!"

            'timeout 1s  
            Threading.Thread.Sleep(1000)

            MsgCritical.Show() 'MsgBox("Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!", MsgBoxStyle.Critical, "Error")

            'Clean variable
            TxtString_msgcritical = Nothing

            'Exit program
            End

        End If

    End Sub

    Sub Load() 'loading timeout

        Cursor = Cursors.Wait

        System.Threading.Thread.Sleep(New TimeSpan(0, 0, 0, 0, 500))

        Cursor = Cursors.Arrow

    End Sub

    Sub Main() 'Main window for User

        'ExpiryDate for TypeUser
        expDate = dr.GetValue(6)

        'Close connection
        conn.Close()
        dr.Close()
        SqlConnection.ClearAllPools()

        'Check expirydate with currentdate
        If curDate >= expDate Then

            Dim ChangePasswordByUser = New ChangePasswordByUser
            ChangePasswordByUser.Show() 'Window changing password

        Else

            Me.Close()

            Dim MainWindow As New MainWindow()
            MainWindow.Show() 'Window for User

        End If

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click 'Cancel button

        'Check connection with db
        If conn Is Nothing Then

        Else

            'Close connection
            conn.Close()
            SqlConnection.ClearAllPools()

            If dr Is Nothing Then

            Else

                dr.Close()

            End If

        End If

            End 'End program

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

    Private Sub TxtUsername_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles txtUsername.MouseDoubleClick

        txtUsername.Clear()

    End Sub

    Private Sub TxtPassword_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles txtPassword.MouseDoubleClick

        txtPassword.Clear()

    End Sub

    'Private Sub txtUsername_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtUsername.TextChanged

    '    Dim digitsOnly As Regex = New Regex("([^\S])|([^pi\d]*$)|(^[\d]*$)|(^[^pi\D]*$)|(^i+)|([pi]{3})|(pp)|(^pi\d*\D)")
    '    txtUsername.Text = digitsOnly.Replace(txtUsername.Text, "")

    '    digitsOnly = Nothing

    'End Sub

    Private Sub Panel_Loaded(sender As Object, e As RoutedEventArgs)

        txtUsername.Text = UserNameLog 'Auto paste username from variable

    End Sub

    Private Sub MainAdmin_Click(sender As Object, e As RoutedEventArgs) Handles MainAdmin.Click

        Me.Close()

        Dim MainAdmin_0 = New MainAdmin()
        MainAdmin_0.Show() 'Window for global hide admin

    End Sub


End Class