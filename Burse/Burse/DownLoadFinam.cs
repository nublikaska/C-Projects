using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Burse
{
    class DownLoadFinam
    {
        public static int Count = 0;
        public static int LastDownloadCount = 0;
        public static String LastName;
        WebClient wb = new WebClient();

        public DownLoadFinam(String connection, String name, int c)
        {
            while (c-1 != LastDownloadCount)
            {
            }

            if (c - 1 == LastDownloadCount)
            {
                try
                {
                    if (LastName == "SBER")
                        Console.Write("хочу  скачать --");
                    wb.DownloadFile(connection, name);
                    Console.Write("скачал --");
                    LastName = name;
                    LastDownloadCount++;
                }
                catch (WebException e) { Thread.Sleep(300); }
            }

        }

            
    }
}
