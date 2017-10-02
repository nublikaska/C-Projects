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
using System.Windows.Controls;
using System.Threading;

namespace Burse
{
    public partial class Page3 : Page
    {
        private string currentTicker = "SBER";
        Finam finam;
        BackgroundWorker backgroundWorker;
        String[] temp;

        public Page3()
        {
            InitializeComponent();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            finam = new Finam();
            temp = new String[2];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            temp[0] = DatePickerFrom.Text.ToString();
            temp[1] = DatePickerTo.Text.ToString();
            backgroundWorker.Dispose();
            backgroundWorker.RunWorkerAsync();
        }
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = MethodByThread();
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TextBlocks.Text = temp[0];
            TextBlocks1.Text = temp[1];
        }

        private bool MethodByThread()
        {
            temp = finam.AverageVolAndCountForPeriod(temp[0], temp[1], currentTicker);
            return true;
        }

        private void HandleUnchecked_Company(object sender, RoutedEventArgs e)
        {
            CompanyText.Text = "Сбербанк";
            Company.Text = "Сбербанк";
            currentTicker = "SBER";

        }

        private void HandleCheck_Company(object sender, RoutedEventArgs e)
        {
            CompanyText.Text = "Газпром";
            Company.Text = "Газпром";
            currentTicker = "GAZP";
        }
    }
}
