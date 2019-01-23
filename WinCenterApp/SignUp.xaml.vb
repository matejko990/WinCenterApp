Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System.ComponentModel
'Imports System.Text
'Imports System.Drawing
'Imports System.Drawing.Drawing2D

Public Class SignUp

    Public connection As SqlConnection
    Dim regDate As DateTime = DateTime.Now
    Dim curDate As Date = Date.Now.ToString("yyyy-MM-dd")
    Dim strDate As String = regDate.ToString("yyyy-MM-dd HH:mm:ss")
    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    'Dim DrawingFont As New Font("Arial", 25)
    'Dim CaptchaImage As New BitmapImage
    'Dim CaptchaGraf As Graphics = Graphics.FromImage(CaptchaImage)
    'Dim Alphabet As String = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz"
    'Dim CaptchaString, TickRandom As String
    'Dim ProcessNumber As Integer

    'Private Sub GenerateCaptcha()

    '    ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
    '    If ProcessNumber < 521 Then
    '        ProcessNumber = ProcessNumber \ 10
    '        CaptchaString = Alphabet.Substring(ProcessNumber, 1)
    '    Else
    '        CaptchaString = CStr(My.Computer.Clock.LocalTime.Second \ 6)
    '    End If
    '    ProcessNumber = My.Computer.Clock.LocalTime.Second
    '    If ProcessNumber < 30 Then
    '        ProcessNumber = Math.Abs(ProcessNumber - 8)
    '        CaptchaString += Alphabet.Substring(ProcessNumber, 1)
    '    Else
    '        CaptchaString += CStr(My.Computer.Clock.LocalTime.Minute \ 6)
    '    End If
    '    ProcessNumber = My.Computer.Clock.LocalTime.DayOfYear
    '    If ProcessNumber Mod 2 = 0 Then
    '        ProcessNumber = ProcessNumber \ 8
    '        CaptchaString += Alphabet.Substring(ProcessNumber, 1)
    '    Else
    '        CaptchaString += CStr(ProcessNumber \ 37)
    '    End If
    '    TickRandom = My.Computer.Clock.TickCount.ToString
    '    ProcessNumber = Val(TickRandom.Substring(TickRandom.Length - 1, 1))
    '    If ProcessNumber Mod 2 = 0 Then
    '        CaptchaString += CStr(ProcessNumber)
    '    Else
    '        ProcessNumber = Math.Abs(Int(Math.Cos(Val(TickRandom)) * 51))
    '        CaptchaString += Alphabet.Substring(ProcessNumber, 1)
    '    End If
    '    ProcessNumber = My.Computer.Clock.LocalTime.Hour
    '    If ProcessNumber Mod 2 = 0 Then
    '        ProcessNumber = Math.Abs(Int(Math.Sin(Val(My.Computer.Clock.LocalTime.Year)) * 51))
    '        CaptchaString += Alphabet.Substring(ProcessNumber, 1)
    '    Else
    '        CaptchaString += CStr(ProcessNumber \ 3)
    '    End If
    '    ProcessNumber = My.Computer.Clock.LocalTime.Millisecond
    '    If ProcessNumber > 521 Then
    '        ProcessNumber = Math.Abs((ProcessNumber \ 10) - 52)
    '        CaptchaString += Alphabet.Substring(ProcessNumber, 1)
    '    Else
    '        CaptchaString += CStr(My.Computer.Clock.LocalTime.Second \ 6)
    '    End If
    '    CaptchaGraf.Clear(Color.White)

    '    For hasher As Integer = 0 To 5
    '        CaptchaGraf.DrawString(CaptchaString.Substring(hasher, 1), DrawingFont, Brushes.Black, hasher * 20 + hasher + ProcessNumber \ 200, (hasher Mod 3) * (ProcessNumber \ 200))
    '    Next
    '    PictureBox.Text = CaptchaImage


    'End Sub

    Private Sub GeraCaptcha()
        Try
            Dim caracteresCaptcha As String = ""
            caracteresCaptcha = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,R,S,T,U,V,W,Q,X,Y,Z"
            caracteresCaptcha += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,r,s,t,u,v,w,q,x,y,z"
            caracteresCaptcha += "1,2,3,4,5,6,7,8,9,0"
            caracteresCaptcha += "@,#,$,%,&,!,?"
            Dim a As Char() = {","c}
            Dim ar As String() = caracteresCaptcha.Split(a)
            Dim senha As String = ""
            Dim temp As String = ""
            Dim r As New Random()

            If RegisterLogin.Text = "" Or RegisterPassword.Password = "" Or TypeUser.Text = "" Or EmailBox.Text = "" Then

                MsgBox("Please fill all data!")

            Else

                For i As Integer = 0 To 5
                    temp = ar((r.Next(0, ar.Length)))
                    senha += temp
                Next
                txtCaptcha.Text = senha

            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


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

    Private Sub Register_Click(sender As Object, e As RoutedEventArgs)

        'Dim TypeUser As String
        'Dim Type As String

        'TypeUser = ChoiseTypeUser.Text

        'If TypeUser = "Admin" Then
        '    Type = "Admin"
        'ElseIf TypeUser = "User" Then
        '    Type = "Admin"
        'End If

        GeraCaptcha()
        VerCode.Focus()

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

        'TypeUser.Text = "Administrator"
        TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
        TimerRefreshTime.Start()

    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick
        RDate.Content = curDate & " " & Now.ToString("HH:mm:ss")
    End Sub

    Private Sub Update_Click(sender As Object, e As RoutedEventArgs)

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

        Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisteredDate from [User]"

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

        Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisteredDate from [User]"

        Dim Adpt As New SqlDataAdapter(com, con)

        Dim ds As DataSet = New DataSet()



        Adpt.Fill(ds, "User")

        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1

            Me.DateBaseSqlView.ItemsSource = ds.Tables(0).DefaultView

        Next

    End Sub

    Private Sub Confirm_Click(sender As Object, e As RoutedEventArgs)

        Dim sql As String = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisteredDate) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text & "','" & strDate & "')"

        'TimerRefreshTime.Stop()
        If txtCaptcha.Text = VerCode.Password Then

            If txtCaptcha.Text = "" Or VerCode.Password = "" Then

                MsgBox("Sign Up Failed, Please generate a code!")
                Codelbl.Foreground = Brushes.Red

                Exit Sub

            End If

            SignUp(sql)
            MsgBox("Sign Up Success about time: " & strDate)

        Else

            MsgBox("Sign Up Failed, Please try again enter code or generate new code!")

        End If


    End Sub


End Class

