Imports System.Data
Imports System.Data.SqlClient

Public Class ChangePasswordByUser

    ' Variable's to another object from class
    Dim ObjTypeUserFromLS As New LoginScreen
    Dim ObjCurDate As New LoginScreen
    Dim ObjExpDate As New LoginScreen
    Dim ObjFDate As New LoginScreen
    Dim exp As Boolean = False

    ' Variable's object with LoginScreen
#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
    Dim CurDateMW As String = ObjCurDate.curDate
    Dim ExpDateMw As String = ObjExpDate.expDate
    Dim TypeUser As String = ObjTypeUserFromLS.TypeUserName
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

    ' Variables cooperating with other windows - shared variable's for comunication
    Public Shared MsgCheck As New MsgCheck()
    Public Shared MsgCritical As New MsgCritical()
    Public Shared MsgInformation = New MsgInformation()
    Public Shared TxtString_msgcheck As String
    Public Shared TxtString_msgcritical As String
    Public Shared TxtString_msginformation As String

    Private Sub Password_Loaded(sender As Object, e As RoutedEventArgs) Handles Password.Loaded

        ' Label content - UserName
        LabelUser.Content = Environment.UserName

        ' Properties loaded window component
        RetypePassword_pass.IsEnabled = False
        NewPassword_pass.IsEnabled = False
        NewPassword_txt.IsEnabled = False
        ShowPass.IsEnabled = False

        ' Compare current date with expired date in database
        If CurDateMW >= ExpDateMw Then

            ChngPass.Content = "Twoje hasło wygasło i musi zostać zmienione! " & "(" & TypeUser & ")"

        Else

            ChngPass.Content = "Zmiana hasła! Wprowadź nowe hasło... " & "(" & TypeUser & ")"
            exp = True

        End If

    End Sub

    Private Sub ChangePass_Click(sender As Object, e As RoutedEventArgs)

        ' Variable location database
        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"
        'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

        ' Check all file which are database
        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        ' Adds name file as String
        Dim value As String = String.Join("", files) & ".mdf"

        ' Other variable for connetion to db
        Dim con As SqlConnection
        Dim cmd As SqlCommand

        ' Compare two passwords
        If NewPassword_pass.Password <> RetypePassword_pass.Password Then

            'MsgBox("Hasła nie pasują do siebie!")

            TxtString_msginformation = "Hasła nie pasują do siebie!"

            MsgInformation.Show() 'Information message

            TxtString_msginformation = Nothing

            ' Clean variable
            NewPassword_pass.Clear()
            RetypePassword_pass.Clear()
            NewPassword_txt.Clear()
            RetypePassword_txt.Clear()

            ' Focus textbox
            NewPassword_pass.Focus()
            NewPassword_txt.Focus()

            ' Brush label
            LblNewPass.Foreground = Brushes.Red
            LblConfirmPass.Foreground = Brushes.Red

            Exit Sub

        End If

        ' New sqlconntection with database
        con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")
        Dim query As String = "UPDATE [User] SET Password = CONVERT(VARCHAR(50),HashBytes('SHA2_512', @Password),2), ExpiryDate = @FutureDate WHERE Username = @Username And TypeUser = @TypeUser"

        ' Open conntection with database
        con.Open()

        ' Check database and save as changed
        cmd = New SqlCommand(query, con)
