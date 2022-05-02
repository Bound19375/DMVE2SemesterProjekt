using _2EksamensProjekt;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DAL
{
    public class API
    {
        #region Fields

        private int MIN { get; set; }
        private int MAX { get; set; }
        public string? AccountUsername { get; set; }
        private string? NewAccountUsername { get; set; }
        private string? CreateAccountUsername { get; set; }
        public string? HouseID { get; set; }
        public string? AccountName { get; set; }
        public string? SpecialCollectionSql { get; set; }
        //public string? User { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Duration { get; set; }
        public string? UnitType { get; set; }
        public int UnitID { get; set; }
        public string? StatisticSQL { get; set; }
        private int CancelBookingID { get; set; }
        private string? AvailableType { get; set; }
        private string? Password { get; set; }
        private string? WaitlistType { get; set; }


        #endregion Fields

        #region Singleton
        static API singleton = new API();
        private API() { } //Private Due to Singleton ^^

        //Singleton
        public static API Getinstance()
        {
            return singleton;
        }
        public SQLCMDS sqlcmds = SQLCMDS.GetInstance();
        #endregion Singleton

        #region SQLCMDS
        public class SQLCMDS
        {
            static SQLCMDS singelton = new SQLCMDS();
            private SQLCMDS() { }
            public static SQLCMDS GetInstance()
            {
                return singelton;
            }
            //Waitlist
            public string Waitlist { get; } = "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;";
            //Residents
            public string CurrentResidents = "SELECT a.username, h.type, r.Name, hr.start_contract, h.m2, h.rental_price FROM housing_residents hr, residents r, housing h, account a WHERE hr.residents_username  = r.account_username AND hr.housing_id = h.id AND r.account_username = a.username ORDER BY a.username;";
            //ReservationIDs
            public string ResidentReservationIDs = "SELECT rrr.id FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp AND r.account_username = @username ORDER BY rrr.end_timestamp;";
            //WashingMachines
            public string WMSORTALL = "SELECT r.id FROM resource r WHERE r.type = 'washingmachine';";
            //PartyHall
            public string PHSortAll = "SELECT r.id FROM resource r WHERE r.type = 'partyhall';";
            //ParkingSpace
            public string PSSortAll = "SELECT r.id FROM resource r WHERE r.type = 'parkingspace';";
            //Booked By User
            public string ResourcesBookedByUsername = "SELECT rrr.id AS 'booking id', a.username, r.Name, r2.`type`, r2.id AS 'unit id', rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp AND r.account_username = @username ORDER BY rrr.end_timestamp;";
            //Booked Overall
            public string AllResourcesBooked = "SELECT rrr.id AS 'booking id', a.username, r.Name, r2.`type`, r2.id AS 'unit id', rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp ORDER BY rrr.end_timestamp;";
            //Available
            public string AvailableResourceIDS = "SELECT r.id FROM resident_resource_reservations rrr, resource r WHERE r.`type` = @availabletype AND ((r.id = rrr.resource_id AND (NOW() > rrr.end_timestamp OR @durationendtime < rrr.start_timestamp)) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;";
            public string AvailableResourcesByType = "SELECT r.id, r.`type` FROM resident_resource_reservations rrr, resource r WHERE r.`type` = @availabletype AND ((r.id = rrr.resource_id AND (NOW() > rrr.end_timestamp OR @durationendtime < rrr.start_timestamp)) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;";
            //Usernames
            public string Usernames = "SELECT r.account_username FROM residents r ORDER BY r.account_username;";
            //StartDate & EndDate
            public string StartDate = "SELECT DISTINCT rrr.start_timestamp FROM resident_resource_reservations rrr ORDER BY rrr.start_timestamp;";
            public string EndDate = "SELECT DISTINCT rrr.end_timestamp FROM resident_resource_reservations rrr ORDER BY rrr.end_timestamp;";
            //Booking Cancel IDS
            public string BookingCancelIDs = "SELECT rrr.id FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp ORDER BY rrr.end_timestamp;";
            //Resident Info
            public string CurrentResidentInfo = "SELECT hr.*, h.`type`, h.m2, h.rental_price, a.username, a.privilege  FROM housing_residents hr, housing h, account a WHERE h.id = hr.housing_id AND (hr.residents_username = @username AND a.username = @username) GROUP BY hr.housing_id;";
        }
        #endregion SQLCMDS

        #region Open/Close-Conn
        //private static string ConnStr = "server=bound1937.asuscomm.com;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
        private static string ConnStr = "server=62.61.157.3;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
        //Open Connection Method 
        public MySqlConnection OpenConn(MySqlConnection conn)
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
        public MySqlConnection CloseConn(MySqlConnection conn)
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

        #region Slogan Thread
        public async Task<string> SloganT()
        {
            try
            {
                do
                {
                    List<string> list = new List<string>() { "Bo godt – bo hos Sønderbo", "test2", "test3", "test4", "test5", "test6", "test7" };

                    Random r = new Random();
                    int slogan = r.Next(0, list.Count);

                    return await Task.FromResult(list[slogan]);
                }
                while (true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return await Task.FromResult("Slogan Error");
        }

        #endregion Slogan Thread

        #region Threading
        #region Database Update Information
        //DataGridView Thread Refresh Method
        private async Task<DateTime> DBUpdateCheck()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);
                string connSql = $"SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = '2SemesterEksamen' ORDER BY UPDATE_TIME DESC LIMIT 1;";
                MySqlCommand cmd = new MySqlCommand(connSql, OpenConn(conn));
                MySqlDataReader reader = cmd.ExecuteReader();
                DateTime DBTime = DateTime.MaxValue;
                while (reader.Read())
                {
                    try
                    {
                        string? timeString = Convert.ToString(reader[0]);
                        if (timeString == string.Empty)
                        {
                            timeString = Convert.ToString(DateTime.MinValue);
                        }
                        if (timeString != null || timeString != string.Empty || timeString != "NULL" || timeString != "[NULL]")
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
                throw new Exception(ex.ToString());
            }
        }
        #endregion Database Update Information
        #region GridviewFill
        public void Gridview(DataGridView gv, string DataTableSql, bool bypass)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);
                DataTable tbl = new DataTable();
                tbl.Clear();
                MySqlCommand cmd1 = new MySqlCommand(DataTableSql, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@min", MIN);
                cmd1.Parameters.AddWithValue("@max", MAX);
                cmd1.Parameters.AddWithValue("@start", Start.ToString("yy-MM-dd HH:mm:ss.ffff"));
                cmd1.Parameters.AddWithValue("@end", End.ToString("yy-MM-dd HH:mm:ss.ffff"));
                cmd1.Parameters.AddWithValue("@unittype", UnitType);
                cmd1.Parameters.AddWithValue("@unitid", UnitID);
                cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);
                cmd1.Parameters.AddWithValue("@durationendtime", Duration.ToString("yy-MM-dd HH:mm:ss.ffff"));
                tbl.Load(cmd1.ExecuteReader());

                CloseConn(conn);

                if (DBUpdateCheck().Result >= DateTime.Now.AddMilliseconds(-5000) || gv.DataSource == null || bypass == true)
                {
                    if (gv.InvokeRequired)
                    {
                        gv.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            var source = new BindingSource(tbl, null);
                            gv.DataSource = source;
                            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion GridviewFill
        #region ComboBoxFill
        public void ComboBoxFill(ComboBox combo, string sql)
        {
            try
            {
                List<string> usernames = new List<string>();
                List<string> currentcomboelements = new List<string>();
                usernames.Clear();
                currentcomboelements.Clear();

                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Append To List
                cmd1 = new MySqlCommand(sql, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@min", MIN);
                cmd1.Parameters.AddWithValue("@max", MAX);
                cmd1.Parameters.AddWithValue("@start", Start.ToString("yy-MM-dd HH:mm:ss.ffff"));
                cmd1.Parameters.AddWithValue("@end", End.ToString("yy-MM-dd HH:mm:ss.ffff"));
                cmd1.Parameters.AddWithValue("@unittype", UnitType);
                cmd1.Parameters.AddWithValue("@unitid", UnitID);
                cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);
                cmd1.Parameters.AddWithValue("@durationendtime", Duration.ToString("yy-MM-dd HH:mm:ss.ffff"));


                MySqlDataReader rdr = cmd1.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        usernames.Add(rdr.GetString(i));
                    }
                }
                rdr.Close();
                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);

                foreach (string items in combo.Items)
                {
                    currentcomboelements.Add(items);
                }

                if (combo.Items.Count < usernames.Count() || combo.Items.Count > usernames.Count() || !usernames.SequenceEqual(currentcomboelements))
                {
                    if (combo.InvokeRequired)
                    {
                        combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            combo.Items.Clear();
                            foreach (string ele in usernames)
                            {
                                combo.Items.Add(ele);
                            }
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        

        public void ComboBoxFillNoSqlInt(ComboBox combo, int amount)
        {
            try
            {
                List<int> countlist = new List<int>();
                countlist.Clear();

                for (int i = 1; i <= amount; i++)
                {
                    countlist.Add(i);
                }

                if (combo.Items.Count != countlist.Count || DBUpdateCheck().Result > DateTime.Now.AddMilliseconds(-5000))
                {
                    if (combo.InvokeRequired)
                    {
                        combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            combo.Items.Clear();
                            foreach (int i in countlist)
                            {
                                combo.Items.Add(i);
                            }
                        });
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion ComboBoxFill
        #region ButtonComboBoxInvoker
        public void ButtonInvoker(Button btn, bool BtnEnableDisable)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    if (BtnEnableDisable)
                        btn.Enabled = true;
                    else 
                        btn.Enabled = false;
                });
            }
        }
        public void ComboBoxInvoker(ComboBox combo, bool CBEnableDisable)
        {
            if (combo.InvokeRequired)
            {
                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    if (CBEnableDisable)
                        combo.Enabled = true;
                    else
                        combo.Enabled = false;
                });
            }
        }
        public void GroupBoxInvoker(GroupBox gb, bool GBEnableDisable)
        {
            if (gb.InvokeRequired)
            {
                gb.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    if (GBEnableDisable)
                        gb.Enabled = true;
                    else
                        gb.Enabled = false;
                });
            }
        }
        #endregion ButtonComboBoxInvoker
        #region GroupBoxReader
        public void groupboxReader(GroupBox gb, string WhichField)
        {
            try
            {
                switch (WhichField)
                {
                    case "AvailableType":
                        if (gb.InvokeRequired)
                        {
                            gb.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                AvailableType = gb.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                            });
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion GroupBoxReader
        #region ComboBoxReader
        public void ComboBoxReader(ComboBox combo, string WhichField)
        {
            try
            {
                switch (WhichField)
                {
                    case "Start":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    bool success = DateTime.TryParse(combo.Text, out DateTime result);
                                    if (success)
                                    {
                                        Start = result;
                                    }
                                });
                            }

                            break;
                        }
                    case "End":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    bool success = DateTime.TryParse(combo.Text, out DateTime result);
                                    if (success)
                                    {
                                        End = result;
                                    }
                                });
                            }

                            break;
                        }
                    case "Duration":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    bool success = Int32.TryParse(combo.Text, out int result);
                                    if (success)
                                    {
                                        Duration = Start.AddHours(Convert.ToDouble(result));
                                    }
                                });
                            }

                            break;
                        }
                    case "User":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    AccountUsername = combo.Text;
                                });
                            }

                            break;
                        }
                    case "UnitID":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    bool success = int.TryParse(combo.Text, out int result);
                                    UnitID = success ? result : 0;
                                });
                            }

                            break;
                        }
                    case "CancelBookingID":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    bool success = int.TryParse(combo.Text, out int result);
                                    CancelBookingID = success ? result : 0;
                                });
                            }

                            break;
                        }
                    case "NewAccountUsername":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    NewAccountUsername = combo.Text;
                                });
                            }

                            break;
                        }
                    case "Password":
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                                {
                                    Password = combo.Text;
                                });
                            }

                            break;
                        }
                }

                if (WhichField == "CreateAccountUsername")
                {
                    if (combo.InvokeRequired)
                    {
                        combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            CreateAccountUsername = combo.Text;
                        });
                    }
                }

                if (WhichField == "WaitlistType")
                {
                    if (combo.InvokeRequired)
                    {
                        combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            WaitlistType = combo.Text;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion ComboBoxReader
        #region TextBoxReader
        public void TextboxReader(TextBox txtbox, string WhichField)
        {
            switch (WhichField)
            {
                case "MIN":
                    {
                        if (txtbox.InvokeRequired)
                        {
                            txtbox.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(txtbox.Text, out int result);
                                MIN = success ? result : 0;
                            });
                        }

                        break;
                    }
                case "MAX":
                    {
                        if (txtbox.InvokeRequired)
                        {
                            txtbox.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(txtbox.Text, out int result);
                                MAX = success ? result : int.MaxValue;
                            });
                        }

                        break;
                    }
            }
        }
        #endregion TextBoxReader
        #region Special Collection Method
        public class Houses
        {
            public int id { get; set; }
            public string? housetype { get; set; }
            public int m2 { get; set; }
            public int price { get; set; }
        }

        private List<Houses> SpecialCollectionList(string dosql)
        {
            List<Houses> collection = new List<Houses>();
            collection.Clear();

            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Set Isolation Level
            string sqlString = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
            cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            sqlString = "START TRANSACTION;";
            cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Append To List
            cmd1 = new MySqlCommand(dosql, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@min", MIN);
            cmd1.Parameters.AddWithValue("@max", MAX);
            MySqlDataReader rdr = cmd1.ExecuteReader();

            while (rdr.Read())
            {
                Houses house = new Houses();

                house.id = rdr.GetInt32(0);
                house.housetype = rdr.GetString(1);
                house.m2 = rdr.GetInt32(3);
                house.price = rdr.GetInt32(2);
                collection.Add(house);
            }
            rdr.Close();
            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new MySqlCommand(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);
            return collection;
        }
        public void GridviewCollection(DataGridView gv, string sql)
        {
            var bindingList = new BindingList<Houses>(SpecialCollectionList(sql)); //Raise an event if the underlying list changes 
            var source = new BindingSource(bindingList, null);
            gv.DataSource = source;
            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        #endregion Special Collection Method
        #endregion Threading
        #region Login
        public async Task<string> Login(string username, string password)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd = new MySqlCommand(sqlString, OpenConn(conn));
                cmd.ExecuteNonQuery();

                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //Input Validation
                string connSql = $"SELECT username, AES_DECRYPT(password, 'key'), privilege FROM account WHERE username = @username";
                cmd = new MySqlCommand(connSql, OpenConn(conn));

                if (regex.IsMatch(username)) //Input Validation Check
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    string dbusername = "NONE";
                    string dbpassword = "NONE";
                    string dbprivilege = "NONE";
                    while (reader.Read())
                    {
                        dbusername = reader.GetString(0);
                        dbpassword = reader.GetString(1);
                        dbprivilege = reader.GetString(2);
                    }
                    reader.Close();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd = new MySqlCommand(commit, OpenConn(conn));
                    cmd.ExecuteNonQuery();

                    CloseConn(conn);

                    if (dbusername == username && dbpassword == password)
                    {
                        AccountUsername = dbusername;
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
                throw new Exception(ex.ToString());
            }
            return await Task.FromResult("NONE");
        }
        #endregion
        #region GetPassword
        public async Task<string> GetPassword(string username)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd = new MySqlCommand(sqlString, OpenConn(conn));
                cmd.ExecuteNonQuery();

                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //Input Validation
                string connSql = $"SELECT username, AES_DECRYPT(password, 'key'), privilege FROM account WHERE username = @username";
                cmd = new MySqlCommand(connSql, OpenConn(conn));

                if (regex.IsMatch(username)) //Input Validation Check
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    string dbusername = "NONE";
                    string dbpassword = "NONE";
                    string dbprivilege = "NONE";
                    while (reader.Read())
                    {
                        dbusername = reader.GetString(0);
                        dbpassword = reader.GetString(1);
                        dbprivilege = reader.GetString(2);
                    }
                    reader.Close();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd = new MySqlCommand(commit, OpenConn(conn));
                    cmd.ExecuteNonQuery();

                    CloseConn(conn);

                    return await Task.FromResult($"Username: {dbusername}\n Password: {dbpassword}\n Privilege: {dbprivilege}");
                }
                else
                {
                    return await Task.FromResult("Incorrect Username Format!\nOnly Accepts A-Z & 0-9");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion GetPassword

        #region SecretaryMethods
        #region Secretary Print Resident (txt)
        public void SecretaryPrint()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Write To txt file
                string cmd_TxtPrint = "SELECT a.username, h.type, r.Name, hr.start_contract, h.m2, h.rental_price FROM housing_residents hr, residents r, housing h, account a WHERE hr.residents_username = r.account_username AND hr.housing_id = h.id AND r.account_username = a.username ORDER BY a.username;";
                cmd1 = new MySqlCommand(cmd_TxtPrint, OpenConn(conn));

                MySqlDataReader rdr = cmd1.ExecuteReader();

                string filePath = @"..\..\..\txts\Residencies.txt";
                using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);

                    while (rdr.Read())
                    {
                        writer.WriteLine(
                            "{\n" +
                            $"\tUsername: {Convert.ToString(rdr[0])}\n" +
                            $"\tType: {Convert.ToString(rdr[1])}\n" +
                            $"\tName: {Convert.ToString(rdr[2])}\n" +
                            $"\tContract_Date: {Convert.ToString(rdr[3])}\n" +
                            $"\tM2: {Convert.ToString(rdr[4])}\n" +
                            $"\tRental_Price: {Convert.ToString(rdr[5])}\n" +
                            "}\n" +
                            "\n");
                    }
                    writer.Close();
                }
                rdr.Close();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                CloseConn(conn);
                MessageBox.Show($"File Downloaded To: {filePath[9..]}");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion SecretaryMethods
        #region Create User And Waitlist
        public void CreateUser_Waitlist()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);
                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Create User
                string sqlcommand = "INSERT INTO account (username, password, privilege) VALUES (@username, AES_ENCRYPT(@password, 'key'), 'waitlist');";
                cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@username", CreateAccountUsername);
                cmd1.Parameters.AddWithValue("@password", Password);

