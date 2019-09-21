Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System.ComponentModel
'Imports System.Windows.Threading
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

'Imports Microsoft.Office.Interop.Outlook

Public Class SignUp

    ' Global variables
    Public UserNameLog As String = Environment.UserName

    'Public Shared LoginScreen As LoginScreen = New LoginScreen
    Public Shared TxtString_msgcheck As String
    Public Shared MsgCheck = New MsgCheck()
    Public Shared TxtString_msgcritical As String
    Public Shared MsgCritical = New MsgCritical()
    Public Shared TxtString_msginformation As String
    Public Shared MsgInformation = New MsgInformation()

    Dim connection As SqlConnection
    Dim regDate As DateTime = DateTime.Now
    Dim regexpDate As DateTime = DateTime.Now.AddDays(30)
    Dim curDate As Date = Date.Now.ToString("yyyy-MM-dd")
    Dim strDate As String = regDate.ToString("yyyy-MM-dd") 'HH:mm:ss")
    Dim expDate As String = regexpDate.ToString("yyyy-MM-dd") 'HH:mm:ss")
    Dim flags As Boolean = False
    Dim sql As String '= "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

    Dim WithEvents TimerRefreshTime As New System.Windows.Threading.DispatcherTimer

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Try

            VerCode.Focus()

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

            Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisterDate, ExpiryDate, IsDelete from [User]"

            Dim Adpt As New SqlDataAdapter(com, con)

            Dim ds As DataSet = New DataSet()

            Adpt.Fill(ds, "User")

            Dim TableCount As Integer = ds.Tables(0).Rows.Count
            'Dim InfoCompareValues_2 As Boolean = CompareValues_2()
            Dim InfoCompareValues_2 As Boolean = CompareValues_LoginAdminFalse()
            Dim InfoCompareValues_1 As Boolean = CompareValues_LoginUserFalse()

            con.Close()

            If TableCount < 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = False Then

                RegisterLogin.IsEnabled = False

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount < 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = True Then

                RegisterLogin.IsEnabled = False

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount < 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = False Then

                RegisterLogin.IsEnabled = False

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount >= 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = False Then

                RegisterLogin.IsEnabled = True

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount >= 2 And InfoCompareValues_2 = False And InfoCompareValues_1 = True Then

                RegisterLogin.IsEnabled = True

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount >= 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = False Then

                RegisterLogin.IsEnabled = True

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount >= 2 And InfoCompareValues_2 = True And InfoCompareValues_1 = True Then

                RegisterLogin.IsEnabled = True

                RegisterLogin.Text = Nothing

            End If

            ' Varaibles load when run this module
            TimerRefreshTime.Interval = New TimeSpan(0, 0, 1)
            TimerRefreshTime.Start()
            EmailBox_1.Text = "@alior.pl"

            'RegisterLogin.Text = UserNameLog
            'RegisterLogin.Focus()

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

        '------------------------------------------------------------------------------------------------------------

        'Select * from tblStocks where Expiry_Date <= DATEADD(day,-30,GETDATE())

        'Dim olApp As Outlook.Application = New Outlook.Application()
        'Dim olNS As Outlook.NameSpace = olApp.GetNamespace("MAPI")
        'Dim olAL As Outlook.AddressList = olNS.AddressLists("Globalna Lista Adresów")
        'Dim oEntries As Outlook.AddressEntry
        'Dim oExUser As Outlook.ExchangeUser


        'Dim Counter As Long
        'For Each olAL In olNS.AddressLists
        '    For Each oEntries In olAL.AddressEntries

        '        Counter = Counter + 1

        '        Dim name = oEntries.Name
        '        oExUser = oEntries.GetExchangeUser
        '        MsgBox(oExUser.Alias)

        '    Next
        '    Next

        '-------------------------------------------------------------------------------------------------------------

        '    Dim colAL As Outlook.AddressLists

        '    Dim oAL As Outlook.AddressList

        '    Dim colAE As Outlook.AddressEntries

        '    Dim oAE As Outlook.AddressEntry

        '    Dim oExUser As Outlook.ExchangeUser

        '    colAL = olApp.Session.AddressLists

        '    For Each oAL In colAL

        '        'Address list is an Exchange Global Address List 

        '        If oAL.AddressListType = Outlook.OlAddressListType.olExchangeGlobalAddressList Then

        '            colAE = oAL.AddressEntries

        '            For Each oAE In colAE


        '                If oAE.AddressEntryUserType = Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry Then

        '                    oExUser = oAE.GetExchangeUser

        '                    'Dim name = oExUser.Name

        '                    Dim aliass As String = oExUser.Alias

        '                    Dim adress As String = oExUser.PrimarySmtpAddress

        '                    If RegisterLogin.Text = aliass Then

        '                        Dim cut_at As String = "@"
        '                        Dim x As Integer = InStr(adress, cut_at)
        '                        Dim string_after As String = adress.Substring(0, x - 1)

        '                        EmailBox.Text = string_after

        '                        Exit Sub

        '                    End If


        '                End If

        '            Next

        '        End If

        '    Next


        'End Sub

        '----------------------------------------------------------------------------------------------------------------

        'Sub GetAddresses()
        '    Dim o, AddressList, AddressEntry
        '    Dim AddressName As String
        '    Dim user As Outlook.ExchangeUser
        '    o = GetObject(, "Outlook.Application")
        '    Dim c As String = "pi17413"

        '    AddressList = o.Session.AddressLists("Global Address List")

        '    AddressName = c
        '    For Each AddressEntry In AddressList.AddressEntries
        '        If AddressEntry.Name = AddressName Then
        '            user = AddressEntry.GetExchangeUser()
        '            c = user.PrimarySmtpAddress
        '            Exit For
        '        End If
        '    Next AddressEntry


    End Sub

    Private Sub TimerRefreshTimeTick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerRefreshTime.Tick

        Try

            ' Time content in label
            lblTime.Content = Now.ToString("HH:mm:ss")
            RDate.Content = curDate & " " & Now.ToString("HH:mm:ss")

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

    End Sub

    Private Sub GeraCaptcha()

        Try

            ' Generate captcha - variables
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

                If DelId.Text <> "" Then

                    GoTo Generate

                Else

                    'MsgBox("Please enter some data if you want create User/Admin or number if you want del some records!")

                    TxtString_msginformation = "Proszę wprowadź jakiekolwiek dane, jeśli chcesz dodać Uzytkownika/Administratora. Ewentualnie numer Id jeśli chcesz usunąć konto!"

                    MsgInformation.Show()

                    TxtString_msginformation = Nothing

                    Exit Sub

                End If

            Else

