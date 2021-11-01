using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Terminal
{

    public partial class Form1 : Form
    {
        public bool TestRun = false;
        public int CurrentFrequency = 0;
        public int mTime = 0;
        public int mModeSwitch = 0; //test phase switch, 0 - pause, 1 - transmit
        public int LinkRate;
        public float timeout = 500;
        public string SatelResponse = null;
        System.Timers.Timer mTimer;
        public string tr_data; // 100 bytes
        public string ModemType;
        public string ModemPort;
        public string ModemFilename;
        private void ModemConnectBTN_Click(object sender, EventArgs e)
        {

            Object selectedport = ComPortList[9].SelectedItem;
            CurrentPort[9] = new SerialPort(selectedport.ToString(), 115200);
            CurrentPort[9].RtsEnable = false;
            CurrentPort[9].DtrEnable = false;
            ModemType = ModemTypeList.Text;
            ModemPort = ModemPortComboBox.Text;
            LinkRate = Convert.ToInt32(LinkRateList.Text);

            switch (LinkRate)
            {
                case 4800:
                    LinkRate = 2300;
                    break;

                case 9600:
                    LinkRate = 4600;
                    break;

                case 19200:
                    LinkRate = 9200;
                    break;
                default:
                    break;
            }

            try
            {
                if (!(CurrentPort[9].IsOpen))
                {
                    CurrentPort[9].Open();
                    ModemDisconnectBTN.Enabled = true;
                    ModemConnectBTN.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //CurrentPort[9].DataReceived += CurrentPort_ModemDataReceived;


            if (ModemPortComboBox.Text != "Direct")
            {
                ReceiverConnect(9);
            }
            ModemConnect();

            for (int j = 0; j < 100; j++)
            {
                tr_data = tr_data + "1";
            }
        }

        private void ModemDisconnectBTN_Click(object sender, EventArgs e)
        {
            ModemDisconnectBTN.Enabled = false;
            ModemConnectBTN.Enabled = true;
            ModemStartBtn.Enabled = false;
            ModemStopBtn.Enabled = false;
            SatelSettingsGet.Enabled = false;
            SatelSettingsSet.Enabled = false;
            CurrentPort[9].Close();
        }

        public string SendData(string data)
        {
            try
            {
                CurrentPort[9].WriteLine(data);
            }
            catch (Exception ex)
            {
                ModemLogUpdate("Communications error: " + ex);
                return null;
            }

            int time = 0;
            while ((CurrentPort[9].BytesToRead <= 0) && (time <= timeout))
            {
                Thread.Sleep(10);
                time = time + 10;
            }
            if (CurrentPort[9].BytesToRead <= 0)
            {
                ModemLogUpdate("Communication timout, no response for command: " + data);
            }
            else
            {
                try
                {
                    string returnedMesssage = CurrentPort[9].ReadExisting();
                    return returnedMesssage;
                }
                catch (TimeoutException ex)
                {
                    ModemLogUpdate("Communications error: " + ex);
                    return null;
                }
            }
            return null;
        }

        public void ModemConnect()
        {
            if (ModemPortComboBox.Text != "Direct")
            {
                SendData("dm,dev/modem/a");
                CurrentPort[9].DiscardInBuffer();
                SendData("dm,dev/ser/a");
                CurrentPort[9].DiscardInBuffer();
                DChain();
            }
                switch (ModemType)
            {
                case "Satel":
                    //modem info request:
                    for (int i = 0; i < 3; i++)
                    {
                        string response = SendData("SL%D?");
                        if (response != null)
                        {
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem model: " + response.Trim() + Environment.NewLine);
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        string response = SendData("SL%H?");
                        if (response != null)
                        {
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem HW: " + response.Trim() + Environment.NewLine);
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        string response = SendData("SL%S?");
                        if (response != null)
                        {
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem serial: " + response.Trim() + Environment.NewLine);
                            break;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        string response = SendData("SL%V?");
                        if (response != null)
                        {
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem FW version: " + response.Trim() + Environment.NewLine);
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
            if (ModemPortComboBox.Text != "Direct")
            {
                DChainOff();
            }
            ModemStartBtn.Enabled = true;
            SatelSettingsGet.Enabled = true;
            SatelSettingsSet.Enabled = true;
        }

        private void ModemStartBtn_Click(object sender, EventArgs e)
        {
            ModemStartBtn.Enabled = false;
            ModemDisconnectBTN.Enabled = false;
            ModemConnectBTN.Enabled = false;
            ModemStopBtn.Enabled = true;
            string response = null;
            Satel_SetSettings();
            CurrentFrequency = Convert.ToInt32(FrqStartTxt.Text);
            //start log
            ModemFilename = "Modem_test_" + DateTime.Now.Hour + "." + DateTime.Now.Minute+"."+ DateTime.Now.Second;

            for (int i = 0; i < 3; i++)
            {
                response = SendData("%%create," + ModemFilename + ".tps");
                if (response != null)
                {
                    break;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                response = SendData("%%em,/cur/file/a,def");
                if (response != null)
                {
                    break;
                }
            }
            System.Threading.Thread.Sleep(25);
            ModemLogUpdate("Receiver log started, filename: " + ModemFilename + " Receiver reply: " + response);
            //starting command timer
            mModeSwitch = 0;
            mTimer = new System.Timers.Timer(1000);
            mTimer.Elapsed += new ElapsedEventHandler(mOnTimedEvent);// Hook up the Elapsed event for the timer. 
            mTimer.AutoReset = true;
            mTime = Convert.ToInt32(PauseTimeTxt.Text);
            mTimer.Start();
            //ModemLog.AppendText("mTimer enabled: " + mTimer.Enabled.ToString() + "mTimer interval: " + mTimer.Interval + Environment.NewLine);
        }

        private void SatelSettingsSet_Click(object sender, EventArgs e)
        {
            Satel_SetSettings();

            Satel_GetSettings();
        }

        private void Satel_SetSettings()
        {
            string command = null;

            if (ModemPortComboBox.Text != "Direct")
            {
                SendData("%%dm,dev/modem/a");
                CurrentPort[9].DiscardInBuffer();
                DChain();
            }
            //Configuring:

            //Setting Spacing
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting spacing: " + SatelSpacingCombobox.Text + Environment.NewLine);
            for (int i = 0; i < 3; i++)
            {
                string response = SendData("SL%D?");
                if (response != null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem model: " + response.Trim() + Environment.NewLine);
                    break;
                }
            }

            switch (SatelSpacingCombobox.SelectedIndex)
            {
                case 0:
                    command = "SL&W=1250";
                    break;
                case 1:
                    command = "SL&W=2500";
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                string response = SendData(command);
                if (response != null && response.Trim() == "OK")
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Spacing set succesfully: " + response.Trim() + Environment.NewLine);
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Spacing setting failed, retrying: " + response + Environment.NewLine);
            }


            //Setting protocol
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting radio protocol: " + SatelModeCombobox.Text + Environment.NewLine);
            switch (SatelModeCombobox.SelectedIndex)
            {
                case 0:
                    command = "SL@S=0";
                    break;
                case 1:
                    command = "SL@S=1";
                    break;
                case 2:
                    command = "SL@S=2";
                    break;
                case 3:
                    command = "SL@S=3";
                    break;
                case 4:
                    command = "SL@S=4";
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                string response = SendData(command);
                if (response != null && response.Trim() == "OK")
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Radio protocol set succesfully: " + response.Trim() + Environment.NewLine);
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Radio protocol setting failed, retrying: " + response + Environment.NewLine);
            }


            //Setting FEC
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting FEC: " + SatelFECComboBox.Text + Environment.NewLine);
            switch (SatelFECComboBox.SelectedIndex)
            {
                case 0:
                    command = "SL%F=1";
                    break;
                case 1:
                    command = "SL%F=0";
                    break;
                default:
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                string response = SendData(command);
                if (response != null && response.Trim() == "OK")
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC set succesfully: " + response.Trim() + Environment.NewLine);
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC setting failed, retrying: " + response + Environment.NewLine);
            }


            //Setting Power
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting Transmit power: " + SatelPowercomboBox1.Text + Environment.NewLine);
            command = "SL@P=" + SatelPowercomboBox1.Text;

            for (int i = 0; i < 3; i++)
            {
                string response = SendData(command);
                if (response != null && response.Trim() == "OK")
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power set succesfully: " + response.Trim() + Environment.NewLine);
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power setting failed, retrying: " + response + Environment.NewLine);
            }


            //Setting Frequency
            command = "SL&F=";
            command = command + SatelFreqTxtbox.Text;
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting Frequency: " + SatelFreqTxtbox.Text + Environment.NewLine);

            for (int i = 0; i < 3; i++)
            {
                string response = SendData(command);
                if (response != null && response.Trim() == "OK")
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Frequency set succesfully: " + response.Trim() + Environment.NewLine);
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Frequency setting failed, retrying: " + response + Environment.NewLine);
            }


            if (ModemPortComboBox.Text != "Direct")
            {
                DChainOff();
            }
        }

        private void Satel_GetSettings()
        {
            if (ModemPortComboBox.Text != "Direct")
            {
                SendData("dm,dev/modem/a");
                CurrentPort[9].DiscardInBuffer();
                DChain();
            }
            
            string response = null;


            //Quering spacing
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering spacing " + Environment.NewLine);

            for (int i = 0; i < 3; i++)
            {
                response = SendData("SL&W?");
                if (response!=null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering spacing succesfully: " + response.Trim() + Environment.NewLine);
                    switch (response.Trim())
                    {
                        case "12.5 kHz":
                            SatelSpacingCombobox.SelectedIndex = 0;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Spacing is set to " + response + Environment.NewLine);
                            break;
                        case "25.0 kHz":
                            SatelSpacingCombobox.SelectedIndex = 1;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC is set to " + response + Environment.NewLine);
                            break;
                        default:
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC response not recognized" + response + Environment.NewLine);
                            break;
                    }
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering spacing failed, retrying: " + response + Environment.NewLine);
            }



            //Quering Frequency
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Frequency " + Environment.NewLine);
            for (int i = 0; i < 3; i++)
            {
                response = SendData("SL&F?");
                if (response!=null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Frequency succesfully: " + response.Trim() + Environment.NewLine);
                    string[] parts = response.Trim().Split(' ');
                    if (double.TryParse(parts[1], out double freq))
                    {
                        SatelFreqTxtbox.Text = freq.ToString("F4");
                        ModemLog.AppendText(DateTime.Now.ToString() + " " + "Frequency is set to: " + parts[1] + Environment.NewLine);
                    }
                    else
                    {
                        ModemLog.AppendText(DateTime.Now.ToString() + " " + "Frequency response not recognized: " + parts[1] + Environment.NewLine);
                    }
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Frequency failed, retrying: " + response + Environment.NewLine);
            }




            //Quering Protocol
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Protocol " + Environment.NewLine);
            for (int i = 0; i < 3; i++)
            {
                response = SendData("SL@S?");
                if (response!=null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Protocol succesfully: " + response.Trim() + Environment.NewLine);
                    switch (response)
                    {
                        case "0":
                            SatelModeCombobox.SelectedIndex = 0;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol is set to Satel3AS" + Environment.NewLine);
                            break;

                        case "1":
                            SatelModeCombobox.SelectedIndex = 1;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol is set to PacCrest 4FSK" + Environment.NewLine);
                            break;

                        case "2":
                            SatelModeCombobox.SelectedIndex = 2;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol is set to PacCrest GMSK" + Environment.NewLine);
                            break;

                        case "3":
                            SatelModeCombobox.SelectedIndex = 3;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol is set to TrimTalk" + Environment.NewLine);
                            break;

                        case "4":
                            SatelModeCombobox.SelectedIndex = 4;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol is set to TrimTalk Trimble" + Environment.NewLine);
                            break;

                        default:
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Protocol response not recognized" + response + Environment.NewLine);
                            break;
                    }
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Protocol failed, retrying: " + response + Environment.NewLine);
            }



            //Quering Power
            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Power " + Environment.NewLine);

            for (int i = 0; i < 3; i++)
            {
                response = SendData("SL@P?");
                if (response!=null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Power succesfully: " + response.Trim() + Environment.NewLine);
                switch (response.Trim())
                    {
                        case "10 mW":
                            SatelPowercomboBox1.SelectedIndex = 0;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "20 mW":
                            SatelPowercomboBox1.SelectedIndex = 1;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "50 mW":
                            SatelPowercomboBox1.SelectedIndex = 2;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "100 mW":
                            SatelPowercomboBox1.SelectedIndex = 3;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "200 mW":
                            SatelPowercomboBox1.SelectedIndex = 4;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "500 mW":
                            SatelPowercomboBox1.SelectedIndex = 5;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        case "1000 mW":
                            SatelPowercomboBox1.SelectedIndex = 6;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power is set to " + response + Environment.NewLine);
                            break;
                        default:
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "Power response not recognized: " + response + Environment.NewLine);
                            break;
                    }
                    
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Power failed, retrying: " + response + Environment.NewLine);
            }




            //Quering FEC
            for (int i = 0; i < 3; i++)
            {
                response = SendData("SL%F?");
                if (response!=null)
                {
                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Power succesfully: " + response.Trim() + Environment.NewLine);
                    switch (response.Trim())
                    {
                        case "0":
                            SatelFECComboBox.SelectedIndex = 1;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC is set to OFF" + Environment.NewLine);
                            break;
                        case "1":
                            SatelFECComboBox.SelectedIndex = 0;
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC is set to ON" + Environment.NewLine);
                            break;
                        default:
                            ModemLog.AppendText(DateTime.Now.ToString() + " " + "FEC response not recognized" + response + Environment.NewLine);
                            break;
                    }
                    break;
                }
                ModemLog.AppendText(DateTime.Now.ToString() + " " + "Quering Power failed, retrying: " + response + Environment.NewLine);
            }



            if (ModemPortComboBox.Text != "Direct") DChainOff();

        }

        private void SatelSettingsGet_Click(object sender, EventArgs e)
        {
            Satel_GetSettings();
        }

        private void ModemStopBtn_Click(object sender, EventArgs e)
        {
            mTimer.Enabled = false;
            ModemStartBtn.Enabled = true;
            ModemDisconnectBTN.Enabled = true;
            ModemConnectBTN.Enabled = false;
            ModemStopBtn.Enabled = false;
            //Stop log
            DChainOff();
            Thread.Sleep(25);
            string response = null;
            for (int i = 0; i < 3; i++)
            {
                response = SendData("%%dm,/cur/file/a");
                if (response != null)
                {
                    break;
                }
            }
            System.Threading.Thread.Sleep(25);
            ModemLogUpdate("Receiver log stopped, " +" Receiver reply: " + response);
            ModemLogSave();
            ModemLog.Text = "";

        }

        public void ModemTestRUN()
        {
            string response = null;
            //ModemLog.AppendText(DateTime.Now.ToString() + " " + "ModemTestRUN " + Environment.NewLine);
            mTimer.Stop();
            switch (mModeSwitch)
                {
                    case 0: //Transmition start

                        DChain();
                        Thread.Sleep(25);
                        string data = "SL&F=";
                        data = data + CurrentFrequency.ToString("F4");
                        this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { data });
                        for (int i = 0; i < 3; i++)
                        {
                            response = SendData(data);
                            if (response != null)
                            {
                                break;
                            }
                            this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Setting Frequency failed, retrying: " + response });
                        }

                        DChainOff();
                    Thread.Sleep(25);
                    this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "response: "+ response });
                    if (response.Trim() == "OK")
                        {
                        CurrentPort[9].WriteLine("%%event,\"_SIT=" + CurrentFrequency.ToString() + "\"");
                        CurrentPort[9].WriteLine("%%event,\"_DYM=DYNAMIC\"");
                        Thread.Sleep(25);
                        CurrentPort[9].DiscardInBuffer();
                        this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Frequency set OK" });
                            mModeSwitch = 1;
                            this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Transmission started" });
                            mTime = Convert.ToInt32(TransmitTimeTxt.Text);
                            DChain();
                            break;
                        }
                        else if (response.Trim() == "ERROR")
                        {
                            CurrentPort[9].WriteLine("%%event,\"_SIT=" + CurrentFrequency.ToString() + " TX OFF" + "\"");
                            this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Frequency cannot be set" });
                            mModeSwitch = 2;
                            mTime = Convert.ToInt32(TransmitTimeTxt.Text);
                            DChain();
                            break;
                        }
                        break;
                    case 1: //Transmition end
                    DChainOff();
                    Thread.Sleep(25);
                    CurrentPort[9].WriteLine("%%event,\"_DYM=STATIC\"");
                    Thread.Sleep(25);
                    CurrentPort[9].DiscardInBuffer();
                    if (CurrentFrequency < Convert.ToInt32(FrqStopTxt.Text))
                        {
                            CurrentFrequency = CurrentFrequency + Convert.ToInt32(FrqStepTxt.Text);
                            this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Transmission stopped" });
                        }
                        else
                        {
                            this.BeginInvoke(new MyDelegate(TestCompleted));
                        }
                        mTime = Convert.ToInt32(PauseTimeTxt.Text);
                        mModeSwitch = 0;
                        break;
                    case 2: //Waiting with TX OFF
                        {
                        if (mTime > 0)
                        {
                            mTime--;
                            break;
                        }
                        else
                        {
                            mModeSwitch = 1;
                            mTime = Convert.ToInt32(TransmitTimeTxt.Text);
                            DChain();
                            break;
                        }

                        }
                default:
                            break;
                }
            mTimer.Start();

        }


        public void mOnTimedEvent(Object source, ElapsedEventArgs e)
        {

            //this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "mTime: " + mTime + "   mModeSwitch: " + mModeSwitch });
            if (mTime > 0)
            {
                mTime--;
                if (mModeSwitch == 1)
                {
                    this.BeginInvoke(new MyDelegate(TransmitData));
                }
            }
            else
            {
                mTimer.Stop();
                this.BeginInvoke(new MyDelegate(ModemTestRUN));
            }

        }

        void TransmitData()
        {
            //this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Data Transmit started" });
            for (int i = 0; i < LinkRate /8; i++)
            {
                CurrentPort[9].Write("1");
            }
            //this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Data Transmit completed" });
        }

        public int DChain()
        {
             switch (ModemPort)
            {
                case "Modem A":
                    ModemLogUpdate("Setting daisy chain to Modem A");
                    //this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Setting daisy chain to Modem A"});
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/echo,/dev/ser/a");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/imode,echo");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/echo,/dev/modem/a");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/imode,echo");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].NewLine= "\r";
                    CurrentPort[9].DiscardInBuffer();
                    break;

                default:
                    break;
            }
            return 0;
        }

        public int DChainOff()
        {
            string data1 = null;
            switch (ModemPort)
            {
                case "Modem A":
                    ModemLogUpdate("Removing daisy chain to Modem A");
                    for (int j = 0; j < 128; j++)
                    {
                        data1 = data1 + Convert.ToChar(0x01);
                    }

                    data1 = data1 + Convert.ToChar(0x0d) + Convert.ToChar(0x0a);
                    CurrentPort[9].WriteLine(data1);
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("#OFF#");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/echo,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/dup,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    //CurrentPort[9].WriteLine("set,/par/dev/modem/a/imode,rtcm3");
                    //System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/echo,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/dup,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/imode,cmd");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].NewLine = Environment.NewLine;
                    break;
                default:
                    break;
            }
            return 0;
        }
        

        void ModemLogUpdate(string str)
        {
            ModemLog.AppendText(DateTime.Now.ToString() + " " + str + Environment.NewLine);
        }

        void ModemLogSave()
        {
            ModemFilename = ModemFilename + ".log";
            filename = @System.IO.Path.Combine(FilepathtextBox3.Text.ToString(), ModemFilename);

            StreamWriter swOut = new StreamWriter(ModemFilename);

            foreach (var line in ModemLog.Lines)
            {
                swOut.WriteLine(line);
            }

            swOut.Close();
        }

        void TestCompleted()
        {
            ModemStopBtn.PerformClick();
            ModemLogUpdate("Modem Test Completed");
        }

    }
}