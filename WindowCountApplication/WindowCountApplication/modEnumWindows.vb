Imports System.Runtime.InteropServices
Imports System.Text

Module modEnumWindows

    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure WINDOWINFO
        Public cbSize As UInteger
        Public rcWindow As RECT
        Public rcClient As RECT
        Public dwStyle As UInteger
        Public dwExStyle As UInteger
        Public dwWindowStatus As UInteger
        Public cxWindowBorders As UInteger
        Public cyWindowBorders As UInteger
        Public atomWindowType As UShort
        Public wCreatorVersion As UShort
    End Structure

    Public Structure WindowDetails
        Public ExtendedWindowStyle As UInteger
        Public WindowClass As StringBuilder
        Public WindowHandle As Integer
        Public WindowTitle As StringBuilder
        Public ProcessId As Integer
        Public Overrides Function ToString() As String
            Return String.Format("{0} - {1}" & vbCrLf & vbTab & vbTab & "{2} - hwnd {3}" & vbCrLf & vbTab & vbTab & "dwExStyle {4}" & vbCrLf, ProcessId, WindowTitle.ToString(), WindowClass.ToString(), WindowHandle, Hex(ExtendedWindowStyle))
        End Function
    End Structure

    Private windowList As New ArrayList
    Private ieTabList As New ArrayList
    Private errMessage As String

    Public Delegate Function MyDelegateCallBack(ByVal hwnd As Integer, ByVal lParam As Integer) As Boolean
    Declare Function EnumWindows Lib "user32" (ByVal x As MyDelegateCallBack, ByVal y As Integer) As Integer
    Declare Function EnumChildWindows Lib "user32" _
        (ByVal hWnd As IntPtr,
        ByVal x As MyDelegateCallBack,
        ByVal y As Integer) As Integer

    Declare Auto Function GetClassName Lib "user32" _
        (ByVal hWnd As IntPtr,
        ByVal lpClassName As System.Text.StringBuilder,
        ByVal nMaxCount As Integer) As Integer

    Declare Auto Function GetWindowText Lib "user32" _
       (ByVal hWnd As IntPtr,
       ByVal lpClassName As System.Text.StringBuilder,
       ByVal nMaxCount As Integer) As Integer

    Declare Auto Function GetWindowThreadProcessId Lib "user32" _
       (ByVal hWnd As IntPtr,
       ByRef lpdword As IntPtr) As Integer

    Declare Auto Function IsWindowVisible Lib "user32" _
       (ByVal hWnd As IntPtr) As Boolean

    Declare Auto Function GetWindowInfo Lib "user32" _
       (ByVal hWnd As IntPtr,
       ByRef pWindowInfo As WINDOWINFO) As Integer

    Private Function EnumWindowProc(ByVal hwnd As Integer, ByVal lParam As Integer) As Boolean

        'working vars
        Dim sTitle As New StringBuilder(255)
        Dim sClass As New StringBuilder(255)
        Dim processId As Integer
        Dim windowInfo As WINDOWINFO

        Try
            Call GetClassName(hwnd, sClass, 255)
            Call GetWindowText(hwnd, sTitle, 255)
            Call GetWindowThreadProcessId(hwnd, processId)
            Call GetWindowInfo(hwnd, windowInfo)

            If IsWindowVisible(hwnd) Then
                'If sClass.ToString() = "IEFrame" Or sClass.ToString() = "Internet Explorer_Hidden" Then
                'If sTitle.Length() > 0 Then
                If Not ((windowInfo.dwExStyle And &H80) = &H80) And Not ((windowInfo.dwExStyle And &H200000) = &H200000) Then
                    'windowList.Add(Hex(windowInfo.dwExStyle) & " | " & sClass.ToString & ", " & hwnd & ", " & sTitle.ToString & " - " & Process.GetProcessById(processId).ToString() & " - " & processId.ToString())
                    If Not sClass.ToString() = "IEFrame" Then
                        windowList.Add(New WindowDetails With {.WindowClass = sClass, .WindowHandle = hwnd, .WindowTitle = sTitle, .ExtendedWindowStyle = windowInfo.dwExStyle, .ProcessId = processId})
                    Else
                        Dim del As MyDelegateCallBack
                        del = New MyDelegateCallBack(AddressOf EnumChildWindowProc)
                        EnumChildWindows(hwnd, del, 0)
                    End If
                End If
            End If


        Catch ex As Exception
            errMessage = ex.Message
            EnumWindowProc = False
            Exit Function
        End Try

        EnumWindowProc = True

    End Function

    Private Function EnumChildWindowProc(ByVal hwnd As Integer, ByVal lParam As Integer) As Boolean

        'working vars
        Dim sTitle As New StringBuilder(255)
        Dim sClass As New StringBuilder(255)
        Dim processId As Integer
        Dim windowInfo As WINDOWINFO

        Try
            Call GetClassName(hwnd, sClass, 255)
            Call GetWindowText(hwnd, sTitle, 255)
            Call GetWindowThreadProcessId(hwnd, processId)
            Call GetWindowInfo(hwnd, windowInfo)

            'If IsWindowVisible(hwnd) Then
            'If sClass.ToString() = "IEFrame" Or sClass.ToString() = "Internet Explorer_Hidden" Then
            'If sTitle.Length() > 0 Then
            'If Not ((windowInfo.dwExStyle And &H80) = &H80) And Not ((windowInfo.dwExStyle And &H200000) = &H200000) Then
            'windowList.Add(Hex(windowInfo.dwExStyle) & " | " & sClass.ToString & ", " & hwnd & ", " & sTitle.ToString & " - " & Process.GetProcessById(processId).ToString() & " - " & processId.ToString())
            If sClass.ToString() = "TabWindowClass" Then
                ieTabList.Add(New WindowDetails With {.WindowClass = sClass, .WindowHandle = hwnd, .WindowTitle = sTitle, .ExtendedWindowStyle = windowInfo.dwExStyle, .ProcessId = processId})
                'Console.WriteLine(String.Format("{0} - {1} - {2}", hwnd, sClass.ToString(), sTitle.ToString()))

                '      End If
                '   End If
            End If


        Catch ex As Exception
            errMessage = ex.Message
            EnumChildWindowProc = False
            Exit Function
        End Try

        EnumChildWindowProc = True

    End Function

    Public Function getWindowList(ByRef wList As ArrayList, ByRef tList As ArrayList, Optional errorMessage As String = "") As Boolean

        windowList.Clear()
        ieTabList.Clear()

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

        tList.Clear()
        wList.Clear()
        wList = windowList
        tList = ieTabList

    End Function

End Module
