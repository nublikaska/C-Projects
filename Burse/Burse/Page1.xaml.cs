using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

namespace Burse
{
    public partial class Page1 : Page
    {
        /// <summary>
        /// Логика взаимодействия для Page1.xaml
        /// </summary>
        private Thread NewThreadSber;
        private Thread NewThreadGazp;
        private Finam finam;


        public Page1()
        {
            InitializeComponent();
            try
            {
                if (NewThreadSber.ThreadState == ThreadState.Running)
                    NewThreadSber.Abort();
                if (NewThreadGazp.ThreadState == ThreadState.Running)
                    NewThreadGazp.Abort();
            }
            catch (Exception e) { }
            finam = new Finam();
            NewThreadSber = new Thread(new ParameterizedThreadStart(MethodByThread));
            NewThreadSber.IsBackground = true;
            NewThreadSber.Name = "SBER";
            NewThreadSber.Start(graphicSber);

            NewThreadGazp = new Thread(new ParameterizedThreadStart(MethodByThread));
            NewThreadGazp.IsBackground = true;
            NewThreadGazp.Name = "GAZP";
            NewThreadGazp.Start(graphicGazp);
        }

        private void ButtonGraph_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MethodByThread(object graphic)
        {
            Double[] Numbers = new Double[5];

            ((Chart)graphic).ChartAreas.Add("Default");
            while (true)
            {
                Console.Write("хочу построить график --");
                Dispatcher.Invoke(() =>
                {
                    ((Chart)graphic).Series[0].Color = Color.Black;
                    ((Chart)graphic).Series[0].Color = Color.Black;
                    ((Chart)graphic).ChartAreas[0].AxisY.IsStartedFromZero = false;
                    ((Chart)graphic).ChartAreas[0].AxisX.Interval = 5;
                    ((Chart)graphic).ChartAreas[0].AxisX.Minimum = 0;
                    ((Chart)graphic).ChartAreas[0].AxisX.Maximum = 37;
                    ((Chart)graphic).Series[0].Color = Color.Black;
                    ((Chart)graphic).ChartAreas[0].AxisY.IsStartedFromZero = false;
                    ((Chart)graphic).ChartAreas[0].AxisX.Interval = 5;
                    ((Chart)graphic).ChartAreas[0].AxisX.Minimum = 0;
                    ((Chart)graphic).ChartAreas[0].AxisX.Maximum = 37;
                    ((Chart)graphic).ChartAreas[0].AxisX.LineColor = Color.Gray;
                    ((Chart)graphic).ChartAreas[0].AxisY.LineColor = Color.Gray;
                    ((Chart)graphic).ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
                    ((Chart)graphic).ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
                    //((Chart)graphic).ChartAreas[0].AxisY.IsStartedFromZero = false;
                    //((Chart)graphic).ChartAreas[0].AxisX.Interval = 4;
                });
                //////////////////////////////////////////////////////////////////////////////////////////////////////////

                finam.DownLoadGraphic("06.10.2017", Thread.CurrentThread.Name);

                FileStream file1 = new FileStream(Thread.CurrentThread.Name, FileMode.Open);
                StreamReader reader = new StreamReader(file1);
                int Count = 0;

                Dispatcher.Invoke(() =>
                {
                    ((Chart)graphic).Series["price"].Points.Clear();
                });

                while ((!reader.EndOfStream))
                {
                    String Time;

                    String currentStr = reader.ReadLine();
                    Time = finam.GetItem(currentStr, 1).Insert(2, ":");
                    Numbers[0] = Convert.ToDouble(finam.GetItem(currentStr, 2).Replace('.', ','));
                    Numbers[1] = Convert.ToDouble(finam.GetItem(currentStr, 3).Replace('.', ','));
                    Numbers[2] = Convert.ToDouble(finam.GetItem(currentStr, 4).Replace('.', ','));
                    Numbers[3] = Convert.ToDouble(finam.GetItem(currentStr, 5).Replace('.', ','));
                    Numbers[4] = Convert.ToDouble(finam.GetItem(currentStr, 6));                    
                    DrawGraphic((Chart)graphic, "price", Time, Numbers, Count++);
                }

                reader.Close();

                finam.DownLoadGraphic("06.10.2017", Thread.CurrentThread.Name);

                Dispatcher.Invoke(() =>
                {
                    ((Chart)graphic).Series["price"]["PriceUpColor"] = "Green";
                    ((Chart)graphic).Series["price"]["PriceDownColor"] = "Red";
                });

                Console.Write("построил по идее --");
                Thread.Sleep(150000);
            }
        }

        private void DrawGraphic(Chart grap, string series, String Time, double[] Numbers, int Count)
        {
            Dispatcher.Invoke(() => 
            {
                //adding time, high
                grap.Series[series].Points.AddXY(Time, Numbers[1]);
                // adding low
                grap.Series[series].Points[Count].YValues[1] = Numbers[2];
                //adding open
                grap.Series[series].Points[Count].YValues[2] = Numbers[0];
                // adding close
                grap.Series[series].Points[Count].YValues[3] = Numbers[3];
            });
            
        }
    }
}
