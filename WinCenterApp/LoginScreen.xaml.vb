Imports System
Imports System.Data
Imports System.Data.SqlClient
'Imports System.Windows.Threading
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class LoginScreen

    Dim WithEvents BackgroundWorker1 As BackgroundWorker

    Public Shared TypeUserName As String

    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()

    Public UserNameLog As String = Environment.UserName

    Dim conn As SqlConnection 'Dim connection As New SqlClient.SqlConnection
    Dim cmd As SqlCommand     'Dim command As New SqlClient.SqlCommand
    Dim dr As SqlDataReader
    Dim X As Integer = 0
    'Dim MainWindow As New MainWindow()
    'Dim MainAdmin_0 = New MainAdmin()
    Dim ProgresBarAnimation = New ProgresBarAnimation()
    Dim inprocent As Integer = 1

    Dim regDate As DateTime = DateTime.Now
    Public Shared NewDate As Date = DateTime.Now.AddDays(30)
    Public Shared FDate As String = NewDate.ToString("yyyy-MM-dd")
    Public curDate As String = regDate.ToString("yyyy-MM-dd") 'HH:mm:ss")
    Public Shared expDate As String '= regexpDate.ToString("yyyy-MM-dd HH:mm:ss")
    Dim flags As Boolean = False

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        'MainWindow.Hide()

        'WindowLS.IsEnabled = True
        'WindowLS.WindowStyle = WindowStyle.ThreeDBorderWindow  

        'Call ForceSingleInstanceApplication()

        'Dim SignUp = New SignUp()

        'Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        'location = New Uri(location).LocalPath & "\"

        'Dim files As New List(Of String)
        'files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
        '           Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        'Dim value As String = String.Join("", files) & ".mdf"

        'Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        'Dim con As New SqlConnection(str)
        'Dim table As New DataTable("Table")
        'Dim username As DataRow = table.NewRow

        'Dim com As String = "Select Id, Username, TypeUser from [User]"

        'Dim Adpt As New SqlDataAdapter(com, con)

        'Dim ds As DataSet = New DataSet()

        'Adpt.Fill(ds, "User")

        'Dim TableCount As Integer = ds.Tables(0).Rows.Count
        'Dim InfoCompareValues_2 As Boolean = CompareValues_2()
        'Dim InfoCompareValues_1 As Boolean = CompareValues_1()

        'con.Close()

        'If TableCount < 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = False Then ' ok

        '    TypeUserName = "Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    MsgBox("Dodaj przynajmniej jednego administratora i użytkownika!")

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount < 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = True Then 'ok


        '    TypeUserName = "Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount < 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = False Then

        '    TypeUserName = "Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount >= 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = False Then

        '    TypeUserName ="Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    MsgBox("Dodaj przynajmniej jednego administratora i użytkownika!")

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount >= 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = True Then'ok

        '    TypeUserName = "Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount >= 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = False Then

        '    TypeUserName = "Administrator"

        '    txtPassword.IsEnabled = False

        '    con.Close()

        '    SignUp.Show()
        '    Me.Hide()

        'ElseIf TableCount >= 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = True Then 'ok

        '    txtPassword.IsEnabled = True

        'End If

    End Sub

    'Public Sub ForceSingleInstanceApplication()
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

    'Public Function CompareValues_1()

    '    Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    '    location = New Uri(location).LocalPath & "\"

    '    Dim files As New List(Of String)
    '    files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
    '               Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

    '    Dim value As String = String.Join("", files) & ".mdf"

    '    Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
    '    Dim con As New SqlConnection(str)
    '    con.Open()

    '    Dim sSql As String = "Select Username, TypeUser From [User] Where Username = @Username And TypeUser = @TypeUser and IsDelete=@IsDelete"

    '    Using Command As New SqlCommand(sSql, con)
    '        Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = UserNameLog
    '        Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Uzytkownik"
    '        Command.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = False
    '        Dim Reader As SqlDataReader = Command.ExecuteReader()
    '        Return Reader.HasRows()

    '    End Using

    'End Function

    'Public Function CompareValues_2()

    '    Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    '    location = New Uri(location).LocalPath & "\"

    '    Dim files As New List(Of String)
    '    files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
    '               Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

    '    Dim value As String = String.Join("", files) & ".mdf"

    '    Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
    '    Dim con As New SqlConnection(str)
    '    con.Open()

    '    Dim sSql As String = "Select Username, TypeUser From [User] Where Username = @Username And TypeUser = @TypeUser and IsDelete=@IsDelete"

    '    Using Command As New SqlCommand(sSql, con)
    '        Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = UserNameLog
    '        Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Administrator"
    '        Command.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = False
    '        Dim Reader As SqlDataReader = Command.ExecuteReader()
    '        Return Reader.HasRows()

    '    End Using

    'End Function

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

        cmd = New SqlCommand("SELECT * FROM [User] WHERE Username COLLATE Latin1_General_CS_AS ='" & txtUsername.Text & "'AND Password COLLATE Latin1_General_CS_AS =CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & txtPassword.Password & "'),2) AND TypeUser COLLATE Latin1_General_CS_AS ='" & ChoiseTypeUser.Text & "'AND IsDelete = '" & flags & "'", conn)

        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then 'If count > 0 Then

            Ps.Foreground = Brushes.Green
            Us.Foreground = Brushes.Green

            If ChoiseTypeUser.Text = "Użytkownik" Then

                TypeUserName = "Uzytkownik"

                conn.Close()
                'Call Load()
                'Me.Hide()
                WindowLS.IsEnabled = False
                'WindowLS.WindowStyle = WindowStyle.None

                ProgresBarAnimation.Show()
                Call Load()
                'MsgCheck.Show()
                Call Main()

            Else

                TypeUserName = "Administrator"

                conn.Close()

                WindowLS.IsEnabled = False

                'Me.Close()
                ProgresBarAnimation.Show()
                Call Load()
                'MsgCheck.Show()

                Call Connectdb()

                cmd = New SqlCommand("Select * from [User] Where Username COLLATE Latin1_General_CS_AS ='" & txtUsername.Text & "'AND TypeUser COLLATE Latin1_General_CS_AS ='" & ChoiseTypeUser.Text & "'", conn)
                dr = cmd.ExecuteReader

                dr.Read()

                expDate = dr.GetValue(6)

                conn.Close()

                If curDate >= expDate Then

                    Dim ChangePasswordByUser = New ChangePasswordByUser

                    ChangePasswordByUser.Show()

                Else

                    Me.Close()
                    Dim AdminWin = New AdminWin()

                    AdminWin.Show()

                End If

            End If

        Else


            If txtPassword.Password = "" Then

                TxtString_msginformation = "Pole 'hasła:' jest puste!" & vbNewLine & "Wprowadź hasło!"

                Ps.Foreground = Brushes.Red
                Us.Foreground = Brushes.White

                MsgInformation.Show()

                TxtString_msginformation = Nothing
                'txtUsername.Clear()
                txtPassword.Focus()
                conn.Close()

                Exit Sub

            Else

                TxtString_msginformation = "Nieprawidłowe hasło!" & vbNewLine & "Wprowadź ponownie poprawne hasło logowania!"

                Ps.Foreground = Brushes.Red

                MsgInformation.Show() 'MsgBox("Incorrect login please check Username Or Password!", MsgBoxStyle.Critical, "Error")

                TxtString_msginformation = Nothing
                txtPassword.Clear()
                conn.Close()

                CountLog()

            End If

        End If

    End Sub

    Sub CountLog()

        'warunek 3 prób logowania i zakończenie programu
        X = X + 1
        If X >= 3 Then

            'nadanie zmiennej globalnej wartości string
            TxtString_msgcritical = "Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!"

            'timeout 1s  
            Threading.Thread.Sleep(1000)

            MsgCritical.Show() 'MsgBox("Przekroczona liczba błędnych logowań - aplikacja zostanie zamknięta!", MsgBoxStyle.Critical, "Error")

            'Clean variable
            TxtString_msgcritical = Nothing

            'zakończenie działania programu
            End

        End If

        'Close connection
        conn.Close()

    End Sub

    Sub Main() 'wyświetla główne okna obsługi i ukrywa ekran logowania 

        Call Connectdb()

        cmd = New SqlCommand("Select * from [User] Where Username COLLATE Latin1_General_CS_AS ='" & txtUsername.Text & "'AND TypeUser COLLATE Latin1_General_CS_AS ='" & ChoiseTypeUser.Text & "'", conn)
        dr = cmd.ExecuteReader

        dr.Read()

        expDate = dr.GetValue(6)

        conn.Close()

        If curDate >= expDate Then

            Dim ChangePasswordByUser = New ChangePasswordByUser
            'Dim MainWindow As New MainWindow()

            ChangePasswordByUser.Show()

            'Window.IsEnabled = False

            'Me.Close()
            'MainWindow.Show()

        Else
            Dim MainWindow As New MainWindow()
            Me.Close()
            MainWindow.Show()

        End If

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click 'obsługa button'a

        If conn Is Nothing Then

        Else

            conn.Close()

        End If

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

    Sub Load()

        Cursor = Cursors.Wait

        System.Threading.Thread.Sleep(New TimeSpan(0, 0, 0, 0, 500))

        Cursor = Cursors.Arrow

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

        txtUsername.Text = UserNameLog

    End Sub

    Private Sub MainAdmin_Click(sender As Object, e As RoutedEventArgs) Handles MainAdmin.Click

        Me.Close()
        Dim MainAdmin_0 = New MainAdmin()

        MainAdmin_0.Show()

    End Sub


End Class