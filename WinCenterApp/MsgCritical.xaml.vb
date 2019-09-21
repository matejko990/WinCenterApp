'Option Strict On
'Option Explicit On
Public Class MsgCritical

    Shared mWpfMessageBoxResult As WpfMessageBoxResult

    Public Overloads Shared Function Show() As WpfMessageBoxResult
        Dim msg As New MsgCritical()
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
        If objcrcMA.TxtString_msgcritical <> "" Then

            txt.Text = objcrcMA.TxtString_msgcritical

            Exit Sub

        ElseIf objcrcLS.TxtString_msgcritical <> "" Then

            txt.Text = objcrcLS.TxtString_msgcritical

            Exit Sub

        ElseIf objcrcSU.TxtString_msgcritical <> "" Then

            txt.Text = objcrcSU.TxtString_msgcritical

            Exit Sub

        ElseIf objcrcCPBU.TxtString_msgcritical <> "" Then

            txt.Text = objcrcCPBU.TxtString_msgcritical

            Exit Sub

        ElseIf objcrcAW.TxtString_msgcritical <> "" Then

            txt.Text = objcrcAW.TxtString_msgcritical

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