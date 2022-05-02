using System;
using MySql.Data.MySqlClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    using API;
    using System.Data;
    using System.Globalization;

    internal class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();
            API api = API.Getinstance();

            //Task t1 = new Task(() => methods.ForceUpdateDB());
            //t1.Start();
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
            API api = API.Getinstance();

            public async Task<bool> ForceUpdateDB()
            {
                try
                {
                    do
                    {
                        List<int> Saved = new List<int>();
                        List<int> Current = new List<int>();
                        Current.Clear();

                        //Current Bookings
                        string available = "SELECT rrr.id AS 'booking id', a.username, r.Name, r2.`type`, r2.id AS 'unit id', rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp ORDER BY rrr.end_timestamp;";

                        MySqlConnection conn = new MySqlConnection(ConnStr);
                        MySqlCommand cmd1 = new MySqlCommand(available, api.OpenConn(conn));

                        MySqlDataReader reader = cmd1.ExecuteReader();

                        while (reader.Read())
                        {
                            Current.Add(Convert.ToInt32(reader.GetString(0)));
                        }
                        api.CloseConn(conn);

                        if (Current.Count != Saved.Count)
                        {
                            Console.WriteLine("true");
                            Saved = Current;
                            return await Task.FromResult(true);
                        }
                        Saved.Clear();
                        Console.WriteLine("false");
                        return await Task.FromResult(false);
                    }
                    while (true);
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.ToString());
                }
            }


        }

        
    }
}


