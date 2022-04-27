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
                throw new Exception(ex.ToString());
            }
        }

        private async Task<DataTable> Datatable(string DataTableSql)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnStr);
                DataTable tbl = new DataTable();
                tbl.Clear();
                MySqlCommand cmd1 = new MySqlCommand(DataTableSql, OpenConn(conn));
                tbl.Load(cmd1.ExecuteReader());
                CloseConn(conn);
                return await Task.FromResult(tbl);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Gridview(DataGridView gv, string sql)
        {
            try
            {
                if (DBUpdateCheck().Result > DateTime.Now.AddMilliseconds(-1000) || gv.DataSource == null)
                {
                    if (gv.InvokeRequired)
                    {
                        gv.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            gv.DataSource = Datatable(sql).Result;
                            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion Datagridview Threading Update

        #region Login
        public string Username { get; set; } = "user";
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
                throw new Exception(ex.ToString());
            }
            return await Task.FromResult("NONE");
        }
        #endregion

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
                cmd1.Parameters.AddWithValue("@username", UserCreateWaitlist.Username);
                cmd1.Parameters.AddWithValue("@password", UserCreateWaitlist.Password);

#pragma warning disable CS8604 // Possible null reference argument.
                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); //Input Validation
                if (regex.IsMatch(UserCreateWaitlist.Username) && regex.IsMatch(UserCreateWaitlist.Password))
#pragma warning restore CS8604 // Possible null reference argument.
                {
                    cmd1.ExecuteNonQuery();

                    //Append Created User To Waitlist
                    string sql = "SELECT * FROM account WHERE username = @username";
                    cmd1 = new MySqlCommand(sql, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@username", UserCreateWaitlist.Username);
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    string dbusername = "NONE";
                    string dbpassword = "NONE";
                    string dbprivilege = "NONE";
                    while (reader.Read())
                    {
                        dbusername = reader.GetString(1);
                        dbpassword = reader.GetString(2);
                        dbprivilege = reader.GetString(3);
                    }
                    reader.Close();
                    string sqlwaitlist = "INSERT INTO waitlist(`type`, account_username) VALUES(@type, @dbusername);";
                    cmd1 = new MySqlCommand(sqlwaitlist, OpenConn(conn));
                    cmd1.Parameters.AddWithValue("@type", UserCreateWaitlist.Type);
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

        #region SecretaryMethods
        #region Secretary Print Resident (txt)
        public void SecretaryPrint()
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
        #endregion
        #endregion SecretaryMethods

        #region AdminMethods
        #endregion AdminMethods

        #region Resident
        #endregion Resident


       
    }
}
