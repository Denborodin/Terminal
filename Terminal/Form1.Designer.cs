namespace Terminal
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Command2TextBox = new System.Windows.Forms.TextBox();
            this.Command1TextBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.NumberOfCyclesTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TTFStopButton = new System.Windows.Forms.Button();
            this.TTFStartButton = new System.Windows.Forms.Button();
            this.Timeout2TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Timeout1TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TTFSW_soltypeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabModem = new System.Windows.Forms.TabPage();
            this.PowerTextBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.ModemStopBtn = new System.Windows.Forms.Button();
            this.ModemStartBtn = new System.Windows.Forms.Button();
            this.PauseTimeTxt = new System.Windows.Forms.TextBox();
            this.TransmitTimeTxt = new System.Windows.Forms.TextBox();
            this.FrqStepTxt = new System.Windows.Forms.TextBox();
            this.FrqStopTxt = new System.Windows.Forms.TextBox();
            this.FrqStartTxt = new System.Windows.Forms.TextBox();
            this.ModemLog = new System.Windows.Forms.TextBox();
            this.ModemDisconnectBTN = new System.Windows.Forms.Button();
            this.ModemConnectBTN = new System.Windows.Forms.Button();
            this.LinkRateList = new System.Windows.Forms.ComboBox();
            this.ModemTypeList = new System.Windows.Forms.ComboBox();
            this.ModemPortComboBox = new System.Windows.Forms.ComboBox();
            this.form1BindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textLabel2 = new System.Windows.Forms.Label();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabTerminal = new System.Windows.Forms.TabPage();
            this.TextBox_Console = new System.Windows.Forms.TextBox();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.LogConsole = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabModem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource2)).BeginInit();
            this.tabStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabTerminal.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabModem);
            this.tabControl1.Controls.Add(this.tabStatistics);
            this.tabControl1.Controls.Add(this.tabTerminal);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 551);
            this.tabControl1.TabIndex = 8;
            // 
            // tabMain
            // 
            this.tabMain.BackColor = System.Drawing.Color.Transparent;
            this.tabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabMain.Controls.Add(this.label5);
            this.tabMain.Controls.Add(this.label18);
            this.tabMain.Controls.Add(this.label16);
            this.tabMain.Controls.Add(this.label14);
            this.tabMain.Controls.Add(this.label12);
            this.tabMain.Controls.Add(this.label10);
            this.tabMain.Controls.Add(this.label8);
            this.tabMain.Controls.Add(this.label6);
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Controls.Add(this.label1);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(616, 525);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "TTFF Test";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "To Terminal";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(287, 208);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 13);
            this.label18.TabIndex = 48;
            this.label18.Text = "Solution:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(287, 181);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "Solution:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(287, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Solution:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(287, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Solution:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(287, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Solution:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Solution:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Solution:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Command2TextBox);
            this.groupBox1.Controls.Add(this.Command1TextBox);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.NumberOfCyclesTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TTFStopButton);
            this.groupBox1.Controls.Add(this.TTFStartButton);
            this.groupBox1.Controls.Add(this.Timeout2TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Timeout1TextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TTFSW_soltypeList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 252);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TTF switch";
            // 
            // Command2TextBox
            // 
            this.Command2TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Terminal.Properties.Settings.Default, "cmd2sett", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Command2TextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Command2TextBox.Location = new System.Drawing.Point(6, 156);
            this.Command2TextBox.Multiline = true;
            this.Command2TextBox.Name = "Command2TextBox";
            this.Command2TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Command2TextBox.Size = new System.Drawing.Size(354, 90);
            this.Command2TextBox.TabIndex = 19;
            this.Command2TextBox.Text = global::Terminal.Properties.Settings.Default.cmd2sett;
            // 
            // Command1TextBox
            // 
            this.Command1TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Terminal.Properties.Settings.Default, "cmd1sett", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Command1TextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Command1TextBox.Location = new System.Drawing.Point(6, 39);
            this.Command1TextBox.Multiline = true;
            this.Command1TextBox.Name = "Command1TextBox";
            this.Command1TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Command1TextBox.Size = new System.Drawing.Size(354, 85);
            this.Command1TextBox.TabIndex = 18;
            this.Command1TextBox.Text = global::Terminal.Properties.Settings.Default.cmd1sett;
            this.Command1TextBox.TextChanged += new System.EventHandler(this.Command1TextBox_TextChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(372, 224);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(224, 22);
            this.progressBar1.TabIndex = 17;
            // 
            // NumberOfCyclesTextBox
            // 
            this.NumberOfCyclesTextBox.Location = new System.Drawing.Point(499, 48);
            this.NumberOfCyclesTextBox.Name = "NumberOfCyclesTextBox";
            this.NumberOfCyclesTextBox.Size = new System.Drawing.Size(97, 20);
            this.NumberOfCyclesTextBox.TabIndex = 16;
            this.NumberOfCyclesTextBox.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(369, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Number of cycles:";
            // 
            // TTFStopButton
            // 
            this.TTFStopButton.Enabled = false;
            this.TTFStopButton.Location = new System.Drawing.Point(499, 175);
            this.TTFStopButton.Name = "TTFStopButton";
            this.TTFStopButton.Size = new System.Drawing.Size(97, 40);
            this.TTFStopButton.TabIndex = 15;
            this.TTFStopButton.Text = "Stop";
            this.TTFStopButton.UseVisualStyleBackColor = true;
            this.TTFStopButton.Click += new System.EventHandler(this.TTFSWStop_Click);
            // 
            // TTFStartButton
            // 
            this.TTFStartButton.Enabled = false;
            this.TTFStartButton.Location = new System.Drawing.Point(372, 175);
            this.TTFStartButton.Name = "TTFStartButton";
            this.TTFStartButton.Size = new System.Drawing.Size(97, 40);
            this.TTFStartButton.TabIndex = 14;
            this.TTFStartButton.Text = "Start";
            this.TTFStartButton.UseVisualStyleBackColor = true;
            this.TTFStartButton.Click += new System.EventHandler(this.TTFSWStart_Click);
            // 
            // Timeout2TextBox
            // 
            this.Timeout2TextBox.Location = new System.Drawing.Point(145, 130);
            this.Timeout2TextBox.Name = "Timeout2TextBox";
            this.Timeout2TextBox.Size = new System.Drawing.Size(55, 20);
            this.Timeout2TextBox.TabIndex = 11;
            this.Timeout2TextBox.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Timeout 2, s:";
            // 
            // Timeout1TextBox
            // 
            this.Timeout1TextBox.Location = new System.Drawing.Point(145, 13);
            this.Timeout1TextBox.Name = "Timeout1TextBox";
            this.Timeout1TextBox.Size = new System.Drawing.Size(55, 20);
            this.Timeout1TextBox.TabIndex = 9;
            this.Timeout1TextBox.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Timeout 1, s:";
            // 
            // TTFSW_soltypeList
            // 
            this.TTFSW_soltypeList.FormattingEnabled = true;
            this.TTFSW_soltypeList.Items.AddRange(new object[] {
            "Standalone",
            "DGNSS",
            "RTK Fixed"});
            this.TTFSW_soltypeList.Location = new System.Drawing.Point(499, 19);
            this.TTFSW_soltypeList.Name = "TTFSW_soltypeList";
            this.TTFSW_soltypeList.Size = new System.Drawing.Size(97, 21);
            this.TTFSW_soltypeList.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(369, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Solution type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Solution:";
            // 
            // tabModem
            // 
            this.tabModem.BackColor = System.Drawing.SystemColors.Control;
            this.tabModem.Controls.Add(this.PowerTextBox);
            this.tabModem.Controls.Add(this.label22);
            this.tabModem.Controls.Add(this.ModemStopBtn);
            this.tabModem.Controls.Add(this.ModemStartBtn);
            this.tabModem.Controls.Add(this.PauseTimeTxt);
            this.tabModem.Controls.Add(this.TransmitTimeTxt);
            this.tabModem.Controls.Add(this.FrqStepTxt);
            this.tabModem.Controls.Add(this.FrqStopTxt);
            this.tabModem.Controls.Add(this.FrqStartTxt);
            this.tabModem.Controls.Add(this.ModemLog);
            this.tabModem.Controls.Add(this.ModemDisconnectBTN);
            this.tabModem.Controls.Add(this.ModemConnectBTN);
            this.tabModem.Controls.Add(this.LinkRateList);
            this.tabModem.Controls.Add(this.ModemTypeList);
            this.tabModem.Controls.Add(this.ModemPortComboBox);
            this.tabModem.Controls.Add(this.label9);
            this.tabModem.Controls.Add(this.label20);
            this.tabModem.Controls.Add(this.label19);
            this.tabModem.Controls.Add(this.label17);
            this.tabModem.Controls.Add(this.label15);
            this.tabModem.Controls.Add(this.label13);
            this.tabModem.Controls.Add(this.label21);
            this.tabModem.Controls.Add(this.label11);
            this.tabModem.Controls.Add(this.textLabel2);
            this.tabModem.Location = new System.Drawing.Point(4, 22);
            this.tabModem.Name = "tabModem";
            this.tabModem.Padding = new System.Windows.Forms.Padding(3);
            this.tabModem.Size = new System.Drawing.Size(616, 525);
            this.tabModem.TabIndex = 4;
            this.tabModem.Text = "Modem Test";
            // 
            // PowerTextBox
            // 
            this.PowerTextBox.Location = new System.Drawing.Point(374, 132);
            this.PowerTextBox.Name = "PowerTextBox";
            this.PowerTextBox.Size = new System.Drawing.Size(42, 20);
            this.PowerTextBox.TabIndex = 10;
            this.PowerTextBox.Text = "1000";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(254, 132);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(103, 23);
            this.label22.TabIndex = 9;
            this.label22.Text = "Transmit power, W:";
            // 
            // ModemStopBtn
            // 
            this.ModemStopBtn.Enabled = false;
            this.ModemStopBtn.Location = new System.Drawing.Point(15, 194);
            this.ModemStopBtn.Name = "ModemStopBtn";
            this.ModemStopBtn.Size = new System.Drawing.Size(75, 46);
            this.ModemStopBtn.TabIndex = 8;
            this.ModemStopBtn.Text = "Stop";
            this.ModemStopBtn.UseVisualStyleBackColor = true;
            this.ModemStopBtn.Click += new System.EventHandler(this.ModemStopBtn_Click);
            // 
            // ModemStartBtn
            // 
            this.ModemStartBtn.Enabled = false;
            this.ModemStartBtn.Location = new System.Drawing.Point(15, 142);
            this.ModemStartBtn.Name = "ModemStartBtn";
            this.ModemStartBtn.Size = new System.Drawing.Size(75, 46);
            this.ModemStartBtn.TabIndex = 8;
            this.ModemStartBtn.Text = "Start";
            this.ModemStartBtn.UseVisualStyleBackColor = true;
            this.ModemStartBtn.Click += new System.EventHandler(this.ModemStartBtn_Click);
            // 
            // PauseTimeTxt
            // 
            this.PauseTimeTxt.Location = new System.Drawing.Point(523, 83);
            this.PauseTimeTxt.Name = "PauseTimeTxt";
            this.PauseTimeTxt.Size = new System.Drawing.Size(42, 20);
            this.PauseTimeTxt.TabIndex = 7;
            this.PauseTimeTxt.Text = "60";
            // 
            // TransmitTimeTxt
            // 
            this.TransmitTimeTxt.Location = new System.Drawing.Point(523, 60);
            this.TransmitTimeTxt.Name = "TransmitTimeTxt";
            this.TransmitTimeTxt.Size = new System.Drawing.Size(42, 20);
            this.TransmitTimeTxt.TabIndex = 7;
            this.TransmitTimeTxt.Text = "60";
            // 
            // FrqStepTxt
            // 
            this.FrqStepTxt.Location = new System.Drawing.Point(374, 106);
            this.FrqStepTxt.Name = "FrqStepTxt";
            this.FrqStepTxt.Size = new System.Drawing.Size(42, 20);
            this.FrqStepTxt.TabIndex = 7;
            this.FrqStepTxt.Text = "1";
            // 
            // FrqStopTxt
            // 
            this.FrqStopTxt.Location = new System.Drawing.Point(374, 83);
            this.FrqStopTxt.Name = "FrqStopTxt";
            this.FrqStopTxt.Size = new System.Drawing.Size(42, 20);
            this.FrqStopTxt.TabIndex = 7;
            this.FrqStopTxt.Text = "470";
            // 
            // FrqStartTxt
            // 
            this.FrqStartTxt.Location = new System.Drawing.Point(374, 60);
            this.FrqStartTxt.Name = "FrqStartTxt";
            this.FrqStartTxt.Size = new System.Drawing.Size(42, 20);
            this.FrqStartTxt.TabIndex = 7;
            this.FrqStartTxt.Text = "400";
            // 
            // ModemLog
            // 
            this.ModemLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ModemLog.Location = new System.Drawing.Point(3, 320);
            this.ModemLog.Multiline = true;
            this.ModemLog.Name = "ModemLog";
            this.ModemLog.ReadOnly = true;
            this.ModemLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ModemLog.Size = new System.Drawing.Size(610, 202);
            this.ModemLog.TabIndex = 6;
            // 
            // ModemDisconnectBTN
            // 
            this.ModemDisconnectBTN.Enabled = false;
            this.ModemDisconnectBTN.Location = new System.Drawing.Point(475, 17);
            this.ModemDisconnectBTN.Name = "ModemDisconnectBTN";
            this.ModemDisconnectBTN.Size = new System.Drawing.Size(75, 22);
            this.ModemDisconnectBTN.TabIndex = 5;
            this.ModemDisconnectBTN.Text = "Disconnect";
            this.ModemDisconnectBTN.UseVisualStyleBackColor = true;
            this.ModemDisconnectBTN.Click += new System.EventHandler(this.ModemDisconnectBTN_Click);
            // 
            // ModemConnectBTN
            // 
            this.ModemConnectBTN.Location = new System.Drawing.Point(394, 17);
            this.ModemConnectBTN.Name = "ModemConnectBTN";
            this.ModemConnectBTN.Size = new System.Drawing.Size(75, 22);
            this.ModemConnectBTN.TabIndex = 5;
            this.ModemConnectBTN.Text = "Connect";
            this.ModemConnectBTN.UseVisualStyleBackColor = true;
            this.ModemConnectBTN.Click += new System.EventHandler(this.ModemConnectBTN_Click);
            // 
            // LinkRateList
            // 
            this.LinkRateList.FormattingEnabled = true;
            this.LinkRateList.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200"});
            this.LinkRateList.Location = new System.Drawing.Point(96, 106);
            this.LinkRateList.Name = "LinkRateList";
            this.LinkRateList.Size = new System.Drawing.Size(121, 21);
            this.LinkRateList.TabIndex = 4;
            // 
            // ModemTypeList
            // 
            this.ModemTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModemTypeList.Enabled = false;
            this.ModemTypeList.FormattingEnabled = true;
            this.ModemTypeList.Items.AddRange(new object[] {
            "Satel",
            "R2Lite"});
            this.ModemTypeList.Location = new System.Drawing.Point(96, 60);
            this.ModemTypeList.Name = "ModemTypeList";
            this.ModemTypeList.Size = new System.Drawing.Size(121, 21);
            this.ModemTypeList.TabIndex = 4;
            // 
            // ModemPortComboBox
            // 
            this.ModemPortComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.form1BindingSource2, "Text", true));
            this.ModemPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModemPortComboBox.Enabled = false;
            this.ModemPortComboBox.FormattingEnabled = true;
            this.ModemPortComboBox.Items.AddRange(new object[] {
            "Modem A",
            "Serial A",
            "Serial B",
            "Serial C",
            "Serial D",
            "Direct"});
            this.ModemPortComboBox.Location = new System.Drawing.Point(257, 17);
            this.ModemPortComboBox.Name = "ModemPortComboBox";
            this.ModemPortComboBox.Size = new System.Drawing.Size(121, 21);
            this.ModemPortComboBox.TabIndex = 4;
            // 
            // form1BindingSource2
            // 
            this.form1BindingSource2.DataSource = typeof(Terminal.Form1);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(171, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "Connected to:";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(422, 86);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 23);
            this.label20.TabIndex = 3;
            this.label20.Text = "Pause, sec:";
            this.label20.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(422, 63);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 23);
            this.label19.TabIndex = 3;
            this.label19.Text = "Transmit time, sec:";
            this.label19.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(254, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 23);
            this.label17.TabIndex = 3;
            this.label17.Text = "Step, MHz:";
            this.label17.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(254, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 23);
            this.label15.TabIndex = 3;
            this.label15.Text = "End Frequency, MHz:";
            this.label15.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(254, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 23);
            this.label13.TabIndex = 3;
            this.label13.Text = "Start Frequency, MHz:";
            this.label13.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(8, 109);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 23);
            this.label21.TabIndex = 3;
            this.label21.Text = "Link rate, bps";
            this.label21.Click += new System.EventHandler(this.Label11_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 23);
            this.label11.TabIndex = 3;
            this.label11.Text = "Modem Type:";
            this.label11.Click += new System.EventHandler(this.Label11_Click);
            // 
            // textLabel2
            // 
            this.textLabel2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.form1BindingSource2, "Text", true));
            this.textLabel2.Location = new System.Drawing.Point(8, 20);
            this.textLabel2.Name = "textLabel2";
            this.textLabel2.Size = new System.Drawing.Size(69, 23);
            this.textLabel2.TabIndex = 3;
            this.textLabel2.Text = "Modem Port:";
            // 
            // tabStatistics
            // 
            this.tabStatistics.Controls.Add(this.dataGridView1);
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(616, 525);
            this.tabStatistics.TabIndex = 1;
            this.tabStatistics.Text = "Statistics";
            this.tabStatistics.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(610, 519);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // tabTerminal
            // 
            this.tabTerminal.Controls.Add(this.TextBox_Console);
            this.tabTerminal.Location = new System.Drawing.Point(4, 22);
            this.tabTerminal.Name = "tabTerminal";
            this.tabTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tabTerminal.Size = new System.Drawing.Size(616, 525);
            this.tabTerminal.TabIndex = 2;
            this.tabTerminal.Text = "Terminal";
            this.tabTerminal.UseVisualStyleBackColor = true;
            // 
            // TextBox_Console
            // 
            this.TextBox_Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Console.Location = new System.Drawing.Point(3, 3);
            this.TextBox_Console.Multiline = true;
            this.TextBox_Console.Name = "TextBox_Console";
            this.TextBox_Console.ReadOnly = true;
            this.TextBox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Console.Size = new System.Drawing.Size(610, 519);
            this.TextBox_Console.TabIndex = 1;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.LogConsole);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(616, 525);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // LogConsole
            // 
            this.LogConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogConsole.Location = new System.Drawing.Point(3, 3);
            this.LogConsole.Multiline = true;
            this.LogConsole.Name = "LogConsole";
            this.LogConsole.ReadOnly = true;
            this.LogConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogConsole.Size = new System.Drawing.Size(610, 519);
            this.LogConsole.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 525);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "About";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(610, 519);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "FT Terminal 1.3.2\r\n19.10.2020\r\n\r\nDenis Borodin\r\ndborodin@topcon.com";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // form1BindingSource3
            // 
            this.form1BindingSource3.DataSource = typeof(Terminal.Form1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 551);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(640, 590);
            this.MinimumSize = new System.Drawing.Size(640, 590);
            this.Name = "Form1";
            this.Text = "FT Terminal 1.3.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabModem.ResumeLayout(false);
            this.tabModem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource2)).EndInit();
            this.tabStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabTerminal.ResumeLayout(false);
            this.tabTerminal.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TabPage tabTerminal;
        private System.Windows.Forms.TextBox TextBox_Console;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.BindingSource form1BindingSource1;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button TTFStopButton;
        private System.Windows.Forms.Button TTFStartButton;
        private System.Windows.Forms.TextBox Timeout2TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Timeout1TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox TTFSW_soltypeList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.TextBox NumberOfCyclesTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox LogConsole;
        private System.Windows.Forms.TabPage tabModem;
        private System.Windows.Forms.ComboBox ModemPortComboBox;
        private System.Windows.Forms.BindingSource form1BindingSource2;
        private System.Windows.Forms.Label textLabel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.BindingSource form1BindingSource3;
        private System.Windows.Forms.Button ModemDisconnectBTN;
        private System.Windows.Forms.Button ModemConnectBTN;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ModemTypeList;
        private System.Windows.Forms.TextBox ModemLog;
        private System.Windows.Forms.TextBox PauseTimeTxt;
        private System.Windows.Forms.TextBox TransmitTimeTxt;
        private System.Windows.Forms.TextBox FrqStepTxt;
        private System.Windows.Forms.TextBox FrqStopTxt;
        private System.Windows.Forms.TextBox FrqStartTxt;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button ModemStopBtn;
        private System.Windows.Forms.Button ModemStartBtn;
        private System.Windows.Forms.ComboBox LinkRateList;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Command2TextBox;
        private System.Windows.Forms.TextBox Command1TextBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox PowerTextBox;
        private System.Windows.Forms.Label label22;
    }
}

