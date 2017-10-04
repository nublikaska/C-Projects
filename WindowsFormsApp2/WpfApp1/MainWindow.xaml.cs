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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void verage_transaction_for_day_Click(object sender, RoutedEventArgs e)
        {
            VerageTransactionForDay vtfd = new VerageTransactionForDay();
            vtfd.ShowDialog();
        }

        private void compare_Click(object sender, RoutedEventArgs e)
        {
            Compare compare = new Compare();
            compare.ShowDialog();
        }

        private void average_for_period_Click(object sender, RoutedEventArgs e)
        {
            AverageForPeriod averageforperiod = new AverageForPeriod();
            averageforperiod.ShowDialog();
        }

        private void average_vol_for_period_Click(object sender, RoutedEventArgs e)
        {
            AverageVolTrasactionForPeriod averageforperiod = new AverageVolTrasactionForPeriod();
            averageforperiod.ShowDialog();
        }
    }
}
