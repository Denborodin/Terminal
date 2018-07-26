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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.NumberOfCyclesTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TTFStopButton = new System.Windows.Forms.Button();
            this.TTFStartButton = new System.Windows.Forms.Button();
            this.Command2TextBox = new System.Windows.Forms.TextBox();
            this.Command1TextBox = new System.Windows.Forms.TextBox();
            this.Timeout2TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Timeout1TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TTFSW_soltypeList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.LogConsole = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabTerminal.SuspendLayout();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabStatistics);
            this.tabControl1.Controls.Add(this.tabTerminal);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 509);
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
            this.tabMain.Size = new System.Drawing.Size(616, 483);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
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
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.NumberOfCyclesTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TTFStopButton);
            this.groupBox1.Controls.Add(this.TTFStartButton);
            this.groupBox1.Controls.Add(this.Command2TextBox);
            this.groupBox1.Controls.Add(this.Command1TextBox);
            this.groupBox1.Controls.Add(this.Timeout2TextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Timeout1TextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TTFSW_soltypeList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 209);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TTF switch";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(145, 181);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(176, 22);
            this.progressBar1.TabIndex = 17;
            // 
            // NumberOfCyclesTextBox
            // 
            this.NumberOfCyclesTextBox.Location = new System.Drawing.Point(145, 155);
            this.NumberOfCyclesTextBox.Name = "NumberOfCyclesTextBox";
            this.NumberOfCyclesTextBox.Size = new System.Drawing.Size(55, 20);
            this.NumberOfCyclesTextBox.TabIndex = 16;
            this.NumberOfCyclesTextBox.Text = "50";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Number of cycles:";
            // 
            // TTFStopButton
            // 
            this.TTFStopButton.Enabled = false;
            this.TTFStopButton.Location = new System.Drawing.Point(66, 181);
            this.TTFStopButton.Name = "TTFStopButton";
            this.TTFStopButton.Size = new System.Drawing.Size(54, 22);
            this.TTFStopButton.TabIndex = 15;
            this.TTFStopButton.Text = "Stop";
            this.TTFStopButton.UseVisualStyleBackColor = true;
            this.TTFStopButton.Click += new System.EventHandler(this.TTFSWStop_Click);
            // 
            // TTFStartButton
            // 
            this.TTFStartButton.Enabled = false;
            this.TTFStartButton.Location = new System.Drawing.Point(6, 181);
            this.TTFStartButton.Name = "TTFStartButton";
            this.TTFStartButton.Size = new System.Drawing.Size(54, 22);
            this.TTFStartButton.TabIndex = 14;
            this.TTFStartButton.Text = "Start";
            this.TTFStartButton.UseVisualStyleBackColor = true;
            this.TTFStartButton.Click += new System.EventHandler(this.TTFSWStart_Click);
            // 
            // Command2TextBox
            // 
            this.Command2TextBox.Location = new System.Drawing.Point(9, 129);
            this.Command2TextBox.Name = "Command2TextBox";
            this.Command2TextBox.Size = new System.Drawing.Size(312, 20);
            this.Command2TextBox.TabIndex = 13;
            this.Command2TextBox.Text = "set,lock/gps/sat,y;set,lock/glo/fcn,y";
            // 
            // Command1TextBox
            // 
            this.Command1TextBox.Location = new System.Drawing.Point(9, 77);
            this.Command1TextBox.Name = "Command1TextBox";
            this.Command1TextBox.Size = new System.Drawing.Size(312, 20);
            this.Command1TextBox.TabIndex = 12;
            this.Command1TextBox.Text = "set,lock/gps/sat,n;set,lock/glo/fcn,n";
            // 
            // Timeout2TextBox
            // 
            this.Timeout2TextBox.Location = new System.Drawing.Point(145, 103);
            this.Timeout2TextBox.Name = "Timeout2TextBox";
            this.Timeout2TextBox.Size = new System.Drawing.Size(55, 20);
            this.Timeout2TextBox.TabIndex = 11;
            this.Timeout2TextBox.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Timeout 2, s:";
            // 
            // Timeout1TextBox
            // 
            this.Timeout1TextBox.Location = new System.Drawing.Point(145, 51);
            this.Timeout1TextBox.Name = "Timeout1TextBox";
            this.Timeout1TextBox.Size = new System.Drawing.Size(55, 20);
            this.Timeout1TextBox.TabIndex = 9;
            this.Timeout1TextBox.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
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
            this.TTFSW_soltypeList.Location = new System.Drawing.Point(117, 27);
            this.TTFSW_soltypeList.Name = "TTFSW_soltypeList";
            this.TTFSW_soltypeList.Size = new System.Drawing.Size(83, 21);
            this.TTFSW_soltypeList.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
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
            // tabStatistics
            // 
            this.tabStatistics.Controls.Add(this.dataGridView1);
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(616, 483);
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
            this.dataGridView1.Size = new System.Drawing.Size(610, 477);
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
            this.tabTerminal.Size = new System.Drawing.Size(616, 483);
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
            this.TextBox_Console.Size = new System.Drawing.Size(610, 477);
            this.TextBox_Console.TabIndex = 1;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.LogConsole);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(616, 483);
            this.tabLog.TabIndex = 3;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(616, 483);
            this.tabSettings.TabIndex = 4;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // LogConsole
            // 
            this.LogConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogConsole.Location = new System.Drawing.Point(3, 3);
            this.LogConsole.Multiline = true;
            this.LogConsole.Name = "LogConsole";
            this.LogConsole.ReadOnly = true;
            this.LogConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogConsole.Size = new System.Drawing.Size(610, 477);
            this.LogConsole.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 509);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FT Terminal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabTerminal.ResumeLayout(false);
            this.tabTerminal.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
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
        private System.Windows.Forms.TextBox Command2TextBox;
        private System.Windows.Forms.TextBox Command1TextBox;
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
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox LogConsole;
    }
}

