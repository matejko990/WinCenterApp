Imports System.Data
Imports System.Data.SqlClient

Public Class ChangePasswordByUser

    Private Sub ChangePass_Click(sender As Object, e As RoutedEventArgs)

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"
        'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim con As SqlConnection
        Dim cmd As SqlCommand
        'Dim table As New DataTable("Table")

        If NewPassword.Password <> RetypePassword.Password Then

            MsgBox("Password doesn't match!")
            Exit Sub

        End If

        'Dim username As String
        'Dim queryuser As String = "SELECT CASE * FFROM [User] WHERE (Username = '" & LabelUser.Content.ToString & "')"

        '(Username = @username)

        'Me.YourTableAdapter.Fill(YourDataSet.TableName, stValueFromTextBox)
        Dim obj As New LoginScreen
#Disable Warning BC42025 ' Dostęp przez wystąpienie do udostępnionej składowej, stałej składowej, składowej wyliczenia lub typu zagnieżdżonego

        LabelUser.Content = obj.username 'Environment.UserName

        con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")
        Dim query As String = "UPDATE [User] SET Password = CONVERT(VARCHAR(50),HashBytes('MD5', '" & RetypePassword.Password & "'),2) WHERE Username = @Username"

        '"UPDATE [User] SET Password = CONVERT(VARCHAR(50),HashBytes('MD5', @Password),2) WHERE Username = @Username"
        con.Open()

        cmd = New SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@Username", LabelUser.Content)
        cmd.Parameters.AddWithValue(RetypePassword.Password, RetypePassword.Password)
        cmd.ExecuteNonQuery()

        MsgBox("Password changed successfully!")

        con.Close()

    End Sub

    Private Sub Quit_Click(sender As Object, e As RoutedEventArgs)

        Me.Close()

    End Sub

    Private Sub X_Click(sender As Object, e As RoutedEventArgs) Handles Cl.Click

        Me.Hide()

    End Sub

End Class
