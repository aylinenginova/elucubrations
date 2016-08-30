Module Module1

    Sub Main()
        Dim windowList As ArrayList
        Dim ieTabsList As ArrayList
        windowList = New ArrayList()
        ieTabsList = New ArrayList()

        getWindowList(windowList, ieTabsList)
        'For Each windowData As String In windowList
        '    Console.WriteLine(windowData)
        'Next


        For Each windowData As WindowDetails In windowList
            Console.WriteLine(windowData.ToString())
        Next

        For Each tabData As WindowDetails In ieTabsList
            Console.WriteLine(tabData.ToString())
        Next

        Console.WriteLine("total (non IE) count: " + windowList.Count().ToString())
        Console.WriteLine("total IE tabs: " + ieTabsList.Count().ToString())
        Console.ReadLine()

    End Sub

End Module
