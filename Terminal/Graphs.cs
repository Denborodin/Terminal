using System;
using System.Collections.Generic;
using ZedGraph;
using System.Windows.Forms;


namespace Terminal
{
    class Graphs
    {
    }

    public partial class Form1 : Form
    {
        private void GraphInit()
        {
            GraphPane pane = zedGraphMain.GraphPane;
            pane.CurveList.Clear();
            pane.IsFontsScaled = false;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.XAxis.Title.Text = "Seconds";
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.Title.Text = "Percentile";
            pane.Title.Text = "Time To First Fix distribution";
            pane.YAxis.Scale.Min = 0;
            pane.YAxis.Scale.Max = 1;

        }
        private void ButtonPlot_Click(object sender, EventArgs e)
        {
            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
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

            // Создадим список точек
            PointPairList list = new PointPairList();

            double xmin = double.Parse(MinPercentile_textBox.Text);
            double xmax = double.Parse(MaxPercentile_textBox.Text);
            double increment = double.Parse(Increment_textBox.Text);
            //LogConsole.AppendText(DateTime.Now.ToString() + "xmin=" + xmin+ "xmax=" + xmax + Environment.NewLine);

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
            if (ttfS_.Count+ ttfD_.Count+ ttfF_.Count+ ttfR_.Count == 0)
            {
                return;
            }

            switch (CurrentMode)
            {
                case 0:
    
                    // Заполняем список точек
                    for (double x = xmax; x >= xmin; x -= increment)
                    {
                        // добавим в список точку
                        if (Percentile(ttfR_.ToArray(), Math.Round(x, 2)) != Percentile(ttfR_.ToArray(), Math.Round(x + increment, 2)))
                        {
                            list.Add(Percentile(ttfS_.ToArray(), x), x);
                        }
                    }
                    break;
                case 1:

                    // Заполняем список точек
                    for (double x = xmax; x >= xmin; x -= increment)
                    {
                        // добавим в список точку
                        if (Percentile(ttfR_.ToArray(), Math.Round(x, 2)) != Percentile(ttfR_.ToArray(), Math.Round(x + increment, 2)))
                        {
                            list.Add(Percentile(ttfD_.ToArray(), x), x);
                        }
                        LogConsole.AppendText("Graph point" + " x=" + x + " y=" + Percentile(ttfD_.ToArray(), (x)) + Environment.NewLine);
                    }
                    break;
                case 2:
                    // Заполняем список точек
                    for (double x = xmax; x >= xmin; x -= increment)
                    {
                        // добавим в список точку
                        if (Math.Round(Percentile(ttfR_.ToArray(), Math.Round(x, 2)),2)!= Math.Round(Percentile(ttfR_.ToArray(), Math.Round(x+increment, 2)),2))
                        {
                            list.Add(Math.Round(Percentile(ttfR_.ToArray(), Math.Round(x, 2)),2), Math.Round(x, 2));
                            LogConsole.AppendText("Graph point" + " x=" + Math.Round(x, 2) + " y=" + Math.Round(Percentile(ttfR_.ToArray(), Math.Round(x, 2)),2) + Environment.NewLine);
                        }
                        
                        
                    }
                    break;
                default:
                    break;
            }

            LineItem myCurve = pane.AddCurve(dataGridView1[12, i + 1].Value.ToString(), list, dataGridView1[11, i + 1].Style.BackColor, SymbolType.None);
            
            //myCurve.Line.IsSmooth = true;
            myCurve.Line.Width = 2;
            myCurve.Line.IsAntiAlias = true;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphMain.AxisChange();

            // Обновляем график
            zedGraphMain.Invalidate();
        }

    }
}
