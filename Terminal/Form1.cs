using System;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Terminal
{
    public struct TTFcycle
    {
        public int number, channel;
        public double TTFS, TTFD, TTFR, TTFF, start, end;


        public TTFcycle(int number_, int channel_, double TTFS_,double TTFD_, double TTFF_, double TTFR_, double start_, double end_)
        {
            number = number_;
            channel = channel_;
            TTFS = TTFS_;
            TTFD = TTFD_;
            TTFF = TTFF_;
            TTFR = TTFR_;
            start = start_;
            end = end_;
        }
    }

    public partial class Form1 : Form
    {
        #region Variables definition
        SerialPort[] CurrentPort = new SerialPort[10];
        FileStream[] fstream = new FileStream[10];
        ArrayList Cycles;
        DateTime timestamp_date;
        public delegate void MyDelegate();

        private delegate void SetTextDeleg(string text, int index);
        private delegate void StatusUpdate(string text, int index);
        private delegate void MLogUpdate(string text);
        private delegate void ProgressUpdate();

        System.Timers.Timer aTimer;
        int[] TTFSWmode=new int[8]; //0-Waiting for fixed 1 - Got FIX, start timeout 1, 2 - Timeout 1 elapsed, Start Timeout 2, 3 -sleep timer active
        int[] TTF_Timeout1 = new int[8];
        int[] TTF_Timeout2 = new int[8];
        int[] cycle_counter = new int[8];
        int[] sleep_timer = new int[8];
        int[] CurrentCommandLine = new int[8];
        int[] counter_S = new int[8];
        int[] counter_D = new int[8];
        int[] counter_F = new int[8];
        int[] counter_R = new int[8];
        Color[] colors =
                    {
                        Color.Red,
                        Color.Blue,
                        Color.Maroon,
                        Color.Black,
                        Color.Gray,
                        Color.Yellow,
                        Color.Teal,
                        Color.DarkBlue,
                    };

        bool[] rcv_connected = new bool[8];
        double[] timestamp = new double[8];
        double[] ttf_stop_timestamp = new double[8];
        double[] ttfS = new double[8];
        double[] ttfD = new double[8];
        double[] ttfF = new double[8];
        double[] ttfR = new double[8];
        string[] receiver_model = new string[8];
        string[] receiver_FW = new string[8];
        string[] receiver_ID = new string[8];
        double ttfS_50, ttfD_50, ttfF_50, ttfR_50;
        double ttfS_90, ttfD_90, ttfF_90, ttfR_90;
        string filename;
        int CurrentMode;

        public delegate int SolTypeHandler();

        ComboBox[] ComPortList = new ComboBox[10];
        ComboBox[] PortSpeedList = new ComboBox[8];
        Button[] ButtonOpen = new Button[8];
        Button[] ButtonClose = new Button[8];
        Label[] SolutionLabel = new Label[8];
        TextBox[] StatusText = new TextBox[8];

#endregion

        public Form1()
        {
            InitializeComponent();
            CreateComponents();
            Cycles = new ArrayList();
        }

        public void CreateComponents()
        {
            //update version numbers
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                .AddDays(version.Build)
                .AddSeconds(version.Revision * 2);

            this.Text = "FT Terminal " + version.Major + "." + version.Minor;
            textBox1.Text = "FT Terminal " + version.Major + "." + version.Minor + "." + version.Build + Environment.NewLine + buildDate.ToString("yyyy-MM-dd HH:mm"); 
            //Create ComPortList comboboxes
            for (int i = 0; i < ComPortList.Length-1; i++)
            {
                ComPortList[i] = new ComboBox
                {
                    Location = new System.Drawing.Point(8, 16 + i * 27),
                    Name = "ComPortList" + i.ToString(),
                    Size = new System.Drawing.Size(80, 21),
                    TabIndex = i,
                    Tag = i,
                };
                ComPortList[i].Items.Add("OFF");
                this.tabMain.Controls.Add(ComPortList[i]);
                ComPortList[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                openFileDialog1.FileName = "";

            }

            // ModemTab components init
            {
                ComPortList[9] = new ComboBox
                {
                    Location = new System.Drawing.Point(83, 17),
                    Name = "ComPortListModem",
                    Size = new System.Drawing.Size(80, 21),
                };
                ComPortList[9].Items.Add("OFF");
                this.tabModem.Controls.Add(ComPortList[9]);
                ComPortList[9].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                ModemPortComboBox.SelectedIndex = 0;
                ModemTypeList.SelectedIndex = 0;
                LinkRateList.SelectedIndex = 1;
            }


            //Create PortSpeedList comboboxes
            for (int i = 0; i < PortSpeedList.Length; i++)
            {
                PortSpeedList[i] = new ComboBox
                {
                    Location = new System.Drawing.Point(93, 16 + i * 27),
                    Name = "PortSpeedList" + i.ToString(),
                    Size = new System.Drawing.Size(71, 21),
                    Tag = i,
                    //TabIndex = i
                };
                PortSpeedList[i].Items.AddRange(new object[] {
                    "9600",
                    "38400",
                    "57600",
                    "115200"});
                PortSpeedList[i].SelectedIndex = 3;
                PortSpeedList[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.tabMain.Controls.Add(PortSpeedList[i]);
            }

            //Create ButtonOpen Buttons
            for (int i = 0; i < ButtonOpen.Length; i++)
            {
                ButtonOpen[i] = new Button
                {
                    Location = new System.Drawing.Point(170, 14 + i * 27),
                    Name = "ButtonOpen" + i.ToString(),
                    Size = new System.Drawing.Size(54, 22),
                    TabIndex = i,
                    Tag = i,
                    Text = "Open",
                };
                this.tabMain.Controls.Add(ButtonOpen[i]);
                this.ButtonOpen[i].Click += new System.EventHandler(this.ButtonOpen_Click);
            }

            //Create ButtonClose Buttons
            for (int i = 0; i < ButtonClose.Length; i++)
            {
                ButtonClose[i] = new Button
                {
                    Location = new System.Drawing.Point(230, 14 + i * 27),
                    Name = "ButtonClose" + i.ToString(),
                    Size = new System.Drawing.Size(54, 22),
                    TabIndex = i,
                    Tag = i,
                    Text = "Close",
                    Enabled = false,
                };
                this.tabMain.Controls.Add(ButtonClose[i]);
                this.ButtonClose[i].Click += new System.EventHandler(this.ButtonClose_Click);
            }

            //Create SoluionLabel Labels
            for (int i = 0; i < ButtonClose.Length; i++)
            {
                SolutionLabel[i] = new Label
                {
                    Location = new System.Drawing.Point(341, 19 + i * 27),
                    Name = "SolutionLabel" + i.ToString(),
                    Size = new System.Drawing.Size(100, 13),
                    TabIndex = i,
                    Text = "N/A",
                };
                this.tabMain.Controls.Add(SolutionLabel[i]);
            }

            //Create StatusText Textboxes
            for (int i = 0; i < ButtonClose.Length; i++)
            {
                StatusText[i] = new TextBox
                {
                    Location = new System.Drawing.Point(446, 16 + i * 27),
                    Name = "StatusText" + i.ToString(),
                    Size = new System.Drawing.Size(150, 13),
                    TabIndex = i,
                    Text = "Port Closed",
                    Enabled = false,
                };
                this.tabMain.Controls.Add(StatusText[i]);

                if (Properties.Settings.Default.LogsFilePath == "")
                {
                    Properties.Settings.Default.LogsFilePath = Application.StartupPath.ToString();
                }

                Command1TextBox.Text = Properties.Settings.Default.cmd1sett;
                Command2TextBox.Text = Properties.Settings.Default.cmd2sett;
                FilepathtextBox3.Text = Properties.Settings.Default.LogsFilePath;

            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // scanning available com ports
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.GetLength(0); i++)
            {
                for (int j = 0; j < ComPortList.Length; j++)
                {
                    ComPortList[j].Items.Add(ports[i]);
                }               
            }
            ComListing();
            GraphInit();

            //Default port = OFF
            for (int j = 0; j < ComPortList.Length; j++)
            {
                ComPortList[j].SelectedIndex = 0;
            }

            //Default solution - RTK fixed
            TTFSW_soltypeList.SelectedIndex = 2;
            
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            TTFStopButton.PerformClick();
            for (int i = 0; i < 8; i++)
            {
                if (rcv_connected[i] == true)
                    ButtonClose[i].PerformClick();
            }
            Properties.Settings.Default.cmd1sett = Command1TextBox.Text;
            Properties.Settings.Default.cmd2sett = Command2TextBox.Text;
            Properties.Settings.Default.LogsFilePath = FilepathtextBox3.Text;
            Properties.Settings.Default.Save();

        }

        async void ButtonOpen_Click(object sender, EventArgs e)
        {
            int i = (int)(sender as Button).Tag;
            //int index = Array.IndexOf(ButtonOpen, (Button)sender);
            //MessageBox.Show(index.ToString());
            Object selectedport = ComPortList[i].SelectedItem;
            CurrentPort[i] = new SerialPort(selectedport.ToString(), Int32.Parse(PortSpeedList[i].SelectedItem.ToString()));
            try
            {
                if (!(CurrentPort[i].IsOpen))
                {
                CurrentPort[i].Open();
                
                ButtonOpen[i].Enabled = false;
                ButtonClose[i].Enabled = true;
                TTFStartButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
  
            CurrentPort[i].DataReceived += CurrentPort_DataReceived;
            StatusText[i].Text = "Connecting";
            ReceiverConnect(i);
            await Task.Delay(500);
            if (rcv_connected[i] == false)
            {
                MessageBox.Show("Receiver not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ButtonClose[i].PerformClick();
                StatusText[i].Text = "Port Closed";
                ButtonOpen[i].Enabled = true;
                ButtonClose[i].Enabled = false;
                return;
            }
            StatusText[i].Text = "Connected";
            return;
        }

        void ButtonClose_Click(object sender, EventArgs e)
                {
                    int i = (int)(sender as Button).Tag;
                    CurrentPort[i].Close();
                    ButtonOpen[i].Enabled = true;
                    ButtonClose[i].Enabled = false;
                    SolutionLabel[i].Text = "N/A";
                    TTFStartButton.Enabled = false;
                    rcv_connected[i] = false;
                    StatusText[i].Text = "Port Closed";
                    foreach (var item in rcv_connected)
                    {
                        if (item)
                        {
                            TTFStartButton.Enabled = true;
                        }
                    }
                }

        void CurrentPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            int index = Array.IndexOf(CurrentPort, port);
            try
            {
                string data = port.ReadLine();
                this.BeginInvoke(new SetTextDeleg(Si_DataReceived), new object[] { data, index });
            }
            catch (Exception ex)
            {
                try
                {
                    this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " Error: " + ex.Message + Environment.NewLine, index });
                }
                catch (Exception ex1)
                {
                }
            }
        }

        void Si_DataReceived(string data, int index) 
        {
          

            if (data.Contains("RE"))
            {
                ReceiverReplyParse(data, index);
            }
            else
            {
                SolutionLabel[index].Text = SolType(data, index);
            }
                

                if (ComPortList[index].Text == ComPortList[8].Text)
                {
                    TextBox_Console.AppendText(data.Trim() + Environment.NewLine);
                }
            
                // запись массива байт в файл
                if (fstream[index]!=null)
                {
                    byte[] input = Encoding.Default.GetBytes(data+"\n");
                    fstream[index].Write(input, 0, input.Length);
                }        
        }  
        
        public string SolType(string data,int i)
        {
            string[] parts = data.Split(',');
            if (parts[0].Equals("$GPGGA"))
            {
                rcv_connected[i] = true;
                if (int.TryParse(parts[6], out int SolTypeInt))
                {
                    try
                    {
                        timestamp_date = DateTime.ParseExact(parts[1], "HHmmss.ff", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        LogConsole.AppendText(DateTime.Now.ToString() + " " + parts[6] + " " + "is not in the correct format. " + parts +Environment.NewLine);
                    }

                    timestamp[i] = timestamp_date.TimeOfDay.TotalSeconds;
                    switch (SolTypeInt)
                    {
                        case 0:
                            return "None Solution";
                        case 1:
                            if (SolutionLabel[i].Text == "None Solution")
                            {
                                //ttfS[i] = time;
                                ttfS[i] = timestamp[i] - ttf_stop_timestamp[i];
                            }
                            if (TTFSW_soltypeList.SelectedIndex == 0)
                            {
                                if (TTFSWmode[i] == 0)
                                {
                                    TTFSWmode[i] = 1;
                                    cycle_counter[i]++;
                                }
                            }
                            return "Standalone";
                        case 2:
                            {
                                if (CurrentMode == 1)
                                    {
                                        if (TTFSWmode[i] == 0)
                                        {
                                            TTFSWmode[i] = 1;
                                            ttfD[i] = timestamp[i] - ttf_stop_timestamp[i];
                                        cycle_counter[i]++;
                                    }
                                    }
                            }
                            return "DGNSS";
                        case 3:
                            return "PPS";
                        case 4:
                            {
                                if (CurrentMode == 2)
                                {
                                    if (TTFSWmode[i] == 0)
                                    {
                                        TTFSWmode[i] = 1;
                                        ttfR[i] = timestamp[i] - ttf_stop_timestamp[i];
                                        cycle_counter[i]++;
                                    }
                                }
                            }
                            return "RTK Fix";
                        case 5:

                            if (SolutionLabel[i].Text == "Standalone" | SolutionLabel[i].Text == "None Solution" | SolutionLabel[i].Text == "DGNSS")
                            {
                                ttfF[i] = timestamp[i] - ttf_stop_timestamp[i];
                            }
                            return "RTK Float";
                        default:
                            return "N/A";
                    }
                }
                return "N/A";
            }
            return "N/A";
        }


        public void TTFSWStart_Click(object sender, EventArgs e)
        {
            Cycles.Clear();
            TTFSW_soltypeList.Enabled = false;
            TTFStartButton.Enabled = false;
            TTFStopButton.Enabled = true;
            Timeout1TextBox.Enabled = false;
            Timeout2TextBox.Enabled = false;
            Command1TextBox.Enabled = false;
            Command2TextBox.Enabled = false;
            NumberOfCyclesTextBox.Enabled = false;
            CurrentMode = TTFSW_soltypeList.SelectedIndex;

            TableInit(CurrentMode);
            //starting command timer
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;// Hook up the Elapsed event for the timer. 
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            int openportscount = 0;
            //Creating log files for each open Com Port and variables init
            for (int i = 0; i < 8; i++)
            {
                cycle_counter[i] = 0;
                counter_S[i] = 0;
                counter_D[i] = 0;
                counter_F[i] = 0;
                counter_R[i] = 0;
                ttfS[i] = 0;
                ttfD[i] = 0;
                ttfF[i] = 0;
                ttfR[i] = 0;
                CurrentCommandLine[i] = 0;
                TTF_Timeout1[i] = int.Parse(Timeout1TextBox.Text);
                TTF_Timeout2[i] = int.Parse(Timeout2TextBox.Text);
                TTFSWmode[i] = 1;
                sleep_timer[i] = 0;
                //Creating log files for each open Com Port
                if (rcv_connected[i] == true)
                {
                    try
                    {
                        filename = ComPortList[i].Text +" "+ receiver_model[i] + " " + receiver_ID[i].Substring(7) + " " + DateTime.Now.ToString("MM-dd HH.mm") + ".gga";
                        filename = @System.IO.Path.Combine(FilepathtextBox3.Text, filename);
                        fstream[i] = new FileStream(filename, FileMode.Create,FileAccess.ReadWrite,FileShare.Read,bufferSize:8);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //SendCommand1(i);
                    dataGridView1[0, i + 1].Value = ComPortList[i].Text;
                    dataGridView1[1, i + 1].Value = receiver_model[i];
                    dataGridView1[2, i + 1].Value = receiver_ID[i];
                    dataGridView1[3, i + 1].Value = receiver_FW[i];
                    dataGridView1[11, i + 1].Style.BackColor=colors[i];
                    dataGridView1[12, i + 1].Value = receiver_model[i] + " " + receiver_FW[i] + " " + receiver_ID[i].Substring(7);
                    openportscount++;
                }              
                //Disabling Open/Close buttons
                ButtonClose[i].Enabled = false;
                ButtonOpen[i].Enabled = false;
            }
            progressBar1.Minimum = 0;
            progressBar1.Maximum = int.Parse(NumberOfCyclesTextBox.Text) * openportscount;
        }

        public void TTFSWStop_Click(object sender, EventArgs e)
        {
            TTFSW_soltypeList.Enabled = true;
            TTFStartButton.Enabled = true;
            TTFStopButton.Enabled = false;
            Timeout1TextBox.Enabled = true;
            Timeout2TextBox.Enabled = true;
            this.Command1TextBox.Enabled = true;
            this.Command2TextBox.Enabled = true;
            NumberOfCyclesTextBox.Enabled = true;
            progressBar1.Value = 0;
            aTimer.Enabled = false;
            aTimer.Close();
            for (int i = 0; i < 8; i++)
            {
            TTFSWmode[i] = 0;
                if (rcv_connected[i]==true)
                {
                    fstream[i].Close();
                    fstream[i] = null;
                    ButtonClose[i].Enabled = true;
                    SendCommand2(i);
                    this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { "Waiting for solution", i }); ;
                }
                else
                {
                    ButtonOpen[i].Enabled = true;
                }
            }    
            string data = TTF_Timeout1.ToString();
            //saving statistics
            filename = "Result" +  " " + DateTime.Now.ToString("yyyy-MM-dd HH.mm") + ".csv";
            filename = @System.IO.Path.Combine(FilepathtextBox3.Text.ToString(), filename);
            WriteCSV(dataGridView1, filename);
            WriteCycles();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 & e.RowIndex != 0)
            {
                colorDialog1.Color = dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor;
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = colorDialog1.Color;
                }
            }
            if (e.ColumnIndex == 12 & e.RowIndex !=0)
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = false;
                dataGridView1.BeginEdit(false);
            }
            dataGridView1[e.ColumnIndex, e.RowIndex].Selected = false;
        }

        private void colorbtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (rcv_connected[i] == true)
                {
                    dataGridView1[12, i + 1].Value = receiver_model[i] + " " + receiver_FW[i] + " " + receiver_ID[i].Substring(7);
                }
            }
            zedGraphMain.Invalidate();
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Application.StartupPath;
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FilepathtextBox3.Text=dialog.FileName;
            }
        }

        private void LoadScriptBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader Scriptreader = new StreamReader(openFileDialog1.FileName);
                string line;
                int i = 0;
                Command1TextBox.Text = "";
                Command2TextBox.Text = "";
                LogConsole.AppendText(DateTime.Now.ToString() + " Script file parsing sarted: " + openFileDialog1.FileName + Environment.NewLine);

                line = Scriptreader.ReadLine().Substring(11);
                Timeout1TextBox.Text = line;
                LogConsole.AppendText("Timeout 1: " + line + Environment.NewLine);

                line = Scriptreader.ReadLine().Substring(11);
                Timeout2TextBox.Text = line;
                LogConsole.AppendText("Timeout 2: " + line + Environment.NewLine);

                line = Scriptreader.ReadLine();
                line = line.Substring(line.Length-1);
                TTFSW_soltypeList.SelectedIndex = int.Parse(line);
                LogConsole.AppendText("Solution Type index: " + line + Environment.NewLine);

                line = Scriptreader.ReadLine().Substring(18);
                NumberOfCyclesTextBox.Text = line;
                LogConsole.AppendText("Number of cycles: " + line + Environment.NewLine);

                line = Scriptreader.ReadLine();
                LogConsole.AppendText(line + Environment.NewLine);

                while ((line = Scriptreader.ReadLine()) != "Command 2:")
                {
                    Command1TextBox.AppendText(line + Environment.NewLine);
                    LogConsole.AppendText(line + Environment.NewLine);
                }
                LogConsole.AppendText(line + Environment.NewLine);

                while ((line = Scriptreader.ReadLine()) != null)
                {
                    Command2TextBox.AppendText(line + Environment.NewLine);
                    LogConsole.AppendText(line + Environment.NewLine);
                }


            }
        }
        private void SaveScriptBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                StreamWriter Scriptwriter = new StreamWriter(saveFileDialog1.FileName);
                Scriptwriter.WriteLine("Timeout 1: " + Timeout1TextBox.Text);
                Scriptwriter.WriteLine("Timeout 2: " + Timeout2TextBox.Text);
                Scriptwriter.WriteLine("Solution Type: " + TTFSW_soltypeList.Text + " " + TTFSW_soltypeList.SelectedIndex);
                Scriptwriter.WriteLine("Number of cycles: " + NumberOfCyclesTextBox.Text);
                Scriptwriter.WriteLine("Command 1:");
                foreach (var line in Command1TextBox.Lines)
                {
                    Scriptwriter.WriteLine(line);
                }
                Scriptwriter.WriteLine("Command 2:");
                foreach (var line in Command2TextBox.Lines)
                {
                    Scriptwriter.WriteLine(line);
                }
                Scriptwriter.Close();
            }
        }





        //Command Timer
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (rcv_connected[i]==true)
                {
                    switch (TTFSWmode[i])
                    {
                        case 0: //waiting for selected solution
                            this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { "Waiting for solution", i });
                            break;
                        case 1: //got solution, wating for timeout 1
                            if (TTF_Timeout1[i] > 0)
                            {
                                TTF_Timeout1[i]--;
                                string str = "Waiting for Timeout 1: " + TTF_Timeout1[i].ToString();
                                try
                                {
                                    this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { str, i }); 
                                }
                                finally { }
                                
                            }
                            else
                            {
                                //sent command 1
                                SendCommand1(i);
                            }
                            break;
                        case 2: //waiting for timeout 2
                            if (TTF_Timeout2[i] > 0)
                            {
                                TTF_Timeout2[i]--;
                                string str = "Waiting for Timeout 2: " + TTF_Timeout2[i].ToString();
                                try
                                {
                                    this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { str, i });
                                }
                                finally { }
                            }
                            else
                            {
                                //sent command 2
                                ttf_stop_timestamp[i] = timestamp[i];
                                SendCommand2(i);
                                TTFSWmode[i] = 0;
                                TTF_Timeout2[i] = int.Parse(Timeout2TextBox.Text);
                               
                            }
                            break;
                        case 3: // on a sleep timer
                            {
                                if (sleep_timer[i] > 0)
                                {
                                    sleep_timer[i]--;
                                    string str = "Waiting for Sleep timer: " + sleep_timer[i].ToString();
                                    try
                                    {
                                        this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { str, i });
                                    }
                                    finally { }
                                }
                                else
                                {
                                    SendCommand1(i);
                                }
                            }
                            break;
                    }
                }
            }
        }

        void TimedStop()
        {
            TTFStopButton.PerformClick();
        }

        void STUpdate (string str, int i)
        {
            StatusText[i].Text = str;
        }

        void LogUpdate(string str, int i)
        {
            LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + i + str + Environment.NewLine);
        }

        void ProgUpdate()
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
            }
            
        }

        void AddCycleResult(int i)
        {
            if (ttfS[i] >=0 && ttfD[i] >= 0 && ttfF[i] >= 0 && ttfR[i] >= 0)
            {
                if (cycle_counter[i]!=-1)
                {
                    Cycles.Add(new TTFcycle(cycle_counter[i], i, ttfS[i], ttfD[i], ttfF[i], ttfR[i], ttf_stop_timestamp[i], ttfR[i] + ttf_stop_timestamp[i]));
                }
                this.BeginInvoke(new ProgressUpdate(ProgUpdate));
            }
        }
    }
}
