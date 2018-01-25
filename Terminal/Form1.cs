using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace Terminal
{
    public struct TTFcycle
    {
        public int number, channel;
        public float TTFS, TTFD, TTFR, TTFF;

        public TTFcycle(int number_, int channel_, float TTFS_,float TTFD_, float TTFF_, float TTFR_)
        {
            number = number_;
            channel = channel_;
            TTFS = TTFS_;
            TTFD = TTFD_;
            TTFF = TTFF_;
            TTFR = TTFR_;
        }
    }

    public partial class Form1 : Form
    {
        SerialPort[] CurrentPort = new SerialPort[8];
        FileStream[] fstream = new FileStream[8];
        ArrayList Cycles;
        
        private delegate void SetTextDeleg(string text, int index);

        System.Timers.Timer aTimer;
        System.Timers.Timer ttfTimer;
        int[] TTFSWmode=new int[8]; //0-Waiting for fixed 1 - Reset timeout, 2 - AntEnable timeout
        int[] TTF_Timeout1 = new int[8];
        int[] TTF_Timeout2 = new int[8];
        int[] cycle_counter = new int[8];
        float[] ttf_counter = new float[8];
        int[] counter_S = new int[8];
        int[] counter_D = new int[8];
        int[] counter_F = new int[8];
        int[] counter_R = new int[8];                                    
        float[] ttfS = new float[8];
        float[] ttfD = new float[8];
        float[] ttfF = new float[8];
        float[] ttfR = new float[8];
        double ttfS_50, ttfD_50, ttfF_50, ttfR_50;
        double ttfS_90, ttfD_90, ttfF_90, ttfR_90;
        string filename;

        ComboBox[] ComPortList = new ComboBox[9];
        ComboBox[] PortSpeedList = new ComboBox[8];
        Button[] ButtonOpen = new Button[8];
        Button[] ButtonClose = new Button[8];
        Label[] SolutionLabel = new Label[8];

        public Form1()
        {
            InitializeComponent();
            CreateComponents();
            Cycles = new ArrayList();
        }

        public void CreateComponents()
        {
            //Create ComPortList comboboxses
            for (int i = 0; i < ComPortList.Length; i++)
            {
                ComPortList[i] = new ComboBox
                {
                    Location = new System.Drawing.Point(8 , 16+ i * 27),
                    Name = "ComPortList" + i.ToString(),
                    Size = new System.Drawing.Size(80, 21),
                    TabIndex = i,
                    Tag = i,
                };
                ComPortList[i].Items.Add("OFF");
                this.tabMain.Controls.Add(ComPortList[i]);
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

            //Initializing table
            {
                dataGridView1.Rows.Add(8);
                    dataGridView1[0, 0].Value = "Port ID";
                    dataGridView1[1, 0].Value = "Cycle count";
                    dataGridView1[2, 0].Value = "Standalone TTF (50/90%)";
                    dataGridView1[3, 0].Value = "DGNSS TTF (50/90%)";
                    dataGridView1[4, 0].Value = "RTK Float TTF (50/90%)";
                    dataGridView1[5, 0].Value = "RTK Fix TTF (50/90%)";   
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

            //Default port = OFF
            for (int j = 0; j < ComPortList.Length; j++)
            {
                ComPortList[j].SelectedIndex = 0;
            }

            //Default solution - RTK fixed
            TTFSW_soltypeList.SelectedIndex = 2;
        }

        void ButtonOpen_Click(object sender, EventArgs e)
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
            }
  
            CurrentPort[i].DataReceived += CurrentPort_DataReceived;
            
        }

        void ButtonClose_Click(object sender, EventArgs e)
                {
                    int i = (int)(sender as Button).Tag;
                    CurrentPort[i].Close();
                    ButtonOpen[i].Enabled = true;
                    ButtonClose[i].Enabled = false;
                    SolutionLabel[i].Text = "N/A";
                    TTFStartButton.Enabled = false;
                }

        void CurrentPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            int index = Array.IndexOf(CurrentPort, (SerialPort)sender);
            try
            {
                string data = port.ReadLine();
                this.BeginInvoke(new SetTextDeleg(Si_DataReceived), new object[] { data, index }); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        void Si_DataReceived(string data, int index) 
        {
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
            
            SolutionLabel[index].Text = SolType(data,index);
        }  
        
        string SolType(string data,int i)
        {
            string[] parts = data.Split(',');
            if (parts[0].Equals("$GPGGA"))
            {
                switch (int.Parse(parts[6]))
                {
                    case 0:
                        return "None Solution";
                    case 1:
                        if (SolutionLabel[i].Text == "None Solution")
                        {
                            ttfS[i] = ttf_counter[i];
                            if (!TTFStartButton.Enabled)
                            {
                                StatisticChange(i);
                            }
                            
                        }
                        if (TTFSW_soltypeList.SelectedIndex == 0)
                        {
                            if (TTFSWmode[i] == 0)
                            {
                                TTFSWmode[i] = 1;
                            }
                        }
                        return "Standalone";
                    case 2:
                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution")
                        {
                            ttfD[i] = ttf_counter[i];
                            if (!TTFStartButton.Enabled)
                            {
                                StatisticChange(i);
                            }
                        }
                        if (TTFSW_soltypeList.SelectedIndex == 1)
                        {
                            if (TTFSWmode[i] == 0)
                            {
                                TTFSWmode[i] = 1;
                            }
                        }
                        return "DGNSS";
                    case 3:
                        return "PPS";
                    case 4:
                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution" | 
                                        SolutionLabel[0].Text == "RTK Float" |  SolutionLabel[0].Text == "DGNSS")
                        {
                            ttfR[i] = ttf_counter[i];
                            if (!TTFStartButton.Enabled)
                            {
                                StatisticChange(i);
                            }
                        }
                        if (TTFSW_soltypeList.SelectedIndex == 2)
                        {
                            if (TTFSWmode[i] == 0)
                            {
                                TTFSWmode[i] = 1;   
                            } 
                        }
                        return "RTK Fix";
                    case 5:

                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution" | SolutionLabel[0].Text == "DGNSS")
                        {
                            if (!TTFStartButton.Enabled)
                            {
                                StatisticChange(i);
                            }
                        }
                        return "RTK Float";
                    default:
                        return "N/A";
                }
            }
            return "N/A"; 
        }

        public void TTFSWStart_Click(object sender, EventArgs e)
        {
            TTFSW_soltypeList.Enabled = false;
            TTFStartButton.Enabled = false;
            TTFStopButton.Enabled = true;
            Timeout1TextBox.Enabled = false;
            Timeout2TextBox.Enabled = false;
            Command1TextBox.Enabled = false;
            Command2TextBox.Enabled = false;

            //starting command timer
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;// Hook up the Elapsed event for the timer. 
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            
            //Creating log files for each open Com Port and variables init
            for (int i = 0; i < 8; i++)
            {
                cycle_counter[i] = 0;
                ttf_counter[i] = 0;
                counter_S[i] = 0;
                counter_D[i] = 0;
                counter_F[i] = 0;
                counter_R[i] = 0;
                ttfS[i] = 0;
                ttfD[i] = 0;
                ttfF[i] = 0;
                ttfR[i] = 0;
                TTF_Timeout1[i] = int.Parse(Timeout1TextBox.Text);
                TTF_Timeout2[i] = int.Parse(Timeout2TextBox.Text);
                TTFSWmode[i] = 0;

                //Creating log files for each open Com Port
                if (ComPortList[i].Text != "OFF")
                {
                    try
                    {
                        filename = "ComPort" + ComPortList[i].Text.Substring(3) +" "+ DateTime.Now.ToString("yyyy-MM-dd HH.mm") + ".gga";
                        filename = @System.IO.Path.Combine(Application.StartupPath.ToString(), filename);
                        fstream[i] = new FileStream(filename, FileMode.Create,FileAccess.ReadWrite,FileShare.Read,bufferSize:8);
                        //Sending command2, if the option is checked
                        if (SendCheckBox.Checked)
                        {
                            CurrentPort[i].WriteLine(Command1TextBox.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                    dataGridView1[0,i+1].Value = ComPortList[i].Text;
                }
                
                //Disabling Open/Close buttons
                ButtonClose[i].Enabled = false;
                ButtonOpen[i].Enabled = false;
            }
            
            //starting ttf timer
            ttfTimer = new System.Timers.Timer(100);
            ttfTimer.Elapsed += OnttfTimedEvent;// Hook up the Elapsed event for the timer. 
            ttfTimer.AutoReset = true;
            ttfTimer.Enabled = true;
        }

        public void TTFSWStop_Click(object sender, EventArgs e)
        {
            TTFSW_soltypeList.Enabled = true;
            TTFStartButton.Enabled = true;
            TTFStopButton.Enabled = false;
            Timeout1TextBox.Enabled = true;
            Timeout2TextBox.Enabled = true;
            Command1TextBox.Enabled = true;
            Command2TextBox.Enabled = true;
            aTimer.Enabled = false;
            aTimer.Close();
            ttfTimer.Enabled = false;
            ttfTimer.Close();
            for (int i = 0; i < 8; i++)
            {
            TTFSWmode[i] = 0;
                if (fstream[i]!= null)
                {
                    fstream[i].Close();
                    fstream[i] = null;
                }
                if (ComPortList[i].Text != "OFF")
                {
                    ButtonClose[i].Enabled = true;
                    CurrentPort[i].WriteLine(Command2TextBox.Text);
                }
                else
                {
                    ButtonOpen[i].Enabled = true;
                }
            }    
            string data = TTF_Timeout1.ToString();
            data = TTF_Timeout2.ToString();
            //saving statistics
            filename = "Result" +  " " + DateTime.Now.ToString("yyyy-MM-dd HH.mm") + ".csv";
            filename = @System.IO.Path.Combine(Application.StartupPath.ToString(), filename);
            WriteCSV(dataGridView1, filename);
        }
        
        //TTF timer, 0.1 sec interval
        public void OnttfTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                ttf_counter[i]++;
            }
        }
        
        //Command Timer
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                switch (TTFSWmode[i])
                {
                    case 0:
                        break;
                    case 1:
                        if (TTF_Timeout1[i] > 0)
                        {
                            TTF_Timeout1[i]--;
                        }
                        else
                        {
                            //sent command 1
                            CurrentPort[i].WriteLine(Command1TextBox.Text);
                            cycle_counter[i]++;
                            TTFSWmode[i] = 2;
                            TTF_Timeout1[i] = int.Parse(Timeout1TextBox.Text);

                            AddCycleResult(i);

                        }
                        break;
                    case 2:
                        if (TTF_Timeout2[i] > 0)
                        {
                            TTF_Timeout2[i]--;
                        }
                        else
                        {
                            //sent command 2
                            CurrentPort[i].WriteLine(Command2TextBox.Text);
                            ttf_counter[i] = 0;
                            TTFSWmode[i] = 0;
                            TTF_Timeout2[i] = int.Parse(Timeout2TextBox.Text);
                        }
                    break;
                }
            }
        }

        void Timeout1Change (string data)
        {
            Timeout1TextBox.Text = data;
        }

        void Timeout2Change (string data)
        {
            Timeout2TextBox.Text = data;
        }

        void AddCycleResult(int i)
        {
            Cycles.Add(new TTFcycle(cycle_counter[i], i, ttfS[i], ttfD[i], ttfF[i], ttfR[i]));
        }

        void StatisticChange(int i)
        {
            List<double> ttfS_ = new List<double>();
            List<double> ttfD_ = new List<double>();
            List<double> ttfF_ = new List<double>();
            List<double> ttfR_ = new List<double>();

            foreach (TTFcycle TTFtemp in Cycles)
            {
                if (TTFtemp.channel == i)
                {
                    ttfS_.Add(TTFtemp.TTFS);
                    ttfD_.Add(TTFtemp.TTFD);
                    ttfF_.Add(TTFtemp.TTFF);
                    ttfR_.Add(TTFtemp.TTFR);
                    /*Calculating average TTF
                    ttfS_sum = ttfS_sum + TTFtemp.TTFS;
                    ttfD_sum = ttfD_sum + TTFtemp.TTFD;
                    ttfR_sum = ttfR_sum + TTFtemp.TTFR;
                    ttfF_sum = ttfF_sum + TTFtemp.TTFF;
                    */
                }
            }
            /*Calculating average TTF
            ttfS_avg = ttfS_sum / cycle_counter[i];
            ttfD_avg = ttfD_sum / cycle_counter[i];
            ttfF_avg = ttfF_sum / cycle_counter[i];
            ttfR_avg = ttfR_sum / cycle_counter[i];
            */
            ttfS_50 = Percentile(ttfS_.ToArray(),0.5);
            ttfD_50 = Percentile(ttfD_.ToArray(),0.5); 
            ttfF_50 = Percentile(ttfF_.ToArray(),0.5);
            ttfR_50 = Percentile(ttfR_.ToArray(),0.5);

            ttfS_90 = Percentile(ttfS_.ToArray(), 0.9);
            ttfD_90 = Percentile(ttfD_.ToArray(), 0.9);
            ttfF_90 = Percentile(ttfF_.ToArray(), 0.9);
            ttfR_90 = Percentile(ttfR_.ToArray(), 0.9);


            dataGridView1[1, i + 1].Value = cycle_counter[i].ToString();
            dataGridView1[2, i + 1].Value = Math.Round((ttfS_50 / 10), 2).ToString() + " / " + Math.Round((ttfS_90 / 10), 2).ToString();
            dataGridView1[3, i + 1].Value = Math.Round((ttfD_50 / 10), 2).ToString() + " / " + Math.Round((ttfD_90 / 10), 2).ToString();
            dataGridView1[4, i + 1].Value = Math.Round((ttfF_50 / 10), 2).ToString() + " / " + Math.Round((ttfF_90 / 10), 2).ToString();
            dataGridView1[5, i + 1].Value = Math.Round((ttfR_50 / 10), 2).ToString() + " / " + Math.Round((ttfR_90 / 10), 2).ToString();

            
        }

        public double Percentile(double[] sequence, double excelPercentile)
        {
            
            Array.Sort(sequence);
            int N = sequence.Length;
            if (N == 0)
            {
                return 0;
            }
            double n = (N - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }

        public void WriteCSV(DataGridView gridIn, string outputFile)
        {
            //test to see if the DataGridView has any rows
            if (gridIn.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile);

                //write header rows to csv
                /*
                for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    swOut.Write(gridIn.Columns[i].HeaderText);
                }

                swOut.WriteLine();
                */
                //write DataGridView rows to csv
                for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = gridIn.Rows[j];
                    if (dr.Cells[0].Value==null)
                    {
                        continue;    
                    }

                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {
                        
                        if (i > 0)
                        {
                            swOut.Write(";");
                        }

                        if (dr.Cells[i].Value == null)
                        {
                            value = "";
                        }
                        else
                        { 
                            value = dr.Cells[i].Value.ToString();
                        }
                        
                        
                        //replace comma's with spaces
                        value = value.Replace(',', ' ');
                        //replace embedded newlines with spaces
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                swOut.Close();
            }
        }

    }
}
