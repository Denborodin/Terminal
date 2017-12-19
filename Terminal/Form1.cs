using System;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Form1 : Form
    {
        SerialPort CurrentPort;

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

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            ComPortsList1.Items.Add("OFF");
            ComPortsList2.Items.Add("OFF");
            ComPortsList3.Items.Add("OFF");
            ComPortsList4.Items.Add("OFF");
            ComPortsList5.Items.Add("OFF");
            ComPortsList6.Items.Add("OFF");
            ComPortsList7.Items.Add("OFF");
            ComPortsList8.Items.Add("OFF");


            // default speed = 115200
            PortSpeedList1.SelectedIndex = 3;
            PortSpeedList2.SelectedIndex = 3;
            PortSpeedList3.SelectedIndex = 3;
            PortSpeedList4.SelectedIndex = 3;
            PortSpeedList5.SelectedIndex = 3;
            PortSpeedList6.SelectedIndex = 3;
            PortSpeedList7.SelectedIndex = 3;
            PortSpeedList8.SelectedIndex = 3;

            // scanning available com ports
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.GetLength(0); i++)
            {
                ComPortsList1.Items.Add(ports[i]);
                ComPortsList2.Items.Add(ports[i]);
                ComPortsList3.Items.Add(ports[i]);
                ComPortsList4.Items.Add(ports[i]);
                ComPortsList5.Items.Add(ports[i]);
                ComPortsList6.Items.Add(ports[i]);
                ComPortsList7.Items.Add(ports[i]);
                ComPortsList8.Items.Add(ports[i]);
            }
            ComPortsList1.SelectedIndex = 0;
            ComPortsList2.SelectedIndex = 0;
            ComPortsList3.SelectedIndex = 0;
            ComPortsList4.SelectedIndex = 0;
            ComPortsList5.SelectedIndex = 0;
            ComPortsList6.SelectedIndex = 0;
            ComPortsList7.SelectedIndex = 0;
            ComPortsList8.SelectedIndex = 0;
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

        void button1_Click(object sender, EventArgs e)
        {
            Object selectedport = ComPortsList1.SelectedItem;
            CurrentPort = new SerialPort(selectedport.ToString(), Int32.Parse(PortSpeedList1.SelectedItem.ToString()));
            try
            {
                if (!(CurrentPort.IsOpen))
                {
                CurrentPort.Open();
                ButtonOpen.Enabled = false;
                ButtonClose.Enabled = true;
                AntSWStart.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);         
            }
  
            CurrentPort.DataReceived += CurrentPort_DataReceived;
        }

        void CurrentPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            try
            {
                string data = port.ReadLine();
                this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data }); 
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

        void si_DataReceived(string data) 
        {
            TextBox_Console.AppendText(data.Trim() + Environment.NewLine);
            SolutionLabel.Text = SolType(data);
        }  
        
        void ButtonClose_Click(object sender, EventArgs e)
        {
            CurrentPort.Close();
            ButtonOpen.Enabled = true;
            ButtonClose.Enabled = false;
            SolutionLabel.Text = "N/A";
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
                        if (SolutionLabel.Text == "None Solution")
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
                        if (SolutionLabel.Text == "Standalone" | SolutionLabel.Text == "None Solution")
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
                        if (SolutionLabel.Text == "Standalone" | SolutionLabel.Text == "None Solution" | 
                                        SolutionLabel.Text == "RTK Float" |  SolutionLabel.Text == "DGNSS")
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

                        if (SolutionLabel.Text == "Standalone" | SolutionLabel.Text == "None Solution" | SolutionLabel.Text == "DGNSS")
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
                            CurrentPort.WriteLine(AntResetCommand1.Text);
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
                            CurrentPort.WriteLine(AntResetCommand2.Text);
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