#pragma warning disable CS8604 // Possible null reference argument.
                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //Input Validation
                if (regex.IsMatch(AccountUsername) && regex.IsMatch(Password))
#pragma warning restore CS8604 // Possible null reference argument.
                {
                    cmd1.ExecuteNonQuery();

                    //Append Created User To Waitlist
                    string sql = "SELECT * FROM account WHERE username = @username";
                    cmd1 = new MySqlCommand(sql, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@username", CreateAccountUsername);
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    string dbusername = "NONE";
                    string dbpassword = "NONE";
                    string dbprivilege = "NONE";
                    while (reader.Read())
                    {
                        dbusername = reader.GetString(0);
                        dbpassword = reader.GetString(1);
                        dbprivilege = reader.GetString(2);
                    }
                    reader.Close();
                    string sqlwaitlist = "INSERT INTO waitlist(`type`, account_username) VALUES(@type, @dbusername);";
                    cmd1 = new MySqlCommand(sqlwaitlist, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@type", WaitlistType);
                    cmd1.Parameters.AddWithValue("@dbusername", dbusername);
                    cmd1.ExecuteNonQuery();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd1 = new MySqlCommand(commit, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    CloseConn(conn);
                }
                else
                {
                    MessageBox.Show("Incorrect Username Format!\nOnly Accepts A-Z & 0-9");
                }
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Username Already Exists!\nSet Another Username");
                }
                else
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        #endregion
        #region CancelReservation
        public void CancelReservation()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Delete Booking
                string insert = "DELETE FROM resident_resource_reservations WHERE id = @id;";
                cmd1 = new MySqlCommand(insert, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@id", CancelBookingID);
                cmd1.ExecuteNonQuery();


                //Alter Booking Count
                /*
                string altercount = "UPDATE resource SET times_reserved = times_reserved - 1 WHERE id = @unitid; ";
                cmd1 = new MySqlCommand(altercount, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@unitid", Resources.UnitID);
                cmd1.ExecuteNonQuery();
                */

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #region Booking
        public void Booking()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Insert Booking
                string insert = "INSERT INTO resident_resource_reservations(residents_username, resource_id, start_timestamp, end_timestamp) VALUES(@user, @unitid, @start, @duration);";
                cmd1 = new MySqlCommand(insert, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@user", AccountUsername);
                cmd1.Parameters.AddWithValue("@start", Convert.ToDateTime(Start));
                cmd1.Parameters.AddWithValue("@unittype", UnitType);
                cmd1.Parameters.AddWithValue("@unitid", UnitID);
                cmd1.Parameters.AddWithValue("@duration", Duration);
                cmd1.ExecuteNonQuery();


                //Alter Booking Count
                string altercount = "UPDATE resource SET times_reserved = times_reserved + 1 WHERE id = @unitid; ";
                cmd1 = new MySqlCommand(altercount, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@unitid", UnitID);

                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);
                
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion Booking
        #region AdminMethods
        #region Grant Housing
        public void GrantHousing()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                string type = String.Empty;
                string housetype = String.Empty;

                //Check User Type
                string sqlcommand = "SELECT w.`type` FROM waitlist w WHERE w.account_username = @username;";
                MySqlCommand cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@username", AccountUsername);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    type = reader.GetString(0);
                }
                reader.Close();

                //Check House Type
                sqlcommand = "SELECT h.`type` FROM housing h WHERE h.id = @id;";
                cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(HouseID));
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    housetype = reader.GetString(0);
                }
                reader.Close();
                if (type == housetype && AccountUsername != null && AccountUsername != String.Empty)
                {
                    //Set Isolation Level
                    string sqlString = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                    cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Begin Transation
                    sqlString = "START TRANSACTION;";
                    cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Insert Into Residents
                    sqlcommand = "INSERT INTO residents (name, account_username) VALUES (@name, @username);";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@name", AccountName);
                    cmd1.Parameters.AddWithValue("@username", AccountUsername);
                    cmd1.ExecuteNonQuery();

                    //Insert Into Housing_Residents
                    sqlcommand = "INSERT INTO housing_residents (housing_id, residents_username, start_contract) VALUES (@id, @username, CURRENT_TIMESTAMP);";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn)); 
                    cmd1.Parameters.AddWithValue("@id", HouseID);
                    cmd1.Parameters.AddWithValue("@username", AccountUsername);
                    cmd1.ExecuteNonQuery();

                    //Update Account Status
                    sqlcommand = "UPDATE account SET privilege = @housetype WHERE username = @username";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@housetype", housetype);
                    cmd1.Parameters.AddWithValue("@username", AccountUsername);

                    //Remove From Waitlist
                    sqlcommand = "DELETE FROM waitlist WHERE account_username = @username;";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@username", AccountUsername);
                    cmd1.ExecuteNonQuery();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd1 = new MySqlCommand(commit, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    CloseConn(conn);
                }
                else
                {
                    MessageBox.Show($"House type: {housetype} is not available for Member type: {type}");
                }
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Username Already Exists!\nSet Another Username");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
           
        }
        #endregion Grant Housing
        #region Create New Housing
        public void CreateNewHouse()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);


                if (AdminCreateHouse.HouseType != string.Empty && AdminCreateHouse.M2 != 0 && AdminCreateHouse.Price != 0)
                {
                    //Set Isolation Level
                    string sqlString = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                    MySqlCommand cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Begin Transation
                    sqlString = "START TRANSACTION;";
                    cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Insert Into Residents
                    string sqlcommand = "INSERT INTO housing (type, m2, rental_price) VALUES (@type, @m2, @price);";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@type", AdminCreateHouse.HouseType);
                    cmd1.Parameters.AddWithValue("@m2", AdminCreateHouse.M2);
                    cmd1.Parameters.AddWithValue("@price", AdminCreateHouse.Price);
                    cmd1.ExecuteNonQuery();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd1 = new MySqlCommand(commit, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    CloseConn(conn);
                }
                else
                {
                    MessageBox.Show($"Fill Out All Information!");
                }
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Username Already Exists!\nSet Another Username");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion Create New Housing
        #region Delete Housing
        public void DeleteHouse()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                if (HouseID != string.Empty)
                {
                    //Set Isolation Level
                    string sqlString = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                    MySqlCommand cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Begin Transation
                    sqlString = "START TRANSACTION;";
                    cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    //Insert Into Residents
                    string sqlcommand = "DELETE FROM housing WHERE id = @id;";
                    cmd1 = new MySqlCommand(sqlcommand, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(HouseID));

                    cmd1.ExecuteNonQuery();

                    //COMMIT
                    string commit = "COMMIT;";
                    cmd1 = new MySqlCommand(commit, OpenConn(conn));
                    cmd1.ExecuteNonQuery();

                    CloseConn(conn);
                }
                else
                {
                    MessageBox.Show($"Select a house id!");
                }
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("Username Already Exists!\nSet Another Username");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion Delete Housing
        #region Admin Statistics Print (txt)
        public void AdminStatisticsPrint(string cmd_TxtPrint)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Write To txt file
                cmd1 = new MySqlCommand(cmd_TxtPrint, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
                DataTable tbl = new DataTable();
                tbl.Load(cmd1.ExecuteReader());

                string filePath = @"..\..\..\txts\Resources.txt";
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 0, true))
                {
                    StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);
                    int i = 0;
                    for (i = 0; i < tbl.Columns.Count; i++)
                    {
                        writer.Write(tbl.Columns[i].ColumnName + "\t\t");
                    }
                    writer.WriteLine("\n");

                    foreach (DataRow row in tbl.Rows)
                    {
                        object[] array = row.ItemArray;

                        for (i = 0; i < array.Length; i++)
                        {
                            writer.Write(array[i].ToString() + ";");
                        }
                        writer.WriteLine();
                    }
                    writer.Close();
                }

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                CloseConn(conn);
                MessageBox.Show($"File Downloaded To: {filePath[9..]}");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion Admin Statistics Print (txt)
        #endregion AdminMethods

        #region Resident
        #region UpdateUsername
        public void UpdateUsername()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Update Username
                string altercount = "UPDATE account SET username = @newaccountusername WHERE username = @username; ";
                cmd1 = new MySqlCommand(altercount, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@newaccountusername", NewAccountUsername);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);

                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion Booking
        #region UpdatePassword
        public void UpdatePassword()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);

                //Set Isolation Level
                string StartTransaction = $"\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new MySqlCommand(StartTransaction, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                string sqlString = "START TRANSACTION;";
                cmd1 = new MySqlCommand(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Update Username
                string altercount = "UPDATE account SET password = AES_ENCRYPT(@newpassword, 'key') WHERE username = @username; ";
                cmd1 = new MySqlCommand(altercount, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@newpassword", Password);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);

                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new MySqlCommand(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion UpdatePassword
        #endregion Resident



    }
}
