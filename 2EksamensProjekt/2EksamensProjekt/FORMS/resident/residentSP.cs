namespace _2EksamensProjekt.FORMS.resident
{
    using _2EksamensProjekt.FORMS.admin;
    using DAL;
    public partial class residentSP : Form
    {
        DAL dal = DAL.Getinstance();
        private static residentSP singleton = new residentSP();
        private residentSP()
        {
            InitializeComponent();
            label5.Text = $"{dal.AccountUsername}";
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
                    dal.groupboxReader(groupBox2, "AvailableType");
                    //StartDate
                    dal.ComboBoxReader(comboBox1, "Start");
                    //UnitID
                    dal.ComboBoxReader(comboBox4, "UnitID");
                    //DurationTime
                    dal.ComboBoxReader(comboBox5, "Duration");
                    //CancelBookingID
                    dal.ComboBoxReader(comboBox6, "CancelBookingID");
                    //AccountNameUpdater
                    dal.ComboBoxReader(comboBox2, "NewAccountUsername");
                    //Password
                    dal.ComboBoxReader(comboBox3, "Password");

                    if (label1.InvokeRequired)
                    {
                        label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                        {
                            label5.Text = dal.AccountUsername; //Calling Async Task SloganT Method From Api Class.
                        });
                    }

                    //StartDate
                    dal.ComboBoxFill(comboBox1, dal.sqlcmds.StartDate);
                    //Booking Cancel IDS
                    dal.ComboBoxFill(comboBox6, dal.sqlcmds.BookingCancelIDs);

                    if (radioButton3.Checked) //WashingMachines
                    {
                        dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        dal.ComboBoxFillNoSqlInt(comboBox5, 4);
                    }
                    else if (radioButton4.Checked) //PartyHall
                    {
                        dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        dal.ComboBoxFillNoSqlInt(comboBox5, 24);
                    }
                    else if (radioButton5.Checked) // ParkingSpace
                    {
                        dal.ComboBoxFill(comboBox4, dal.sqlcmds.AvailableResourceIDS);
                        dal.ComboBoxFillNoSqlInt(comboBox5, 48);
                    }

                    //Booked
                    dal.Gridview(dataGridView4, dal.sqlcmds.AllResourcesBooked, true);
                    //Available
                    dal.Gridview(dataGridView1, dal.sqlcmds.AvailableResourcesByType, true);
                    //Resident Information
                    dal.Gridview(dataGridView5, dal.sqlcmds.CurrentResidentInfo, true);
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
                dal.CancelReservation();
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
                dal.UpdateUsername();
                dal.UpdatePassword();
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
                dal.Booking();
            }
            else
            {
                MessageBox.Show($"User: {dal.AccountUsername}\nStart Date: {dal.Start}\nUnit ID: {dal.UnitID}\nDuration: {dal.Duration}\n\nARE REQUIRED TO BOOK!");
            }
        }
    }
}
