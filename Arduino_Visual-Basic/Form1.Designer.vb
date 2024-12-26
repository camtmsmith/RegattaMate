<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.COMport_LBL = New System.Windows.Forms.Label()
        Me.recData_LBL = New System.Windows.Forms.Label()
        Me.timer_LBL = New System.Windows.Forms.Label()
        Me.timer_LBL_2 = New System.Windows.Forms.Label()
        Me.comPort_ComboBox = New System.Windows.Forms.ComboBox()
        Me.recData_RichTextBox = New System.Windows.Forms.RichTextBox()
        Me.count_lbl = New System.Windows.Forms.Label()
        Me.TimerSpeed_lbl = New System.Windows.Forms.Label()
        Me.commandCountVal_lbl = New System.Windows.Forms.Label()
        Me.TimerSpeed_value_lbl = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PlaceCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LaneCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MarginCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RaceLabel = New System.Windows.Forms.Label()
        Me.RaceSaveBtn = New System.Windows.Forms.Button()
        Me.RaceTextBox = New System.Windows.Forms.TextBox()
        Me.VidCapChk = New System.Windows.Forms.CheckBox()
        Me.clear_BTN = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RefreshCommButton = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.RaceCombo = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.RP7_CheckBox = New System.Windows.Forms.CheckBox()
        Me.FinishTxtBox = New System.Windows.Forms.TextBox()
        Me.StartTxtBox = New System.Windows.Forms.TextBox()
        Me.RegattaLbl = New System.Windows.Forms.Label()
        Me.RegattaTxtBox = New System.Windows.Forms.TextBox()
        Me.AddRaceBtn = New System.Windows.Forms.Button()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lstConsole = New System.Windows.Forms.ListBox()
        Me.TimerConnect = New System.Windows.Forms.Timer(Me.components)
        Me.connect_BTN = New System.Windows.Forms.Button()
        Me.connect_lbl = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Race = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Delay = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.StartTime = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        '
        'COMport_LBL
        '
        Me.COMport_LBL.AutoSize = True
        Me.COMport_LBL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.COMport_LBL.Location = New System.Drawing.Point(531, 517)
        Me.COMport_LBL.Name = "COMport_LBL"
        Me.COMport_LBL.Size = New System.Drawing.Size(88, 20)
        Me.COMport_LBL.TabIndex = 0
        Me.COMport_LBL.Text = "Comm Port"
        '
        'recData_LBL
        '
        Me.recData_LBL.AutoSize = True
        Me.recData_LBL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.recData_LBL.Location = New System.Drawing.Point(531, 108)
        Me.recData_LBL.Name = "recData_LBL"
        Me.recData_LBL.Size = New System.Drawing.Size(115, 20)
        Me.recData_LBL.TabIndex = 1
        Me.recData_LBL.Text = "DEBUG DATA"
        '
        'timer_LBL
        '
        Me.timer_LBL.AutoSize = True
        Me.timer_LBL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.timer_LBL.Location = New System.Drawing.Point(1, 389)
        Me.timer_LBL.Name = "timer_LBL"
        Me.timer_LBL.Size = New System.Drawing.Size(99, 20)
        Me.timer_LBL.TabIndex = 2
        Me.timer_LBL.Text = "TIMER: OFF"
        '
        'timer_LBL_2
        '
        Me.timer_LBL_2.AutoSize = True
        Me.timer_LBL_2.Location = New System.Drawing.Point(2, 409)
        Me.timer_LBL_2.Name = "timer_LBL_2"
        Me.timer_LBL_2.Size = New System.Drawing.Size(218, 26)
        Me.timer_LBL_2.TabIndex = 3
        Me.timer_LBL_2.Text = "The timer is used to check for received data." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "It is turned on when a connection " &
    "is made." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'comPort_ComboBox
        '
        Me.comPort_ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comPort_ComboBox.FormattingEnabled = True
        Me.comPort_ComboBox.Location = New System.Drawing.Point(625, 512)
        Me.comPort_ComboBox.Name = "comPort_ComboBox"
        Me.comPort_ComboBox.Size = New System.Drawing.Size(121, 28)
        Me.comPort_ComboBox.TabIndex = 5
        '
        'recData_RichTextBox
        '
        Me.recData_RichTextBox.Location = New System.Drawing.Point(535, 131)
        Me.recData_RichTextBox.Name = "recData_RichTextBox"
        Me.recData_RichTextBox.Size = New System.Drawing.Size(176, 242)
        Me.recData_RichTextBox.TabIndex = 7
        Me.recData_RichTextBox.Text = ""
        '
        'count_lbl
        '
        Me.count_lbl.AutoSize = True
        Me.count_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.count_lbl.Location = New System.Drawing.Point(13, 493)
        Me.count_lbl.Name = "count_lbl"
        Me.count_lbl.Size = New System.Drawing.Size(122, 20)
        Me.count_lbl.TabIndex = 44
        Me.count_lbl.Text = "CODE COUNT: "
        '
        'TimerSpeed_lbl
        '
        Me.TimerSpeed_lbl.AutoSize = True
        Me.TimerSpeed_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimerSpeed_lbl.Location = New System.Drawing.Point(13, 463)
        Me.TimerSpeed_lbl.Name = "TimerSpeed_lbl"
        Me.TimerSpeed_lbl.Size = New System.Drawing.Size(126, 20)
        Me.TimerSpeed_lbl.TabIndex = 43
        Me.TimerSpeed_lbl.Text = "TIMER SPEED: "
        '
        'commandCountVal_lbl
        '
        Me.commandCountVal_lbl.AutoSize = True
        Me.commandCountVal_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.commandCountVal_lbl.Location = New System.Drawing.Point(177, 493)
        Me.commandCountVal_lbl.Name = "commandCountVal_lbl"
        Me.commandCountVal_lbl.Size = New System.Drawing.Size(18, 20)
        Me.commandCountVal_lbl.TabIndex = 45
        Me.commandCountVal_lbl.Text = "0"
        Me.commandCountVal_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TimerSpeed_value_lbl
        '
        Me.TimerSpeed_value_lbl.AutoSize = True
        Me.TimerSpeed_value_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimerSpeed_value_lbl.Location = New System.Drawing.Point(177, 463)
        Me.TimerSpeed_value_lbl.Name = "TimerSpeed_value_lbl"
        Me.TimerSpeed_value_lbl.Size = New System.Drawing.Size(18, 20)
        Me.TimerSpeed_value_lbl.TabIndex = 46
        Me.TimerSpeed_value_lbl.Text = "0"
        Me.TimerSpeed_value_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PlaceCol, Me.LaneCol, Me.MarginCol, Me.TimeCol})
        Me.DataGridView1.Location = New System.Drawing.Point(5, 64)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(516, 249)
        Me.DataGridView1.TabIndex = 49
        '
        'PlaceCol
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.PlaceCol.DefaultCellStyle = DataGridViewCellStyle1
        Me.PlaceCol.HeaderText = "Place"
        Me.PlaceCol.Name = "PlaceCol"
        Me.PlaceCol.ReadOnly = True
        Me.PlaceCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'LaneCol
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.LaneCol.DefaultCellStyle = DataGridViewCellStyle2
        Me.LaneCol.HeaderText = "Lane"
        Me.LaneCol.Name = "LaneCol"
        Me.LaneCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MarginCol
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.MarginCol.DefaultCellStyle = DataGridViewCellStyle3
        Me.MarginCol.HeaderText = "Margin"
        Me.MarginCol.Name = "MarginCol"
        Me.MarginCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TimeCol
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.TimeCol.DefaultCellStyle = DataGridViewCellStyle4
        Me.TimeCol.HeaderText = "Time"
        Me.TimeCol.Name = "TimeCol"
        Me.TimeCol.ReadOnly = True
        Me.TimeCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RaceLabel
        '
        Me.RaceLabel.AutoSize = True
        Me.RaceLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceLabel.Location = New System.Drawing.Point(9, 33)
        Me.RaceLabel.Name = "RaceLabel"
        Me.RaceLabel.Size = New System.Drawing.Size(55, 20)
        Me.RaceLabel.TabIndex = 48
        Me.RaceLabel.Text = "Race :"
        '
        'RaceSaveBtn
        '
        Me.RaceSaveBtn.Enabled = False
        Me.RaceSaveBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceSaveBtn.Location = New System.Drawing.Point(423, 320)
        Me.RaceSaveBtn.Name = "RaceSaveBtn"
        Me.RaceSaveBtn.Size = New System.Drawing.Size(100, 30)
        Me.RaceSaveBtn.TabIndex = 50
        Me.RaceSaveBtn.Text = "SaveData"
        Me.RaceSaveBtn.UseVisualStyleBackColor = True
        '
        'RaceTextBox
        '
        Me.RaceTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceTextBox.Location = New System.Drawing.Point(326, 437)
        Me.RaceTextBox.Name = "RaceTextBox"
        Me.RaceTextBox.Size = New System.Drawing.Size(100, 26)
        Me.RaceTextBox.TabIndex = 47
        Me.RaceTextBox.Text = "0"
        '
        'VidCapChk
        '
        Me.VidCapChk.AutoSize = True
        Me.VidCapChk.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VidCapChk.Location = New System.Drawing.Point(5, 320)
        Me.VidCapChk.Name = "VidCapChk"
        Me.VidCapChk.Size = New System.Drawing.Size(134, 24)
        Me.VidCapChk.TabIndex = 51
        Me.VidCapChk.Text = "Video Capture:"
        Me.VidCapChk.UseVisualStyleBackColor = True
        '
        'clear_BTN
        '
        Me.clear_BTN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clear_BTN.Location = New System.Drawing.Point(636, 379)
        Me.clear_BTN.Name = "clear_BTN"
        Me.clear_BTN.Size = New System.Drawing.Size(75, 30)
        Me.clear_BTN.TabIndex = 6
        Me.clear_BTN.Text = "CLEAR"
        Me.clear_BTN.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(423, 350)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 30)
        Me.Button1.TabIndex = 52
        Me.Button1.Text = ">>"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(181, 350)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 30)
        Me.Button2.TabIndex = 53
        Me.Button2.Text = "RP7 Dir"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RefreshCommButton
        '
        Me.RefreshCommButton.Font = New System.Drawing.Font("Wingdings 3", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.RefreshCommButton.Location = New System.Drawing.Point(752, 512)
        Me.RefreshCommButton.Name = "RefreshCommButton"
        Me.RefreshCommButton.Size = New System.Drawing.Size(30, 30)
        Me.RefreshCommButton.TabIndex = 54
        Me.RefreshCommButton.Text = "1"
        Me.RefreshCommButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(5, 350)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(128, 30)
        Me.Button3.TabIndex = 56
        Me.Button3.Text = "View Videos"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'RaceCombo
        '
        Me.RaceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RaceCombo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RaceCombo.FormattingEnabled = True
        Me.RaceCombo.ItemHeight = 20
        Me.RaceCombo.Location = New System.Drawing.Point(82, 32)
        Me.RaceCombo.Name = "RaceCombo"
        Me.RaceCombo.Size = New System.Drawing.Size(105, 28)
        Me.RaceCombo.Sorted = True
        Me.RaceCombo.TabIndex = 57
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(270, 350)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(128, 30)
        Me.Button4.TabIndex = 58
        Me.Button4.Text = "Open Race Files"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(283, 401)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 26)
        Me.TextBox1.TabIndex = 59
        Me.TextBox1.Text = "123.456.789"
        '
        'RP7_CheckBox
        '
        Me.RP7_CheckBox.AutoSize = True
        Me.RP7_CheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RP7_CheckBox.Location = New System.Drawing.Point(270, 320)
        Me.RP7_CheckBox.Name = "RP7_CheckBox"
        Me.RP7_CheckBox.Size = New System.Drawing.Size(121, 24)
        Me.RP7_CheckBox.TabIndex = 60
        Me.RP7_CheckBox.Text = "RP7 on Save"
        Me.RP7_CheckBox.UseVisualStyleBackColor = True
        '
        'FinishTxtBox
        '
        Me.FinishTxtBox.Enabled = False
        Me.FinishTxtBox.Location = New System.Drawing.Point(397, 40)
        Me.FinishTxtBox.Name = "FinishTxtBox"
        Me.FinishTxtBox.Size = New System.Drawing.Size(100, 20)
        Me.FinishTxtBox.TabIndex = 62
        '
        'StartTxtBox
        '
        Me.StartTxtBox.Enabled = False
        Me.StartTxtBox.Location = New System.Drawing.Point(291, 40)
        Me.StartTxtBox.Name = "StartTxtBox"
        Me.StartTxtBox.Size = New System.Drawing.Size(100, 20)
        Me.StartTxtBox.TabIndex = 63
        '
        'RegattaLbl
        '
        Me.RegattaLbl.AutoSize = True
        Me.RegattaLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegattaLbl.Location = New System.Drawing.Point(9, 9)
        Me.RegattaLbl.Name = "RegattaLbl"
        Me.RegattaLbl.Size = New System.Drawing.Size(67, 20)
        Me.RegattaLbl.TabIndex = 64
        Me.RegattaLbl.Text = "Regatta"
        '
        'RegattaTxtBox
        '
        Me.RegattaTxtBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegattaTxtBox.Location = New System.Drawing.Point(82, 4)
        Me.RegattaTxtBox.Name = "RegattaTxtBox"
        Me.RegattaTxtBox.Size = New System.Drawing.Size(105, 26)
        Me.RegattaTxtBox.TabIndex = 65
        Me.RegattaTxtBox.Text = "0"
        '
        'AddRaceBtn
        '
        Me.AddRaceBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddRaceBtn.Location = New System.Drawing.Point(193, 33)
        Me.AddRaceBtn.Name = "AddRaceBtn"
        Me.AddRaceBtn.Size = New System.Drawing.Size(27, 23)
        Me.AddRaceBtn.TabIndex = 66
        Me.AddRaceBtn.Text = "+"
        Me.AddRaceBtn.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(560, 463)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(109, 30)
        Me.btnConnect.TabIndex = 67
        Me.btnConnect.Text = "Button5"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lstConsole
        '
        Me.lstConsole.FormattingEnabled = True
        Me.lstConsole.Location = New System.Drawing.Point(704, 450)
        Me.lstConsole.Name = "lstConsole"
        Me.lstConsole.Size = New System.Drawing.Size(148, 56)
        Me.lstConsole.TabIndex = 68
        '
        'TimerConnect
        '
        '
        'connect_BTN
        '
        Me.connect_BTN.BackColor = System.Drawing.SystemColors.Control
        Me.connect_BTN.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.connect_BTN.Location = New System.Drawing.Point(423, 3)
        Me.connect_BTN.Name = "connect_BTN"
        Me.connect_BTN.Size = New System.Drawing.Size(100, 30)
        Me.connect_BTN.TabIndex = 4
        Me.connect_BTN.Text = "CONNECT"
        Me.connect_BTN.UseVisualStyleBackColor = False
        '
        'connect_lbl
        '
        Me.connect_lbl.AutoSize = True
        Me.connect_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.connect_lbl.Location = New System.Drawing.Point(201, 4)
        Me.connect_lbl.Name = "connect_lbl"
        Me.connect_lbl.Size = New System.Drawing.Size(216, 20)
        Me.connect_lbl.TabIndex = 69
        Me.connect_lbl.Text = "Timing Control not connected"
        Me.connect_lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Race, Me.Delay, Me.StartTime})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(535, 3)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(176, 97)
        Me.ListView1.TabIndex = 71
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Race
        '
        Me.Race.Text = "Race"
        Me.Race.Width = 40
        '
        'Delay
        '
        Me.Delay.Text = "Delay"
        Me.Delay.Width = 50
        '
        'StartTime
        '
        Me.StartTime.Text = "Start Time"
        Me.StartTime.Width = 82
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 571)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.connect_lbl)
        Me.Controls.Add(Me.lstConsole)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.AddRaceBtn)
        Me.Controls.Add(Me.RegattaTxtBox)
        Me.Controls.Add(Me.RegattaLbl)
        Me.Controls.Add(Me.StartTxtBox)
        Me.Controls.Add(Me.FinishTxtBox)
        Me.Controls.Add(Me.RP7_CheckBox)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.RaceCombo)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.RefreshCommButton)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.VidCapChk)
        Me.Controls.Add(Me.RaceTextBox)
        Me.Controls.Add(Me.RaceSaveBtn)
        Me.Controls.Add(Me.RaceLabel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TimerSpeed_value_lbl)
        Me.Controls.Add(Me.commandCountVal_lbl)
        Me.Controls.Add(Me.count_lbl)
        Me.Controls.Add(Me.TimerSpeed_lbl)
        Me.Controls.Add(Me.recData_RichTextBox)
        Me.Controls.Add(Me.clear_BTN)
        Me.Controls.Add(Me.comPort_ComboBox)
        Me.Controls.Add(Me.connect_BTN)
        Me.Controls.Add(Me.timer_LBL_2)
        Me.Controls.Add(Me.timer_LBL)
        Me.Controls.Add(Me.recData_LBL)
        Me.Controls.Add(Me.COMport_LBL)
        Me.Name = "Form1"
        Me.Text = "Regatta Mate - Finish"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Timer1 As Timer
    Friend WithEvents COMport_LBL As Label
    Friend WithEvents recData_LBL As Label
    Friend WithEvents timer_LBL As Label
    Friend WithEvents timer_LBL_2 As Label
    Friend WithEvents comPort_ComboBox As ComboBox
    Friend WithEvents recData_RichTextBox As RichTextBox
    Friend WithEvents count_lbl As Label
    Friend WithEvents TimerSpeed_lbl As Label
    Friend WithEvents commandCountVal_lbl As Label
    Friend WithEvents TimerSpeed_value_lbl As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents RaceLabel As Label
    Friend WithEvents RaceSaveBtn As Button
    Friend WithEvents RaceTextBox As TextBox
    Friend WithEvents VidCapChk As CheckBox
    Friend WithEvents clear_BTN As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button2 As Button
    Friend WithEvents RefreshCommButton As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button3 As Button
    Friend WithEvents RaceCombo As ComboBox
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents RP7_CheckBox As CheckBox
    Friend WithEvents FinishTxtBox As TextBox
    Friend WithEvents StartTxtBox As TextBox
    Friend WithEvents RegattaLbl As Label
    Friend WithEvents RegattaTxtBox As TextBox
    Friend WithEvents AddRaceBtn As Button
    Friend WithEvents PlaceCol As DataGridViewTextBoxColumn
    Friend WithEvents LaneCol As DataGridViewTextBoxColumn
    Friend WithEvents MarginCol As DataGridViewTextBoxColumn
    Friend WithEvents TimeCol As DataGridViewTextBoxColumn
    Friend WithEvents btnConnect As Button
    Friend WithEvents lstConsole As ListBox
    Friend WithEvents TimerConnect As Timer
    Friend WithEvents connect_BTN As Button
    Friend WithEvents connect_lbl As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Race As ColumnHeader
    Friend WithEvents Delay As ColumnHeader
    Friend WithEvents StartTime As ColumnHeader
End Class
