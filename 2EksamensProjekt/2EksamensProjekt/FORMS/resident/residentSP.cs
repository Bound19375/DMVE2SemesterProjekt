namespace _2EksamensProjekt.FORMS.resident;

public partial class residentSP : Form
{
    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();

    private static readonly residentSP Singleton = new();
    private residentSP()
    {
        InitializeComponent();
        comboBox7.Text = $@"{_api.AccountUsername}";
        radioButton3.Checked = true;
        comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ff");
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
                //Username
                _api.ComboBoxReader(comboBox7, API.SetReaderField.SortUsername);
                //GroupBoxUnitChoice
                _api.GroupboxReader(groupBox2, API.SetReaderField.AvailableType);
                //StartDate
                _api.ComboBoxReader(comboBox1, API.SetReaderField.Start);
                //UnitID
                _api.ComboBoxReader(comboBox4, API.SetReaderField.UnitID);
                //DurationTime
                _api.ComboBoxReader(comboBox5, API.SetReaderField.Duration);
                //CancelBookingID
                _api.ComboBoxReader(comboBox6, API.SetReaderField.CancelBookingID);
                //AccountNameUpdater
                _api.ComboBoxReader(comboBox2, API.SetReaderField.NewAccountUsername);
                //Password
                _api.ComboBoxReader(comboBox3, API.SetReaderField.Password);

                if (label1.InvokeRequired)
                {
                    label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                    {
                        comboBox6.Text = _api.AccountUsername; //Calling Async Task SloganT Method From Api Class.
                    });
                }

                //StartDate
                _api.ComboBoxFill(comboBox1, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.StartDate));
                //Booking Cancel IDS
                _api.ComboBoxFill(comboBox6, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.BookingCancelIDs));

                if (radioButton3.Checked) //WashingMachines
                {
                    _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    _api.ComboBoxFillNoSqlInt(comboBox5, 4);
                }
                else if (radioButton4.Checked) //PartyHall
                {
                    _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    _api.ComboBoxFillNoSqlInt(comboBox5, 24);
                }
                else if (radioButton5.Checked) // ParkingSpace
                {
                    _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    _api.ComboBoxFillNoSqlInt(comboBox5, 48);
                }

                //Booked
                _api.Gridview(dataGridView4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AllResourcesBooked), true, DataGridViewAutoSizeColumnMode.Fill);
                //Available
                _api.Gridview(dataGridView1, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourcesByType), true, DataGridViewAutoSizeColumnMode.Fill);
                //Resident Information
                _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.CurrentResidentInfo), true, DataGridViewAutoSizeColumnMode.Fill);
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
        _api.Booking();
    }
}