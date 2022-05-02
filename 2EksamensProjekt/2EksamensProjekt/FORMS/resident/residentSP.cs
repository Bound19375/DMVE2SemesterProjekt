namespace _2EksamensProjekt.FORMS.resident
{
    using _2EksamensProjekt.FORMS.admin;
    using API;
    public partial class residentSP : Form
    {
        API api = API.Getinstance();
        private static residentSP singleton = new residentSP();
        private residentSP()
        {
            InitializeComponent();
            label5.Text = $"{api.AccountUsername}";
            radioButton3.Checked = true;
            comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm");
            groupBox2.Text = "washingmachine";
            Task t2 = new Task(() => Worker());
            t2.Start();
        }

        public static residentSP GetInstance()
        {
            return singleton;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Login obj = Login.GetInstance();
            obj.Show();
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
                    //UnitID
                    api.ComboBoxReader(comboBox4, "UnitID");
                    //DurationTime
                    api.ComboBoxReader(comboBox5, "Duration");
                    //CancelBookingID
                    api.ComboBoxReader(comboBox6, "CancelBookingID");
                    //AccountNameUpdater
                    api.ComboBoxReader(comboBox2, "NewAccountUsername");
                    //Password
                    api.ComboBoxReader(comboBox3, "Password");

                    if (label1.InvokeRequired)
                    {
                        label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                        {
                            label5.Text = api.AccountUsername; //Calling Async Task SloganT Method From Api Class.
                        });
                    }

                    //StartDate
                    api.ComboBoxFill(comboBox1, api.sqlcmds.StartDate);
                    //Booking Cancel IDS
                    api.ComboBoxFill(comboBox6, api.sqlcmds.BookingCancelIDs);

                    if (radioButton3.Checked) //WashingMachines
                    {
                        api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        api.ComboBoxFillNoSqlInt(comboBox5, 4);
                    }
                    else if (radioButton4.Checked) //PartyHall
                    {
                        api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        api.ComboBoxFillNoSqlInt(comboBox5, 24);
                    }
                    else if (radioButton5.Checked) // ParkingSpace
                    {
                        api.ComboBoxFill(comboBox4, api.sqlcmds.AvailableResourceIDS);
                        api.ComboBoxFillNoSqlInt(comboBox5, 48);
                    }

                    //Booked
                    api.Gridview(dataGridView4, api.sqlcmds.AllResourcesBooked, true);
                    //Available
                    api.Gridview(dataGridView1, api.sqlcmds.AvailableResourcesByType, true);
                    //Resident Information
                    api.Gridview(dataGridView5, api.sqlcmds.CurrentResidentInfo, true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                api.CancelReservation();
            }
            catch
            {
                MessageBox.Show("Select ID");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != String.Empty && comboBox3.Text != String.Empty)
            {
                api.UpdateUsername();
                api.UpdatePassword();
                MessageBox.Show("Account Updated Successfully\n\nLogging Out!");
                Login obj = Login.GetInstance();
                obj.Show();
                this.Hide();
            }
            else
            {
                if (comboBox2.Text == String.Empty && comboBox3.Text != String.Empty)
                {
                    MessageBox.Show("Account Field Cannot Be Empty");
                }
                if (comboBox3.Text == String.Empty && comboBox2.Text != String.Empty)
                {
                    MessageBox.Show("Password Field Cannot Be Empty");
                }
                if (comboBox3.Text == String.Empty && comboBox2.Text == String.Empty)
                {
                    MessageBox.Show("Both Fields Must Be Filled");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != String.Empty && comboBox4.Text != String.Empty && comboBox5.Text != String.Empty)
            {
                api.Booking();
            }
            else
            {
                MessageBox.Show($"User: {api.AccountUsername}\nStart Date: {api.Start}\nUnit ID: {api.UnitID}\nDuration: {api.Duration}\n\nARE REQUIRED TO BOOK!");
            }
        }
    }
}
