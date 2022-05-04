namespace _2EksamensProjekt.FORMS.resident;

public partial class residentSP : Form
{
    private readonly API _api = API.GetInstance();
    private static readonly residentSP Singleton = new();
    private residentSP()
    {
        InitializeComponent();
        label5.Text = $@"{_api.AccountUsername}";
        radioButton3.Checked = true;
        comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        groupBox2.Text = @"washingmachine";
        Task t2 = new(Worker);
        t2.Start();
    }

    public static residentSP GetInstance()
    {
        return Singleton;
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        Login obj = Login.GetInstance();
        obj.Show();
        Hide();
    }

    private void Worker()
    {
        do
        {
            try
            {
                //GroupBoxUnitChoice
                _api.GroupboxReader(groupBox2, "AvailableType");
                //StartDate
                _api.ComboBoxReader(comboBox1, "Start");
                //UnitID
                _api.ComboBoxReader(comboBox4, "UnitID");
                //DurationTime
                _api.ComboBoxReader(comboBox5, "Duration");
                //CancelBookingID
                _api.ComboBoxReader(comboBox6, "CancelBookingID");
                //AccountNameUpdater
                _api.ComboBoxReader(comboBox2, "NewAccountUsername");
                //Password
                _api.ComboBoxReader(comboBox3, "Password");

                if (label1.InvokeRequired)
                {
                    label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                    {
                        label5.Text = _api.AccountUsername; //Calling Async Task SloganT Method From Api Class.
                    });
                }

                //StartDate
                _api.ComboBoxFill(comboBox1, _api.sqlcmds.StartDate);
                //Booking Cancel IDS
                _api.ComboBoxFill(comboBox6, _api.sqlcmds.BookingCancelIDs);

                if (radioButton3.Checked) //WashingMachines
                {
                    _api.ComboBoxFill(comboBox4, _api.sqlcmds.AvailableResourceIDS);
                    _api.ComboBoxFillNoSqlInt(comboBox5, 4);
                }
                else if (radioButton4.Checked) //PartyHall
                {
                    _api.ComboBoxFill(comboBox4, _api.sqlcmds.AvailableResourceIDS);
                    _api.ComboBoxFillNoSqlInt(comboBox5, 24);
                }
                else if (radioButton5.Checked) // ParkingSpace
                {
                    _api.ComboBoxFill(comboBox4, _api.sqlcmds.AvailableResourceIDS);
                    _api.ComboBoxFillNoSqlInt(comboBox5, 48);
                }

                //Booked
                _api.Gridview(dataGridView4, _api.sqlcmds.AllResourcesBooked, true);
                //Available
                _api.Gridview(dataGridView1, _api.sqlcmds.AvailableResourcesByType, true);
                //Resident Information
                _api.Gridview(dataGridView5, _api.sqlcmds.CurrentResidentInfo, true);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
        while (true);
    }

    private void button7_Click(object sender, EventArgs e)
    {
        try
        {
            _api.CancelReservation();
        }
        catch
        {
            MessageBox.Show(@"Select ID");
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (comboBox2.Text != String.Empty && comboBox3.Text != String.Empty)
        {
            _api.UpdateUsername();
            _api.UpdatePassword();
            MessageBox.Show(@"Account Updated Successfully

Logging Out!");
            Login obj = Login.GetInstance();
            obj.Show();
            Hide();
        }
        else
        {
            if (comboBox2.Text == String.Empty && comboBox3.Text != String.Empty)
            {
                MessageBox.Show(@"Account Field Cannot Be Empty");
            }
            if (comboBox3.Text == String.Empty && comboBox2.Text != String.Empty)
            {
                MessageBox.Show(@"Password Field Cannot Be Empty");
            }
            if (comboBox3.Text == String.Empty && comboBox2.Text == String.Empty)
            {
                MessageBox.Show(@"Both Fields Must Be Filled");
            }
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (comboBox1.Text != String.Empty && Convert.ToDateTime(comboBox1.Text) >= DateTime.Now && comboBox4.Text != String.Empty && comboBox5.Text != String.Empty)
        {
            _api.Booking();
        }
        else
        {
            MessageBox.Show(@"Incorrect Information!");
        }
    }
}