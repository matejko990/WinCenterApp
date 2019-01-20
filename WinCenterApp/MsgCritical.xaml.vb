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

        Dim obj As New LoginScreen
#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego
        txt.Text = obj.TxtString
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