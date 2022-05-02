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


        public Resources()
        {
            InitializeComponent();
            radioButton3.Checked = true;
            radioButton6.Checked = true;
            radioButton8.Checked = true;
            button3.Enabled = false;
            comboBox3.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            groupBox2.Text = "washingmachine";
            comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
            Task t2 = new Task(() => Worker());
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

        private void Worker()
        {
            do
            {
                try
                {
                    //GroupBoxUnitChoice
                    dal.groupboxReader(groupBox2, "AvailableType");
                    //StartDate
                    dal.ComboBoxReader(comboBox1, "Start");
                    //EndDate
                    dal.ComboBoxReader(comboBox2, "End");
                    //User
                    dal.ComboBoxReader(comboBox3, "User");
                    //UnitID
                    dal.ComboBoxReader(comboBox4, "UnitID");
                    //DurationTime
                    dal.ComboBoxReader(comboBox5, "Duration");
                    //CancelBookingID
                    dal.ComboBoxReader(comboBox6, "CancelBookingID");

                    //Usernames
                    dal.ComboBoxFill(comboBox3, dal.sqlcmds.Usernames);
                    //StartDate
                    dal.ComboBoxFill(comboBox1, dal.sqlcmds.StartDate);
                    //EndDate
                    dal.ComboBoxFill(comboBox2, dal.sqlcmds.EndDate);
                    //Booking Cancel IDS
                    dal.ComboBoxFill(comboBox6, dal.sqlcmds.BookingCancelIDs);

                    if (radioButton3.Checked) //WashingMachines
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.WMSORTALL);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        }
                        dal.ComboBoxFillNoSqlInt(comboBox5, 4);
                    }
                    else if (radioButton4.Checked) //PartyHall
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.PHSortAll);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        }
                        dal.ComboBoxFillNoSqlInt(comboBox5, 24);
                    }
                    else if (radioButton5.Checked) // ParkingSpace
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.PSSortAll);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        }
                        dal.ComboBoxFillNoSqlInt(comboBox5, 48);
                    }

                    //Booked
                    dal.Gridview(dataGridView4, dal.sqlcmds.AllResourcesBooked, true);
                    //Available
                    dal.Gridview(dataGridView1, dal.sqlcmds.AvailableResourcesByType, true);

                    if (radioButton6.Checked) //All User
                    {
                        dal.StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username  = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username ORDER BY rrr.end_timestamp DESC ;";
                        dal.Gridview(dataGridView5, dal.StatisticSQL, true);
                    }
                    else if (radioButton7.Checked) //All Per Unit (Count)
                    {
                        dal.StatisticSQL = "SELECT * FROM resource r WHERE r.`type` = @availabletype;";
                        dal.Gridview(dataGridView5, dal.StatisticSQL, true);
                    }
                    else if (radioButton1.Checked) //Per User
                    {
                        dal.StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND rrr.start_timestamp >= @start AND rrr.end_timestamp <= @end AND rrr.residents_username = @username AND r2.type = @unittype ORDER BY rrr.end_timestamp;";
                        dal.Gridview(dataGridView5, dal.StatisticSQL, true);
                    }
                    else if (radioButton2.Checked) //Per Unit
                    {
                        dal.StatisticSQL = "SELECT * FROM resource r WHERE id = @unitid;";
                        dal.Gridview(dataGridView5, dal.StatisticSQL, true);
                    }
                    
                    //Invokers
                    if (radioButton8.Checked) //Sort
                    {
                        dal.ButtonInvoker(button3, false);
                        dal.GroupBoxInvoker(groupBox1, true);
                        dal.ComboBoxInvoker(comboBox3, false);
                        dal.ComboBoxInvoker(comboBox1, false);
                        dal.ComboBoxInvoker(comboBox2, false);
                        dal.ComboBoxInvoker(comboBox4, false);
                        dal.ComboBoxInvoker(comboBox5, false);

                        if (radioButton1.Checked) // Per User
                        {
                            dal.ComboBoxInvoker(comboBox3, true);
                            dal.ComboBoxInvoker(comboBox1, true);
                            dal.ComboBoxInvoker(comboBox2, true);
                        }

                        if (radioButton2.Checked) // Per Unit
                        {
                            dal.ComboBoxInvoker(comboBox4, true);
                            dal.ComboBoxInvoker(comboBox1, true);
                            dal.ComboBoxInvoker(comboBox2, true);
                        }

                        if (radioButton6.Checked || radioButton7.Checked) //All
                        {
                            dal.ComboBoxInvoker(comboBox3, false);
                            dal.ComboBoxInvoker(comboBox4, false);

                            if (radioButton7.Checked)
                            {
                                dal.GroupBoxInvoker(groupBox2, true);
                                dal.groupboxReader(groupBox2, "AvailableType");
                            }
                        }
                    }

                    if (radioButton9.Checked) //Book
                    {
                        dal.ButtonInvoker(button3, true);
                        dal.ComboBoxInvoker(comboBox2, false);
                        dal.ComboBoxInvoker(comboBox3, true);
                        dal.ComboBoxInvoker(comboBox5, true);
                        dal.ComboBoxInvoker(comboBox1, true);
                        dal.ComboBoxInvoker(comboBox4, true);
                        dal.GroupBoxInvoker(groupBox1, false);
                        dal.GroupBoxInvoker(groupBox2, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != String.Empty && comboBox1.Text != String.Empty && comboBox4.Text != String.Empty && comboBox5.Text != String.Empty)
            {
                dal.Booking();
            }
            else
            {
                MessageBox.Show($"User: {dal.AccountUsername}\nStart Date: {dal.Start}\nUnit ID: {dal.UnitID}\nDuration: {dal.Duration}\n\nARE REQUIRED TO BOOK!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dal.AdminStatisticsPrint(dal.StatisticSQL);
            }
            catch (Exception)
            {
                if (radioButton1.Checked)
                {
                    MessageBox.Show($"User: {dal.AccountUsername}\nStart Date: {dal.Start}\nEnd Date: {dal.End}\nUnit Type: {dal.UnitType}\nARE REQUIRED TO SORT BY USER");
                }
                else if (radioButton2.Checked)
                {
                    MessageBox.Show($"Start Date: {dal.Start}\nEnd Date: {dal.End}\nUnit Type: {dal.UnitType}\nUnit ID: {dal.UnitID}\nARE REQUIRED TO SORT BY UNIT");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox6.Text == String.Empty)
            {
                MessageBox.Show("Select ID");
            }
            else
            {
                dal.CancelReservation();
            }
        }
    }
}
