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
            this.tabMain = new System.Windows.Forms.TabPage();
            this.ComPortsListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AntSWStop = new System.Windows.Forms.Button();
            this.AntSWStart = new System.Windows.Forms.Button();
            this.AntResetCommand2 = new System.Windows.Forms.TextBox();
            this.AntResetCommand1 = new System.Windows.Forms.TextBox();
            this.AntEnableTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AntResetTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AntSW_soltype = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SolutionLabel = new System.Windows.Forms.Label();
            this.ComPortsList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortSpeedList = new System.Windows.Forms.ComboBox();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabTerminal = new System.Windows.Forms.TabPage();
            this.TextBox_Console = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabTerminal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.BackColor = System.Drawing.Color.Transparent;
            this.tabMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabMain.Controls.Add(this.ComPortsListBox);
            this.tabMain.Controls.Add(this.groupBox1);
            this.tabMain.Controls.Add(this.SolutionLabel);
            this.tabMain.Controls.Add(this.ComPortsList);
            this.tabMain.Controls.Add(this.label1);
            this.tabMain.Controls.Add(this.PortSpeedList);
            this.tabMain.Controls.Add(this.ButtonOpen);
            this.tabMain.Controls.Add(this.ButtonClose);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(625, 281);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main";
            // 
            // ComPortsListBox
            // 
            this.ComPortsListBox.FormattingEnabled = true;
            this.ComPortsListBox.Location = new System.Drawing.Point(9, 19);
            this.ComPortsListBox.Name = "ComPortsListBox";
            this.ComPortsListBox.Size = new System.Drawing.Size(194, 214);
            this.ComPortsListBox.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AntSWStop);
            this.groupBox1.Controls.Add(this.AntSWStart);
            this.groupBox1.Controls.Add(this.AntResetCommand2);
            this.groupBox1.Controls.Add(this.AntResetCommand1);
            this.groupBox1.Controls.Add(this.AntEnableTimeout);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.AntResetTimeout);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.AntSW_soltype);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(210, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 209);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TTF switch";
            // 
            // AntSWStop
            // 
            this.AntSWStop.Enabled = false;
            this.AntSWStop.Location = new System.Drawing.Point(66, 181);
            this.AntSWStop.Name = "AntSWStop";
            this.AntSWStop.Size = new System.Drawing.Size(54, 22);
            this.AntSWStop.TabIndex = 15;
            this.AntSWStop.Text = "Stop";
            this.AntSWStop.UseVisualStyleBackColor = true;
            this.AntSWStop.Click += new System.EventHandler(this.RTKSWStop_Click);
            // 
            // AntSWStart
            // 
            this.AntSWStart.Enabled = false;
            this.AntSWStart.Location = new System.Drawing.Point(6, 181);
            this.AntSWStart.Name = "AntSWStart";
            this.AntSWStart.Size = new System.Drawing.Size(54, 22);
            this.AntSWStart.TabIndex = 14;
            this.AntSWStart.Text = "Start";
            this.AntSWStart.UseVisualStyleBackColor = true;
            this.AntSWStart.Click += new System.EventHandler(this.RTKSWStart_Click);
            // 
            // AntResetCommand2
            // 
            this.AntResetCommand2.Location = new System.Drawing.Point(9, 129);
            this.AntResetCommand2.Name = "AntResetCommand2";
            this.AntResetCommand2.Size = new System.Drawing.Size(191, 20);
            this.AntResetCommand2.TabIndex = 13;
            this.AntResetCommand2.Text = "set,lock/gps/sat,y;set,lock/glo/fcn,y";
            // 
            // AntResetCommand1
            // 
            this.AntResetCommand1.Location = new System.Drawing.Point(9, 77);
            this.AntResetCommand1.Name = "AntResetCommand1";
            this.AntResetCommand1.Size = new System.Drawing.Size(191, 20);
            this.AntResetCommand1.TabIndex = 12;
            this.AntResetCommand1.Text = "set,lock/gps/sat,n;set,lock/glo/fcn,n";
            // 
            // AntEnableTimeout
            // 
            this.AntEnableTimeout.Location = new System.Drawing.Point(145, 103);
            this.AntEnableTimeout.Name = "AntEnableTimeout";
            this.AntEnableTimeout.Size = new System.Drawing.Size(55, 20);
            this.AntEnableTimeout.TabIndex = 11;
            this.AntEnableTimeout.Text = "10";
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
            // AntResetTimeout
            // 
            this.AntResetTimeout.Location = new System.Drawing.Point(145, 51);
            this.AntResetTimeout.Name = "AntResetTimeout";
            this.AntResetTimeout.Size = new System.Drawing.Size(55, 20);
            this.AntResetTimeout.TabIndex = 9;
            this.AntResetTimeout.Text = "10";
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
            // AntSW_soltype
            // 
            this.AntSW_soltype.FormattingEnabled = true;
            this.AntSW_soltype.Items.AddRange(new object[] {
            "Standalone",
            "DGNSS",
            "RTK Fixed"});
            this.AntSW_soltype.Location = new System.Drawing.Point(117, 24);
            this.AntSW_soltype.Name = "AntSW_soltype";
            this.AntSW_soltype.Size = new System.Drawing.Size(83, 21);
            this.AntSW_soltype.TabIndex = 7;
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
            // SolutionLabel
            // 
            this.SolutionLabel.AutoSize = true;
            this.SolutionLabel.Location = new System.Drawing.Point(257, 17);
            this.SolutionLabel.Name = "SolutionLabel";
            this.SolutionLabel.Size = new System.Drawing.Size(27, 13);
            this.SolutionLabel.TabIndex = 6;
            this.SolutionLabel.Text = "N/A";
            // 
            // ComPortsList
            // 
            this.ComPortsList.FormattingEnabled = true;
            this.ComPortsList.Location = new System.Drawing.Point(6, 250);
            this.ComPortsList.Name = "ComPortsList";
            this.ComPortsList.Size = new System.Drawing.Size(121, 21);
            this.ComPortsList.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Solution:";
            // 
            // PortSpeedList
            // 
            this.PortSpeedList.FormattingEnabled = true;
            this.PortSpeedList.Items.AddRange(new object[] {
            "9600",
            "38400",
            "57600",
            "115200"});
            this.PortSpeedList.Location = new System.Drawing.Point(133, 250);
            this.PortSpeedList.Name = "PortSpeedList";
            this.PortSpeedList.Size = new System.Drawing.Size(71, 21);
            this.PortSpeedList.TabIndex = 2;
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Location = new System.Drawing.Point(210, 248);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(54, 22);
            this.ButtonOpen.TabIndex = 3;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.button1_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Enabled = false;
            this.ButtonClose.Location = new System.Drawing.Point(270, 248);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(54, 22);
            this.ButtonClose.TabIndex = 4;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Controls.Add(this.tabStatistics);
            this.tabControl1.Controls.Add(this.tabTerminal);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 307);
            this.tabControl1.TabIndex = 8;
            // 
            // tabStatistics
            // 
            this.tabStatistics.Controls.Add(this.dataGridView1);
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatistics.Size = new System.Drawing.Size(625, 281);
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
            this.Column2});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(303, 266);
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
            // tabTerminal
            // 
            this.tabTerminal.Controls.Add(this.TextBox_Console);
            this.tabTerminal.Location = new System.Drawing.Point(4, 22);
            this.tabTerminal.Name = "tabTerminal";
            this.tabTerminal.Padding = new System.Windows.Forms.Padding(3);
            this.tabTerminal.Size = new System.Drawing.Size(625, 281);
            this.tabTerminal.TabIndex = 2;
            this.tabTerminal.Text = "Terminal";
            this.tabTerminal.UseVisualStyleBackColor = true;
            // 
            // TextBox_Console
            // 
            this.TextBox_Console.Location = new System.Drawing.Point(6, 6);
            this.TextBox_Console.Multiline = true;
            this.TextBox_Console.Name = "TextBox_Console";
            this.TextBox_Console.ReadOnly = true;
            this.TextBox_Console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Console.Size = new System.Drawing.Size(628, 238);
            this.TextBox_Console.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 329);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FT Terminal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabTerminal.ResumeLayout(false);
            this.tabTerminal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button AntSWStop;
        private System.Windows.Forms.Button AntSWStart;
        private System.Windows.Forms.TextBox AntResetCommand2;
        private System.Windows.Forms.TextBox AntResetCommand1;
        private System.Windows.Forms.TextBox AntEnableTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AntResetTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox AntSW_soltype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SolutionLabel;
        private System.Windows.Forms.ComboBox ComPortsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PortSpeedList;
        private System.Windows.Forms.Button ButtonOpen;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabStatistics;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.CheckedListBox ComPortsListBox;
        private System.Windows.Forms.TabPage tabTerminal;
        private System.Windows.Forms.TextBox TextBox_Console;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

