Imports System.Data
Imports System.Data.SqlClient

Public Class AdminWin

    Public UserNameLog As String = Environment.UserName

    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()
    Public Shared ChangePasswordByUser = New ChangePasswordByUser

    ' Timer Events
    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        Dim table As New DataTable("Table")
        Dim username As DataRow = table.NewRow

        Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisterDate from [User]"

        Dim Adpt As New SqlDataAdapter(com, con)

        Dim ds As DataSet = New DataSet()

        Adpt.Fill(ds, "User")

        Dim TableCount As Integer = ds.Tables(0).Rows.Count
        Dim InfoCompareValues_2 As Boolean = CompareValues_2()

        If TableCount <= 2 And InfoCompareValues_2 = False Then

            Chngps.IsEnabled = False

        ElseIf TableCount <= 2 And InfoCompareValues_2 = True Then

            Chngps.IsEnabled = True

        End If

        con.Close()

        ' Label content - UserName
        UserNameLabel.Content = "Zalogowany użytkownik: " & Environment.UserName & " :D"

        ' Timer starts
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()

    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick

        ' Label content - Acctually time
        lblTime.Content = Now.ToString("HH:mm:ss")

    End Sub

    Public Function CompareValues_2()

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        con.Open()

        Dim sSql As String = "Select Username, TypeUser From [User] Where Username = @Username And TypeUser = @TypeUser"

        Using Command As New SqlCommand(sSql, con)
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = UserNameLog
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Administrator"
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    'Private Sub Registration_Click(sender As Object, e As RoutedEventArgs)

    '    ' New window "SignUp"
    '    Dim SignUp As SignUp = New SignUp

    '    ' Open the window "SignUp" and close acctually window
    '    SignUp.Show()
    '    Me.Hide()

    'End Sub

    Private Sub Chngpass(sender As Object, e As RoutedEventArgs)

        ' Open the window "ChangePasswordByUser"
        ChangePasswordByUser.Show()

    End Sub

    Private Sub Lgout(sender As Object, e As RoutedEventArgs)

        ' Message about Logout
        'MsgBox("You are Now Logged Out", MsgBoxStyle.Information, "Login")

        TxtString_msginformation = "Zostaniesz wylogowan-y/-a!"

        MsgInformation.Show()

        TxtString_msginformation = Nothing

        ChangePasswordByUser.Hide()

        ' Close current window
        Me.Hide()

        ' Variable and call to the login window
        Dim LoginScreen = New LoginScreen()

        ' Open the window "LoginScreen"
        LoginScreen.Show()

    End Sub


End Class
