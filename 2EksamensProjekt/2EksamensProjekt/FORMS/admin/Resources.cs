using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2EksamensProjekt.FORMS.admin
{
    using DAL;
    public partial class Resources : Form
    {
        DAL dal = DAL.Getinstance();
        private static Resources singleton = new Resources();

        public static string? User { get; set; }
        public static DateTime Start { get; set; }
        public static DateTime End { get; set; }
        public static DateTime Duration { get; set; }
        public static string? UnitType { get; set; }
        public static int UnitID { get; set; }
        public static string? StatisticSQL { get; set; }
        public static int CancelBookingID { get; set; }

        public Resources()
        {
            InitializeComponent();
            radioButton3.Checked = true;
            radioButton6.Checked = true;
            radioButton8.Checked = true;
            button3.Enabled = false;
            Task t1 = new Task(() => UpdateComboBox());
            Task t2 = new Task(() => gridViewTimer());
            t1.Start();
            t2.Start();
        }

        public static Resources GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        string activeRButton = "NONE";
        private void UpdateComboBox()
        {
            do
            {
                //Usernames
                dal.ComboBoxFill(comboBox3, "SELECT r.account_username FROM residents r ORDER BY r.account_username;");
                //StartDate
                dal.ComboBoxFill(comboBox1, "SELECT DISTINCT rrr.start_timestamp FROM resident_resource_reservations rrr ORDER BY rrr.start_timestamp;");
                //EndDate
                dal.ComboBoxFill(comboBox2, "SELECT DISTINCT rrr.end_timestamp FROM resident_resource_reservations rrr ORDER BY rrr.end_timestamp;");
                //Booking Cancel IDS
                dal.ComboBoxFill(comboBox6, "SELECT rrr.id FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp ORDER BY rrr.end_timestamp;");
                
                if (radioButton3.Checked) //WashingMachines
                {
                    if (activeRButton != "radioButton3")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radioButton3";
                    }
                    if (radioButton8.Checked == true)//Sort
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'washingmachine';");
                    }
                    else if (radioButton9.Checked == true)//Book
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resident_resource_reservations rrr, resource r WHERE r.`type` = 'washingmachine' AND ((r.id = rrr.resource_id AND NOW() >= rrr.end_timestamp) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;");
                    }
                    dal.ComboBoxFillNoSqlInt(comboBox5, 4);
                }
                else if (radioButton4.Checked) //PartyHall
                {
                    if(activeRButton != "radioButton4")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radioButton4";
                    }
                    if (radioButton8.Checked == true)//Sort
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'partyhall';");
                    }
                    else if (radioButton9.Checked == true)//Book
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resident_resource_reservations rrr, resource r WHERE r.`type` = 'partyhall' AND ((r.id = rrr.resource_id AND NOW() >= rrr.end_timestamp) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;");
                    }
                    dal.ComboBoxFillNoSqlInt(comboBox5, 24);
                }
                else if (radioButton5.Checked) // ParkingSpace
                {
                    if (activeRButton != "radioButton5")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radioButton5";
                    }
                    if (radioButton8.Checked == true)//Sort
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'parkingspace';");
                    }
                    else if (radioButton9.Checked == true)//Book
                    {
                        dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resident_resource_reservations rrr, resource r WHERE r.`type` = 'parkingspace' AND ((r.id = rrr.resource_id AND NOW() >= rrr.end_timestamp) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;");
                    }
                    dal.ComboBoxFillNoSqlInt(comboBox5, 48);
                }
            }
            while (true);
        }

        private void gridViewTimer()
        {
            do
            {
                try
                {
                    //Booked
                    dal.Gridview(dataGridView4, "SELECT rrr.id AS 'booking id', a.username, r.Name, r2.`type`, r2.id AS 'unit id', rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND NOW() < rrr.end_timestamp ORDER BY rrr.end_timestamp;", dal.bypassDatatableUpdate);
                    //Available
                    dal.Gridview(dataGridView1, "SELECT r.id, r.`type` FROM resident_resource_reservations rrr, resource r WHERE ((r.id = rrr.resource_id AND NOW() >= rrr.end_timestamp) OR (r.id NOT IN(SELECT rrr2.resource_id FROM resident_resource_reservations rrr2))) GROUP BY r.id ORDER BY r.id;", dal.bypassDatatableUpdate);

                    if (radioButton6.Checked) //All User
                    {
                        StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username  = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username ORDER BY rrr.end_timestamp DESC ;";
                        dal.Gridview(dataGridView5, StatisticSQL, dal.bypassDatatableUpdate);
                    }
                    else if (radioButton7.Checked) //All Per Unit (Count)
                    {
                        StatisticSQL = StatisticSQL = "SELECT * FROM resource r;";
                        dal.Gridview(dataGridView5, StatisticSQL, dal.bypassDatatableUpdate);
                    }
                    else if (radioButton1.Checked) //Per User
                    {
                        StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND rrr.start_timestamp >= @start AND rrr.end_timestamp <= @end AND rrr.residents_username = @user AND r2.type = @unittype ORDER BY rrr.end_timestamp;";
                        dal.Gridview(dataGridView5, StatisticSQL, dal.bypassDatatableUpdate);
                    }
                    else if (radioButton2.Checked) //Per Unit
                    {
                        StatisticSQL = "SELECT * FROM resource r WHERE id = @unitid; ";
                        dal.Gridview(dataGridView5, StatisticSQL, dal.bypassDatatableUpdate);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dal.bypassDatatableUpdate = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    User = comboBox3.Text;
                    Start = Convert.ToDateTime(comboBox1.Text);
                    End = Convert.ToDateTime(comboBox2.Text);
                    UnitType = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                }
                else if (radioButton2.Checked)
                {
                    Start = Convert.ToDateTime(comboBox1.Text);
                    End = Convert.ToDateTime(comboBox2.Text);
                    UnitType = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                    UnitID = Convert.ToInt32(comboBox4.Text);
                }
                dal.bypassDatatableUpdate = true;
            }
            catch (Exception)
            {
                if (radioButton1.Checked)
                {
                    MessageBox.Show($"User: {User}\nStart Date: {Start}\nEnd Date: {End}\nUnit Type: {UnitType}\nARE REQUIRED TO SORT BY USER");
                }
                else if (radioButton2.Checked)
                {
                    MessageBox.Show($"Start Date: {Start}\nEnd Date: {End}\nUnit Type: {UnitType}\nUnit ID: {UnitID}\nARE REQUIRED TO SORT BY UNIT");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                User = comboBox3.Text;
                Start = Convert.ToDateTime(comboBox1.Text);
                UnitType = groupBox2.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked).Text;
                UnitID = Convert.ToInt32(comboBox4.Text);
                Duration = Start.AddHours(Convert.ToDouble(comboBox5.Text));

                dal.Booking();
                dal.bypassDatatableUpdate = true;
            }
            catch (Exception)
            {
                MessageBox.Show($"User: {User}\nStart Date: {Start}\nUnit ID: {UnitID}\nDuration: {Duration}\n\nARE REQUIRED TO BOOK!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dal.AdminStatisticsPrint(StatisticSQL, dataGridView5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Text = DateTime.Now.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                button2.Enabled = true;
                button3.Enabled = false;
                activeRButton = "NONE";
            }
            else if (radioButton9.Checked == true)
            {
                button2.Enabled = false;
                button3.Enabled = true;
                activeRButton = "NONE";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                CancelBookingID = Convert.ToInt32(comboBox6.Text);
                dal.CancelReservation();
                dal.bypassComboBoxFillCount = true;
            }
            catch
            {
                MessageBox.Show("Select ID");
            }
        }
    }
}
