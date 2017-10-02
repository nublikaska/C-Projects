using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.IO;
using System.Windows.Media.Animation;
using System.Drawing.Text;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

namespace Burse
{
    public partial class MainWindow:Window
    {
        Finam finam;
        String[] resultSber;
        String[] resultGazp;
        BackgroundWorker backgroundWorker;

        public object Date { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Page1();
            finam = new Finam();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = MethodByThread();
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshingSber();
            RefreshingGazp();
            LastReload.Text = "Последнее обновление: \n" + DateTime.Now.ToShortTimeString();
            backgroundWorker.Dispose();
            backgroundWorker.RunWorkerAsync();
        }

        private bool MethodByThread()
        {
            resultSber = finam.GetLastVolAndPrice("SBER");
            resultGazp = finam.GetLastVolAndPrice("GAZP");
            Thread.Sleep(5000);
            return true;
        }        

        private void RefreshingSber()
        {
            TextBlockSber.Text = resultSber[0];
            VolSber.Text = resultSber[1];
            if (resultSber[2] == "B")
                TextBlockSber.Foreground = System.Windows.Media.Brushes.Green;
            else if (resultSber[2] == "S")
                TextBlockSber.Foreground = System.Windows.Media.Brushes.Red;
        }

        private void RefreshingGazp()
        {
            TextBlockGazp.Text = resultGazp[0];
            VolGazp.Text = resultGazp[1];
            if (resultGazp[2] == "B")
                TextBlockGazp.Foreground = System.Windows.Media.Brushes.Green;
            else if (resultGazp[2] == "S")
                TextBlockGazp.Foreground = System.Windows.Media.Brushes.Red;
        }
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation thkAnim = new ThicknessAnimation(new Thickness(-200, 52, 803, 0), new Thickness(0, 52, 603, 0), new Duration(TimeSpan.FromMilliseconds(320)));
            ContextMenu.BeginAnimation(MaterialDesignThemes.Wpf.ColorZone.MarginProperty, thkAnim);
        }

        public void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation thkAnim = new ThicknessAnimation(new Thickness(0, 52, 603, 0), new Thickness(-200, 52, 803, 0), new Duration(TimeSpan.FromMilliseconds(320)));
            ContextMenu.BeginAnimation(MaterialDesignThemes.Wpf.ColorZone.MarginProperty, thkAnim);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page1();
            Сaption.Text = "Биржа";
            ToggleBut.IsChecked = false;
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2();
            Сaption.Text = BursePage1.Header.ToString();
            ToggleBut.IsChecked = false;
        }

        private void Page3_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3();
            Сaption.Text = BursePage2.Header.ToString();
            ToggleBut.IsChecked = false;
        }

        private void Page4_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page4();
            Сaption.Text = BursePage3.Header.ToString();
            ToggleBut.IsChecked = false;
        }


        private void Page6_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page6();
            Сaption.Text = TelegramPage.Header.ToString();
            ToggleBut.IsChecked = false;
        }
    }
}
