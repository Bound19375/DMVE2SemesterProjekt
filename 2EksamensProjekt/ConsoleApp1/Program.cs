using System;
using MySql.Data.MySqlClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    using DAL;
    using System.Data;
    using System.Globalization;

    internal class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();
            DAL dal = DAL.Getinstance();

            List<string> usernames = new List<string>();
            List<string> currentcomboelements = new List<string>();
            usernames.Add("hej");
            currentcomboelements.Add("hej");

            if (dal.DBUpdateCheck().Result >= DateTime.Now.AddMilliseconds(-5000))
            {
                Console.WriteLine("Update now");
            }
            else
            {
                Console.WriteLine("dont update");
            }

        }

        public class Houses
        {
            public int id { get; set; }
            public string? housetype { get; set; }
            public int m2 { get; set; }
            public int price { get; set; }
        }
        
        
        public class Methods
        {
            private static string ConnStr = "server=bound1937.asuscomm.com;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
            DAL dal = DAL.Getinstance();

            


        }

        
    }
}


