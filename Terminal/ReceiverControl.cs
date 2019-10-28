using System;
using System.Management;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Linq;

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

            if (i != 9)
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
