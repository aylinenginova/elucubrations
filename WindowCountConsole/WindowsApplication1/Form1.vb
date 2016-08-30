Option Explicit On
Public Class Form1


    Private Sub Command1_Click()
        Dim lRet As Long, lParam As Long
        Dim lhWnd As Long

        lhWnd = Me.hwnd  ' Find the Form's Child Windows
        ' Comment the line above and uncomment the line below to
        ' enumerate Windows for the DeskTop rather than for the Form
        'lhWnd = GetDesktopWindow()  ' Find the Desktop's Child Windows
        ' enumerate the list
        lRet = EnumChildWindows(lhWnd, AddressOf EnumChildProc, lParam)
    End Sub

    Private Sub Command2_Click()
        Dim lRet As Long
        Dim lParam As Long

        'enumerate the list
        lRet = EnumWindows(AddressOf EnumWinProc, lParam)
        ' How many Windows did we find?
        Debug.Print TopCount; " Total Top level Windows"
         Debug.Print ChildCount; " Total Child Windows"
         Debug.Print ThreadCount; " Total Thread Windows"
         Debug.Print "For a grand total of "; TopCount + _
         ChildCount + ThreadCount; " Windows!"
      End Sub

End Class


