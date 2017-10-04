using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Burse
{
    class Finam
    {
        private string date;
        private string connection;
        private string from;
        private string df;
        private string mf;
        private string yf;

        private string to;
        private string dt;
        private string mt;
        private string yt;
        private string em;
        private string code;
        private string cn;
        private string p;
        private string datf;

        private string f { get; set; }

        private void SetDatf(string datf)
        {
            this.datf = "&datf=" + datf;
        }

        private void SetP(string p)
        {
            this.p = "&p=" + p;
        }

        private void SetFrom(string d, string m, string y)
        {
            from = "&from=" + d + "." + m + "." + y;
            df = "&df=" + (Convert.ToInt32(d)).ToString();
            mf = "&mf=" + (Convert.ToInt32(m) - 1).ToString();
            yf = "&yf=" + (Convert.ToInt32(y)).ToString();
        }

        private void SetTo(string d, string m, string y)
        {
            to = "&to=" + d + "." + m + "." + y;
            dt = "&dt=" + (Convert.ToInt32(d)).ToString();
            mt = "&mt=" + (Convert.ToInt32(m) - 1).ToString();
            yt = "&yt=" + (Convert.ToInt32(y)).ToString();
        }

        private void SetConnection()
        {
            connection = "http://export.finam.ru/statistic.txt?+market=1" +
                em +
                code +
                "&apply=0" +
                df +
                mf +
                yf +
                from +
                dt +
                mt +
                yt +
                to +
                p +
                "&f=GAZP_170928_170928" +
                "&e=.txt" +
                cn +
                "&dtf=3" +
                "&tmf=2" +
                "&MSOR=0" +
                "&mstime=on" +
                "&mstimever=1" +
                "&sep=5" +
                "&sep2=1" +
                datf;
        }

        private void SetEm_code_cn(string em, string code, string cn)
        {
            this.em = em;
            this.code = code;
            this.cn = cn;
        }

        private Boolean Download()
        {
            WebClient wb = new WebClient();
            try
            {
                wb.DownloadFile(connection, "statistic.txt");
                return true;
            }
            catch (WebException e)
            {
                return false;
            }
        }

        private string GetItem(string tick, int numberItem)
        {
            String[] words = tick.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words[numberItem];
        }

        public string GetTransactionForDay(String Date, String Ticker, String ReturnVolOrCount)
        {
            Double result = 0;
            Double count = 0;

            if (Ticker == "RadioButtonGazprom")
                SetEm_code_cn("&em=16842", "&code=GAZP", "&cn=GAZP");
            else
                SetEm_code_cn("&em=3", "&code=SBER", "&cn=SBER");
            SetP("1");
            SetDatf("12");
            SetFrom(Date.Substring(0, 2), Date.Substring(3, 2), Date.Substring(6));
            SetTo(Date.Substring(0, 2), Date.Substring(3, 2), Date.Substring(6));
            SetConnection();

            Download();

            FileStream file1 = new FileStream("statistic.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file1);

            while (!reader.EndOfStream)
            {
                result += Convert.ToDouble(GetItem(reader.ReadLine(), 3));
                count++;
            }
            reader.Close();

            if (count > 0)
            {
                Double res = result / count;
                if (ReturnVolOrCount == "Vol")
                    return res.ToString("G17");
                else if (ReturnVolOrCount == "Count")
                    return count.ToString();
            }
            else
                return "error";

            return "error";
        }

        public string[] Compare(String Date1, String Date2, String Ticker)
        {
            int before;
            int today;

            try
            {
                before = Convert.ToInt32(GetTransactionForDay(Date1, Ticker, "Vol"));
            }
            catch (Exception e)
            {
                return new string[] { "error", "error", "error" };
            };

            try
            {
                today = Convert.ToInt32(GetTransactionForDay(Date2, Ticker, "Vol"));
            }
            catch (Exception e)
            {
                return new string[] { "error", "error", "error" };
            };

            if (today > before)
                return new string[] { today.ToString(), ">", before.ToString() };
            else if (today < before)
                return new string[] { today.ToString(), "<", before.ToString() };
            else
                return new string[] { today.ToString(), "=", before.ToString() };
        }

        public string AverageForPeriod(ref DatePicker DateFrom, DatePicker DateTo, String Ticker)
        {
            Double Summ = 0;
            string temp = "";
            Double Count = 0;

            while (DateFrom.Text != DateTo.Text)
            {
                temp = GetTransactionForDay(DateFrom.Text, Ticker, "Count");
                if (temp != "error")
                {
                    Summ += Convert.ToInt32(temp);
                    Count++;
                }

                DateFrom.SelectedDate = DateFrom.SelectedDate.Value.AddDays(1);
            }

            temp = GetTransactionForDay(DateFrom.Text, Ticker, "Count");
            if (temp != "error")
            {
                Summ += Convert.ToDouble(temp);
                Count++;
            }

            Double res = Summ / Count;
            return res.ToString("G17");
        }

        public string AverageVolForPeriod(DatePicker DateFrom, DatePicker DateTo, String Ticker)
        {
            Double Summ = 0;
            string temp = "";
            Double Count = 0;

            while (DateFrom.Text != DateTo.Text)
            {
                temp = GetTransactionForDay(DateFrom.Text, Ticker, "Vol");
                if (temp != "error")
                {
                    Summ += Convert.ToDouble(temp);
                    Count++;
                }

                DateFrom.SelectedDate = DateFrom.SelectedDate.Value.AddDays(1);
            }

            temp = GetTransactionForDay(DateFrom.Text, Ticker, "Vol");
            if (temp != "error")
            {
                Summ += Convert.ToDouble(temp);
                Count++;
            }

            Double res = Summ / Count;
            return res.ToString("G17");
        }

        public String[] GetMaxMinForDay(String Date, String TimeFrom, String TimeTo, String Ticker)
        {
            Double resultMax;
            Double resultMin;
            string currentStr;

            if (Ticker == "SBER")
                SetEm_code_cn("&em=3", "&code=SBER", "&cn=SBER");
            else if (Ticker == "GAZP")
                SetEm_code_cn("&em=16842", "&code=GAZP", "&cn=GAZP");
            SetP("2");
            SetDatf("5");
            SetFrom(Date.Substring(0, 2), Date.Substring(3, 2), Date.Substring(6, 4));
            SetTo(Date.Substring(0, 2), Date.Substring(3, 2), Date.Substring(6, 4));
            SetConnection();

            Download();

            FileStream file1 = new FileStream("statistic.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file1);

            currentStr = reader.ReadLine();
            resultMax = Convert.ToDouble(GetItem(currentStr, 3).Replace('.', ','));
            resultMin = Convert.ToDouble(GetItem(currentStr, 4).Replace('.', ','));

            while ( (!reader.EndOfStream) && 
                    (Convert.ToInt32(GetItem(currentStr, 1)) > Convert.ToInt32(TimeTo.Replace(":", ""))) && 
                    (Convert.ToInt32(GetItem(currentStr, 1)) > Convert.ToInt32(TimeTo.Replace(":", "")))
                  )
            {
                Double tempMax;
                Double tempMin;
                currentStr = reader.ReadLine();
                tempMax = Convert.ToDouble(GetItem(currentStr, 3).Replace('.', ','));
                tempMin = Convert.ToDouble(GetItem(currentStr, 4).Replace('.', ','));
                if (tempMax > resultMax)
                    resultMax = tempMax;
                if (tempMin < resultMin)
                    resultMin = tempMin;
            }

            return new String[] {resultMax.ToString("G17"), resultMin.ToString("G17") };;
        }

        public String[] GetLastVolAndPrice(String Ticker)
        {
            string LastVol;
            string LastPrice;
            string BorS;
            string currentStr;


            if (Ticker == "SBER")
                SetEm_code_cn("&em=3", "&code=SBER", "&cn=SBER");
            else if (Ticker == "GAZP")
                SetEm_code_cn("&em=16842", "&code=GAZP", "&cn=GAZP");
            SetP("1");
            SetDatf("12");
            string DateNow = DateTime.Now.Date.ToShortDateString();
            SetFrom(DateNow.Substring(0, 2), DateNow.Substring(3, 2), DateNow.Substring(6, 4));
            SetTo(DateNow.Substring(0, 2), DateNow.Substring(3, 2), DateNow.Substring(6, 4));
            SetConnection();

            Download();

            FileStream file1 = new FileStream("statistic.txt", FileMode.Open);
            StreamReader reader = new StreamReader(file1);

            currentStr = reader.ReadLine();
            LastVol = GetItem(currentStr, 3);
            LastPrice = GetItem(currentStr, 2);
            BorS = GetItem(currentStr, 5);


            while (!reader.EndOfStream)
                currentStr = reader.ReadLine();

            LastVol = GetItem(currentStr, 3);
            LastPrice = GetItem(currentStr, 2);
            BorS = GetItem(currentStr, 5);

            return new String[] { LastPrice, LastVol, BorS };
        }
    }
}
