Public Class MsgInformation

    'Option Strict On
    'Option Explicit On

    Shared mWpfMessageBoxResult As WpfMessageBoxResult

        Public Overloads Shared Function Show() As WpfMessageBoxResult
        Dim msg As New MsgInformation()
        msg.ShowDialog()
            Return mWpfMessageBoxResult
        End Function

        Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
            'txt.Text = "tutorialspont.com"

            Dim objcrcLS As New LoginScreen
            Dim objcrcSU As New SignUp
            Dim objcrcMA As New MainAdmin
            Dim objcrcCPBU As New ChangePasswordByUser
            Dim objcrcAW As New AdminWin

#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        If objcrcMA.TxtString_msginformation <> "" Then

            txt.Text = objcrcMA.TxtString_msginformation

            Exit Sub

        ElseIf objcrcLS.TxtString_msginformation <> "" Then

            txt.Text = objcrcLS.TxtString_msginformation

            Exit Sub

        ElseIf objcrcSU.TxtString_msginformation <> "" Then

            txt.Text = objcrcSU.TxtString_msginformation

            Exit Sub

        ElseIf objcrcCPBU.TxtString_msginformation <> "" Then

            txt.Text = objcrcCPBU.TxtString_msginformation

            Exit Sub

        ElseIf objcrcAW.TxtString_msginformation <> "" Then

            txt.Text = objcrcAW.TxtString_msginformation

                Exit Sub

            End If
#Enable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

    End Sub

        Private Sub X_Click(sender As Object, e As RoutedEventArgs) Handles Cl.Click

            Me.Hide()

        End Sub

        Private Sub Ok_Click(sender As Object, e As RoutedEventArgs) Handles Okey.Click

            Me.Hide()

        End Sub

        Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
            If e.Key = Key.Enter Then
                Call Ok_Click(sender, e)
                e.Handled = True
            End If
        End Sub


    End Class
