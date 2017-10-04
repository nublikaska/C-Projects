using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Compare.xaml
    /// </summary>
    public partial class Compare : Window
    {
        private Finam finam;
        private bool setedCalendar1 = false;
        private bool setedCalendar2 = false;

        public Compare()
        {
            finam = new Finam();
            InitializeComponent();
        }

        private void DatePicker1_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (((DatePicker)sender).Text.Length != 0)
                setedCalendar1 = true;
        }

        private void DatePicker2_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (((DatePicker)sender).Text.Length != 0)
                setedCalendar2 = true;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            label1.Content = "";
            button.Background = Brushes.Red;
            button.IsEnabled = false;
            string[] Result = new string[3];

            if ((RadioButtonGazprom.IsChecked == true) || (RadioButtonSberBank.IsChecked == true))
            {
                if (((setedCalendar1 == true) && (setedCalendar2 == true)) || (checkBox.IsChecked == true))
                {
                    if (RadioButtonGazprom.IsChecked == true)
                        Result = finam.Compare(DatePicker1.Text, DatePicker2.Text, RadioButtonGazprom);
                    else
                        Result = finam.Compare(DatePicker1.Text, DatePicker2.Text, RadioButtonSberBank);
                    if (Result[0] != "error")
                    {
                        LabelTodayNumber.Content = Result[0];
                        LabelZnak.Content = Result[1];
                        LabelBeforeNumber.Content = Result[2];

                        if (checkBox.IsChecked == true)
                            LabelToday.Content = "Сегодня";
                        else
                            LabelToday.Content = DatePicker2.Text;

                        LabelBefore.Content = DatePicker1.Text;
                    }
                    else
                    {
                        LabelTodayNumber.Content = "";
                        LabelZnak.Content = "";
                        LabelBeforeNumber.Content = "";
                        LabelToday.Content = "";
                        LabelBefore.Content = "";
                        label1.Content = "error";
                    } 
                }
                else MessageBox.Show("Выберите дни");
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

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                DatePicker2.IsEnabled = false;
                DatePicker2.SelectedDate = DateTime.Today;
            }
            else DatePicker2.IsEnabled = true;
        }
    }
}
