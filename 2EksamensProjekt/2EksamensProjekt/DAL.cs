using _2EksamensProjekt.FORMS.secretary;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DAL
{
    public class DAL
    {
        #region Singleton
        static DAL singleton = new DAL();
        private DAL() //Private Due to Singleton ^^
        {
        }
        //Singleton
        public static DAL Getinstance()
        {
            return singleton;
        }
        #endregion Singleton

        #region Open/Close-Conn
        private static string ConnStr = "server=bound1937.asuscomm.com;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
        //Open Connection Method 
        private MySqlConnection OpenConn(MySqlConnection conn)
        {
            string state = conn.State.ToString();
            if (state == "Closed")
            {
                try
                {
                    conn.Open();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return conn;
        }

        //Close Connection Method
        private MySqlConnection CloseConn(MySqlConnection conn)
        {
            string state = conn.State.ToString();
            if (state == "Open")
            {
                try
                {
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return conn;
        }
        #endregion Open/Close-Conn

        #region Datagridview Threading Update
        //DataGridView Thread Refresh Method
        public async Task<DateTime> DBUpdateCheck()
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            try
            {
                string connSql = $"SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = '2SemesterEksamen' ORDER BY UPDATE_TIME DESC LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(connSql, OpenConn(conn));
                MySqlDataReader reader = cmd.ExecuteReader();
                DateTime DBTime = DateTime.MaxValue;
                while (reader.Read())
                {
                    try
                    {
                        string? timeString = Convert.ToString(reader[0]);
                        if (timeString != null || timeString != "NULL" || timeString != "[NULL]")
                        {
                            string[] dateFormats = { "dd/MM/yyyy HH.mm.ss", "M/d/yyyy H:mm:ss tt", "M/d/yyyy HH:mm:ss tt", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "ddMMyyyy", "yyyy.MM.dd", "yyyy-MM-dd", "yyyy/MM/dd", "yyyyMMdd" };
                            #pragma warning disable CS8604 // Possible null reference argument.
                            DBTime = Convert.ToDateTime(DateTime.ParseExact(s: timeString, formats: dateFormats, provider: DateTimeFormatInfo.InvariantInfo, style: DateTimeStyles.None).ToString("dd-MM-yyyy HH:mm:ss"));
                            #pragma warning restore CS8604 // Possible null reference argument.
                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                reader.Close();
                CloseConn(conn);
                return await Task.FromResult(DBTime);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return await Task.FromResult(DateTime.MinValue);
        }

        public async Task<DataTable> Datatable(string DataTableSql)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            DataTable tbl = new DataTable();
            tbl.Clear();
            MySqlCommand cmd1 = new MySqlCommand(DataTableSql, OpenConn(conn));
            tbl.Load(cmd1.ExecuteReader());
            CloseConn(conn);
            return await Task.FromResult(tbl);
        }
        #endregion Datagridview Threading Update

        #region GetOffMeLawn
        //public void CreateSubject()
        //{
        //    // opret en (eller flere tabeller, med indhold, remote)

        //    string createSQL = "create table FlightSeats2 (flightNo int,seatsFree int)";

        //    MySqlCommand cmd2 = new MySqlCommand(createSQL, OpenConn()); cmd2.ExecuteNonQuery();

        //    string insertSQL;

        //    insertSQL = "insert into FlightSeats2 values (1,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    insertSQL = "insert into FlightSeats2 values (2,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    insertSQL = "insert into FlightSeats2 values (3,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    insertSQL = "insert into FlightSeats2 values (4,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    insertSQL = "insert into FlightSeats2 values (5,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    insertSQL = "insert into FlightSeats2 values (6,100);";

        //    cmd2 = new MySqlCommand(insertSQL, OpenConn());
        //    cmd2.ExecuteNonQuery();

        //    CloseConn();
        //}
        #endregion

        #region Login
        public string Username { get; set; } = "user";
        public async Task<string> Login(string username, string password)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);
                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //Input Validation
                string connSql = $"SELECT Username, Password, Privilege, id FROM account WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(connSql, OpenConn(conn));

                if (regex.IsMatch(username)) //Input Validation Check
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    string dbusername = "NONE";
                    string dbpassword = "NONE";
                    string dbprivilege = "NONE";
                    int dbid = 0;
                    while (reader.Read())
                    {
                        dbusername = reader.GetString(0);
                        dbpassword = reader.GetString(1);
                        dbprivilege = reader.GetString(2);
                        dbid = Convert.ToInt32(reader.GetString(3));
                    }
                    reader.Close();
                    CloseConn(conn);

                    if (dbusername == username && dbpassword == password)
                    {
                        Username = dbusername;
                        if (dbprivilege == "secretary")
                        {
                            return await Task.FromResult("secretary");
                        }
                        else if (dbprivilege == "admin")
                        {
                            return await Task.FromResult("admin");
                        }
                        else if (dbprivilege == "youth" || dbprivilege == "senior" || dbprivilege == "normal")
                        {
                            if (dbprivilege == "youth")
                            {
                                return await Task.FromResult("youth");
                            }
                            else if (dbprivilege == "senior")
                            {
                                return await Task.FromResult("senior");
                            }
                            else if (dbprivilege == "normal")
                            {
                                return await Task.FromResult("normal");
                            }
                        }
                    }
                    else
                    {
                        return await Task.FromResult("Username or Password Does Not Exist");
                    }
                }
                else
                {
                    return await Task.FromResult("Incorrect Username Format!\nOnly Accepts A-Z & 0-9");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return await Task.FromResult("NONE");
        }
        #endregion

        /*
        public async Task IsolationLevel()
        {
            Task task = new Task(() => {
                try
                {
                    string FlightSeatsTable = "FlightSeats2";

                    /// Flight choice:
                    int flightNo = _2EksamensProjekt.Login.Flightnumber;

                    /// Menu: isolation level choice
                    string level = _2EksamensProjekt.Login.conn;

                    /// Menu: Seats Choice
                    int seats = _2EksamensProjekt.Login.Seats;

                    /// SET TRANSACTION
                    String sqlString = $"\nSET TRANSACTION ISOLATION LEVEL {level}";
                    MySqlCommand cmd = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd.ExecuteNonQuery();

                    /// BEGIN TRANSACTION
                    sqlString = "START TRANSACTION";
                    cmd = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd.ExecuteNonQuery();

                    /// UPDATE FlightSeats                   
                    sqlString = $"UPDATE {FlightSeatsTable} SET seatsFree = seatsFree - {seats} WHERE flightNo = {flightNo}";
                    cmd = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd.ExecuteNonQuery();

                    while (true)
                    {
                        if (YESNO.Check == "COMMIT" || YESNO.Check == "ROLLBACK")
                        {
                            sqlString = YESNO.Check;
                            cmd = new MySqlCommand(sqlString, OpenConn(conn));
                            cmd.ExecuteNonQuery();
                            CloseConn(conn);
                            if (sqlString == "COMMIT")
                            {
                                MessageBox.Show("Transaction Completed!");
                            }
                            else
                            {
                                MessageBox.Show("ROLLBACK");
                            }
                            YESNO.Check = null;
                            break;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    if (ex.Code == 0)
                    {
                        MessageBox.Show($"Error: {ex.Code}" + "\nYou've Reserved Too Many Seats" + "\n\nPick Another Flight" + $"\n + {ex}"); //Deadlock Detection
                    }
                    else if (ex.Code == 1213)
                    {
                        String sqlString = $"ROLLBACK";
                        MySqlCommand cmd = new MySqlCommand(sqlString, OpenConn(conn));
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show($"Error: {ex.Code}" + $"\n{ex}" + "\n\nTry again"); //Deadlock Detection
                    }
                }
                try
                {
                    CloseConn(conn);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            });
            task.Start();
            await task;
        }

        public void ResetDB()
        {
            string sqlString = string.Empty;
            try
            {
                /// Menu: isolation level choice
                string level = _2EksamensProjekt.Login.conn;
                /// SET TRANSACTION
                sqlString = $"SET TRANSACTION ISOLATION LEVEL {level};";
                MySqlCommand cmd = new MySqlCommand(sqlString, OpenConn(conn));
                cmd.ExecuteNonQuery();

                /// BEGIN TRANSACTION
                sqlString = "START TRANSACTION;";
                cmd = new MySqlCommand(sqlString, OpenConn(conn));
                cmd.ExecuteNonQuery();

                /// UPDATE FlightSeats
                for (int i = 1; i <= 6; i++)
                {
                    sqlString = $"UPDATE FlightSeats2 SET seatsFree = 100 WHERE flightNo = {i}";
                    if (i == 6)
                    {
                        sqlString += ";";
                    }
                    cmd = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd.ExecuteNonQuery();
                }

                // COMMIT or ROLLBACK:
                if (MessageBox.Show("Do You Wish To Commit?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sqlString = "COMMIT;";
                }
                else
                {
                    sqlString = "ROLLBACK;";
                }
                cmd = new MySqlCommand(sqlString, OpenConn(conn));
                cmd.ExecuteNonQuery();
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + ex.Code + sqlString);
            }
        }
        */
    }
}
