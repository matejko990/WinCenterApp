Option Strict On
Option Explicit On
Partial Public Class MsgCheck

    Shared mWpfMessageBoxResult As WpfMessageBoxResult

    Public Overloads Shared Function Show() As WpfMessageBoxResult
        Dim msg As New MsgCheck()
        msg.ShowDialog()
        Return mWpfMessageBoxResult
    End Function

    Private Sub X_Click(sender As Object, e As RoutedEventArgs) Handles Cl.Click
        'Dim LoginScreen = New LoginScreen()
        'Dim MainWindow = New MainWindow()

        Me.Hide()

        'LoginScreen.Close()
        'MainWindow.Show()

    End Sub

    Private Sub Ok_Click(sender As Object, e As RoutedEventArgs) Handles Okey.Click

        'Dim LoginScreen = New LoginScreen()
        'Dim MainWindow = New MainWindow()
        'Dim ProgresBarAnimation = New ProgresBarAnimation()

        Me.Hide()

        'ProgresBarAnimation.Show()

        'LoginScreen.Close()
        'MainWindow.Show()

    End Sub

    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            Call Ok_Click(sender, e)
            e.Handled = True
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Dim objchckCPBU As New ChangePasswordByUser
        Dim objchckSU As New SignUp

        'ładowanie elementów startu
#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        If objchckCPBU.TxtString_msgcheck = "" Then

            txt.Text = objchckSU.TxtString_msgcheck

        ElseIf objchckSU.TxtString_msgcheck = "" Then

            txt.Text = objchckCPBU.TxtString_msgcheck

        End If
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

    End Sub
End Class