Generate:

                For i As Integer = 0 To 5
                    temp = ar((r.Next(0, ar.Length)))
                    senha += temp
                Next
                txtCaptcha.Text = senha

            End If

        Catch ex As Exception

            'Throw ex

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

    End Sub

    Private Sub CreateUserOrAdmin_Click(sender As Object, e As RoutedEventArgs)

        Try

            ' Open connetcion - variables
            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            location = New Uri(location).LocalPath & "\"

            Dim files As New List(Of String)
            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            Dim value As String = String.Join("", files) & ".mdf"

            connection = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")

            If connection.State = ConnectionState.Closed Then

                connection.Open()

            End If

            'Dim sql As String = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

            'TimerRefreshTime.Stop()

            If txtCaptcha.Text = VerCode.Password Then

                If txtCaptcha.Text = "" Or VerCode.Password = "" Then

                    'MsgBox("Sign Up Failed, Please generate a code!")

                    TxtString_msginformation = "Nie udało się dodać konta do bazy danych! Proszę wygneruj i wprowadź najpierw kod captcha!"

                    MsgInformation.Show()

                    TxtString_msginformation = Nothing

                    connection.Close()

                    Codelbl.Foreground = Brushes.Red

                    Exit Sub

                End If

                If System.Text.RegularExpressions.Regex.IsMatch(EmailBox.Text + EmailBox_1.Text, "([a-z]{1,}[.][a-z]{1,}[@alior.pl]{9})") Then

                    If System.Text.RegularExpressions.Regex.IsMatch(RegisterLogin.Text, "([^\S])|([^pi\d]*$)|(^[\d]*$)|(^[^pi\D]*$)|(^i+)|([pi]{3})|(pp)|(^pi\d*\D)") Then ' ([a-z]{2}[\d]{5}$)

                        'next if

                    Else

                        'MsgBox("Login is not validated! Fill Login as: pixxxxx")

                        TxtString_msginformation = "Login nie spełnia wymogów walidacji! Proszę podaj login w formacie: piXXXXX"

                        MsgInformation.Show()

                        TxtString_msginformation = Nothing

                        connection.Close()

                        Exit Sub

                    End If

                Else

                    'MsgBox("Email is not validated! Must be content a dot, for exapmle: test.test")

                    TxtString_msginformation = "Email nie spełnia wymogów walidacji! Podaj email w formacie, np. test.test - wymagana '.' pomiędzy imieniem i nazwiskiem!"

                    MsgInformation.Show()

                    TxtString_msginformation = Nothing

                    connection.Close()

                    Exit Sub

                End If

                Call Check_Available_Account()

                'sql = "UPDATE [User] SET Password = CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2), Email = '" & EmailBox.Text + EmailBox_1.Text & "', RegisterDate = '" & strDate & "', ExpiryDate = '" & expDate & "', IsDelete = '" & flags & "' WHERE Username = '" & RegisterLogin.Text & "', TypeUser = '" & TypeUser.Text & "')"


                ' Opening connection 
                Dim cmd As New SqlCommand With {
                    .Connection = connection,
                    .CommandType = CommandType.Text,
                    .CommandText = sql
                }

                cmd.ExecuteNonQuery()
                cmd.Dispose()
                connection.Close()

                'MsgBox("Sign Up Success about time: " & strDate)

                TxtString_msgcheck = "Konto zostało dodane do bazy danych! Czas: " & strDate

                MsgCheck.Show()

                TxtString_msgcheck = Nothing

                'RegisterLogin.Text = Nothing
                RegisterPassword.Password = Nothing
                EmailBox.Text = Nothing

                Codelbl.Foreground = Brushes.White

                txtCaptcha.Text = Nothing
                VerCode.Password = Nothing

                UpdateTable()

                Call Check_Available_Account()

            Else

                'MsgBox("Sign Up Failed, Please try again enter code!")

                TxtString_msginformation = "Konto nie zostało dodane do bazy danych! Proszę podaj ponownie poprawny kod captcha!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                VerCode.Password = Nothing

                connection.Close()

                Call GeraCaptcha()

            End If

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

            connection.Close()

        End Try

    End Sub

    Private Sub DateBaseSqlView_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded

        Try

            'RegisterLogin.Text = UserNameLog

            ' Loading table with database
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

            Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisterDate, ExpiryDate, IsDelete from [User]"

            Dim Adpt As New SqlDataAdapter(com, con)

            Dim ds As DataSet = New DataSet()

            Adpt.Fill(ds, "User")

            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1

                Me.DateBaseSqlView.ItemsSource = ds.Tables(0).DefaultView

                'Dim em As String = ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item("Email").ToString 'sprawdzenie aresu mailowego

            Next

            con.Close()

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

    End Sub

    Private Sub UpdateTable()

        Try

            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            location = New Uri(location).LocalPath & "\"

            Dim files As New List(Of String)
            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
               Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            Dim value As String = String.Join("", files) & ".mdf"

            Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
            Dim con As New SqlConnection(str)
            Dim com As String = "Select Id, Username, Password, TypeUser, Email, RegisterDate, ExpiryDate, IsDelete from [User]"
            Dim Adpt As New SqlDataAdapter(com, con)
            Dim ds As DataSet = New DataSet()

            Adpt.Fill(ds, "User")

            Me.DateBaseSqlView.ItemsSource = ds.Tables(0).DefaultView

            Dim TableCount As Integer = ds.Tables(0).Rows.Count

            If TableCount < 2 Then

                RegisterLogin.IsEnabled = False

                RegisterLogin.Text = UserNameLog

                Call Check_Available_Account()

            ElseIf TableCount >= 2 Then

                RegisterLogin.IsEnabled = True

                RegisterLogin.Text = Nothing

            End If

            con.Close()

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

        End Try

    End Sub

    Private Sub Delete_Click(sender As Object, e As RoutedEventArgs)

        Try

            Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
            location = New Uri(location).LocalPath & "\"
            'Dim inputString As String = InputBox("Please input a file name:", "File Name", "DefaultFileName")

            Dim files As New List(Of String)
            files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

            Dim value As String = String.Join("", files) & ".mdf"

            Dim connection As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30")

            'Dim sql As String = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

            Dim command As New SqlCommand("UPDATE [User] SET IsDelete=@IsDelete WHERE Id=@Id", connection)

            command.Parameters.Add("@Id", SqlDbType.Int).Value = DelId.Text
            command.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = True

            connection.Open()

            'TimerRefreshTime.Stop()
            If txtCaptcha.Text = VerCode.Password Then

                If txtCaptcha.Text = "" Or VerCode.Password = "" Then

                    'MsgBox("Delete failed, Please generate a code!")

                    TxtString_msginformation = "Usuwanie rekordu nie powiodło się! Wygeneruj i przepisz kod captcha, a następnie spróbuj ponownie!"

                    MsgInformation.Show()

                    TxtString_msginformation = Nothing

                    Codelbl.Foreground = Brushes.Red

                    connection.Close()

                    Exit Sub

                End If

                If command.ExecuteNonQuery() Then

                    'MessageBox.Show("Data Deleted!")

                    TxtString_msgcheck = "Rekord został usunięty!"

                    MsgCheck.Show()

                    TxtString_msgcheck = Nothing

                    connection.Close()

                    DelId.Text = Nothing
                    txtCaptcha.Text = Nothing
                    VerCode.Password = Nothing

                    Codelbl.Foreground = Brushes.White

                    UpdateTable()

                Else

                    'MessageBox.Show("Error - number is out of range!")

                    TxtString_msginformation = "Numer Id poza zakresem!"

                    MsgInformation.Show()

                    TxtString_msginformation = Nothing

                    connection.Close()

                End If


            ElseIf VerCode.Password = "" Then

                'MsgBox("Delete failed, Please enter a code!")

                TxtString_msginformation = "Usuwanie rekordu nie powiodło się! Proszę wprowadź kod captcha!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                Call GeraCaptcha()

            ElseIf txtCaptcha.Text <> VerCode.Password Then

                'MsgBox("Delete failed, Please enter true code!")

                TxtString_msginformation = "Usuwanie rekordu nie powiodło się! Proszę wprowadź poprawny kod captcha!"

                MsgInformation.Show()

                TxtString_msginformation = Nothing

                VerCode.Password = Nothing

                Call GeraCaptcha()

            End If

            connection.Close()

        Catch ex As Exception

            TxtString_msgcritical = "Wystąpił nieoczekiwany błąd! Szczegóły: " & ex.ToString

            MsgCritical.Show()

            TxtString_msgcritical = Nothing

            connection.Close()

        End Try

    End Sub

    Public Function CompareValues_Id()

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        con.Open()

        Dim sSql As String = "Select Id From [User] Where Id = @Id"

        Using Command As New SqlCommand(sSql, con)
            Command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Me.DelId.Text
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Public Function CompareValues_LoginUsername()

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
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Me.RegisterLogin.Text
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Uzytkownik"
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Public Function CompareValues_LoginAdmin()

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
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Me.RegisterLogin.Text
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Administrator"
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Public Function CompareValues_LoginUserFalse()

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        con.Open()

        Dim sSql As String = "Select Username, TypeUser, IsDelete From [User] Where Username = @Username And TypeUser = @TypeUser and IsDelete=@IsDelete"

        Using Command As New SqlCommand(sSql, con)
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Me.RegisterLogin.Text
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Uzytkownik"
            Command.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = False
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Public Function CompareValues_LoginAdminFalse()

        Dim location As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        location = New Uri(location).LocalPath & "\"

        Dim files As New List(Of String)
        files.AddRange(IO.Directory.GetFiles(location, "*.mdf").
                   Select(Function(f) IO.Path.GetFileNameWithoutExtension(f)))

        Dim value As String = String.Join("", files) & ".mdf"

        Dim str As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" & location & value & ";Integrated Security=True;Connect Timeout=30"
        Dim con As New SqlConnection(str)
        con.Open()

        Dim sSql As String = "Select Username, TypeUser, IsDelete From [User] Where Username = @Username And TypeUser = @TypeUser and IsDelete=@IsDelete"

        Using Command As New SqlCommand(sSql, con)
            Command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Me.RegisterLogin.Text
            Command.Parameters.Add("@TypeUser", SqlDbType.VarChar).Value = "Administrator"
            Command.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = False
            Dim Reader As SqlDataReader = Command.ExecuteReader()
            Return Reader.HasRows()

        End Using

    End Function

    Private Sub GetCode_Click(sender As Object, e As RoutedEventArgs)

        ' Action after click button "GetCode"
        GeraCaptcha()
        VerCode.Focus()

    End Sub

    Private Sub VerCode_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles VerCode.PasswordChanged

        If txtCaptcha.Text = "" Then

            VerCode.Password = Nothing

        End If

    End Sub

    Private Sub DelId_TextChanged(sender As Object, e As TextChangedEventArgs) Handles DelId.TextChanged

        'RegisterLogin.Text = Nothing
        'RegisterPassword.Password = Nothing
        'EmailBox.Text = Nothing

        DelId.BorderBrush = Brushes.White

        Dim digitsOnly As Regex = New Regex("([^\S])|([^\d])")
        DelId.Text = digitsOnly.Replace(DelId.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub EmailBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles EmailBox.TextChanged

        'DelId.Text = Nothing

        Dim digitsOnly As Regex = New Regex("([^\S])|([^\D])")
        EmailBox.Text = digitsOnly.Replace(EmailBox.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub RegisterLogin_TextChanged(sender As Object, e As TextChangedEventArgs) Handles RegisterLogin.TextChanged

        'DelId.Text = Nothing

        Dim digitsOnly As Regex = New Regex("([^\S])|([^pi\d]*$)|(^[\d]*$)|(^[^pi\D]*$)|(^i+)|([pi]{3})|(pp)|(^pi\d*\D)")
        RegisterLogin.Text = digitsOnly.Replace(RegisterLogin.Text, "")

        digitsOnly = Nothing

    End Sub

    Private Sub Back_Click(sender As Object, e As RoutedEventArgs)

        ' Close current window
        'MsgCritical.Close()
        'MsgCheck.Close()
        Me.Close()

        RegisterPassword.Clear()
        EmailBox.Clear()
        VerCode.Clear()
        txtCaptcha.Text = ""
        DelId.Clear()

        ' Variable and call to the login window        
        Dim LoginScreen As LoginScreen = New LoginScreen
        LoginScreen.Show()

    End Sub

    Private Sub RegisterPassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles RegisterPassword.LostFocus

        DelId.Text = Nothing

        If RegisterPassword.Password.Length <= 5 Then

            RegisterPassword.Clear()

            'MsgBox("Wprowadź min. 6 znaków")

            TxtString_msginformation = "Wprowadź min. 6 znaków!"

            MsgInformation.Show()

            TxtString_msginformation = Nothing

        End If

    End Sub

    Private Sub DelId_LostFocus(sender As Object, e As RoutedEventArgs) Handles DelId.LostFocus

        RegisterLogin.Text = Nothing
        RegisterPassword.Password = Nothing
        EmailBox.Text = Nothing

        Dim CompareValues_Id_Info As String = CompareValues_Id()

        If CompareValues_Id_Info = False Or DelId.Text = "" Then

            DelId.Clear()

            'MsgBox("This Id doesn't Exists")

            TxtString_msginformation = "Rekord o takim Id nie istenieje w bazie!"

            MsgInformation.Show()

            TxtString_msginformation = Nothing

        End If

    End Sub

    Private Sub RegisterLogin_LostFocus(sender As Object, e As RoutedEventArgs) Handles RegisterLogin.LostFocus

        Dim digitsOnly As Regex = New Regex("([^\S])|([^pi\d]*$)|(^[\d]*$)|(^[^pi\D]*$)|(^i+)|([pi]{3})|(pp)|(^pi\d*\D)")
        RegisterLogin.Text = digitsOnly.Replace(RegisterLogin.Text, "")

        digitsOnly = Nothing

        If RegisterLogin.Text.Length <= 6 Or RegisterLogin.Text = "" Then

            RegisterLogin.Clear()

            'MsgBox("Wprowadź min. 6 znaków")

            TxtString_msginformation = "Wprowadź login w formacie: piXXXXX!!"

            MsgInformation.Show()

            TxtString_msginformation = Nothing

        End If

        Call Check_Available_Account()

    End Sub

    Public Sub Check_Available_Account()

        Dim CompareValues_Info_LoginUserFalse As String = CompareValues_LoginUserFalse()
        Dim CompareValues_Info_LoginAdminFalse As String = CompareValues_LoginAdminFalse()
        Dim CompareValues_Info_LoginUsername As String = CompareValues_LoginUsername()
        Dim CompareValues_Info_LoginAdmin As String = CompareValues_LoginAdmin()

        If CompareValues_Info_LoginUsername = False And CompareValues_Info_LoginAdmin = False And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = False Then

            UserItem.IsEnabled = True
            AdminItem.IsEnabled = True

            sql = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = False And CompareValues_Info_LoginUserFalse = True And CompareValues_Info_LoginAdminFalse = False Then

            AdminItem.IsSelected = True
            UserItem.IsEnabled = False
            AdminItem.IsEnabled = True

            sql = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

        ElseIf CompareValues_Info_LoginUsername = False And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = True Then

            UserItem.IsSelected = True
            AdminItem.IsEnabled = False
            UserItem.IsEnabled = True

            sql = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = True And CompareValues_Info_LoginAdminFalse = True Then

            TypeUser.Text = Nothing
            AdminItem.IsEnabled = False
            UserItem.IsEnabled = False

            TxtString_msgcheck = "Są już założone konta Aministratora i użytkownika na login: " & RegisterLogin.Text

            MsgCheck.Show()

            TxtString_msgcheck = Nothing

            RegisterLogin.Text = Nothing
            RegisterPassword.Password = Nothing
            EmailBox.Text = Nothing
            Codelbl.Foreground = Brushes.White

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = False Then

            'TypeUser.Text = Nothing
            AdminItem.IsEnabled = True
            UserItem.IsEnabled = True

            sql = "UPDATE [User] SET Password=CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),Email='" & EmailBox.Text + EmailBox_1.Text & "',RegisterDate='" & strDate & "',ExpiryDate='" & expDate & "',IsDelete='" & flags & "' WHERE Username='" & RegisterLogin.Text & "' AND TypeUser='" & TypeUser.Text & "'"

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = True And CompareValues_Info_LoginAdminFalse = False Then

            AdminItem.IsSelected = True
            AdminItem.IsEnabled = True
            UserItem.IsEnabled = False

            sql = "UPDATE [User] SET Password=CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),Email='" & EmailBox.Text + EmailBox_1.Text & "',RegisterDate='" & strDate & "',ExpiryDate='" & expDate & "',IsDelete='" & flags & "' WHERE Username='" & RegisterLogin.Text & "' AND TypeUser='" & TypeUser.Text & "'"

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = True Then

            UserItem.IsSelected = True
            AdminItem.IsEnabled = False
            UserItem.IsEnabled = True

            sql = "UPDATE [User] SET Password=CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),Email='" & EmailBox.Text + EmailBox_1.Text & "',RegisterDate='" & strDate & "',ExpiryDate='" & expDate & "',IsDelete='" & flags & "' WHERE Username='" & RegisterLogin.Text & "' AND TypeUser='" & TypeUser.Text & "'"

        ElseIf CompareValues_Info_LoginUsername = False And CompareValues_Info_LoginAdmin = True And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = False Then

            Dim response = MsgBox("W bazie znajduję się taki login! Login przypisany jest do nieaktywnego konta administratora... czy chcesz aktywować konto administratora - wybierz 'TAK', czy dodać nowe konto użytkownika dla tego loginu - wybierz 'NIE'!", MsgBoxStyle.YesNo, "Uwaga!")

            If response = MsgBoxResult.Yes = True Then

                AdminItem.IsSelected = True
                AdminItem.IsEnabled = True
                UserItem.IsEnabled = False

                sql = "UPDATE [User] SET Password=CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),Email='" & EmailBox.Text + EmailBox_1.Text & "',RegisterDate='" & strDate & "',ExpiryDate='" & expDate & "',IsDelete='" & flags & "' WHERE Username='" & RegisterLogin.Text & "' AND TypeUser='" & TypeUser.Text & "'"

            Else

                UserItem.IsSelected = True
                AdminItem.IsEnabled = False
                UserItem.IsEnabled = True

                sql = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

            End If

        ElseIf CompareValues_Info_LoginUsername = True And CompareValues_Info_LoginAdmin = False And CompareValues_Info_LoginUserFalse = False And CompareValues_Info_LoginAdminFalse = False Then

            Dim response = MsgBox("W bazie znajduję się taki login! Login przypisany jest do nieaktywnego konta użytkownika... czy chcesz aktywować konto użytkownika - wybierz 'TAK', czy dodać nowe konto administratora dla tego loginu - wybierz 'NIE'!", MsgBoxStyle.YesNo, "Uwaga!")

            If response = MsgBoxResult.Yes = True Then

                UserItem.IsSelected = True
                AdminItem.IsEnabled = False
                UserItem.IsEnabled = True

                sql = "UPDATE [User] SET Password=CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),Email='" & EmailBox.Text + EmailBox_1.Text & "',RegisterDate='" & strDate & "',ExpiryDate='" & expDate & "',IsDelete='" & flags & "' WHERE Username='" & RegisterLogin.Text & "' AND TypeUser='" & TypeUser.Text & "'"

            Else

                AdminItem.IsSelected = True
                AdminItem.IsEnabled = True
                UserItem.IsEnabled = False

                sql = "INSERT INTO [User](Username,Password,TypeUser,Email,RegisterDate,ExpiryDate,IsDelete) VALUES('" & RegisterLogin.Text & "',CONVERT(VARCHAR(50),HashBytes('SHA2_512','" & RegisterPassword.Password & "'),2),'" & TypeUser.Text & "','" & EmailBox.Text + EmailBox_1.Text & "','" & strDate & "','" & expDate & "','" & flags & "')"

            End If

        End If

    End Sub

    Private Sub EmailBox_LostFocus(sender As Object, e As RoutedEventArgs) Handles EmailBox.LostFocus

        DelId.Text = Nothing

    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)

        RegisterPassword.Clear()
        EmailBox.Clear()
        VerCode.Clear()
        txtCaptcha.Text = ""
        DelId.Clear()

    End Sub

    Private Sub Window_IsVisibleChanged(sender As Object, e As DependencyPropertyChangedEventArgs)

        RegisterPassword.Clear()
        EmailBox.Clear()
        VerCode.Clear()
        txtCaptcha.Text = ""
        DelId.Clear()

    End Sub


End Class

