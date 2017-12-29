﻿using System;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Form1 : Form
    {
        SerialPort[] CurrentPort = new SerialPort[8];

        private delegate void SetTextDeleg(string text, int index);

        System.Timers.Timer aTimer;
        System.Timers.Timer ttfTimer;
        int[] TTFSWmode=new int[8]; //0-Waiting for fixed 1 - Reset timeout, 2 - AntEnable timeout
        int[] TTF_Timeout1 = new int[8];
        int[] TTF_Timeout2 = new int[8];
        int[] cycle_counter = new int[8];
        int[] ttf_counter = new int[8];
        int[] counter_S = new int[8];
        int[] counter_D = new int[8];
        int[] counter_F = new int[8];
        int[] counter_R = new int[8];
        float[] ttfS = new float[8];
        float[] ttfD = new float[8];
        float[] ttfF = new float[8];
        float[] ttfR = new float[8];
        float[] ttfS_sum = new float[8];
        float[] ttfD_sum = new float[8];
        float[] ttfF_sum = new float[8];
        float[] ttfR_sum = new float[8];

        ComboBox[] ComPortList = new ComboBox[9];
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
                dataGridView1.Rows.Add(4);
                dataGridView1[0, 0].Value = "Cycle count";
                dataGridView1[0, 1].Value = "Standalone TTF";
                dataGridView1[0, 2].Value = "DGNSS TTF";
                dataGridView1[0, 3].Value = "RTK Float TTF";
                dataGridView1[0, 4].Value = "RTK Fix TTF";
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
            TTFSW_soltype.SelectedIndex = 2;
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
                AntSWStart.Enabled = true;
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
                    AntSWStart.Enabled = false;
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
                            ttfS_sum[i] = ttfS_sum[i] + ttf_counter[i];
                            counter_S[i] = counter_S[i] + 1;
                            ttfS[i] = ttfS_sum[i] / counter_S[i];
                           //StatisticChange();
                        }
                        if (TTFSW_soltype.SelectedIndex == 0)
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
                            ttfD_sum[i] = ttfD_sum[i] + ttf_counter[i];
                            counter_D[i] = counter_D[i] + 1;
                            ttfD[i] = ttfD_sum[i] / counter_D[i];
                            //StatisticChange();
                        }
                        if (TTFSW_soltype.SelectedIndex == 1)
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
                            ttfR_sum[i] = ttfR_sum[i] + ttf_counter[i];
                            counter_R[i] = counter_R[i] + 1;
                            ttfR[i] = ttfR_sum[i] / counter_R[i];
                            //StatisticChange();
                        }
                        if (TTFSW_soltype.SelectedIndex == 2)
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
                            ttfF_sum[i] = ttfF_sum[i] + ttf_counter[i];
                            counter_F[i] = counter_F[i] + 1;
                            ttfF[i] = ttfF_sum[i] / counter_F[i];
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
            TTFSW_soltype.Enabled = false;
            AntSWStart.Enabled = false;
            AntSWStop.Enabled = true;
            AntResetTimeout.Enabled = false;
            AntEnableTimeout.Enabled = false;
            AntResetCommand1.Enabled = false;
            AntResetCommand2.Enabled = false;
            for (int i = 0; i < 8; i++)
            {
            TTF_Timeout1[i] = int.Parse(AntResetTimeout.Text);
            TTF_Timeout2[i] = int.Parse(AntEnableTimeout.Text);
            }

            //starting command timer
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;// Hook up the Elapsed event for the timer. 
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

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
            }


            //starting ttf timer
            ttfTimer = new System.Timers.Timer(100);
            ttfTimer.Elapsed += OnttfTimedEvent;// Hook up the Elapsed event for the timer. 
            ttfTimer.AutoReset = true;
            ttfTimer.Enabled = true;
        }

        public void RTKSWStop_Click(object sender, EventArgs e)
        {
            TTFSW_soltype.Enabled = true;
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
            for (int i = 0; i < 8; i++)
            {
            TTFSWmode[i] = 0;
            }
            
            string data = TTF_Timeout1.ToString();
            //this.BeginInvoke(new SetTextDeleg(RTKResetTimeoutChange), new object[] { data });
            data = TTF_Timeout2.ToString();
            //this.BeginInvoke(new SetTextDeleg(RTKEnableTimeoutChange), new object[] { data });
        }

        
        public void OnttfTimedEvent(Object source, ElapsedEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                ttf_counter[i]++;
            }
            
        }
        
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
                            CurrentPort[i].WriteLine(AntResetCommand1.Text);
                            //TextBox_Console.AppendText(AntResetCommand1.Text + Environment.NewLine);
                            cycle_counter[i]++;
                            TTFSWmode[i] = 2;
                            TTF_Timeout1[i] = int.Parse(AntResetTimeout.Text);

                            StatisticChange(i);
                            //sent command 1
                        }
                        break;
                    case 2:
                        if (TTF_Timeout2[i] > 0)
                        {
                            TTF_Timeout2[i]--;
                        }
                        else
                        {
                            CurrentPort[i].WriteLine(AntResetCommand2.Text);
                            //TextBox_Console.AppendText(AntResetCommand2.Text + Environment.NewLine);
                            //sent command 2
                            ttf_counter[i] = 0;
                            TTFSWmode[i] = 0;
                            TTF_Timeout2[i] = int.Parse(AntEnableTimeout.Text);
                        }
                        break;

                }
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

        void StatisticChange(int i)
        {
            dataGridView1[1, 0].Value = cycle_counter[i].ToString();
            dataGridView1[1, 1].Value = Math.Round((ttfS[i] / 10), 2).ToString();
            dataGridView1[1, 2].Value = Math.Round((ttfD[i] / 10), 2).ToString();
            dataGridView1[1, 3].Value = Math.Round((ttfF[i] / 10), 2).ToString();
            dataGridView1[1, 4].Value = Math.Round((ttfR[i] / 10), 2).ToString();
        }
    }
}