using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> lst;

        public Form1()
        {
            InitializeComponent();
            lst = new List<string>();
        }

        private void SetComboBoxs()
        {
            string hour = "";
            string minutes = "";
            for (int i = 0; i < 24; i++)
                for (int j = 0; j < 60; j++)
                {
                    hour = i.ToString();
                    minutes = j.ToString();
                    if (i < 10)
                        hour = "0" + i.ToString();
                    if (j < 10)
                        minutes = "0" + j.ToString();
                    comboBox1.Items.Add(hour + ":" + minutes);
                    comboBox2.Items.Add(hour + ":" + minutes);
                }
            //comboBox1.Text = comboBox2.Text = "00:00";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetComboBoxs();
            dataGridView1.Columns.Add("date", "date");
            dataGridView1.Columns.Add("close", "close");
            dataGridView1.Columns.Add("max", "max");
            dataGridView1.Columns.Add("min", "min");
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
            button1.Enabled = false;
            download();
            float[] inf = new float[3];
            inf = Getscore();
            if (inf[0] != -1f)
            {
                dataGridView1.Rows.Add();
                dataGridView1["date", dataGridView1.Rows.Count - 1].Value = dateTimePicker1.Text + " " + comboBox1.Text + " - " + dateTimePicker2.Text + " " + comboBox2.Text;
                dataGridView1["close", dataGridView1.Rows.Count - 1].Value = inf[0];
                dataGridView1["max", dataGridView1.Rows.Count - 1].Value = inf[1];
                dataGridView1["min", dataGridView1.Rows.Count - 1].Value = inf[2];
                button1.BackColor = Color.White;
                button1.Enabled = true;
            }
        }

        private void download()
        {
            string from = "&from=" + dateTimePicker1.Text;
            DateTime dateToTemp = dateTimePicker2.Value.AddDays(1);
            string to = "&to=" + dateToTemp.ToShortDateString();
            string connection = "http://history.alor.ru/export/file.php?board=MICEX&ticker=sber&period=1" + from + to + "&file_name=&formatFiles=1&format=5&formatDate=3&formatTime=3&fieldSeparator=4&formatSeparatorDischarge=2&but.x=124&but.y=24&but=%D1%EA%E0%F7%E0%F2%FC+%F4%E0%E9%EB";
            WebClient wb = new WebClient();
            wb.DownloadFile(connection, "statistic.txt");
            WriteToList();
        }
        private void WriteToList()
        {
            lst.Clear();
            FileStream file1 = new FileStream("statistic.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file1);
            while (!reader.EndOfStream)
                lst.Add(reader.ReadLine());
            reader.Close();
        }
        private string GetItemForNumberStr(int numberStr, int numberItem)
        {
            String[] words = lst[numberStr].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words[numberItem];
        }
        private int GetCountItemForTime(string date, string time)
        {
            int count = 0;
            time = time.Replace(":", "");
            date = date.Replace(".", "").Replace("2017", "");
            date = date + "17";
            while (lst.Count != count)
            {                
                string time2 = GetItemForNumberStr(count, 1);
                string date2 = GetItemForNumberStr(count, 0);

                if ((time == time2) && (date == date2))
                    return count;
                count++;
            }
            return -1;
        }
        private float[] Getscore()
        {
            int from = GetCountItemForTime(dateTimePicker1.Text, comboBox1.Text);
            int to = GetCountItemForTime(dateTimePicker2.Text, comboBox2.Text);
            float[] inf = new float[3]; //close max min
            Single.TryParse(GetItemForNumberStr(from, 4), out inf[2]);
            float tempMax;
            float tempMin;

            if ((from != -1) & (to != -1))
                for (int numberItem = from; numberItem <= to; numberItem++)
                {
                    Single.TryParse(GetItemForNumberStr(numberItem, 3), out tempMax);
                    if (tempMax > inf[1])
                        inf[1] = tempMax;

                    Single.TryParse(GetItemForNumberStr(numberItem, 4), out tempMin);
                    if (tempMin < inf[2])
                        inf[2] = tempMin;
                }               
            else
            {
                MessageBox.Show("дата и время не найдены");
                inf[0] = -1f;
                return inf;
            }

            float tempClose;
            Single.TryParse(GetItemForNumberStr(to, 5), out tempClose);
            inf[0] = tempClose;

            return inf;
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (dateTimePicker1.Text == dateTimePicker2.Text)
            {
                ReSetTimeComboBox2();
            }
            else if (dateTimePicker2.Value < dateTimePicker1.Value)
                dateTimePicker2.Value = dateTimePicker1.Value;
            else if (dateTimePicker2.Value > dateTimePicker1.Value)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                SetComboBoxs();
            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if ((dateTimePicker2.Value < dateTimePicker1.Value) || (dateTimePicker2.Value == dateTimePicker1.Value))
            {
                dateTimePicker1.Value = dateTimePicker2.Value;
                ReSetTimeComboBox2();
            }
            else if (dateTimePicker2.Value > dateTimePicker1.Value)
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                SetComboBoxs();
            }
        }

        private void ReSetTimeComboBox2()
        {
            String[] time = comboBox1.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            int hourInt = Convert.ToInt32(time[0]);
            int minutesInt = Convert.ToInt32(time[1]);

            comboBox2.Items.Clear();

            string hour = "";
            string minutes = "";
            if (hourInt < 10)
                hour = "0" + hourInt.ToString();
            else
                hour = hourInt.ToString();
            for (int j = minutesInt; j < 60; j++)
            {
                minutes = j.ToString();
                if (j < 10)
                    minutes = "0" + j.ToString();
                comboBox2.Items.Add(hour + ":" + minutes);
            }

            for (int i = hourInt + 1; i < 24; i++)
                for (int j = 0; j < 60; j++)
                {


                    if (i < 10)
                        hour = "0" + i.ToString();
                    else
                        hour = i.ToString();
                    if (j < 10)
                        minutes = "0" + j.ToString();
                    else
                        minutes = j.ToString();
                    comboBox2.Items.Add(hour + ":" + minutes);
                }

            comboBox2.Text = comboBox1.Text;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Text == dateTimePicker2.Text)
            {
                ReSetTimeComboBox2();
            }
        }
    }
}
