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
            Run();
        }

        private void Run()
        {            
            Thread thread = new Thread(Start);
            thread.Start();
            Label1.Content = "sdsda";
            Thread.CurrentThread.Join();
        }

        private void Start()
        {
            Label1.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                while (true)
                {
                    Label1.Content = Label1.Content.ToString() + "2 thread:" + "\n";
                    Thread.Sleep(100);
                Console.WriteLine("Thread 2");
                Thread.Sleep(1000);
                }
            }));
        }
    }
}
