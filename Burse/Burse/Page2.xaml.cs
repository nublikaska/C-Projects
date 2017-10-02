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

namespace Burse
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private string currentTicker = "SBER";
        BackgroundWorker backgroundWorker;
        String[] temp;
        Finam finam;


        public Page2()
        {
            InitializeComponent();
            finam = new Finam();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            temp = new String[3];
            temp[0] = DatePicker.Text;
            temp[1] = TimePickerFrom.Text;
            temp[2] = TimePickerTo.Text;
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {            
            e.Result = Method();
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TextBlockMax.Text = temp[0];
            TextBlockMin.Text = temp[1];
        }        

        private bool Method()
        {
            temp = finam.GetMaxMinForDay(temp[0], temp[1], temp[2], currentTicker);
            return true;
        }

        private void HandleCheck_Company(object sender, RoutedEventArgs e)
        {
            Coterovki.Content = "Котировки Газпрома";
            Company.Text = "Газпром";
            currentTicker = "GAZP";
        }

        private void HandleUnchecked_Company(object sender, RoutedEventArgs e)
        {
            Coterovki.Content = "Котировки Сбербанка";
            Company.Text = "Сбербанк";
            currentTicker = "SBER";
        }
    }
}
