using System;
using System.Management;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Terminal
{
    public partial class Form1 : Form
    {

        private void ReceiverConnect(int i)
        {
            string data1 = null;

            System.Threading.Thread.Sleep(50);

            for (int j = 0; j < 128; j++)
            {
                data1 = data1 + Convert.ToChar(0x01);
            }

            data1 = data1 + Convert.ToChar(0x0d) + Convert.ToChar(0x0a);

            CurrentPort[i].WriteLine(data1);

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine(data1);

            data1 = null;

            for (int j = 0; j < 129; j++)
            {
                data1 = data1 + Convert.ToChar(0x02);
            }

            CurrentPort[i].WriteLine(data1);

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine(data1);

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine("dm");

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine("%0%print,/par/rcv/model");

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine("%1%print,/par/rcv/ver/main");

            System.Threading.Thread.Sleep(25);

            CurrentPort[i].WriteLine("%2%print,/par/rcv/id");

            if (i != 8)
            {
                CurrentPort[i].WriteLine("em,,nmea/GGA:0.1");
            }

            
            return;
        }
        
        private void ReceiverReplyParse(string data, int index)
        {
            char[] charsToTrim = { '\r', '\n'};
            try
            {
                switch (Int32.Parse(data[6].ToString()))
                {
                    case 0: //receiver name
                        LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " Receiver name: " + data.Substring(8) + Environment.NewLine);
                        receiver_model[index] = data.Substring(8).TrimEnd(charsToTrim);
                        break;
                    case 1: //FW ver
                        LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " FW version: " + data.Substring(8) + Environment.NewLine);
                        receiver_FW[index] = data.Substring(8).TrimEnd(charsToTrim);
                        break;

                    case 2: //Receiver ID
                        LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " Receiver ID: " + data.Substring(8) + Environment.NewLine);
                        receiver_ID[index] = data.Substring(8).TrimEnd(charsToTrim);
                        break;

                    default:
                        LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " " + data + Environment.NewLine);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + ex.Message + Environment.NewLine);
            }
        }

        private void SendCommand1(int index)
        {
            if (CurrentCommandLine[index] < Command1TextBox.Lines.Length)
            {
                for (int j = CurrentCommandLine[index]; j < Command1TextBox.Lines.Length; j++)
                {
                    if (Command1TextBox.Lines[j].StartsWith("@sleep"))
                    {
                        sleep_timer[index] = Int32.Parse(Command1TextBox.Lines[j].Substring(7));
                        CurrentCommandLine[index]++;
                        TTFSWmode[index] = 3;
                        return;
                        //LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " Sleep timer started" + sleep_timer[index] + Environment.NewLine);
                    }
                    else
                    {
                        CurrentPort[index].WriteLine(Command1TextBox.Lines[j]);
                        this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " Command sent: " + Command1TextBox.Lines[j], index });
                        System.Threading.Thread.Sleep(3);
                        CurrentCommandLine[index]++;
                        if (CurrentCommandLine[index] == (Command1TextBox.Lines.Length-1)) CurrentCommandLine[index] = 0;
                    }
                
                }
                return;
            }
            else
            {
                CurrentCommandLine[index] = 0;
                return;
            }
        }

        private async void Command1Switch(int index)
        {
            if (!SyncCheckBox1.Checked)
            {
                SendCommand1(index);
                TTFSWmode[index] = 2;
                TTF_Timeout1[index] = int.Parse(Timeout1TextBox.Text);
                if (cycle_counter[index] != 0)
                {
                    AddCycleResult(index);
                    StatisticChange(index);
                }
                ttfS[index] = 0; ttfD[index] = 0; ttfF[index] = 0; ttfR[index] = 0;
                if (Min_nonzero(cycle_counter) >= int.Parse(NumberOfCyclesTextBox.Text))
                {
                    this.BeginInvoke(new MyDelegate(TimedStop));
                }
            }
            else
            {
                    int connected_receivers = 0;
                    int ready_receivers = 0;
                    for (int i = 0; i < 8; i++)
                    {
                    if (rcv_connected[i]) connected_receivers++;
                    if (RcvGotSol[i]) ready_receivers++;

                }
                this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " RcvConnected " + connected_receivers, index });
                this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " ReadyReceivers " + connected_receivers, index });
                if (connected_receivers == ready_receivers && ready_receivers!=0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (rcv_connected[i])
                        {
                            RcvGotSol[i] = false;
                            TTFSWmode[i] = 2;
                            TTF_Timeout1[i] = int.Parse(Timeout1TextBox.Text);
                            await Task.Run(() => SendCommand1(i));
                            if (cycle_counter[i] != 0)
                            {
                                AddCycleResult(i);
                                StatisticChange(i);
                            }
                            ttfS[i] = 0; ttfD[i] = 0; ttfF[i] = 0; ttfR[i] = 0;
                            if (Min_nonzero(cycle_counter) >= int.Parse(NumberOfCyclesTextBox.Text))
                            {
                                this.BeginInvoke(new MyDelegate(TimedStop));
                            }
                        }
                    }
                }
                else
                {
                    RcvGotSol[index] = true;
                    string str = "Waiting for other receivers";
                    try
                    {
                        this.BeginInvoke(new StatusUpdate(STUpdate), new object[] { str, index });
                    }
                    finally { }
                    this.BeginInvoke(new StatusUpdate(LogUpdate), new object[] { " Receiver waiting for other " + (connected_receivers-ready_receivers), index });
                }
            }
        }

        private void SendCommand2(int index)
        {
            for (int CurrentCommand = 0; CurrentCommand < Command2TextBox.Lines.Length; CurrentCommand++)
            {
                CurrentPort[index].WriteLine(Command2TextBox.Lines[CurrentCommand]);
                System.Threading.Thread.Sleep(25);
            }
        }

        private void ComListing()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Caption like '%(COM%'"))
                {
                    var portnames = SerialPort.GetPortNames();
                    var ports = searcher.Get().Cast<ManagementBaseObject>().ToList().Select(p => p["Caption"].ToString());

                    var portList = portnames.Select(n => n + " - " + ports.FirstOrDefault(s => s.Contains(n+")"))).ToList();

                    foreach (string s in portList)
                    {
                        LogConsole.AppendText(s + Environment.NewLine);

                        //LogUpdate(s, 10);
                    }
                    /*
                    foreach (string s in portList)
                    { 
                        LogConsole.AppendText(s.Substring(s.IndexOf("(")+1, s.IndexOf(")") - s.IndexOf("(")-1) + Environment.NewLine);
                        //LogUpdate(s, 10);
                    }
                    */
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
