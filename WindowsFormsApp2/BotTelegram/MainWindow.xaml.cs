using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Telegram.Bot;

namespace BotTelegram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Finam finam;
        BackgroundWorker bw;

        public MainWindow()
        {
            InitializeComponent();
            finam = new Finam();
            this.bw = new BackgroundWorker();
            this.bw.DoWork += this.bw_DoWork; // метод bw_DoWork будет работать асинхронно
        }

        async void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var key = e.Argument as String; // получаем ключ из аргументов
            try
            {
                TelegramBotClient Bot = new TelegramBotClient("259629628:AAGW3exFto-l7uovTW3NXsr2fdoFWL1OWsU"); // инициализируем API
                await Bot.SetWebhookAsync("");
                //Bot.SetWebhook(""); // Обязательно! убираем старую привязку к вебхуку для бота
                int offset = 0; // отступ по сообщениям
                while (true)
                {
                    var updates = await Bot.GetUpdatesAsync(offset); // получаем массив обновлений

                    foreach (var update in updates) // Перебираем все обновления
                    {
                        var message = update.Message;
                        if (message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
                        {
                            switch (message.Text)
                            {
                                case "/AverageVolGazpromForToday":
                                    RadioButton r = new RadioButton();
                                    r.Name = "RadioButtonGazprom";
                                    // в ответ на команду /saysomething выводим сообщение
                                    await Bot.SendTextMessageAsync(message.Chat.Id, finam.GetTransactionForDay(DateTime.Now.ToShortTimeString().ToString(), r, "Vol"),
                                       replyToMessageId: message.MessageId);
                                    break;
                                case "/AverageVolSberForToday":
                                    r = new RadioButton();
                                    r.Name = "RadioButtonSberBank";
                                    // в ответ на команду /saysomething выводим сообщение
                                    await Bot.SendTextMessageAsync(message.Chat.Id, finam.GetTransactionForDay(DateTime.Now.ToShortTimeString().ToString(), r, "Vol"),
                                       replyToMessageId: message.MessageId);
                                    break;
                                case "/help":
                                    r = new RadioButton();
                                    r.Name = "RadioButtonSberBank";
                                    // в ответ на команду /saysomething выводим сообщение
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "/AverageVolGazpromForToday - Средний объем сделки Сбербанка за сегодня /n" + "/AverageVolSberForToday - Средний объем сделки Газпрома за сегодня",
                                       replyToMessageId: message.MessageId);
                                    break;
                            }
                        }
                        offset = update.Id + 1;
                    }

                }
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message); // если ключ не подошел - пишем об этом в консоль отладки
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (this.bw.IsBusy != true) // если не запущен
            {
                this.bw.RunWorkerAsync(""); // запускаем
            }
        }
    }
}
