Imports System
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography
Public Class ClientSideXssGuard
    'Initiate Timer
    Dim StartTimeEnter, StartTimeGo, StartTimeRef As DateTime
    Dim EndTimeEnter, EndTimeGo, EndTimeRef As DateTime
    Dim SpanEnter, SpanGo, SpanRef As TimeSpan

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "KMSY Project Browser (Client Side XSS Guard) : "
        WebBrowser1.Navigate("")
    End Sub
    'ENTER Key press event
    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        On Error Resume Next
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 13 Then
            WebBrowser1.Navigate(TextBox1.Text)
            StartTimeEnter = DateTime.Now()
            e.Handled = True
        End If
    End Sub
    'BACK Button Code here
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        On Error Resume Next
        WebBrowser1.GoBack()
    End Sub
    'FORWARD Button Code here
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        On Error Resume Next
        WebBrowser1.GoForward()
    End Sub
    'GO Button Code here
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        On Error Resume Next
        WebBrowser1.Navigate(TextBox1.Text)
        StartTimeGo = DateTime.Now()
    End Sub
    'REFRESH Button Code here
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        On Error Resume Next
        WebBrowser1.Refresh()
        StartTimeRef = DateTime.Now()
    End Sub
    'STOP Button Code here
    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        On Error Resume Next
        WebBrowser1.Stop()
    End Sub
    Private Sub WebBrowser1_Navigating(sender As Object, e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating
        Label2.Text = "Loading......"
    End Sub
    'New Window opening restricted to prevent risk
    Private Sub WebBrowser1_NewWindow(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        e.Cancel = True
    End Sub
    'PROGRESS BAR Display
    Private Sub WebBrowser1_ProgressChanged(sender As Object, e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles WebBrowser1.ProgressChanged
        'Dim x As Long
        ProgressBar2.Visible = False 'This makes the progress bar disappear after the page is loaded.
        ProgressBar2.Maximum = e.MaximumProgress
        'If e.CurrentProgress = x Then
        'ProgressBar2.Value = e.CurrentProgress + 1
        If e.CurrentProgress > 0 And e.MaximumProgress > 0 Then
            ProgressBar2.Visible = True
            ProgressBar2.Value = e.CurrentProgress / e.MaximumProgress
        End If
    End Sub
    'TIMER measures time taken and displays date
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        'Label6.Text = Format(Now, "dd/mm/yyyy hh:mm:ss")
        'Dim dt As DateTime = DateTime.Now
        'Label6.Text = dt.ToLongTimeString
        Label6.Text = Date.Now.ToString("dd MMM yyyy hh:mm:ss")
    End Sub
    'MAIN PROCESSING
    Private Sub WebBrowser1_DocumentCompleted(sender As System.Object, e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        TextBox1.Text = WebBrowser1.Url.ToString
        Dim b As String
        If ((WebBrowser1.DocumentTitle.Length > 0) And (WebBrowser1.DocumentTitle.Length < 15)) Then
            b = WebBrowser1.DocumentTitle.Substring(0, WebBrowser1.DocumentTitle.Length)
        ElseIf (WebBrowser1.DocumentTitle.Length >= 15) Then
            b = WebBrowser1.DocumentTitle.Substring(0, 20)
        ElseIf (WebBrowser1.DocumentTitle.Length <= 0) Then
            b = "Blank"
        End If
        Label2.Text = "Done :" + b
        'MsgBox(WebBrowser1.Url.ToString)
        Dim blank As String = "about:blank"
        Dim blank1 As String = "file:///C:/Users/SUPER PAULINS/Documents/Visual Studio 2010/Projects/WindowsApplication1/WindowsApplication1/AppData/WhiteList.xml"
        Dim blank2 As String = "file:///C:/Users/SUPER PAULINS/Documents/Visual Studio 2010/Projects/WindowsApplication1/WindowsApplication1/AppData/BlackList.xml"
        Dim blank3 As String = "file:///C:/Users/SUPER PAULINS/Documents/Visual Studio 2010/Projects/WindowsApplication1/WindowsApplication1/AppData/GreyList.xml"
        Me.Text() = "KMSY Project Browser (Client Side XSS Guard) : " + b
        If ((String.Equals(WebBrowser1.Url.ToString, blank) = False) Or (String.Equals(WebBrowser1.Url.ToString, blank1) = True) Or (String.Equals(WebBrowser1.Url.ToString, blank2) = True) Or (String.Equals(WebBrowser1.Url.ToString, blank3) = True)) Then
            Try
                '******************PART 1**************************
                'Initially GreyList will be EMPTY (It has only the ROOT Element)
                'Adding extracted scripts into GreyList
                'Actual Process : Appending the URL and script to the GreyList Database
                'Following Code EXTRACTS the scripts from the loading web page
                'Dim pageSource As String
                'Dim scriptExtract As String
                Dim HTML As String
                'XML Decl
                Dim doc As New XmlDocument()
                doc.Load("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
                Dim doc1 As New XmlDocument()
                doc1.Load("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\ManualUpdate.xml")
                'pageSource = WebBrowser1.DocumentText
                'WebBrowser1.DocumentText = pageSource
                'scriptExtract = ReturnAllElementInstances(pageSource, "script")
                'WebBrowser1.DocumentText = scriptExtract
                ' MsgBox(scriptExtract)
                'TextBox2.Text = scriptExtract
                'Here comes the code for extracting scripts
                HTML = WebBrowser1.DocumentText
                'Single line changed to multiline and ECMA script
                Dim options As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Multiline Or RegexOptions.ECMAScript
                'Dim regex As Regex = New Regex("(<script((\S*\s?)*)(?<theScripts>.*)(</script>)?)+", options)
                'Dim regex As Regex = New Regex("(<script)(?<scripts>(.*\r\n)*?)(</script>)", options)
                'Dim regex As Regex = New Regex("(<script)(?<scripts>.*)(</script>)", options)
                'Following REGEX Works for all combination
                Dim regex As Regex = New Regex("<script(.*)>(.*)</script>", options)
                Dim match As MatchCollection = regex.Matches(HTML)
                'Dim sb As StringBuilder = New StringBuilder
                For Each items As Match In match
                    'for appending into GreyList
                    Dim List As XmlElement = doc.CreateElement("List")
                    Dim UrlEl As XmlElement = doc.CreateElement("Url")
                    UrlEl.InnerText = WebBrowser1.Url.ToString
                    Dim ScriptExtractEl As XmlElement = doc.CreateElement("ScriptExtract")
                    ScriptExtractEl.InnerText = items.ToString
                    List.AppendChild(UrlEl)
                    List.AppendChild(ScriptExtractEl)
                    doc.DocumentElement.AppendChild(List)
                    'sb.Append(items.ToString & vbLf)
                    'Transfer contents from GreyList to ManualUpdate
                    'for appending into ManualUpdate
                    Dim List1 As XmlElement = doc1.CreateElement("List")
                    Dim UrlEl1 As XmlElement = doc1.CreateElement("Url")
                    UrlEl1.InnerText = WebBrowser1.Url.ToString
                    Dim ScriptExtractEl1 As XmlElement = doc1.CreateElement("ScriptExtract")
                    ScriptExtractEl1.InnerText = items.ToString
                    List1.AppendChild(UrlEl1)
                    List1.AppendChild(ScriptExtractEl1)
                    doc1.DocumentElement.AppendChild(List1)
                Next
                'WebBrowser1.DocumentText = sb.ToString
                'The above code shows error since it is a script
                'MsgBox(sb.ToString)
                doc.Save("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
                doc1.Save("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\ManualUpdate.xml")
                'STORING into GreyList & appending into ManualUpdate Completed
                Dim xmlnode As XmlNodeList
                xmlnode = doc.GetElementsByTagName("List")
                Label7.Text = xmlnode.Count
                Dim GCount As Integer = xmlnode.Count
                '**************PART 2 ************   
                Dim strurl As String
                strurl = WebBrowser1.Url.ToString.ToLower()
                Dim url As String
                Dim urldoc As New XmlDocument()
                Dim urlnode As XmlNodeList
                Dim l, C As Integer
                Dim flag As Integer = 0
                Dim x As String
                urldoc.Load("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\ScriptCount.xml")
                urlnode = urldoc.GetElementsByTagName("List")
                For l = 0 To urlnode.Count - 1
                    url = urlnode(l).ChildNodes.Item(0).InnerText.Trim().ToLower
                    If String.Equals(strurl, url) Then
                        flag = flag + 1
                        x = urlnode(l).ChildNodes.Item(1).InnerText
                        Integer.TryParse(x, C) 'Converts string to integer
                        'C = Int32.Parse(url)
                        Console.WriteLine(x & C & url)
                    End If
                Next
                'Following code for existing url
                If flag > 0 Then
                    If (GCount > C) Then    'Malicious
                        MsgBox("The Current webpage is XSS attacked..." & vbLf & "Contact the publisher...", MsgBoxStyle.Critical)
                        WebBrowser1.Stop()
                    Else   'Legitimate Site
                        MsgBox("The Current webpage is Legitimate..." & vbLf & "User can proceed...", MsgBoxStyle.Information)
                        WebBrowser1.Stop()
                    End If
                Else
                    'This for new url
                    'Following code compares the (BlackList & WhiteList) to GreyList 
                    Dim xmldoc1 As New XmlDocument()
                    Dim xmlnode1 As XmlNodeList
                    Dim xmldoc2 As New XmlDocument()
                    Dim xmlnode2 As XmlNodeList
                    Dim xmldoc3 As New XmlDocument()
                    Dim xmlnode3 As XmlNodeList
                    Dim i, j, BCount, WCount As Integer
                    Dim str1, str2 As String
                    Dim fs1 As New FileStream("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\BlackList.xml", FileMode.Open, FileAccess.Read)
                    xmldoc1.Load(fs1)
                    Dim fs2 As New FileStream("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\WhiteList.xml", FileMode.Open, FileAccess.Read)
                    xmldoc2.Load(fs2)
                    Dim fs3 As New FileStream("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml", FileMode.Open, FileAccess.Read)
                    xmldoc3.Load(fs3)
                    'Byte Array decl
                    Dim tmpSource() As Byte
                    Dim tmpHash() As Byte
                    Dim tmpNewHash() As Byte
                    xmlnode1 = xmldoc1.GetElementsByTagName("List")
                    xmlnode2 = xmldoc2.GetElementsByTagName("List")
                    xmlnode3 = xmldoc3.GetElementsByTagName("List")
                    BCount = 0
                    WCount = 0
                    'Compare BlackList & GreyList
                    For i = 0 To xmlnode3.Count - 1
                        'xmlnode3(i).ChildNodes.Item(0).InnerText.Trim()
                        str1 = xmlnode3(i).ChildNodes.Item(1).InnerText.Trim().ToLower
                        'Create a byte array from source data.
                        tmpSource = ASCIIEncoding.ASCII.GetBytes(str1)
                        'Compute hash based on source data.
                        tmpHash = New SHA1CryptoServiceProvider().ComputeHash(tmpSource)
                        'Console.WriteLine(ByteArrayToString(tmpHash))
                        For j = 0 To xmlnode1.Count - 1
                            'xmlnode1(j).ChildNodes.Item(0).InnerText.Trim()
                            str2 = xmlnode1(j).ChildNodes.Item(1).InnerText.Trim().ToLower
                            tmpSource = ASCIIEncoding.ASCII.GetBytes(str2)
                            Dim bEqual As Boolean = False
                            tmpNewHash = New SHA1CryptoServiceProvider().ComputeHash(tmpSource)
                            If tmpNewHash.Length = tmpHash.Length Then
                                Dim k As Integer
                                Do While (k < tmpNewHash.Length) AndAlso (tmpNewHash(k) = tmpHash(k))
                                    k += 1
                                Loop
                                If k = tmpNewHash.Length Then
                                    bEqual = True
                                    BCount = BCount + 1
                                End If
                            End If
                            'If String.Equals(str1, str2) Then
                            'BCount = BCount + 1
                            'End If
                        Next
                    Next
                    'Compare WhiteList & GreyList
                    For i = 0 To xmlnode3.Count - 1
                        xmlnode3(i).ChildNodes.Item(0).InnerText.Trim()
                        str1 = xmlnode3(i).ChildNodes.Item(1).InnerText.Trim().ToLower
                        'Create a byte array from source data.
                        tmpSource = ASCIIEncoding.ASCII.GetBytes(str1)
                        'Compute hash based on source data.
                        tmpHash = New SHA1CryptoServiceProvider().ComputeHash(tmpSource)
                        'Console.WriteLine(ByteArrayToString(tmpHash))
                        For j = 0 To xmlnode2.Count - 1
                            xmlnode2(j).ChildNodes.Item(0).InnerText.Trim()
                            str2 = xmlnode2(j).ChildNodes.Item(1).InnerText.Trim().ToLower
                            tmpSource = ASCIIEncoding.ASCII.GetBytes(str2)
                            Dim bEqual As Boolean = False
                            tmpNewHash = New SHA1CryptoServiceProvider().ComputeHash(tmpSource)
                            If tmpNewHash.Length = tmpHash.Length Then
                                Dim k As Integer
                                Do While (k < tmpNewHash.Length) AndAlso (tmpNewHash(k) = tmpHash(k))
                                    k += 1
                                Loop
                                If k = tmpNewHash.Length Then
                                    bEqual = True
                                    WCount = WCount + 1
                                End If
                            End If
                            'If String.Equals(str1, str2) Then
                            'WCount = WCount + 1
                            'End If
                        Next
                    Next
                    If WCount > BCount Then
                        MsgBox("WhiteList Match : " & WCount & vbLf & "BlackList Match : " & BCount & vbLf & "The Site is Legitimate...(Free from XSS)" & vbLf &
                         "User can Proceed", MsgBoxStyle.Critical)
                        WebBrowser1.Stop()
                    ElseIf WCount < BCount Then
                        MsgBox("WhiteList Match : " & WCount & vbLf & "BlackList Match : " & BCount & vbLf & "!!!!ALERT!!!!...The Site is Vulnerable to XSS" & vbLf &
                        "User requested to revert back", MsgBoxStyle.Information)
                        WebBrowser1.GoBack()
                    Else
                        MsgBox("WhiteList Match : " & WCount & vbLf & "BlackList Match : " & BCount & vbLf & "BL = WL" & vbLf &
                        "The page status will be checked later", MsgBoxStyle.Exclamation)
                        WebBrowser1.Stop()
                    End If
                    fs1.Close()
                    fs2.Close()
                    fs3.Close()
                    'Clear GreyList
                    Dim xml As XmlDocument
                    xml = New XmlDocument
                    ' Dim fs4 As New FileStream("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml", FileMode.Open, FileAccess.ReadWrite)
                    xml.Load("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
                    'Dim nds As XmlNodeList = xml.GetElementsByTagName("List")
                    ''xml.SelectNodes("/GreyList/List")
                    'Dim ndsn As XmlNode
                    'For i = 0 To GCount - 1
                    '    ndsn = nds(i)
                    '    ndsn.ParentNode.RemoveChild(ndsn) 'delete!
                    '    xml.Save("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
                    'Next 'goes onto next node
                    Dim root As XmlNode = xml.DocumentElement
                    'Remove all attribute and child nodes.
                    root.RemoveAll()
                    xml.Save("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
                    'fs4.Close()
                End If
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try
            'STOP TIMER here
            'Check with vbNull for the endTimers
            EndTimeEnter = DateTime.Now
            EndTimeGo = DateTime.Now
            EndTimeRef = DateTime.Now
            SpanEnter = -(StartTimeEnter - EndTimeEnter)
            SpanGo = -(StartTimeGo - EndTimeGo)
            SpanRef = -(StartTimeRef - EndTimeRef)
            Label8.Text = "Enter : " & SpanEnter.TotalMilliseconds / 1000 & "Secs" & vbLf & "Go : " & SpanGo.TotalMilliseconds / 1000 & "Secs" & vbLf & "Refresh : " & SpanRef.TotalMilliseconds / 1000 & "Secs" & vbLf
            'Console.WriteLine("Go : " & SpanGo.TotalMilliseconds)
            'Label8.Text = "Go : " & EndTimeGo & StartTimeGo
            'Console.WriteLine("Refresh : " & SpanRef.TotalMilliseconds)
        End If
    End Sub
    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        'Displays GreyList Database
        WebBrowser1.Navigate("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\GreyList.xml")
    End Sub
    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        'Displays WhiteList Database
        WebBrowser1.Navigate("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\WhiteList.xml")
    End Sub
    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        'Displays BlackList Database
        WebBrowser1.Navigate("C:\Users\SUPER PAULINS\documents\visual studio 2010\Projects\WindowsApplication1\WindowsApplication1\AppData\BlackList.xml")
    End Sub
End Class
