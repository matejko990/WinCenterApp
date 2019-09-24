Public Class MainAdmin

    ' Shared variable's for comunication
    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()
    'Public Shared SignUp = New SignUp()

    'Other variable's
    Dim X As Integer = 0

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        txtUsername.Text = "alior_rrb"

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click

        End ' Exit program

    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As RoutedEventArgs) Handles btnSubmit.Click

        If txtUsername.Text = "alior_rrb" And txtPassword.Password = "admin_rrb" Then

            Ps.Foreground = Brushes.Green

            Me.Close()

            Dim SignUp = New SignUp()
            SignUp.Show() 'Registraion window

            txtPassword.Clear() ' czyszczenie pola
            Ps.Foreground = Brushes.White

        ElseIf txtUsername.Text <> "alior_rrb" Or txtPassword.Password <> "admin_rrb" Then

            TxtString_msginformation = "Podaj prawidłowe hasło!"

            MsgInformation.Show() ' Information message

            TxtString_msginformation = Nothing

            Ps.Foreground = Brushes.Red

            txtPassword.Password = Nothing

            CountLog() 'Check count login

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

            'zakończenie działania programu
            End

        End If

        'Clean variable
        TxtString_msgcritical = Nothing

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

    Private Sub TxtPassword_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles txtPassword.MouseDoubleClick

        txtPassword.Clear()

    End Sub

End Class