#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = LabelUser.Content
        cmd.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = ObjTypeUserFromLS.TypeUserName
        cmd.Parameters.Add("@FutureDate", SqlDbType.VarChar).Value = ObjFDate.FDate
        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = RetypePassword_pass.Password
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

        If OldPassword.Password = "" Then

            'MsgBox("Hasło musi zawierać min. 6 znaków!")

            TxtString_msginformation = "Hasło musi zawierać min. 6 znaków!"

            MsgInformation.Show() 'Information message

            TxtString_msginformation = Nothing

            LblOldPass.Foreground = Brushes.Red

            OldPassword.Focus()

            ' Close connetion with database
            con.Close()

            Exit Sub

        Else

            ' Save changes
            cmd.ExecuteNonQuery()

            ' Close connetion with database
            con.Close()

        End If

        ' Content for msgbox
        TxtString_msgcheck = "Hasło zostało zmienione!"

        ' Show msgbox
        MsgCheck.Show()
        TxtString_msgcheck = Nothing

        ' Close acctually window
        Me.Close()

        '--------------------------------------------------------------------------------------------------------------------------------------

        ' Properties for window
        Cl.IsEnabled = True
        Quit.IsEnabled = True

#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        Dim TypeUser As String = ObjTypeUserFromLS.TypeUserName
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

        If ChngPass.Content = "Zmiana hasła! Wprowadź nowe hasło... " & "(" & TypeUser & ")" Then

            ' Nothing

        Else

            ' Show Admin or User panel
            If TypeUser = "Administrator" Then

                For count As Integer = My.Application.Windows.Count - 1 To 1 Step -1

                    My.Application.Windows(count).Close() 'Close all windows

                Next

                Dim ObjWinADM As New AdminWin
                ObjWinADM.Show()

            ElseIf TypeUser = "Uzytkownik" Then

                'For count As Integer = My.Application.Windows.Count - 1 To 1 Step -1

                My.Application.Windows(3).Close()

                'Next

                Dim ObjWinMW As New MainWindow
                ObjWinMW.Show()

            End If

        End If

        ' Close connetion with database
        con.Close()

    End Sub

    Public Function CompareValues_OldPassword() ' Compare new password with password from database

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        Dim TypeUser As String = ObjTypeUserFromLS.TypeUserName
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        con.Open()

        Dim sSql As String = "Select Username, TypeUser, Password From [User] Where Username = @Username And TypeUser = @TypeUser And Password COLLATE Latin1_General_CS_AS =CONVERT(VARCHAR(50),HashBytes('SHA2_512', @Password),2)"

        Using Command As New SqlCommand(sSql, con)
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = LabelUser.Content
            Command.Parameters.Add("@Password", SqlDbType.VarChar).Value = OldPassword.Password
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = TypeUser
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Private Sub NewPassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles NewPassword_pass.PasswordChanged

        ' Chceck length password
        If NewPassword_pass.Password.Length <= 5 Then

            ' Clear and disabled retype textbox
            RetypePassword_pass.IsEnabled = False
            RetypePassword_pass.Clear()

            ' Brush label
            LblNewPass.Foreground = Brushes.White

        Else

            ' Brush label
            LblNewPass.Foreground = Brushes.Green

            ' Enabled retype textbox
            RetypePassword_pass.IsEnabled = True

        End If

    End Sub

    Private Sub NewPassword_1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles NewPassword_txt.TextChanged

        ' The same as above
        If NewPassword_txt.Text.Length <= 5 Then

            RetypePassword_txt.IsEnabled = False
            RetypePassword_txt.Clear()

            LblNewPass.Foreground = Brushes.White

        Else

            LblNewPass.Foreground = Brushes.Green

            RetypePassword_txt.IsEnabled = True

        End If

    End Sub

    Private Sub ShowPass_Checked(sender As Object, e As RoutedEventArgs)

        ' Visability properties 
        NewPassword_pass.Visibility = Visibility.Hidden
        NewPassword_txt.Visibility = Visibility.Visible
        NewPassword_txt.Text = NewPassword_pass.Password

        If NewPassword_pass.Password.Length <= 5 Then

            RetypePassword_pass.Visibility = Visibility.Hidden
            RetypePassword_txt.Visibility = Visibility.Visible
            RetypePassword_txt.Text = RetypePassword_pass.Password
            RetypePassword_txt.IsEnabled = False

        Else

            RetypePassword_pass.Visibility = Visibility.Hidden
            RetypePassword_txt.Visibility = Visibility.Visible
            RetypePassword_txt.Text = RetypePassword_pass.Password
            RetypePassword_txt.IsEnabled = True

        End If

    End Sub

    Private Sub ShowPass_Unchecked(sender As Object, e As RoutedEventArgs) Handles ShowPass.Unchecked

        ' The same as above
        NewPassword_pass.Visibility = Visibility.Visible
        NewPassword_txt.Visibility = Visibility.Hidden
        NewPassword_pass.Password = NewPassword_txt.Text

        If NewPassword_pass.Password.Length <= 5 Then

            RetypePassword_pass.Visibility = Visibility.Visible
            RetypePassword_txt.Visibility = Visibility.Hidden
            RetypePassword_pass.Password = RetypePassword_txt.Text
            RetypePassword_txt.IsEnabled = False

        Else

            RetypePassword_pass.Visibility = Visibility.Visible
            RetypePassword_txt.Visibility = Visibility.Hidden
            RetypePassword_pass.Password = RetypePassword_txt.Text
            RetypePassword_txt.IsEnabled = True

        End If

    End Sub

    Private Sub OldPassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles OldPassword.LostFocus

        ' Variable string for compare two variable - boolen type as string
        Dim OldPass As String = CompareValues_OldPassword()

        ' Textbox is empty
        If OldPassword.Password = "" Then

            Exit Sub

        Else

            ' Compare two variable 
            If OldPass = True Then

                ' Properties when true
                LblOldPass.Foreground = Brushes.Green
                NewPassword_pass.IsEnabled = True
                NewPassword_txt.IsEnabled = True
                ShowPass.IsEnabled = True

            Else

                ' Properties when false
                LblOldPass.Foreground = Brushes.Red

                'MsgBox("Bieżące hasło nie jest tożsame z tym w bazie! Wprowadź poprawnie bieżące hasło!")

                TxtString_msginformation = "Bieżące hasło nie jest tożsame z tym w bazie! Wprowadź poprawnie bieżące hasło!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                'OldPassword.Focus() ' not required

                ' Disabled textbox
                NewPassword_pass.IsEnabled = False
                NewPassword_txt.IsEnabled = False
                ShowPass.IsEnabled = False

                ' Clear variable
                OldPassword.Clear()
                NewPassword_pass.Clear()
                NewPassword_txt.Clear()

            End If

        End If

    End Sub

    Private Sub RetypePassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles RetypePassword_pass.PasswordChanged

        If RetypePassword_pass.Password.Length <= 5 Then

            LblConfirmPass.Foreground = Brushes.White

        Else

            LblConfirmPass.Foreground = Brushes.Green


        End If

    End Sub

    Private Sub RetypePassword_1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles RetypePassword_txt.TextChanged

        If RetypePassword_txt.Text.Length <= 5 Then

            LblConfirmPass.Foreground = Brushes.White

        Else

            LblConfirmPass.Foreground = Brushes.Green

        End If

    End Sub

    Private Sub NewPassword_1_LostFocus(sender As Object, e As RoutedEventArgs) Handles NewPassword_txt.LostFocus

        If NewPassword_txt.Text = "" Then

            Exit Sub

        Else

            If NewPassword_txt.Text = OldPassword.Password Then

                'MsgBox("Hasło nie może być tożsme z bieżący hasłem, podaj inne hasło!")

                TxtString_msginformation = "Hasło nie może być tożsme z bieżący hasłem, podaj inne hasło!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                LblNewPass.Foreground = Brushes.Red

                'NewPassword_1.Focus() ' not required

                NewPassword_txt.Clear()

                Exit Sub

            End If

        End If

        NewPassword_pass.Password = NewPassword_txt.Text

    End Sub

    Private Sub RetypePassword_1_LostFocus(sender As Object, e As RoutedEventArgs) Handles RetypePassword_txt.LostFocus

        RetypePassword_pass.Password = RetypePassword_txt.Text

    End Sub

    Private Sub NewPassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles NewPassword_pass.LostFocus

        If NewPassword_pass.Password = "" Then

            Exit Sub

        Else

            If NewPassword_pass.Password = OldPassword.Password Then

                'MsgBox("Hasło nie może być tożsme z bieżący hasłem, podaj inne hasło!")

                TxtString_msginformation = "Hasło nie może być tożsme z bieżący hasłem, podaj inne hasło!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                LblNewPass.Foreground = Brushes.Red

                'NewPassword.Focus() ' not required

                NewPassword_pass.Clear()

                Exit Sub

            End If
        End If

    End Sub

    Private Sub OldPassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles OldPassword.PasswordChanged

        If OldPassword.Password.Length <= 5 Then

            NewPassword_pass.IsEnabled = False
            NewPassword_txt.IsEnabled = False
            ShowPass.IsEnabled = False

            LblNewPass.Foreground = Brushes.White

        Else

            LblOldPass.Foreground = Brushes.Yellow

            NewPassword_pass.IsEnabled = True
            NewPassword_txt.IsEnabled = True
            ShowPass.IsEnabled = True

        End If

    End Sub

    Private Sub OldPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles OldPassword.KeyDown

        If e.Key = Key.Enter Or e.Key = Key.Tab Then ' press Enter or tab
            NewPassword_pass.Focus()
            e.Handled = True
        End If

    End Sub

    Private Sub NewPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles NewPassword_pass.KeyDown

        If e.Key = Key.Enter Or e.Key = Key.Tab Then ' press Enter or tab
            ShowPass.Focus()
            e.Handled = True
        End If

    End Sub

    Private Sub ShowPass_KeyDown(sender As Object, e As KeyEventArgs) Handles ShowPass.KeyDown

        If e.Key = Key.Enter Or e.Key = Key.Tab Then ' press Enter or tab

            If ShowPass.IsChecked = False Then

                RetypePassword_pass.Focus()

            Else

                RetypePassword_txt.Focus()

            End If

            e.Handled = True

        End If

    End Sub

    Private Sub RetypePassword_KeyDown(sender As Object, e As KeyEventArgs) Handles RetypePassword_pass.KeyDown

        If e.Key = Key.Enter Then ' press Enter
            Call ChangePass_Click(sender, e)
            e.Handled = True
        End If

    End Sub

    Private Sub NewPassword_1_KeyDown(sender As Object, e As KeyEventArgs) Handles NewPassword_txt.KeyDown

        If e.Key = Key.Enter Or e.Key = Key.Tab Then ' press Enter or tab
            ShowPass.Focus()
            e.Handled = True
        End If

    End Sub

    Private Sub RetypePassword_1_KeyDown(sender As Object, e As KeyEventArgs) Handles RetypePassword_txt.KeyDown

        If e.Key = Key.Enter Then ' press Enter or tab
            Call ChangePass_Click(sender, e)
            e.Handled = True
        End If

    End Sub

    Private Sub ChngPassWin_Loaded(sender As Object, e As RoutedEventArgs) Handles ChngPassWin.Loaded

        ' Variable - cooperate with other window
        Dim CurDateMW As String = ObjCurDate.curDate

#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        Dim ExpDateMw As String = ObjExpDate.expDate
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

        ' Compare to date from database
        If CurDateMW >= ExpDateMw Then

            ' Properties for window component - disabled component
            Cl.IsEnabled = False
            Quit.IsEnabled = False

        End If

    End Sub

    Private Sub Quit_Click(sender As Object, e As RoutedEventArgs)

        Me.Close()

    End Sub

    Private Sub X_Click(sender As Object, e As RoutedEventArgs) Handles Cl.Click

        'If exp = False Then

        '    ' Close window
        '    Me.Close() 'Me.Hide

        'End If

        Me.Close()

    End Sub


End Class
