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

namespace Burse
{
    public partial class MainWindow:Window
    {
        Finam finam;
        Thread newThread;

        public MainWindow()
        {
            InitializeComponent();
            finam = new Finam();
            newThread = new Thread(MethodByThread);
            newThread.Start();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //newThread.Start();
        }

        private void MethodByThread()
        {
            String[] result;
            while (true)
            {
                result = finam.GetLastVolAndPrice("SBER");
                RefreshingSber(result);
                result = finam.GetLastVolAndPrice("GAZP");
                RefreshingGazp(result);

                Thread.Sleep(5000);
            }
        }

        private void RefreshingSber(String[] result)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(new Action<string>((s) =>
                {
                    TextBlockSber.Text = s;
                    VolSber.Text = s;
                    if (result[2] == "B")
                        TextBlockSber.Foreground = System.Windows.Media.Brushes.Green;
                    else if (result[2] == "S")
                        TextBlockSber.Foreground = System.Windows.Media.Brushes.Red;
                }), result[0]);
            }
            else
            {
                TextBlockSber.Text = result[0];
                VolSber.Text = result[1];
                if (result[2] == "B")
                    TextBlockSber.Foreground = System.Windows.Media.Brushes.Green;
                else if (result[2] == "S")
                    TextBlockSber.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void RefreshingGazp(String[] result)
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(new Action<string>((s) =>
                {
                    TextBlockGazp.Text = s;
                    VolSber.Text = s;
                    if (result[2] == "B")
                        TextBlockGazp.Foreground = System.Windows.Media.Brushes.Green;
                    else if (result[2] == "S")
                        TextBlockGazp.Foreground = System.Windows.Media.Brushes.Red;
                }), result[0]);

                Thread.Sleep(10000);
            }
            else
            {
                TextBlockGazp.Text = result[0];
                VolSber.Text = result[1];
                if (result[2] == "B")
                    TextBlockGazp.Foreground = System.Windows.Media.Brushes.Green;
                else if (result[2] == "S")
                    TextBlockGazp.Foreground = System.Windows.Media.Brushes.Red;
            }
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
