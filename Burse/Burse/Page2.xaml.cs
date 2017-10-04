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
      
        public Page2()
        {
            InitializeComponent();
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finam finam = new Finam();
            String[] result = finam.GetMaxMinForDay(DatePicker.Text, TimePickerFrom.Text, TimePickerTo.Text, currentTicker);
            TextBlockMax.Text = result[0];
            TextBlockMin.Text = result[1];
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
