using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AverageForPeriod.xaml
    /// </summary>
    public partial class AverageForPeriod : Window
    {
        Finam finam;
        private bool setedCalendar1 = false;
        private bool setedCalendar2 = false;

        public AverageForPeriod()
        {
            InitializeComponent();
            DatePicker1.SelectedDate = DateTime.Now;
            DatePicker2.SelectedDate = DateTime.Now;
        }

        private void DatePicker1_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (((DatePicker)sender).Text.Length != 0)
                setedCalendar1 = true;

            if ((DatePicker1.SelectedDate.Value < DatePicker2.SelectedDate.Value) || (DatePicker2.SelectedDate.Value == null))
                DatePicker2.SelectedDate = DatePicker1.SelectedDate.Value;
        }

        private void DatePicker2_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (((DatePicker)sender).Text.Length != 0)
                setedCalendar2 = true;

            if ((DatePicker2.SelectedDate.Value > DatePicker1.SelectedDate.Value) || (DatePicker1.SelectedDate.Value == null))
                DatePicker1.SelectedDate = DatePicker2.SelectedDate.Value;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = "";
            finam = new Finam();
            button.Background = Brushes.Red;
            button.IsEnabled = false;

            if ((RadioButtonGazprom.IsChecked == true) || (RadioButtonSberBank.IsChecked == true))
            {
                if ((setedCalendar1 == true) && (setedCalendar2 == true))
                {
                    if (RadioButtonGazprom.IsChecked == true)
                        label1.Content = "Среднее количество сделок: " + finam.AverageForPeriod(ref DatePicker2, DatePicker1, RadioButtonGazprom);
                    else
                        label1.Content = "Среднее количество сделок: " + finam.AverageForPeriod(ref DatePicker2, DatePicker1, RadioButtonSberBank);
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
            setedCalendar1 = false;
        }

        private void DatePicker2_TextInput(object sender, TextCompositionEventArgs e)
        {
            DatePicker2.Text = "";
            setedCalendar2 = false;
        }
    }
}
