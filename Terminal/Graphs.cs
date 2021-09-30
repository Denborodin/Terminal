using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.Windows.Forms;
using System.Drawing;

namespace Terminal
{
    class Graphs
    {
    }

    public partial class Form1 : Form
    {
        private void ButtonPlot_Click(object sender, EventArgs e)
        {
            zedGraphMain.GraphPane.CurveList.Clear();
            for (int i = 0; i < 8; i++)
                if (rcv_connected[i] == true)
                {
                    DrawGraph(i);
                }
        
        }

        private void DrawGraph(int i)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphMain.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            

            // Создадим список точек
            PointPairList list = new PointPairList();

            double xmin = 5;
            double xmax = 95;

            //вычисляем перцентили для каждого канала
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
                }
            }


            switch (CurrentMode)
            {
                case 0:
    
                    // Заполняем список точек
                    for (double x = xmin; x <= xmax; x += 5)
                    {
                        // добавим в список точку
                        list.Add(x, Percentile(ttfS_.ToArray(), x / 100));
                    }
                    break;
                case 1:

                    // Заполняем список точек
                    for (double x = xmin; x <= xmax; x += 5)
                    {
                        // добавим в список точку
                        list.Add(x, Percentile(ttfD_.ToArray(), x / 100));
                    }
                    break;
                case 2:
                    // Заполняем список точек
                    for (double x = xmin; x <= xmax; x += 5)
                    {
                        // добавим в список точку
                        list.Add(x, Percentile(ttfR_.ToArray(), x / 100));
                    }
                    break;
                default:
                    break;
            }


                    LineItem myCurve = pane.AddCurve(dataGridView1[0, i + 1].Value.ToString(), list, dataGridView1[11, i + 1].Style.BackColor, SymbolType.None);

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphMain.AxisChange();

            // Обновляем график
            zedGraphMain.Invalidate();
        }

    }
}
