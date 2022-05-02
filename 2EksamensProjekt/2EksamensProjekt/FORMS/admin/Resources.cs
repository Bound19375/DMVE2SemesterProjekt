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
    using API;
    public partial class Resources : Form
    {
        API api = API.Getinstance();
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
                    api.groupboxReader(groupBox2, "AvailableType");
                    //StartDate
                    api.ComboBoxReader(comboBox1, "Start");
                    //EndDate
                    api.ComboBoxReader(comboBox2, "End");
                    //User
                    api.ComboBoxReader(comboBox3, "User");
                    //UnitID
                    api.ComboBoxReader(comboBox4, "UnitID");
                    //DurationTime
                    api.ComboBoxReader(comboBox5, "Duration");
                    //CancelBookingID
                    api.ComboBoxReader(comboBox6, "CancelBookingID");

                    //Usernames
                    api.ComboBoxFill(comboBox3, api.sqlcmds.Usernames);
                    //StartDate
                    api.ComboBoxFill(comboBox1, api.sqlcmds.StartDate);
                    //EndDate
                    api.ComboBoxFill(comboBox2, api.sqlcmds.EndDate);
                    //Booking Cancel IDS
                    api.ComboBoxFill(comboBox6, api.sqlcmds.BookingCancelIDs);

                    if (radioButton3.Checked) //WashingMachines
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.WMSORTALL);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        }
                        api.ComboBoxFillNoSqlInt(comboBox5, 4);
                    }
                    else if (radioButton4.Checked) //PartyHall
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.PHSortAll);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        }
                        api.ComboBoxFillNoSqlInt(comboBox5, 24);
                    }
                    else if (radioButton5.Checked) // ParkingSpace
                    {
                        if (radioButton8.Checked == true)//Sort
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.PSSortAll);
                        }
                        else if (radioButton9.Checked == true)//Book
                        {
                            api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        }
                        api.ComboBoxFillNoSqlInt(comboBox5, 48);
                    }

                    //Booked
                    api.Gridview(dataGridView4, api.sqlcmds.AllResourcesBooked, true);
                    //Available
                    api.Gridview(dataGridView1, api.sqlcmds.AvailableResourcesByType, true);

                    if (radioButton6.Checked) //All User
                    {
                        api.StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username  = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username ORDER BY rrr.end_timestamp DESC ;";
                        api.Gridview(dataGridView5, api.StatisticSQL, true);
                    }
                    else if (radioButton7.Checked) //All Per Unit (Count)
                    {
                        api.StatisticSQL = "SELECT * FROM resource r WHERE r.`type` = @availabletype;";
                        api.Gridview(dataGridView5, api.StatisticSQL, true);
                    }
                    else if (radioButton1.Checked) //Per User
                    {
                        api.StatisticSQL = "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND rrr.start_timestamp >= @start AND rrr.end_timestamp <= @end AND rrr.residents_username = @username AND r2.type = @unittype ORDER BY rrr.end_timestamp;";
                        api.Gridview(dataGridView5, api.StatisticSQL, true);
                    }
                    else if (radioButton2.Checked) //Per Unit
                    {
                        api.StatisticSQL = "SELECT * FROM resource r WHERE id = @unitid;";
                        api.Gridview(dataGridView5, api.StatisticSQL, true);
                    }
                    
                    //Invokers
                    if (radioButton8.Checked) //Sort
                    {
                        api.ButtonInvoker(button3, false);
                        api.GroupBoxInvoker(groupBox1, true);
                        api.ComboBoxInvoker(comboBox3, false);
                        api.ComboBoxInvoker(comboBox1, false);
                        api.ComboBoxInvoker(comboBox2, false);
                        api.ComboBoxInvoker(comboBox4, false);
                        api.ComboBoxInvoker(comboBox5, false);

                        if (radioButton1.Checked) // Per User
                        {
                            api.ComboBoxInvoker(comboBox3, true);
                            api.ComboBoxInvoker(comboBox1, true);
                            api.ComboBoxInvoker(comboBox2, true);
                        }

                        if (radioButton2.Checked) // Per Unit
                        {
                            api.ComboBoxInvoker(comboBox4, true);
                            api.ComboBoxInvoker(comboBox1, true);
                            api.ComboBoxInvoker(comboBox2, true);
                        }

                        if (radioButton6.Checked || radioButton7.Checked) //All
                        {
                            api.ComboBoxInvoker(comboBox3, false);
                            api.ComboBoxInvoker(comboBox4, false);

                            if (radioButton7.Checked)
                            {
                                api.GroupBoxInvoker(groupBox2, true);
                                api.groupboxReader(groupBox2, "AvailableType");
                            }
                        }
                    }

                    if (radioButton9.Checked) //Book
                    {
                        api.ButtonInvoker(button3, true);
                        api.ComboBoxInvoker(comboBox2, false);
                        api.ComboBoxInvoker(comboBox3, true);
                        api.ComboBoxInvoker(comboBox5, true);
                        api.ComboBoxInvoker(comboBox1, true);
                        api.ComboBoxInvoker(comboBox4, true);
                        api.GroupBoxInvoker(groupBox1, false);
                        api.GroupBoxInvoker(groupBox2, true);
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
                api.Booking();
            }
            else
            {
                MessageBox.Show($"User: {api.AccountUsername}\nStart Date: {api.Start}\nUnit ID: {api.UnitID}\nDuration: {api.Duration}\n\nARE REQUIRED TO BOOK!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                api.AdminStatisticsPrint(api.StatisticSQL);
            }
            catch (Exception)
            {
                if (radioButton1.Checked)
                {
                    MessageBox.Show($"User: {api.AccountUsername}\nStart Date: {api.Start}\nEnd Date: {api.End}\nUnit Type: {api.UnitType}\nARE REQUIRED TO SORT BY USER");
                }
                else if (radioButton2.Checked)
                {
                    MessageBox.Show($"Start Date: {api.Start}\nEnd Date: {api.End}\nUnit Type: {api.UnitType}\nUnit ID: {api.UnitID}\nARE REQUIRED TO SORT BY UNIT");
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
                api.CancelReservation();
            }
        }
    }
}
