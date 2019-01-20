Imports System.ComponentModel

Public Class ProgresBarAnimation

    Dim WithEvents BackgroundWorker1 As BackgroundWorker
    Dim inprocent As Integer = 1

    Shared mWpfMessageBoxResult As WpfMessageBoxResult

    Public Overloads Shared Function Show() As WpfMessageBoxResult
        Dim msg As New ProgresBarAnimation()
        msg.ShowDialog()
        Return mWpfMessageBoxResult
    End Function

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Me.BackgroundWorker1 = New BackgroundWorker
        'Enable progress reporting
        BackgroundWorker1.WorkerReportsProgress = True
        'Set the progress state as "normal"
        TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Normal
        'Start the work 
        BackgroundWorker1.RunWorkerAsync()
        'DoWork Event occurs
        'Now control will goes to worker_DoWork Sub because it handles the DoWork Event

        'timeout 5s  
        'Threading.Thread.Sleep(5000)
        'Me.Hide()

    End Sub

    Private Sub worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For i As Integer = 0 To 99 'Step 10
            System.Threading.Thread.Sleep(15)
            'Raises the ProgressChanged event passing the value
            CType(sender, System.ComponentModel.BackgroundWorker).ReportProgress(i)
            'Now control will goes to worker_ProgressChanged Sub because it handles the ProgressChanged Event
        Next i
    End Sub

    Private Sub worker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'Increment the value on progress bars in window
        ProgressBar1.Value = e.ProgressPercentage
        'Increment the value on progress bars in Taskbar
        procento.Content = inprocent & "%"
        inprocent = inprocent + 1
        TaskbarItemInfo.ProgressValue = CDbl(e.ProgressPercentage) / 100
    End Sub

    ' Work completed
    Private Sub worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ProgressBar1.Value = 100
        TaskbarItemInfo.ProgressValue = 1.0
        'Set the progress state as "indeterminate"
        TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Indeterminate
        'display a message box and keep the result in variable result
        'Dim result = MessageBox.Show("The progress completed. Would you like to exit now?", "Message - Progress Completed", MessageBoxButton.YesNo)
        ''if result is Yes - Close the application
        'If result = MessageBoxResult.Yes Then End
        'Call Load()
        'MsgCheck.Show()
        'Call Main()
        Me.Hide()

    End Sub

End Class
