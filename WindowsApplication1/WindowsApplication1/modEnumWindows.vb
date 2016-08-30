Imports System.Runtime.InteropServices
Imports System.Text

    Module modEnumWindows

        Private windowList As New ArrayList
        Private errMessage As String

        Public Delegate Function MyDelegateCallBack(ByVal hwnd As Integer, ByVal lParam As Integer) As Boolean
        Declare Function EnumWindows Lib "user32" (ByVal x As MyDelegateCallBack, ByVal y As Integer) As Integer

        Declare Auto Function GetClassName Lib "user32" _
        (ByVal hWnd As IntPtr,
        ByVal lpClassName As System.Text.StringBuilder,
        ByVal nMaxCount As Integer) As Integer

    Declare Auto Function GetWindowText Lib "user32" _
       (ByVal hWnd As IntPtr,
       ByVal lpClassName As System.Text.StringBuilder,
       ByVal nMaxCount As Integer) As Integer

    Declare Auto Function IsWindowVisible Lib "user32" _
       (ByVal hWnd As IntPtr) As Boolean

    Private Function EnumWindowProc(ByVal hwnd As Integer, ByVal lParam As Integer) As Boolean

            'working vars
            Dim sTitle As New StringBuilder(255)
        Dim sClass As New StringBuilder(255)


        Try

                Call GetClassName(hwnd, sClass, 255)
            Call GetWindowText(hwnd, sTitle, 255)
            If IsWindowVisible(hwnd) Then
                If sTitle.Length() > 0 Then
                    windowList.Add(sClass.ToString & ", " & hwnd & ", " & sTitle.ToString)
                End If
            End If


        Catch ex As Exception
                errMessage = ex.Message
                EnumWindowProc = False
                Exit Function
            End Try

            EnumWindowProc = True

        End Function

        Public Function getWindowList(ByRef wList As ArrayList, Optional errorMessage As String = "") As Boolean

            windowList.Clear()

            Try
                Dim del As MyDelegateCallBack
                del = New MyDelegateCallBack(AddressOf EnumWindowProc)
                EnumWindows(del, 0)
                getWindowList = True
            Catch ex As Exception
                getWindowList = False
                errorMessage = errMessage
                Exit Function
            End Try

            wList.Clear()
            wList = windowList

        End Function

    End Module
