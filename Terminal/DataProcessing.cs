﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;

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

                //write header rows to csv
                /*
                for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    swOut.Write(gridIn.Columns[i].HeaderText);
                }

                swOut.WriteLine();
                */
                //write DataGridView rows to csv
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
                swOut.Close();
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


            dataGridView1[1, i + 1].Value = cycle_counter[i].ToString();
            dataGridView1[2, i + 1].Value = Math.Round((ttfS_50 / 10), 2).ToString() + " / " + Math.Round((ttfS_90 / 10), 2).ToString();
            switch (CurrentMode)
            {
                case 0:
                    dataGridView1[3, i + 1].Value = Math.Round(ttfS_.Min() / 10, 2); 
                    dataGridView1[4, i + 1].Value = Math.Round(ttfS_.Max() / 10, 2);
                    dataGridView1[5, i + 1].Value = Math.Round((ttfS_50 / 10), 2).ToString() + " / " + Math.Round((ttfS_90 / 10), 2).ToString();
                    break;
                case 1:
                    dataGridView1[3, i + 1].Value = Math.Round(ttfD_.Min() / 10, 2);
                    dataGridView1[4, i + 1].Value = Math.Round(ttfD_.Max() / 10, 2);
                    dataGridView1[5, i + 1].Value = Math.Round((ttfD_50 / 10), 2).ToString() + " / " + Math.Round((ttfD_90 / 10), 2).ToString();
                    break;
                case 2:
                    dataGridView1[3, i + 1].Value = Math.Round(ttfR_.Min() / 10, 2);
                    dataGridView1[4, i + 1].Value = Math.Round(ttfR_.Max() / 10, 2);
                    dataGridView1[5, i + 1].Value = Math.Round((ttfR_50 / 10), 2).ToString() + " / " + Math.Round((ttfR_90 / 10), 2).ToString();
                    break;
                default:
                break;
            }
            
            //dataGridView1[4, i + 1].Value = Math.Round((ttfF_50 / 10), 2).ToString() + " / " + Math.Round((ttfF_90 / 10), 2).ToString();
            


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
    }
}