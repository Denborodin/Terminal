using System;
using System.Windows.Forms;


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

            CurrentPort[i].WriteLine("em,,nmea/GGA:0.1");

            return;
        }
        
        private void ReceiverReplyParse(string data, int index)
        {
            switch (Int32.Parse(data[6].ToString()))
            {
                case 0: //receiver name
                    LogConsole.AppendText(DateTime.Now.ToString() +" Channel " + index + " Receiver name: " + data.Substring(8) + Environment.NewLine);
                    receiver_model[index] = data.Substring(8);
                    break;
                case 1: //FW ver
                    LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " FW version: " + data.Substring(8) + Environment.NewLine);
                    receiver_FW[index] = data.Substring(8);
                    break;
                default:
                    LogConsole.AppendText(DateTime.Now.ToString() + " Channel " + index + " " + data + Environment.NewLine);
                    break;
            }
        }
    }
}
