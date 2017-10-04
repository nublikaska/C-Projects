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
    public partial class Page3 : Page
    {
        private string currentTicker = "SBER";

        public Page3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finam finam = new Finam();
            TextBlocks.Text = finam.AverageVolForPeriod(DatePickerFrom, DatePickerTo, currentTicker);
            TextBlocks1.Text = finam.AverageForPeriod(DatePickerFrom, DatePickerTo, currentTicker);
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
