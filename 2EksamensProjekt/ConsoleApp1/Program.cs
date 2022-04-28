using System;
using MySql.Data.MySqlClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    using DAL;
    internal class Program
    {
        static void Main(string[] args)
        {
            //Methods methods = new Methods();
            //foreach (var s in methods.SpecialCollectionGridView())
            //{
            //    Console.WriteLine($"ID: {s.id}" +
            //        $"\n\tHouseType: {s.housetype}" +
            //        $"\n\tM2: {s.m2}" +
            //        $"\n\tPrice: {s.price}\n");
            //}
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
            //private static string ConnStr = "server=bound1937.asuscomm.com;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
            //DAL dal = DAL.Getinstance();

            
        }

        
    }
}


