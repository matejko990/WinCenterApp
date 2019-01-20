Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable

Public Class SignUp

    Public connection As SqlConnection
    Dim regDate As DateTime = DateTime.Now
    Dim curDate As Date = Date.Now.ToString("yyyy-MM-dd")
    Dim strDate As String = regDate.ToString("yyyy-MM-dd HH:mm:ss")
    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Sub OpenConnection()

        Try

            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            location = New Uri(location).LocalPath & "\"
            'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

            Dim files As New List(Of String)
            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            'Dim newdir As Object = files

            Dim value As String = String.Join("", files) & ".mdf"

            connection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")
            If connection.State = ConnectionState.Closed Then
                connection.Open()

            End If

        Catch ex As Exception

            MsgBox("Failed to connect, Error as " & ex.ToString)

        End Try

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        'Dim TypeUser As String
        'Dim Type As String

        'TypeUser = ChoiseTypeUser.Text

        'If TypeUser = "Admin" Then
        '    Type = "Admin"
        'ElseIf TypeUser = "User" Then
        '    Type = "Admin"
        'End If

        Dim sql As String = "INSERT INTO [User](Username,Password,TypeUser,RegisteredDate) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('MD5','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & strDate & "')"

        'TimerRefreshTime.Stop()

        SignUp(sql)
        MsgBox("Sign Up Success about time: " & strDate)


    End Sub

    Private Sub SignUp(ByVal sql As String)

        Dim cmd As New SqlCommand
        OpenConnection()

        Try

            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            connection.Close()

        Catch ex As Exception

            MsgBox("Failed" & ex.ToString)

        End Try

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        TypeUser.Text = "Administrator"
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()

    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick
        RDate.Content = curDate & " " & Now.ToString("HH:mm:ss")
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"
        'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        'Dim newdir As Object = files

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        'Dim table As New DataTable("Table")

        Dim com As String = "Select Id, Username, Password, TypeUser, RegisteredDate from [User]"

        Dim Adpt As New SqlDataAdapter(com, con)

        Dim ds As DataSet = New DataSet()

        Adpt.Fill(ds, "User")

        'Dim i As Integer
        'For i = 0 To ds.Tables(0).Rows.Count - 1

        Me.DateBaseSqlView.ItemsSource = ds.Tables(0).DefaultView

        ''Next

    End Sub

    Private Sub DateBaseSqlView_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"
        'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        'Dim newdir As Object = files

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        Dim table As New DataTable("Table")
        Dim username As DataRow = table.NewRow

        Dim com As String = "Select Id, Username, Password, TypeUser, RegisteredDate from [User]"

        Dim Adpt As New SqlDataAdapter(com, con)

        Dim ds As DataSet = New DataSet()

        Adpt.Fill(ds, "User")

        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1

            Me.DateBaseSqlView.ItemsSource = ds.Tables(0).DefaultView

        Next

    End Sub

End Class

