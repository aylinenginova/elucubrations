﻿Module Module1

    Sub Main()
        Dim windowList As ArrayList
        windowList = New ArrayList()
        getWindowList(windowList)
        'For Each windowData As String In windowList
        '    Console.WriteLine(windowData)
        'Next

        For Each windowData As WindowDetails In windowList
            Console.WriteLine(windowData.ToString())
        Next
        Console.WriteLine("total count: " + windowList.Count().ToString())
        Console.ReadLine()

    End Sub

End Module
