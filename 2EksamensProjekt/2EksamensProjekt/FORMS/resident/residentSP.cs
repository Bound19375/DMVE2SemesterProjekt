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
            label5.Text = $"{dal.Username}";
            radioButton3.Checked = true;
            comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy hh");
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
                    dal.Gridview(dataGridView4, dal.sqlcmds.AllResourcesBooked);
                    //Available
                    dal.Gridview(dataGridView1, dal.sqlcmds.AvailableResourcesByType);

                   
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
                dal.CancelBookingID = Convert.ToInt32(comboBox6.Text);
                dal.CancelReservation();
            }
            catch
            {
                MessageBox.Show("Select ID");
            }
        }
    }
}
