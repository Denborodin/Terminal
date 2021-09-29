using System;
using System.Management;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Terminal
{



    public partial class Form1 : Form
    {
        public bool SatelRawData = false;
        public int CurrentFrequency = 0;
        public int mTime = 0;
        public int mModeSwitch = 0; //test phase switch, 0 - pause, 1 - transmit
        public int LinkRate;
        System.Timers.Timer mTimer;
        public string tr_data; // 100 bytes
        public string ModemType; 
        public string ModemPort;
        async void ModemConnectBTN_Click(object sender, EventArgs e)
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

            CurrentPort[9].DataReceived += CurrentPort_ModemDataReceived;

            if (ModemPortComboBox.Text != "Direct")
            {
                ReceiverConnect(9);
            }
            await Task.Delay(1000);
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
            CurrentPort[9].Close();
        }


        async public void ModemConnect()
        {
            if (ModemPortComboBox.Text != "Direct")
            {
                CurrentPort[9].WriteLine("dm,dev/modem/a");
                System.Threading.Thread.Sleep(25);
                DChain();
                await Task.Delay(300);
            }
                switch (ModemType)
            {
                case "Satel":
                    //modem info request:

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem model: " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%D?");
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem HW: " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%H?");
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem serial: " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%S?");
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Modem FW version: " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%V?");
                    SatelRawData = true;
                    await Task.Delay(50);

                    //Configuring:

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting spacing: 12.5 KHz " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL&W=1250");
                    SatelRawData = true;
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting radio protocol: Satel " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL@S=0");
                    SatelRawData = true;
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting FEC: ON " + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%F=1");
                    SatelRawData = true;
                    await Task.Delay(50);

                    ModemLog.AppendText(DateTime.Now.ToString() + " " + "Setting Transmit power: " + PowerTextBox.Text + Environment.NewLine);
                    CurrentPort[9].WriteLine("SL%F="+ PowerTextBox.Text);
                    SatelRawData = true;
                    await Task.Delay(50);

                    break;
                default:
                    break;
            }
            if (ModemPortComboBox.Text != "Direct")
            {
                DChainOff();
                await Task.Delay(300);
            }
            ModemStartBtn.Enabled = true;
        }

        
        private void ModemStartBtn_Click(object sender, EventArgs e)
        {
            ModemStartBtn.Enabled = false;
            ModemDisconnectBTN.Enabled = false;
            ModemConnectBTN.Enabled = false;
            ModemStopBtn.Enabled = true;
            CurrentFrequency = Convert.ToInt32(FrqStartTxt.Text);
            //start log
            string str = DateTime.Now.Hour +"."+ DateTime.Now.Minute;
            CurrentPort[9].WriteLine("create,Modem_test_"+ str);
            System.Threading.Thread.Sleep(25);
            CurrentPort[9].WriteLine("em,/cur/file/a,def");
            System.Threading.Thread.Sleep(25);
            ModemLogUpdate("Receiver log started, filename: Modem_test_" + str);
            //starting command timer
            mTimer = new System.Timers.Timer(1000);
            mTimer.Elapsed += mOnTimedEvent;// Hook up the Elapsed event for the timer. 
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTime = Convert.ToInt32(PauseTimeTxt.Text);
            mModeSwitch = 0;
        }

        private void ModemStopBtn_Click(object sender, EventArgs e)
        {
            mTimer.Enabled = false;
            ModemStartBtn.Enabled = true;
            ModemDisconnectBTN.Enabled = true;
            ModemConnectBTN.Enabled = false;
            ModemStopBtn.Enabled = false;
            //Stop log
            CurrentPort[9].WriteLine("dm,/cur/file/a");

        }

        public void mOnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (mTime > 0)
            {
                mTime--;
                if (mModeSwitch == 1)
                {
                    TransmitData();
                }
            }
            else
            {
                switch (mModeSwitch)
                {
                    case 0: //Transmition start
                        CurrentPort[9].WriteLine("event,\"_SIT="+CurrentFrequency.ToString()+"\"");
                        DChain();
                        System.Threading.Thread.Sleep(100);
                        string data = "SL&F=";
                        SatelRawData = true;
                        data = data + CurrentFrequency.ToString("F4");
                        this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] {  data });
                        CurrentPort[9].WriteLine(data);
                        System.Threading.Thread.Sleep(50);
                        mModeSwitch = 1;
                        mTime = Convert.ToInt32(TransmitTimeTxt.Text);
                        break;
                    case 1: //Transmition end
                        DChainOff();
                        System.Threading.Thread.Sleep(100);
                        if (CurrentFrequency < Convert.ToInt32(FrqStopTxt.Text))
                        {
                            CurrentFrequency = CurrentFrequency + Convert.ToInt32(FrqStepTxt.Text);
                        }
                        else
                        {
                            this.BeginInvoke(new MyDelegate(TestCompleted));
                        }
                        mTime = Convert.ToInt32(PauseTimeTxt.Text);
                        mModeSwitch = 0;
                        break;
                }
            }
        }

        public void TransmitData()
        {
            for (int i = 0; i < LinkRate/8/100; i++)
            {
                CurrentPort[9].WriteLine(tr_data);
            }
        }

        public void DChain()
        {
             switch (ModemPort)
            {
                case "Modem A":
                    this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Setting daisy chain to Modem A"});
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/echo,/dev/ser/a");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/imode,echo");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/echo,/dev/modem/a");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/imode,echo");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].NewLine= "\r";
                    break;

                default:
                    break;
            }
        }

        public void DChainOff()
        {
            string data1 = null;
            switch (ModemPort)
            {
                case "Modem A":
                    this.BeginInvoke(new MLogUpdate(ModemLogUpdate), new object[] { "Removing daisy chain to Modem A" });
                    for (int j = 0; j < 128; j++)
                    {
                        data1 = data1 + Convert.ToChar(0x01);
                    }

                    data1 = data1 + Convert.ToChar(0x0d) + Convert.ToChar(0x0a);

                    CurrentPort[9].WriteLine(data1);

                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/echo,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/dup,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/modem/a/imode,none");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/echo,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/dup,/dev/null");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].WriteLine("set,/par/dev/ser/a/imode,cmd");
                    System.Threading.Thread.Sleep(25);
                    CurrentPort[9].NewLine = Environment.NewLine;
                    //CurrentPort[9].WriteLine("print,/par/rcv/model");
                    //System.Threading.Thread.Sleep(25);
                    break;

                default:
                    break;
            }
        }
        
        void CurrentPort_ModemDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var port = (SerialPort)sender;
            string data = null;
            try
            {
                if (SatelRawData)
                {
                    data = port.ReadExisting();
                    SatelRawData = false;
                }
                else
                {
                    data = port.ReadLine();
                }
                
                this.BeginInvoke(new SetTextDeleg(ModemDataReceived), new object[] { data , 9 });
            }
            catch (Exception ex)
            {
                this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " Error: " + ex.Message + Environment.NewLine });
            }
        }

        void ModemDataReceived (string data, int i)
        {
            ModemLog.AppendText(DateTime.Now.ToString() +"  "+ data + Environment.NewLine);
        }

        void ModemLogUpdate(string str)
        {
            ModemLog.AppendText(DateTime.Now.ToString() + " " + str + Environment.NewLine);
        }

        void TestCompleted()
        {
            ModemStopBtn.PerformClick();
        }

    }
}