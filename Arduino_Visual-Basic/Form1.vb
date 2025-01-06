' Arduino and Visual Basic Part 1: Receiving Data From An Arduino
' A simple example of recieving serial data from an Arduino and displaying it in a text box
' https://www.martyncurrey.com/arduino-and-visual-basic-part-1-receiving-data-from-the-arduino/
'

Imports System
Imports System.CodeDom
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.Common
Imports System.Data.SqlTypes
Imports System.Globalization
Imports System.IO
Imports System.IO.Ports
Imports System.Linq.Expressions
Imports System.Net
Imports System.Reflection
Imports System.Reflection.Emit
Imports System.Runtime.InteropServices
Imports System.Runtime.InteropServices.ComTypes
Imports System.Runtime.Serialization
Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms.AxHost
Imports System.Windows.Forms.Layout
Imports System.Windows.Forms.VisualStyles
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft
Imports Microsoft.Win32


Public Class Form1

    ' Global variables.
    ' Anything defined here is available in the whole app
    Dim selected_COM_PORT As String
    Dim receivedData As String = ""
    Dim commandCount As Integer = 0
    Dim RP7Dir As String = Environment.GetFolderPath(Environment.SpecialFolder.System.UserProfile) + "\AppData\Roaming\RP Xtras\Timing Data"
    Dim RaceFileDir As String = Environment.GetFolderPath(Environment.SpecialFolder.System.UserProfile) + "\Gizmo"
    Dim ExpVidDir As String = Environment.GetFolderPath(Environment.SpecialFolder.System.UserProfile) + "\Videos"
    Dim TM_ChromeLanes As Boolean = False
    Dim PrevRace As String = ""
    Dim RaceNoFmt As String = "000" 'Digits for displaying Race Number
    Dim RagattaNoFmt As String = "0000" 'Digits for displaying Race Number
    Dim FinishTimeFmt As String = "hh':'mm':'ss'.'ff" 'format for displaying Times
    Dim RP7TimeFmt As String = "hh':'mm" 'format for displaying Times
    Dim RaceTimeFmt As String = "mm':'ss'.'ff" 'format for displaying Times
    Dim ArduinoConnected As Boolean


    ' This called when the app first starts. Used to initial what ever needs initialising.
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim SoftwareKey As RegistryKey
        Dim ComStr As String
        TimerConnect.Enabled = False
        ArduinoConnected = False
        autoconnect() 'Search for Finish control

        RefreshCommButton.Text = Char.ConvertFromUtf32(81)
        'Set Registry keys
        SoftwareKey = Registry.CurrentUser.OpenSubKey("RQGizmo", True)
        ' Create Key if not existing
        If SoftwareKey Is Nothing Then SoftwareKey = Registry.CurrentUser.CreateSubKey("RQGizmo")

        'Check Commport exists other wise setup
        If SoftwareKey.GetValue("ComPort") Is Nothing Then SoftwareKey.SetValue("Regatta", "")

        'Load availabile Commports
        For Each sp As String In My.Computer.Ports.SerialPortNames
            comPort_ComboBox.Items.Add(sp)
        Next

        'Set to value from registry else last in list
        ComStr = SoftwareKey.GetValue("ComPort", "")
        If comPort_ComboBox.FindStringExact(ComStr) < 0 Then
            comPort_ComboBox.SelectedIndex = comPort_ComboBox.Items.Count - 1
        Else
            comPort_ComboBox.SelectedItem = SoftwareKey.GetValue("ComPort", "").ToString()
        End If

        'Set Regatta to last used
        RegattaTxtBox.Text = SoftwareKey.GetValue("Regatta", "0").ToString()
        'load existing races for regatta
        LoadRaces()

        ' Check is OBS is installed and open instace
        Dim OBSKey As RegistryKey = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("OBS Studio")
        If OBSKey Is Nothing Then
            VidCapChk.Checked = False
            VidCapChk.Enabled = False
            VidCapChk.Text = "Video Capture : No OBS Installed"
        Else
            VidCapChk.Checked = False
            VidCapChk.Text = "Video Capture : OFF"
        End If

        ' Check to see if RP7 Installed by dir C:\Users\username\AppData\Roaming\RP Xtras\Timing Data
        If Directory.Exists(RP7Dir) Then
            'MsgBox("found")
            RP7_CheckBox.Text = "RP7 on Save"
            RP7_CheckBox.Checked = True
            Button2.Enabled = True
        Else
            'MsgBox("none")
            RP7_CheckBox.Text = "RP7 Not Found"
            RP7_CheckBox.Checked = False
            RP7_CheckBox.Enabled = False
            Button2.Enabled = False
        End If

        ' Create tempory racefile directory
        Directory.CreateDirectory(RaceFileDir)
        'Start Chrome is not running
        If Process.GetProcessesByName("chrome").Length = 0 Then
            'Process.Start("chrome.exe")
            Dim URL As String = "https://timing.rowingmanager.com/"
            Process.Start(URL)
        End If
        'Set App to focus
        Me.BringToFront()
        Me.Width = 545
        Me.Height = 425

        TimerSpeed_value_lbl.Text = Timer1.Interval
    End Sub

    ' When the value of the COM PORT drop doewn list changes,
    ' copy the new value to the comPORT variable.
    Private Sub comPort_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comPort_ComboBox.SelectedIndexChanged
        If (comPort_ComboBox.SelectedItem <> "") Then
            selected_COM_PORT = comPort_ComboBox.SelectedItem
            'Store last selected Com Port
            Dim SoftwareKey As RegistryKey = Registry.CurrentUser.CreateSubKey("RQGizmo", True)
            SoftwareKey.SetValue("ComPort", selected_COM_PORT)
        End If
    End Sub

    ' Try to open the com port or close the port if already open
    ' Note: There is no error catching. If the connection attampt fails the app will crash!
    Private Sub connect_BTN_Click(sender As Object, e As EventArgs) Handles connect_BTN.Click
        If (connect_BTN.Text = "CONNECT") Then
            autoconnect()
        Else
            Try
                SerialPort1.Close()
                btnConnect.Text = "Connect"
                ArduinoConnected = False
            Catch ex As Exception
                MessageBox.Show("Serial Port is already closed!")
            End Try
        End If
        ' Separating opening the port and setting the variables like this makes the
        ' code a little more clear.
        If (SerialPort1.IsOpen) = True Then
            'connect_lbl.Text = "Timing Control on :" + SerialPort1.PortName
            'connect_BTN.Text = "DIS-CONNECT"
            'Timer1.Enabled = True
            'timer_LBL.Text = "TIMER: ON"
            connect_BTN.BackColor = SystemColors.Control
        Else
            connect_lbl.Text = "Timing Control Not connected"
            connect_BTN.Text = "CONNECT"
            connect_BTN.BackColor = Color.IndianRed
            Timer1.Enabled = False
            timer_LBL.Text = "TIMER: OFF"
        End If

    End Sub

    ' Process received data. Append the data to the text box
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'stop the timer (stops this function being called while it is still working
        Timer1.Enabled = False
        timer_LBL.Text = "TIMER: OFF"

        'Check if connection lost   
        If Not SerialPort1.IsOpen And ArduinoConnected Then
            SerialPort1.Close()
            btnConnect.Text = "Connect"
            ArduinoConnected = False
            connect_lbl.Text = "Timing Control Not connected"
            connect_BTN.Text = "CONNECT"
            connect_BTN.BackColor = Color.IndianRed
            MsgBox("Connectin was lost with timing control")
        Else
            ' get any new data and add the the global variable receivedData
            receivedData = ReceiveSerialData()

            If (receivedData <> "") Then
                recData_RichTextBox.AppendText(receivedData) '"RD = " +

                'If receivedData contains a "<" and a ">" then we have data
                If ((receivedData.Contains("<") And receivedData.Contains(">"))) Then
                    parseData()
                End If

                recData_RichTextBox.SelectionStart = recData_RichTextBox.TextLength
                recData_RichTextBox.ScrollToCaret()

            End If
            ' restart the timer
            Timer1.Enabled = True
            timer_LBL.Text = "TIMER: ON"

        End If

    End Sub

    ' Check for new data
    Function ReceiveSerialData() As String
        Dim returnData As String = ""
        Dim Incoming As String = ""
        Try
            Incoming = SerialPort1.ReadExisting()
            If Incoming IsNot Nothing Then
                returnData = Incoming
            End If
        Catch ex As TimeoutException
            Return "Error: Serial Port read timed out."
        End Try
        Return returnData
    End Function

    Function parseData()

        ' uses the global variable receivedData
        Dim pos1 As Integer
        Dim pos2 As Integer
        Dim length As Integer
        Dim newCommand As String
        Dim done As Boolean = False

        While (Not done)

            pos1 = receivedData.IndexOf("<") + 1
            pos2 = receivedData.IndexOf(">") + 1

            'occasionally we may not get complete data and the end marker will be in front of the start marker
            ' for exampe "55><"
            ' if pos2 < pos1 then remove the first part of the string from receivedData
            If (pos2 < pos1) Then
                receivedData = Microsoft.VisualBasic.Mid(receivedData, pos2 + 1)
                pos1 = receivedData.IndexOf("<") + 1
                pos2 = receivedData.IndexOf(">") + 1
            End If

            If (pos1 = 0 Or pos2 = 0) Then
                ' we do not have both start and end markers and we are done
                done = True

            Else
                ' we have both start and end markers

                length = pos2 - pos1 + 1
                If (length > 0) Then

                    'remove the start and end markers from the command
                    newCommand = Mid(receivedData, pos1 + 1, length - 2)

                    ' show the command in the text box
                    'recData_RichTextBox.AppendText("CMD = " & newCommand & vbCrLf)

                    'remove the command from receivedData
                    receivedData = Mid(receivedData, pos2 + 1)


                    ' M for Margins
                    If newCommand(0) = "M" Then
                        If TM_ChromeLanes Then AppActivate("Chrome")
                        ReceiveResults(newCommand)
                    End If '(newCommand(0) = "M")

                    ' V for Video Capture
                    If newCommand(0) = "V" Then
                        StartStopRace(newCommand)
                    End If ' (newCommand(0) = "B")

                    ' R for Race Number
                    If newCommand(0) = "R" Then
                        Dim DataVal() As String = Split(newCommand, ",")
                        'AddRace(DataVal(1).ToString)
                        AddRace(DataVal(1))
                    End If

                    ' S for Start Time
                    If newCommand(0) = "S" Then
                        'MsgBox("caught")
                        'START,Race,ms since start
                        'Split the command up        
                        Dim DataVal() As String = Split(newCommand, ",")

                        'Ignore if Race 0 - test signal
                        If DataVal(1) > 0 Then
                            'record time received
                            Dim MsgTime As TimeSpan = DateTime.Now.TimeOfDay
                            'calculate delay time from packet
                            Dim DelayTime As TimeSpan = TimeSpan.FromMilliseconds(CInt(DataVal(2)))

                            'Add Start times to form
                            Dim adi As New ListViewItem(DataVal(1)) ' will add to column(0)
                            adi.SubItems.Add(CInt(DataVal(2)).ToString(RaceNoFmt)) 'will add to column(1)
                            adi.SubItems.Add(MsgTime.Subtract(DelayTime).ToString(FinishTimeFmt)) 'will add to column(2)
                            ListView1.Items.Add(adi)
                            ListView1.View = View.Details

                            'Add Start times to output file
                            Dim filePath As String = Path.Combine(RaceFileDir, DateTime.Today.ToString("yyyymmdd") & "StartLog.txt")
                            Dim fileExists As Boolean = File.Exists(filePath)

                            Using writer As New StreamWriter(filePath, True)
                                If Not fileExists Then
                                    writer.WriteLine("Start Time Log for today " & DateTime.Today.ToString("dd-MMM-yyyy"))
                                    writer.WriteLine("Logged Time,Start Time,Race,Delay")
                                End If
                                writer.WriteLine(DateTime.Now & ", " & MsgTime.Subtract(DelayTime).ToString(FinishTimeFmt) & ", " & newCommand)
                            End Using
                        End If
                    End If

                    ' F for Finish Time. First crew across line
                    If newCommand(0) = "F" Then
                        'Split the command up        
                        Dim DataVal() As String = Split(newCommand, ",")
                        ' Set Race nummber from Serial
                        Dim RaceNoStr = CInt(DataVal(1)).ToString(RaceNoFmt)
                        ' Set up New Race
                        AddRace(RaceNoStr)
                        ' prevent changing race no
                        RaceCombo.Enabled = False
                        FinishTxtBox.Text = DateTime.Now.TimeOfDay.ToString(FinishTimeFmt)
                        If TM_ChromeLanes Then AppActivate("Chrome")
                    End If

                    commandCount = commandCount + 1
                    commandCountVal_lbl.Text = commandCount

                End If ' (length > 0) 

            End If '(pos1 = 0 Or pos2 = 0)

        End While

    End Function

    ' Clear the text box
    Private Sub clear_BTN_Click(sender As Object, e As EventArgs) Handles clear_BTN.Click
        recData_RichTextBox.Text = ""

        commandCount = 0
        commandCountVal_lbl.Text = commandCount
    End Sub

    ' Test if time value is seconds, then check for valid time formats m.s.f or m:s.f or h.m.s
    Function ConvertToTime(str As String) As TimeSpan
        'Check for empty string
        If String.IsNullOrEmpty(str) Then

        Else
            'Check if valid number of second and convert to time

            If Double.TryParse(str, 0) Then
                ConvertToTime = TimeSpan.FromSeconds(Convert.ToDouble(str))
                'MsgBox("Time is number" + Convert.ToDouble(str).ToString + " -- " + ConvertToTime.ToString(RaceTimeFmt))
            Else
                'if not seconds check for time
                'check if 2 decimals ie m.s.ms and replace first . with :
                If str.Length - str.Replace(".", "").Length = 2 Then
                    Dim position As Integer = str.IndexOf(".")
                    str = str.Substring(0, position) + ":" + str.Substring(position + ".".Length)
                End If
                ' check if m.s.ms and add h:
                If str.Length - str.Replace(":", "").Length = 1 Then
                    str = "0:" + str
                End If
                If Not TimeSpan.TryParse(str, ConvertToTime) Then
                    'if not valid time format set to negative time
                    ConvertToTime = TimeSpan.FromSeconds(-1)
                    'MsgBox("Time a string " + ConvertToTime.ToString)
                Else
                    '   MsgBox("Time is time format " + ConvertToTime.ToString(RaceTimeFmt))
                End If
            End If
        End If
    End Function

    ' ** CHECKED ** Send trigger to OBS Studio to start and stop recording VR/VO[0],Race[1],Chrome[2]
    Sub StartStopRace(Keys As String, Optional Race As Integer = 0)
        'Split the command up        
        Dim DataVal() As String = Split(Keys, ",")
        Dim TrigStr As String = ""
        Dim RaceVidFile As String
        ' Assign Start or stop functions'
        If DataVal(0) = "VO" Then
            VidCapChk.Text = "Video Capture : OFF"
            TrigStr = "{F9}" 'Stop Capture
            If DataGridView1.RowCount > 1 Then RaceSaveBtn.Enabled = True
            RaceCombo.Enabled = True
            Me.BackColor = DefaultBackColor
        ElseIf DataVal(0) = "VR" Then
            ' Set Race nummber from Serial
            Dim RaceNoStr = CInt(DataVal(1)).ToString(RaceNoFmt)
            Me.BackColor = Color.IndianRed
            ' Set up New Race
            'AddRace(RaceNoStr)
            ' prevent changing race no
            'RaceCombo.Enabled = False
            'Start VideoCapture if active
            If VidCapChk.Checked Then
                VidCapChk.Text = "Video Capture : Recording" 'Set label to show video recording
                ' Update Video Capture Race Number
                Dim OBSRaceNo As String = "C:\Users\camtm\OneDrive\RQ_BRO\CodingStuff\OBSRaceNo.txt"
                'Pass the filepath And filename to the StreamWriter Constructor
                Dim sw As StreamWriter = New StreamWriter(OBSRaceNo)
                'Write a line of text
                sw.WriteLine("Race : " + RaceNoStr)
                sw.Close()
            End If
            If DataVal.Count = 3 Then
                TM_ChromeLanes = True
            Else
                TM_ChromeLanes = False
            End If
            TrigStr = "{F8}" 'Start Capture
            'If DataGridView1.RowCount > 1 Then DataGridView1.Rows.Clear() '- clear the table

        End If

        'If video capture enabled then send hotkey
        If VidCapChk.Checked Then

            '***Fixes*** Add Error checking ** What if application not running + dynamic name + return focus to this app
            ' Activate the OBS application.
            AppActivate("OBS ")
            ' Send the keystrokes to the Notepad application.
            My.Computer.Keyboard.SendKeys(TrigStr, True)

            If TrigStr = "{F9}" Then
                'Rename last file in Video directory with RaceNo
                RaceVidFile = "Regatta" + CInt(RegattaTxtBox.Text).ToString(RagattaNoFmt) + "Race" + CInt(RaceCombo.Text).ToString(RaceNoFmt) + ".mkv"

                'fails if filenot closed by OBS so loop
                Dim Filsaved As Boolean = False
                Dim Attempts As Int16 = 0
                Do While Not Filsaved
                    Try
                        My.Computer.FileSystem.RenameFile(FindLastModified, RaceVidFile)
                        Filsaved = True
                    Catch ex As Exception
                        Attempts = Attempts + 1
                    End Try
                Loop
                'MsgBox(Attempts)
            End If
        End If
        Me.Activate()

        If TM_ChromeLanes Then AppActivate("Chrome")

    End Sub

    ' Searches the directory to find the most recent file
    Function FindLastModified() As String
        Dim dteDate As Date
        Dim sFilePathName As String = ""
        'Dim RaceFileFormat As String = "Regatta" + RegattaNo.ToString(RagattaNoFmt) + "Race*.txt"
        Dim VideoFiles As String() = Directory.GetFiles(ExpVidDir)
        For Each fileName As String In VideoFiles
            If dteDate < FileDateTime(fileName) Then
                dteDate = FileDateTime(fileName)
                sFilePathName = fileName
            End If
        Next

        FindLastModified = sFilePathName
    End Function

    ' ** CHECKED ** Listens for Data from Gizmo <MDATA[0],Race[1],Place[2],Seconds[3]>
    ' Verifys and adds to DataGridView.row       Place[0],Lane[1],Margin[2],Time[3]
    Sub ReceiveResults(Data As String)
        Debug.Print(Data)
        Dim DataVal() As String
        Dim MagrinStr As Double
        Dim StartTime As TimeSpan
        Dim FinishTime As TimeSpan
        If Data <> "" Then
            'Set Race Label
            DataVal = Split(Data, ",")
            'Update current Race
            RaceCombo.Text = DataVal(1)
            'Get Margin as seconds
            Double.TryParse(DataVal(3), MagrinStr)
            'Enter data into Grid
            If DataVal(2) = "1" Then
                'Calculate Race Time or set to 0
                If Not TimeSpan.TryParse(FinishTxtBox.Text, FinishTime) Then FinishTime = TimeSpan.Zero
                If Not TimeSpan.TryParse(StartTxtBox.Text, StartTime) Then StartTime = FinishTime
                DataGridView1.Rows.Add(DataVal(2), "", (FinishTime - StartTime).ToString(RaceTimeFmt))
            Else
                DataGridView1.Rows.Add(DataVal(2), "", MagrinStr.ToString("0.00"))
            End If
            'Set backgroud to Red
            DataGridView1.Rows(DataGridView1.RowCount - 2).Cells(1).Style.BackColor = Color.Red
        End If
        CalcMargins()
        RaceSaveBtn.Enabled = True
        ' Move to first place lane no cell
        DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(1)
        DataGridView1.Focus()
    End Sub

    ' ** CHECKED ** Recalcualtes Margins Columns in DataGrid Place[0],Lane[1],Margin[2],Time[3]
    Sub CalcMargins()
        Dim PlaceTime As TimeSpan
        Dim PlaceMargin As TimeSpan

        'Ensure margins are sorted by place order
        'DataGridView1.Sort(DataGridView1.Columns(0), 0)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Index() < (DataGridView1.RowCount - 1) Then
                PlaceMargin = ConvertToTime(row.Cells(2).Value)
                'MsgBox(row.Index().ToString + "  " + PlaceMargin.ToString)
                If row.Index = 0 Then
                    ' For 1st place convert margin to time else 0
                    If PlaceMargin < TimeSpan.Zero Then PlaceMargin = TimeSpan.FromSeconds(0)
                    row.Cells(3).Value = PlaceMargin.ToString(RaceTimeFmt)
                Else
                    ' For places convert margin to time else 0 then add to previous time
                    If PlaceMargin < TimeSpan.Zero Then PlaceMargin = TimeSpan.FromSeconds(0)
                    PlaceTime = ConvertToTime(DataGridView1.Rows(row.Index - 1).Cells(3).Value)
                    If PlaceTime < TimeSpan.Zero Then PlaceTime = TimeSpan.FromSeconds(0)
                    PlaceTime = PlaceMargin + PlaceTime
                    row.Cells(3).Value = PlaceTime.ToString(RaceTimeFmt)
                End If
            End If
        Next
    End Sub

    ' ** CHECKED ** Create .txt file for RP7 then copies into RP7 location
    'Reads DataGridView PLACE[0],LANE[1],MARGIN[2],TIME[3]
    'populates a text file RESULTS[0],LANE[1],PLACE[2],TIME[3]
    Private Sub RP7But_Click(sender As Object, e As EventArgs) Handles RaceSaveBtn.Click
        SaveRace(RaceCombo.SelectedItem)
    End Sub
    Private Sub SaveRace(RaceNoStr As String)
        'MsgBox("Saving Race : " + RaceNoStr)
        Dim RaceFile As String
        Dim RaceFilePath As String
        Dim PlaceTime As TimeSpan
        Dim Lane As String = ""
        Dim NTTExpStr As String = ""
        Dim FinishTime As TimeSpan
        Dim StartTime As TimeSpan
        Dim RaceTime As TimeSpan

        ' Must be at least 1 entry to create file
        If DataGridView1.RowCount > 1 Then
            RaceFile = "Regatta" + CInt(RegattaTxtBox.Text).ToString(RagattaNoFmt) + "Race" + CInt(RaceNoStr).ToString(RaceNoFmt) + ".txt"
            RaceFilePath = Path.Combine(RaceFileDir, RaceFile)
            'Check if already existing
            If Dir(RaceFilePath) <> "" Then
                If MsgBox("Already existing, Overwrite ?", vbYesNo) = vbNo Then Exit Sub
            End If

            'Pass the filepath And filename to the StreamWriter Constructor
            Dim sw As New StreamWriter(RaceFilePath)

            'Search for Start and Finish times
            If TimeSpan.TryParse(FinishTxtBox.Text, FinishTime) Then
                If Not TimeSpan.TryParse("00:" & DataGridView1.Rows(0).Cells(3).Value, RaceTime) Then RaceTime = TimeSpan.Zero
                If Not TimeSpan.TryParse(StartTxtBox.Text, StartTime) Then
                    sw.WriteLine("STARTTIME," + (FinishTime - RaceTime).ToString(RP7TimeFmt))
                    StartTxtBox.Text = (FinishTime - RaceTime).ToString(FinishTimeFmt)
                Else
                    sw.WriteLine("STARTTIME," + StartTime.ToString(RP7TimeFmt))
                End If

                sw.WriteLine("RQSTARTTIME," + StartTxtBox.Text)
                sw.WriteLine("FINISHTIME," + FinishTxtBox.Text)
            End If

            'Sort by Place number
            'DataGridView1.Sort(DataGridView1.Columns(0), 0)
            NTTExpStr = ""
            'Cycle through rows of Grid
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Index < DataGridView1.RowCount - 1 Then
                    'If Lane is number ??? why ??
                    If Double.TryParse(row.Cells(1).Value, 0) Then Lane = row.Cells(1).Value Else Lane = 0
                    PlaceTime = ConvertToTime(row.Cells(2).Value.ToString)
                    If PlaceTime <= TimeSpan.Zero Then
                        ' 1st place 'NO TIME' if NTT and export margins not time.
                        If row.Index = 0 Then
                            sw.WriteLine("RESULT," + Lane + "," + row.Cells(0).Value.ToString + ",NO TIME")
                            NTTExpStr = "+"
                        Else
                            ' use margin text for placing
                            sw.WriteLine("RESULT," + Lane + ",0," + row.Cells(2).Value.ToString) '(RaceTimeFmt))
                        End If
                    Else
                        If NTTExpStr = "+" Then
                            sw.WriteLine("RESULT," + Lane + "," + row.Cells(0).Value.ToString + "," + row.Cells(2).Value.ToString)
                        Else
                            sw.WriteLine("RESULT," + Lane + "," + row.Cells(0).Value.ToString + "," + row.Cells(3).Value.ToString)
                        End If
                    End If
                End If
            Next
            'Close File
            sw.Close()
            ' if the RP7 Export selected copy file across
            If RP7_CheckBox.Checked Then
                ' Copies to RP7 Dir and overrites any existing file
                'MsgBox(Path.Combine(RP7Dir, "Race" + CInt(RaceNoStr).ToString(RaceNoFmt) + ".txt"))
                File.Copy(RaceFilePath, Path.Combine(RP7Dir, "Race" + CInt(RaceNoStr).ToString(RaceNoFmt) + ".txt"), True)
            End If
            If TM_ChromeLanes Then SendLanes()
            RaceSaveBtn.Enabled = False
        End If
        'MsgBox("save sucessful")
    End Sub
    ' Shows and Hides the debug window
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = ">>" Then
            Me.Width = Me.Width + 200
            Button1.Text = "<<"
        Else
            Button1.Text = ">>"
            Me.Width = Me.Width - 200
        End If
    End Sub

    '** CHECKED ** Updates the available comports
    Private Sub RefreshCommButton_Click(sender As Object, e As EventArgs) Handles RefreshCommButton.Click
        selected_COM_PORT = comPort_ComboBox.SelectedItem
        comPort_ComboBox.Items.Clear()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            comPort_ComboBox.Items.Add(sp)
        Next
        If (comPort_ComboBox.SelectedItem <> "") Then
            comPort_ComboBox.SelectedItem = selected_COM_PORT
        End If
    End Sub
    '** CHECKED ** Opens the Video storage folder
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'MsgBox(Environment.GetFolderPath(Environment.SpecialFolder.System.UserProfile) + "\Videos")
        'Dim RaceVidFile As String = "Regatta" + CInt(RegattaTxtBox.Text).ToString(RagattaNoFmt) + "Race" + CInt(RaceCombo.Text).ToString(RaceNoFmt) + ".mkv"
        'MsgBox(ExpVidDir + "\" + RaceVidFile)
        Process.Start("explorer.exe", ExpVidDir)
    End Sub
    ''** CHECKED ** unchecks capture if OBS is not running --- Fix in Future
    Private Sub VidCapChk_CheckedChanged(sender As Object, e As EventArgs) Handles VidCapChk.CheckedChanged
        If VidCapChk.Checked And Process.GetProcessesByName("obs64").Length = 0 Then
            '*** start it if on machine !!! :(
            'Process.Start("obs64", System.Environment.GetFolderPath(Environment.SpecialFolder.MyVideos))
            MsgBox("Ensure OBS is running to capture races")
            VidCapChk.Checked = False
        End If
    End Sub

    '** CHECKED ** Looks for *.txt files in the directory and populates the Race ComboBox
    Sub LoadRaces(Optional curRace As String = "")
        ' Search directory for RaceFiles
        Dim RegattaNo As Int16
        If Int16.TryParse(RegattaTxtBox.Text, RegattaNo) Then
            Dim RaceFileFormat As String = "Regatta" + RegattaNo.ToString(RagattaNoFmt) + "Race*.txt"
            Dim RaceFiles As String() = Directory.GetFiles(RaceFileDir, RaceFileFormat)
            RaceCombo.Items.Clear()
            For Each fileName As String In RaceFiles
                If (System.IO.File.Exists(fileName)) Then
                    If (Regex.IsMatch(fileName, "race\d{" + RaceNoFmt.Length.ToString + "}.txt$", RegexOptions.IgnoreCase)) Then
                        'Read File and Print Result if its true
                        Dim RaceNo As String = Mid(fileName, fileName.Length - 6, 3)
                        RaceCombo.Items.Add(RaceNo)
                    End If
                End If
            Next

            If curRace <> "" And RaceCombo.SelectedItem <> curRace Then RaceCombo.SelectedItem = curRace
        End If

    End Sub

    '** CHECKED ** Reads a text file RESULTS[0],LANE[1],PLACE[2],TIME[3]
    '           Populates DataGridView PLACE[0],LANE[1],MARGIN[2],TIME[3]
    Sub LoadRace(RaceNoStr As String)
        Dim RaceFile As String
        Dim CurrTime As TimeSpan
        Dim PrevTime As TimeSpan
        RaceFile = "Regatta" + CInt(RegattaTxtBox.Text).ToString(RagattaNoFmt) + "Race" + RaceNoStr + ".txt"
        RaceFile = Path.Combine(RaceFileDir, RaceFile)
        'Check the file exists, if not do nothing
        If Dir(RaceFile) <> "" Then
            ' Clear datagrid for new inforation
            DataGridView1.Rows.Clear()
            StartTxtBox.Text = ""
            FinishTxtBox.Text = ""
            Dim lines = File.ReadLines(RaceFile)
            Dim NTT As Boolean = False
            For Each line In lines
                'Split data by comma
                Dim DataVal() As String = Split(line, ",")
                ' Add Start or Finish time if available
                If Mid(line, 1, 6).ToString = "RQSTAR" Then StartTxtBox.Text = DataVal(1).ToString
                If Mid(line, 1, 6).ToString = "FINISH" Then FinishTxtBox.Text = DataVal(1).ToString
                ' If line begins with RESULT create row in datagrid
                If Mid(line, 1, 6).ToString = "RESULT" Then
                    'Check if margin is time    
                    If Not TimeSpan.TryParse("00:" + DataVal(3), CurrTime) Then CurrTime = TimeSpan.FromSeconds(0)
                    'Find previous time for places >2
                    If DataGridView1.RowCount > 1 Then

                        TimeSpan.TryParse("0:" + DataGridView1.Rows(DataGridView1.RowCount - 2).Cells(3).Value, PrevTime)
                        '    MsgBox("find last time" + PrevTime.ToString)
                    Else
                        PrevTime = TimeSpan.FromSeconds(0)
                    End If

                    'check if place is integer
                    Dim Place As Integer
                    Integer.TryParse(DataVal(2).ToString, Place)
                    'MsgBox("valid time 2+ placing " + Place.ToString + " C " + CurrTime.ToString(RaceTimeFmt) + " P " + PrevTime.ToString(RaceTimeFmt))
                    Select Case Place
                        Case 1
                            If DataVal(3) = "NO TIME" Then
                                DataGridView1.Rows.Add(Place.ToString, DataVal(1).ToString, "NTT", CurrTime.ToString(RaceTimeFmt))
                                NTT = True
                            Else
                                DataGridView1.Rows.Add(Place.ToString, DataVal(1).ToString, CurrTime.ToString(RaceTimeFmt), CurrTime.ToString(RaceTimeFmt))
                            End If

                        Case 0
                            'Output if Place is string
                            DataGridView1.Rows.Add(DataGridView1.RowCount, DataVal(1).ToString, DataVal(3).ToString, CurrTime.ToString(RaceTimeFmt))
                        Case Else
                            If NTT Then
                                DataGridView1.Rows.Add(Place.ToString, DataVal(1).ToString, DataVal(3).ToString, CurrTime.ToString(RaceTimeFmt))
                            Else
                                DataGridView1.Rows.Add(Place.ToString, DataVal(1).ToString, (CurrTime - PrevTime).TotalSeconds.ToString("0.00"), CurrTime.ToString(RaceTimeFmt))

                            End If
                    End Select

                End If
            Next
            RaceSaveBtn.Enabled = False
        Else
            'If new Race File then clear data
            'MsgBox(RaceNoStr + " new race no created")
            If DataGridView1.RowCount > 1 Then DataGridView1.Rows.Clear()
        End If
    End Sub

    'loads race if combobox changed
    Private Sub RaceCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RaceCombo.SelectedIndexChanged
        'check if existing has been edited and save if entries exist
        If RaceSaveBtn.Enabled And DataGridView1.RowCount > 1 And PrevRace <> "" Then
            SaveRace(PrevRace)
            'MsgBox(PrevRace + " saved, loading " + RaceCombo.SelectedItem)
        End If
        'Set new race into Prev race
        If RaceCombo.SelectedItem <> PrevRace Then PrevRace = RaceCombo.SelectedItem

        'search race times and select start if available
        StartTxtBox.Text = ""
        For Each lvi As ListViewItem In ListView1.Items
            If CInt(lvi.SubItems(0).Text) = CInt(RaceCombo.SelectedItem) Then
                ''MsgBox("first race time found")
                StartTxtBox.Text = lvi.SubItems(2).Text
                'ListView1.SelectedIndices.Add(lvi.Index)
                Exit For
            End If
        Next
        'Load new selection if box enabled
        If RaceCombo.Enabled Then LoadRace(RaceCombo.SelectedItem)
        RaceCombo.Enabled = True
    End Sub

    'Opens the Race File location
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Process.Start("explorer.exe", RaceFileDir)
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'MsgBox(DataGridView1.CurrentCellAddress.ToString)
        'MsgBox(e.ColumnIndex.ToString)

    End Sub
    ' ** CHECKED ** if cell updated then check some things
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Only check if more than 1 row
        If DataGridView1.RowCount > 1 Then
            Select Case e.ColumnIndex
                'if margin updated then recalc
                Case 2
                    ' Get time from margin changed
                    Dim CurrTime As TimeSpan = ConvertToTime(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    ' check if value then set to time format else check string and highlight error
                    If CurrTime < TimeSpan.Zero Then
                        Select Case (DataGridView1.CurrentCell.Value)
                            Case "DNF", "DNS", "NTT"
                                DataGridView1.CurrentCell.Style.BackColor = Color.Green
                            Case Else
                                'MsgBox("here?")
                                DataGridView1.CurrentCell.Style.BackColor = Color.Red
                        End Select
                    Else
                        DataGridView1.CurrentCell.Style.BackColor = Color.White
                        'if 1st - format time else format seconds
                        If e.RowIndex = 0 Then
                            DataGridView1.CurrentCell.Value = CurrTime.ToString(RaceTimeFmt)
                        Else
                            DataGridView1.CurrentCell.Value = CurrTime.TotalSeconds.ToString("0.00")
                        End If
                    End If
                    CalcMargins()
                    RaceSaveBtn.Enabled = True
                Case 1
                    ' Check valid lane
                    Dim Lane As Double
                    If Double.TryParse(DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, Lane) Then
                        If Lane <= 10 And Lane >= 0 Then
                            If Lane = 0 Then DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "10"
                            DataGridView1.CurrentCell.Style.BackColor = Color.White
                        Else
                            'MsgBox("Lane No too large")
                            DataGridView1.CurrentCell.Style.BackColor = Color.Red
                        End If
                    Else
                        'MsgBox("Lane No not numeric")
                        DataGridView1.CurrentCell.Style.BackColor = Color.Red
                        'DataGridView1.CurrentCell.BeginEdit(True)
                    End If
                    'If (e.RowIndex = DataGridView1.RowCount - 2 And DataGridView1.Rows(e.RowIndex).Cells(0).Value IsNot Nothing) Then DataGridView1.CurrentCell = DataGridView1(2, 0) 'MsgBox("Lastrow")
                    RaceSaveBtn.Enabled = True
            End Select
            'Add Lane number to a new row
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = 0 Then DataGridView1.Rows(e.RowIndex).Cells(0).Value = e.RowIndex + 1
        End If

    End Sub
    'Send Lanes to Chrome for Timing.rowingmanager.com
    Sub SendLanes()
        'Activate Chrome
        AppActivate("Chrome")

        'Sends lanes to Chrome - change 10 to 0
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Index < DataGridView1.RowCount - 1 Then
                Dim Lane As String = row.Cells(1).Value
                If Lane = "10" Then Lane = "0"
                My.Computer.Keyboard.SendKeys(Lane, True)
            End If
        Next

        'Return to this app
        Me.Activate()

    End Sub
    'Open RP7 Files
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("explorer.exe", RP7Dir)
    End Sub

    Private Sub DataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellChanged
        'MsgBox(sender.CurrentCell.ToString + " " + DataGridView1.RowCount.ToString)
        Exit Sub
        ' Check if there is at least on row of data
        If DataGridView1.RowCount > 1 Then
            'Check if cell is last row and column 1
            If sender.CurrentCell.RowIndex = DataGridView1.RowCount - 1 And sender.CurrentCell.ColumnIndex = 1 Then
                ' move to time entry
                'MsgBox("last lane entered" + DataGridView1(2, 0).Value.ToString)
                'DataGridView1(2, 0).Selected = True
                'DataGridView1.CurrentCell.RowIndex = 0
                'DataGridView1.CurrentCell.RIndex = 0
                'DataGridView1.Rows(0).Cells(1).Selected = True
                '                DataGridView1.CurrentCell = DataGridView1(2, 0)
            End If
        End If
    End Sub
    Private Sub SendKeysToAnyApplication()
        'For Each 
    End Sub

    Private Sub RegattaTxtBox_Leave(sender As Object, e As EventArgs) Handles RegattaTxtBox.Leave
        Dim RegattaNo As Int16 'String = InputBox("RaceNo", "Manual Race Entry")
        'Check it integer and less than 1000
        If Int16.TryParse(RegattaTxtBox.Text, RegattaNo) Then
            If RegattaNo >= 0 And RegattaNo < 10000 Then
                Dim SoftwareKey As RegistryKey = Registry.CurrentUser.CreateSubKey("RQGizmo", True)
                SoftwareKey.SetValue("Regatta", RegattaTxtBox.Text)
                LoadRaces()
            Else
                MsgBox("Race exceeds 9999")
                RegattaTxtBox.Clear()
            End If
        Else
            'Incorrect Racenumber - blank or string
            MsgBox("Incorrect Race Number")
            RegattaTxtBox.Clear()
        End If
    End Sub

    'Manually Add Race 
    Private Sub AddRaceBtn_Click(sender As Object, e As EventArgs) Handles AddRaceBtn.Click
        Dim RaceNo As Int16 'String = InputBox("RaceNo", "Manual Race Entry")
        'Check it integer and less than 1000
        If Int16.TryParse(InputBox("RaceNo", "Manual Race Entry"), RaceNo) Then
            If RaceNo > 0 And RaceNo < 1000 Then
                AddRace(RaceNo.ToString(RaceNoFmt), True)
            Else
                MsgBox("Race exceeds 999")
            End If
        Else
            'Incorrect Racenumber - blank or string
            MsgBox("Incorrect Race Number")
        End If
    End Sub
    Private Sub AddRace(RaceNoStr As String, Optional CopyMargins As Boolean = False)
        'Check Race has leading 0s
        If RaceNoStr.Length < RaceNoFmt.Length Then RaceNoStr = Strings.StrDup(RaceNoFmt.Length - RaceNoStr.Length, "0") + RaceNoStr
        'check if exsisting else add
        If RaceCombo.FindString(RaceNoStr) < 0 Then RaceCombo.Items.Add(RaceNoStr)
        ' If margins exist, ask to copy to new race
        If DataGridView1.RowCount > 1 And CopyMargins Then
            If MsgBox("Copy Margins", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then RaceCombo.Enabled = False
        End If
        ' Change to the new race if not selected
        If RaceCombo.SelectedItem <> RaceNoStr Then RaceCombo.SelectedItem = RaceNoStr
        RaceSaveBtn.Enabled = True
    End Sub

    'Seach open serial port for connection, send "3/" and listen for reply
    Private Sub autoconnect()
        For Each sp As String In My.Computer.Ports.SerialPortNames
            Try
                SerialPort1.PortName = sp                           '**
                SerialPort1.BaudRate = 9600                         '**
                SerialPort1.DataBits = 8                            '**
                SerialPort1.Parity = Parity.None                    '**
                SerialPort1.StopBits = StopBits.One                 '**
                SerialPort1.Handshake = Handshake.None              '**
                SerialPort1.Encoding = System.Text.Encoding.Default '**

                SerialPort1.DtrEnable = True
                SerialPort1.RtsEnable = True
                SerialPort1.ReadTimeout = 10000

                SerialPort1.Open()
                'Sent msg and wait for response
                SerialPort1.Write("3")
                TimerConnect.Interval = 250
                TimerConnect.Start()
                'Dim str As String = SerialPort1.ReadExisting()
                'MsgBox(SerialPort1.PortName + " " + str)
                If False Then 'str.Contains("51") Then
                    'MsgBox("Gizmo Found " + SerialPort1.PortName + " - " + Str())
                    ArduinoConnected = True
                    lstConsole.Items.Add("Arduino Connected")
                    btnConnect.Text = "Connected !" + SerialPort1.PortName
                End If
                While TimerConnect.Enabled And ArduinoConnected = False
                    Application.DoEvents()
                End While
                If ArduinoConnected Then
                    connect_lbl.Text = "Timing Control on :" + SerialPort1.PortName
                    connect_BTN.Text = "DIS-CONNECT"
                    connect_BTN.BackColor = SystemColors.Control
                    Timer1.Enabled = True
                    Timer1.Interval = 100
                    timer_LBL.Text = "TIMER: ON"
                    btnConnect.Text = "Disconnect" '' old button
                    'SerialPort1.Write("0")
                    Exit For
                End If
                SerialPort1.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Next
        If ArduinoConnected = False Then
            MsgBox("Timing control failed to connect. Please check that it is plugged in.")
            connect_BTN.BackColor = Color.IndianRed
        End If
    End Sub

    Private Sub BtnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Test()
        End
        If ArduinoConnected Then
            SerialPort1.Close()
            btnConnect.Text = "Connect"
            ArduinoConnected = False
        Else
            autoconnect()
        End If
    End Sub

    ' Timer for arduino search
    Public Sub TimerConnect_Tick(sender As Object, e As EventArgs) Handles TimerConnect.Tick
        TimerConnect.Stop()
    End Sub

    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles ListView1.ItemSelectionChanged
        'MsgBox(sender.ToString)
        MsgBox(e.ItemIndex)
        If ListView1.FocusedItem Is Nothing Then
        Else
            MsgBox("Race sel : " + ListView1.Items(e.ItemIndex).SubItems(0).Text)

            If CInt(RaceCombo.SelectedItem) <> ListView1.Items(e.ItemIndex).SubItems(0).Text Then
                MsgBox("bad race selected")
            Else
                StartTxtBox.Text = ListView1.Items(e.ItemIndex).SubItems(2).Text

            End If

        End If

        Exit Sub
        For Each lvi As ListViewItem In ListView1.Items

            If CInt(lvi.SubItems(0).Text) = CInt(RaceCombo.SelectedItem) Then
                StartTxtBox.Text = lvi.SubItems(2).Text
                lvi.Selected = True
                'ListView1.SelectedIndices.Add(lvi.Index)
                Exit For
            End If

        Next

        '   MsgBox(ListView1.SelectedItems.Item(ListView1.SelectedItems.Index))
        'StartTxtBox.Text = ListView1.SelectedItems.Item(2).ToString 'MsgTime.Subtract(DelayTime).ToString("hh':'mm':'ss'.'fff")

    End Sub

    Dim currentMouseOverRow As Integer

    Structure MyStructure
        Public x As Integer
        Public y As Integer
    End Structure

    Private Sub DataGridView1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseClick
        Dim Mystruct As MyStructure
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim m As New System.Windows.Forms.ContextMenuStrip
            Dim MymenuToolStripMenuItem1 As New System.Windows.Forms.ToolStripMenuItem
            MymenuToolStripMenuItem1.Text = "insert"
            AddHandler MymenuToolStripMenuItem1.Click, AddressOf MymenuToolStripMenuItem1_Click
            m.Items.Add(MymenuToolStripMenuItem1)
            'Dim MymenuToolStripMenuItem2 As New System.Windows.Forms.ToolStripMenuItem
            'MymenuToolStripMenuItem2.Text = "menu2"
            'AddHandler MymenuToolStripMenuItem2.Click, AddressOf MymenuToolStripMenuItem2_Click
            'm.Items.Add(MymenuToolStripMenuItem2)
            Mystruct.x = e.X
            Mystruct.x = e.X
            'MymenuToolStripMenuItem2.Tag = Mystruct
            currentMouseOverRow = DataGridView1.HitTest(e.X, e.Y).RowIndex

            m.Show(DataGridView1, New Point(e.X, e.Y))
        End If
    End Sub

    'Insert Row above and renumber places
    Private Sub MymenuToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'MessageBox.Show("click Menu1:" & currentMouseOverRow)
        DataGridView1.Rows.Insert(currentMouseOverRow)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Index < DataGridView1.RowCount - 1 Then
                row.Cells(0).Value = row.Index + 1
            End If
        Next
    End Sub

    Private Sub DataGridView1_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Index < DataGridView1.RowCount - 1 Then
                row.Cells(0).Value = row.Index + 1
            End If
        Next
    End Sub

    Private Sub RP7_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles RP7_CheckBox.CheckedChanged
        If RP7_CheckBox.Checked Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub
    Sub Test()
        Dim URL As String = "https://timing.rowingmanager.com/"
        Process.Start(URL)
    End Sub

    ' This runs in the background and cannot update GUI.
    ' If data is received and not connected, check if it's the finish control
    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        If Not ArduinoConnected Then
            If SerialPort1.ReadExisting.Contains("Arduino Finish") Then
                'MsgBox("Control Found " + SerialPort1.PortName)
                ArduinoConnected = True
            End If
        End If
    End Sub
End Class