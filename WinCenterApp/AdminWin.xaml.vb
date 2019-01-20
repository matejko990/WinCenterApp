Imports System.Data
Imports System.Data.SqlClient

Public Class AdminWin

    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()
    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick
        lblTime.Content = Now.ToString("HH:mm:ss")
    End Sub

    ''    Public connection As SqlConnection

    ''    Sub OpenConnection()

    ''        Try

    ''            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
    ''            location = New Uri(location).LocalPath & "\"
    ''            'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

    ''            Dim files As New List(Of String)
    ''            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
    ''                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

    ''            'Dim newdir As Object = files

    ''            Dim value As String = String.Join("", files) & ".mdf"

    ''            connection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")
    ''            If connection.State = ConnectionState.Closed Then
    ''                connection.Open()

    ''            End If

    ''        Catch ex As Exception

    ''            MsgBox("Failed to connect, Error as " & ex.ToString)

    ''        End Try

    ''    End Sub

    ''    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

    ''        OpenConnection()
    ''        Dim dr As SqlDataReader
    ''        Dim cmd As SqlCommand
    ''        Dim sql As String

    ''#Disable Warning IDE0017 ' Uprość inicjowanie obiektów
    ''        cmd = New SqlCommand
    ''#Enable Warning IDE0017 ' Uprość inicjowanie obiektów
    ''        cmd.CommandType = CommandType.Text
    ''        cmd.Connection = connection

    ''        sql = "SELECT * FROM [User] WHERE Username COLLATE Latin1_General_CS_AS ='" & AdminLogin.Text & "'AND Password COLLATE Latin1_General_CS_AS =CONVERT(NVARCHAR(32)HashBytes('MD5','" & AdminPassword.Text & "'),2)"
    ''        cmd.CommandText = sql
    ''        dr = cmd.ExecuteReader()
    ''        If dr.HasRows Then
    ''            dr.Read()
    ''            If dr.Item("Level") = 1 Then

    ''                MsgBox("Welcome Administrator")

    ''            ElseIf dr.Item("Level") = 2 Then

    ''                MsgBox("Welcome User")

    ''            End If

    ''        Else

    ''            MsgBox("Access denied")

    ''        End If

    ''        connection.Close()
    ''        cmd.Dispose()

    ''    End Sub

    Private Sub Registration_Click(sender As Object, e As RoutedEventArgs)

        Dim SignUp As SignUp = New SignUp

        SignUp.Show()
        Me.Hide()

    End Sub

End Class
