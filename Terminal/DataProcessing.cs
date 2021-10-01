using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace Terminal
{
    public partial class Form1 : Form
    {
        public void WriteCSV(DataGridView gridIn, string outputFile)
        {
            //test to see if the DataGridView has any rows
            if (gridIn.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile);           
                for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = gridIn.Rows[j];
                    if (dr.Cells[0].Value == null)
                    {
                        continue;
                    }

                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {

                        if (i > 0)
                        {
                            swOut.Write(";");
                        }

                        if (dr.Cells[i].Value == null)
                        {
                            value = "";
                        }
                        else
                        {
                            value = dr.Cells[i].Value.ToString();
                        }


                        //replace comma's with spaces
                        value = value.Replace(',', ' ');
                        //replace embedded newlines with spaces
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                dr.Dispose();
                swOut.Close();
            }
        }

        public void WriteCycles()
        {
            for (int i = 0; i < 8; i++)
            {
                if (rcv_connected[i] == true)
                {

                    try
                    {
                        string filename = "Cycles" + " " + ComPortList[i].Text + " " + receiver_model[i] + " " + receiver_ID[i].Substring(7) + " " + DateTime.Now.ToString("MM-dd HH.mm") + ".csv";
                        filename = @System.IO.Path.Combine(Application.StartupPath.ToString(), filename);

                        StreamWriter sw_cycles = new StreamWriter(filename);
                        sw_cycles.WriteLine("Cycles Number" + ";" + "ttfS" + ";" + "ttfD" + ";" + "ttfF" + ";" + "ttfR" + ";" + "Start" + ";" + "End");
                        foreach (TTFcycle TTFtemp in Cycles)
                        {
                            if (TTFtemp.channel == i)
                            {
                                string start_ = TimeSpan.FromSeconds(TTFtemp.start).ToString("g");
                                string end_ = TimeSpan.FromSeconds(TTFtemp.end).ToString("g");
                                sw_cycles.WriteLine(TTFtemp.number + ";" + Math.Round(TTFtemp.TTFS, 2) + ";" + Math.Round(TTFtemp.TTFD, 2) + ";"
                                    + Math.Round(TTFtemp.TTFF, 2) + ";" + Math.Round(TTFtemp.TTFR, 2) + ";" + start_ + ";" + end_);
                            }
                        }
                        sw_cycles.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public double Percentile(double[] sequence, double excelPercentile)
        {

            Array.Sort(sequence);
            int N = sequence.Length;
            if (N == 0)
            {
                return 0;
            }
            double n = (N - 1) * excelPercentile + 1;
            // Another method: double n = (N + 1) * excelPercentile;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }

        void StatisticChange(int i)
        {
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

            ttfS_50 = Percentile(ttfS_.ToArray(), 0.5);
            ttfD_50 = Percentile(ttfD_.ToArray(), 0.5);
            ttfF_50 = Percentile(ttfF_.ToArray(), 0.5);
            ttfR_50 = Percentile(ttfR_.ToArray(), 0.5);

            ttfS_90 = Percentile(ttfS_.ToArray(), 0.9);
            ttfD_90 = Percentile(ttfD_.ToArray(), 0.9);
            ttfF_90 = Percentile(ttfF_.ToArray(), 0.9);
            ttfR_90 = Percentile(ttfR_.ToArray(), 0.9);

            dataGridView1[4, i + 1].Value = cycle_counter[i].ToString();
            dataGridView1[5, i + 1].Value = Math.Round(ttfS_50, 2).ToString("F1");
            dataGridView1[6, i + 1].Value = Math.Round(ttfS_90, 2).ToString("F1");
            switch (CurrentMode)
            {
                case 0:
                    dataGridView1[7, i + 1].Value = Math.Round(ttfS_.Min(), 2); 
                    dataGridView1[8, i + 1].Value = Math.Round(ttfS_.Max(), 2);
                    dataGridView1[9, i + 1].Value = Math.Round(ttfS_50, 2).ToString("F1");
                    dataGridView1[10, i + 1].Value = Math.Round(ttfS_90, 2).ToString("F1");
                    break;
                case 1:
                    dataGridView1[7, i + 1].Value = Math.Round(ttfD_.Min(), 2);
                    dataGridView1[8, i + 1].Value = Math.Round(ttfD_.Max(), 2);
                    dataGridView1[9, i + 1].Value = Math.Round(ttfD_50, 2).ToString("F1");
                    dataGridView1[10, i + 1].Value = Math.Round(ttfD_90, 2).ToString("F1");
                    break;
                case 2:
                    dataGridView1[7, i + 1].Value = Math.Round(ttfR_.Min(), 2);
                    dataGridView1[8, i + 1].Value = Math.Round(ttfR_.Max(), 2);
                    dataGridView1[9, i + 1].Value = Math.Round(ttfR_50, 2).ToString("F1");
                    dataGridView1[10, i + 1].Value = Math.Round(ttfR_90, 2).ToString("F1");
                    break;
                default:
                break;
            }
        }

        static int Min_nonzero(int[] array)
        {
            int min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (min > array[i] && array[i]!=0)
                {
                    min = array[i];
                }
            }
            return min;
        }

        private void TableInit(int Soltype)
        {
            string solution ="N/A";

            switch (Soltype)
            {
                case 0:
                    solution = "Standalone";
                    break;
                case 1:
                    solution = "DGNSS";
                    break;
                case 2:
                    solution = "RTK";
                    break;
            }
            //Initializing table
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(8);
            dataGridView1[0, 0].Value = "Port ID";
            dataGridView1[1, 0].Value = "Receiver board";
            dataGridView1[2, 0].Value = "Receiver ID";
            dataGridView1[3, 0].Value = "FW ver.";
            dataGridView1[4, 0].Value = "Cycle count";
            dataGridView1[5, 0].Value = "Standalone TTF (50%)";
            dataGridView1[6, 0].Value = "Standalone TTF (90%)";
            dataGridView1[7, 0].Value = "Min " + solution + " TTF";
            dataGridView1[8, 0].Value = "Max " + solution + " TTF";
            dataGridView1[9, 0].Value = solution + " TTF (50)";
            dataGridView1[10, 0].Value = solution + " TTF (90%)";
            dataGridView1[11, 0].Value = "Color";
            dataGridView1[12, 0].Value = "Graph label";

        }
    }
}
