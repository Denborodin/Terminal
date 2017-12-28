using System;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Form1 : Form
    {
        SerialPort[] CurrentPort = new SerialPort[8];

        private delegate void SetTextDeleg(string text);

        System.Timers.Timer aTimer;
        System.Timers.Timer ttfTimer;

        int AntSWmode; //0-Waiting for fixed 1 - Reset timeout, 2 - AntEnable timeout
        int AntResetTimeout_int;
        int AntEnableTimeout_int;
        int cycle_counter;
        int ttf_counter, counter_S, counter_D, counter_F, counter_R;
        float ttfS, ttfD, ttfF, ttfR;
        float ttfS_sum, ttfD_sum, ttfF_sum, ttfR_sum;
        ComboBox[] ComPortList = new ComboBox[8];
        ComboBox[] PortSpeedList = new ComboBox[8];
        Button[] ButtonOpen = new Button[8];
        Button[] ButtonClose = new Button[8];
        Label[] SolutionLabel = new Label[8];

        public Form1()
        {
            InitializeComponent();
            CreateComponents();
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
                    TabIndex = i
                };
                ComPortList[i].Items.Add("OFF");
                this.tabMain.Controls.Add(ComPortList[i]);
            }

            //Create PortSpeedList comboboxses
            for (int i = 0; i < PortSpeedList.Length; i++)
            {
                PortSpeedList[i] = new ComboBox
                {
                    Location = new System.Drawing.Point(93, 16 + i * 27),
                    Name = "PortSpeedList" + i.ToString(),
                    Size = new System.Drawing.Size(71, 21),
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
                    Text = "Open",
                };
                this.tabMain.Controls.Add(ButtonOpen[i]);
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
                    Text = "Close",
                };
                this.tabMain.Controls.Add(ButtonClose[i]);
            }

            //Create SoluionLabel Labels
            for (int i = 0; i < ButtonClose.Length; i++)
            {
                SolutionLabel[i] = new Label
                {
                    Location = new System.Drawing.Point(341, 19 + i * 27),
                    Name = "SolutionLabel" + i.ToString(),
                    Size = new System.Drawing.Size(27, 13),
                    TabIndex = i,
                    Text = "N/A",
                };
                this.tabMain.Controls.Add(SolutionLabel[i]);
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            
            // default speed = 115200


            // scanning available com ports
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.GetLength(0); i++)
            {
                for (int j = 0; j < ComPortList.Length; j++)
                {
                    ComPortList[j].Items.Add(ports[i]);
                }               
            }

            for (int j = 0; j < ComPortList.Length; j++)
            {
                ComPortList[j].SelectedIndex = 0;
            }

            //Default solution - RTK fixed
            AntSW_soltype.SelectedIndex = 2;

            //Initializing table
            
            dataGridView1.Rows.Add(4);
            dataGridView1[0, 0].Value = "Cycle count";
            dataGridView1[0, 1].Value = "Standalone TTF";
            dataGridView1[0, 2].Value = "DGNSS TTF";
            dataGridView1[0, 3].Value = "RTK Float TTF";
            dataGridView1[0, 4].Value = "RTK Fix TTF";

        }

        void ButtonOpen_Click(object sender, EventArgs e)
        {
            Object selectedport = ComPortList[1].SelectedItem;
            CurrentPort[0] = new SerialPort(selectedport.ToString(), Int32.Parse(PortSpeedList[0].SelectedItem.ToString()));
            try
            {
                if (!(CurrentPort[0].IsOpen))
                {
                CurrentPort[0].Open();
                ButtonOpen[0].Enabled = false;
                ButtonClose[0].Enabled = true;
                AntSWStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);         
            }
  
            CurrentPort[0].DataReceived += CurrentPort_DataReceived;
        }

        void CurrentPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            try
            {
                string data = port.ReadLine();
                this.BeginInvoke(new SetTextDeleg(Si_DataReceived), new object[] { data }); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ComPortsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void Si_DataReceived(string data) 
        {
            TextBox_Console.AppendText(data.Trim() + Environment.NewLine);
            SolutionLabel[0].Text = SolType(data);
        }  
        
        void ButtonClose_Click(object sender, EventArgs e)
        {
            CurrentPort[0].Close();
            ButtonOpen[0].Enabled = true;
            ButtonClose[0].Enabled = false;
            SolutionLabel[0].Text = "N/A";
            AntSWStart.Enabled = false;
        }

        string SolType(string data)
        {
            string[] parts = data.Split(',');
            if (parts[0].Equals("$GPGGA"))
            {
                switch (int.Parse(parts[6]))
                {
                    case 0:
                        return "None Solution";
                    case 1:
                        if (SolutionLabel[0].Text == "None Solution")
                        {
                            ttfS_sum = ttfS_sum + ttf_counter;
                            counter_S = counter_S + 1;
                            ttfS = ttfS_sum / counter_S;
                           //StatisticChange();
                        }
                        if (AntSW_soltype.SelectedIndex == 0)
                        {
                            if (AntSWmode == 0)
                            {
                                AntSWmode = 1;
                            }
                        }
                        return "Standalone";
                    case 2:
                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution")
                        {
                            ttfD_sum = ttfD_sum + ttf_counter;
                            counter_D = counter_D + 1;
                            ttfD = ttfD_sum / counter_D;
                            //StatisticChange();
                        }
                        if (AntSW_soltype.SelectedIndex == 1)
                        {
                            if (AntSWmode == 0)
                            {
                                AntSWmode = 1;
                            }
                        }
                        return "DGNSS";
                    case 3:
                        return "PPS";
                    case 4:
                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution" | 
                                        SolutionLabel[0].Text == "RTK Float" |  SolutionLabel[0].Text == "DGNSS")
                        {
                            ttfR_sum = ttfR_sum + ttf_counter;
                            counter_R = counter_R + 1;
                            ttfR = ttfR_sum / counter_R;
                            //StatisticChange();
                        }
                        if (AntSW_soltype.SelectedIndex == 2)
                        {
                            if (AntSWmode == 0)
                            {
                                AntSWmode = 1;   
                            } 
                        }
                        return "RTK Fix";
                    case 5:

                        if (SolutionLabel[0].Text == "Standalone" | SolutionLabel[0].Text == "None Solution" | SolutionLabel[0].Text == "DGNSS")
                        {
                            ttfF_sum = ttfF_sum + ttf_counter;
                            counter_F = counter_F + 1;
                            ttfF = ttfF_sum / counter_F;
                            //StatisticChange();
                        }

                        return "RTK Float";
                    default:
                        return "N/A";
                }
            }
            return "N/A"; 
        }

        public void RTKSWStart_Click(object sender, EventArgs e)
        {
            AntSW_soltype.Enabled = false;
            AntSWStart.Enabled = false;
            AntSWStop.Enabled = true;
            AntResetTimeout.Enabled = false;
            AntEnableTimeout.Enabled = false;
            AntResetCommand1.Enabled = false;
            AntResetCommand2.Enabled = false;
            AntResetTimeout_int = int.Parse(AntResetTimeout.Text);
            AntEnableTimeout_int = int.Parse(AntEnableTimeout.Text);
            //starting command timer
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;// Hook up the Elapsed event for the timer. 
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            cycle_counter = 0;
            ttf_counter = 0;
            counter_S = 0;
            counter_D = 0;
            counter_F = 0;
            counter_R = 0;
            ttfS = 0;
            ttfD = 0;
            ttfF = 0;
            ttfR = 0;

            //starting ttf timer
            ttfTimer = new System.Timers.Timer(100);
            ttfTimer.Elapsed += OnttfTimedEvent;// Hook up the Elapsed event for the timer. 
            ttfTimer.AutoReset = true;
            ttfTimer.Enabled = true;
        }

        public void RTKSWStop_Click(object sender, EventArgs e)
        {
            AntSW_soltype.Enabled = true;
            AntSWStart.Enabled = true;
            AntSWStop.Enabled = false;
            AntResetTimeout.Enabled = true;
            AntEnableTimeout.Enabled = true;
            AntResetCommand1.Enabled = true;
            AntResetCommand2.Enabled = true;
            aTimer.Enabled = false;
            aTimer.Close();
            ttfTimer.Enabled = false;
            ttfTimer.Close();
            AntSWmode = 0;
            string data = AntResetTimeout_int.ToString();
            this.BeginInvoke(new SetTextDeleg(RTKResetTimeoutChange), new object[] { data });
            data = AntEnableTimeout_int.ToString();
            this.BeginInvoke(new SetTextDeleg(RTKEnableTimeoutChange), new object[] { data });
        }

        public void OnttfTimedEvent(Object source, ElapsedEventArgs e)
        {
            ttf_counter = ttf_counter + 1;
        }

        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            switch (AntSWmode)
            {
                case 0:
                    break;
                case 1:
                    if (int.Parse(AntResetTimeout.Text)>0)
                        {
                            string data = (int.Parse(AntResetTimeout.Text) - 1).ToString();
                            this.BeginInvoke(new SetTextDeleg(RTKResetTimeoutChange), new object[] { data });
                        }
                        else
                        {
                            CurrentPort[0].WriteLine(AntResetCommand1.Text);
                            //TextBox_Console.AppendText(AntResetCommand1.Text + Environment.NewLine);
                            string data = AntResetTimeout_int.ToString();
                            this.BeginInvoke(new SetTextDeleg(RTKResetTimeoutChange), new object[] { data });
                            cycle_counter = cycle_counter + 1;
                            AntSWmode = 2;
                            StatisticChange();
                            //sent command 1
                        }
                    break;
                case 2:
                    if (int.Parse(AntEnableTimeout.Text) > 0)
                        {
                            string data = (int.Parse(AntEnableTimeout.Text) - 1).ToString();
                            this.BeginInvoke(new SetTextDeleg(RTKEnableTimeoutChange), new object[] { data });
                        }
                        else
                        {
                            CurrentPort[0].WriteLine(AntResetCommand2.Text);
                            //TextBox_Console.AppendText(AntResetCommand2.Text + Environment.NewLine);
                            string data = AntEnableTimeout_int.ToString();
                            this.BeginInvoke(new SetTextDeleg(RTKEnableTimeoutChange), new object[] { data });
                            //sent command 2
                            ttf_counter = 0;
                            AntSWmode = 0;
                        }
                    break;

            }
        }

        void RTKResetTimeoutChange (string data)
        {
            AntResetTimeout.Text = data;
        }

        void RTKEnableTimeoutChange(string data)
        {
            AntEnableTimeout.Text = data;
        }

        void StatisticChange()
        {
            dataGridView1[1, 0].Value = cycle_counter.ToString();
            dataGridView1[1, 1].Value = Math.Round((ttfS / 10), 2).ToString();
            dataGridView1[1, 2].Value = Math.Round((ttfD / 10), 2).ToString();
            dataGridView1[1, 3].Value = Math.Round((ttfF / 10), 2).ToString();
            dataGridView1[1, 4].Value = Math.Round((ttfR / 10), 2).ToString();
        }
    }
}
