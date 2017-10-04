using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для VerageTransactionForDay.xaml
    /// </summary>
    public partial class VerageTransactionForDay : Window
    {
        private Finam finam;
        private bool setedCalendar = false;

        public VerageTransactionForDay()
        {
            finam = new Finam();
            InitializeComponent();
        }

        private void DatePicker1_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (DatePicker1.Text.Length != 0)
                setedCalendar = true;
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = "";
            button.Background = Brushes.Red;
            button.IsEnabled = false;
            if ((RadioButtonGazprom.IsChecked == true) || (RadioButtonSberBank.IsChecked == true))
            {
                if ((setedCalendar == true) || (checkBox.IsChecked == true))
                {
                    if (RadioButtonGazprom.IsChecked == true)
                        label1.Content = "Средний размер сделки: " + finam.GetTransactionForDay(DatePicker1.Text, RadioButtonGazprom, "Vol");
                    else
                        label1.Content = "Средний размер сделки: " + finam.GetTransactionForDay(DatePicker1.Text, RadioButtonSberBank, "Vol");
                }
                else MessageBox.Show("Выберите день");
            }
            else MessageBox.Show("Выберите тикер");

            button.IsEnabled = true;
            button.Background = Brushes.White;
        }

        private void DatePicker1_TextInput(object sender, TextCompositionEventArgs e)
        {
            DatePicker1.Text = "";
            setedCalendar = false;
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                DatePicker1.IsEnabled = false;
                DatePicker1.SelectedDate = DateTime.Today;
            }
            else DatePicker1.IsEnabled = true;
        }
    }
}
