using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL
    {
        static DAL singleton = new DAL();
        private static string ConnStr = "server=bound1937.asuscomm.com;port=80;database=FlerbrugerEksperiment;user=plebs;password=1234;SslMode=none;";
        private static MySqlConnection conn = new MySqlConnection(ConnStr);


        private DAL()
        {
        }

        public static DAL Getinstance()
        {
            return singleton;
        }

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

        public DateTime lastUpdateTime;
        public DateTime time;
        private DataTable tbl = new DataTable();
        public DataTable Refresh_conn(string DataTableSql)
        {
            try
            {
                string connSql = $"SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = 'FlerbrugerEksperiment' AND TABLE_NAME = 'FlightSeats2';";
                MySqlCommand cmd = new MySqlCommand(connSql, OpenConn(conn));
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        string timeString = Convert.ToString(reader[0]);
                        if (timeString != null || timeString != "NULL" || timeString != "[NULL]")
                        {
                            string[] dateFormats = { "dd/MM/yyyy HH.mm.ss", "M/d/yyyy H:mm:ss tt", "M/d/yyyy HH:mm:ss tt", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "ddMMyyyy", "yyyy.MM.dd", "yyyy-MM-dd", "yyyy/MM/dd", "yyyyMMdd" };
                            DateTime DBTime = DateTime.ParseExact(timeString, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
                            time = Convert.ToDateTime(DBTime.ToString("dd-MM-yyyy HH:mm:ss"));
                            lastUpdateTime = Convert.ToDateTime(lastUpdateTime.ToString("dd-MM-yyyy HH:mm:ss"));
                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                reader.Close();
                CloseConn(conn);
                if (time > lastUpdateTime)
                {
                    tbl.Clear();
                    lastUpdateTime = time;
                    MySqlCommand cmd1 = new MySqlCommand(DataTableSql, OpenConn(conn));
                    tbl.Load(cmd1.ExecuteReader());
                    CloseConn(conn);
                    return tbl;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return tbl;
        }

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

        public async Task IsolationLevel()
        {
            Task task = new Task(() => {
                try
                {
                    string FlightSeatsTable = "FlightSeats2";

                    /// Flight choice:
                    int flightNo = Form1.Flightnumber;

                    /// Menu: isolation level choice
                    string level = Form1.conn;

                    /// Menu: Seats Choice
                    int seats = Form1.Seats;

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
            await Task.WhenAll(task);
        }

        public void ResetDB()
        {
            string sqlString = string.Empty;
            try
            {
                /// Menu: isolation level choice
                string level = Form1.conn;
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
    }
}
