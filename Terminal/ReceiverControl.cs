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

            System.Threading.Thread.Sleep(50);

            CurrentPort[i].WriteLine(data1);

            data1 = null;

            for (int j = 0; j < 129; j++)
            {
                data1 = data1 + Convert.ToChar(0x02);
            }

            CurrentPort[i].WriteLine(data1);

            System.Threading.Thread.Sleep(50);

            CurrentPort[i].WriteLine(data1);

            System.Threading.Thread.Sleep(50);

            CurrentPort[i].WriteLine("dm");

            System.Threading.Thread.Sleep(50);

            CurrentPort[i].WriteLine("em,,nmea/GGA:0.1");

            return;
        }

    }
}
