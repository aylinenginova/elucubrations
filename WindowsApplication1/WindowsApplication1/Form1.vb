Option Explicit On
Imports WindowsApplication1.modEnumWindows

Public Class Form1
    Private Sub Button1_Click() Handles Button1.Click

        Dim windowList As ArrayList
        windowList = New ArrayList()
        getWindowList(windowList)
        TextBox1.AppendText(windowList.Count())

    End Sub
End Class
