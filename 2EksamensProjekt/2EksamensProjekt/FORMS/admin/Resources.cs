namespace _2EksamensProjekt.FORMS.admin;

public partial class Resources : Form
{
    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();
    private static readonly Resources Singleton = new();

    private Resources()
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
        groupBox2.Text = @"washingmachine";
        comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        Task t2 = new(Worker);
        t2.Start();
    }

    public static Resources GetInstance()
    {
        return Singleton;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
        
    }

    private void Worker()
    {
        do
        {
            try
            {
                //GroupBoxUnitChoice
                _api.GroupboxReader(groupBox2, API.SetReaderField.AvailableType);
                //StartDate
                _api.ComboBoxReader(comboBox1, API.SetReaderField.Start);
                //EndDate
                _api.ComboBoxReader(comboBox2, API.SetReaderField.End);
                //User
                _api.ComboBoxReader(comboBox3, API.SetReaderField.AccountUsername);
                //UnitID
                _api.ComboBoxReader(comboBox4, API.SetReaderField.UnitID);
                //DurationTime
                _api.ComboBoxReader(comboBox5, API.SetReaderField.Duration);
                //CancelBookingID
                _api.ComboBoxReader(comboBox6, API.SetReaderField.CancelBookingID);

                //Usernames
                _api.ComboBoxFill(comboBox3, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.Usernames));
                //StartDate
                _api.ComboBoxFill(comboBox1, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.StartDate));
                //EndDate
                _api.ComboBoxFill(comboBox2, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.EndDate));
                //Booking Cancel IDS
                _api.ComboBoxFill(comboBox6, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.BookingCancelIDs));

                if (radioButton3.Checked) //WashingMachines
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.WMSORTALL));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 4);
                }
                else if (radioButton4.Checked) //PartyHall
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.PHSortAll));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 24);
                }
                else if (radioButton5.Checked) // ParkingSpace
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.PSSortAll));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 48);
                }

                //Booked
                _api.Gridview(dataGridView4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AllResourcesBooked), true);
                //Available
                _api.Gridview(dataGridView1, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableResourcesByType), true);

                if (radioButton6.Checked) //All User
                {
                    _api.Gridview(dataGridView5, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.ResourceSortAllUsers), true);

                }
                else if (radioButton7.Checked) //All Per Unit (Count)
                {
                    _api.Gridview(dataGridView5, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.ResourceSortAllPerUnit), true);
                }
                else if (radioButton1.Checked) //Per User
                {
                    _api.Gridview(dataGridView5, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.ResourceSortPerUser), true);
                }
                else if (radioButton2.Checked) //Per Unit
                {
                    _api.Gridview(dataGridView5, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.ResourceSortPerUnit), true);
                }
                    
                //Invokers
                if (radioButton8.Checked) //Sort
                {
                    _api.ButtonInvoker(button3, false);
                    _api.GroupBoxInvoker(groupBox1, true);
                    _api.ComboBoxInvoker(comboBox3, false);
                    _api.ComboBoxInvoker(comboBox1, false);
                    _api.ComboBoxInvoker(comboBox2, false);
                    _api.ComboBoxInvoker(comboBox4, false);
                    _api.ComboBoxInvoker(comboBox5, false);

                    if (radioButton1.Checked) // Per User
                    {
                        _api.ComboBoxInvoker(comboBox3, true);
                        _api.ComboBoxInvoker(comboBox1, true);
                        _api.ComboBoxInvoker(comboBox2, true);
                    }

                    if (radioButton2.Checked) // Per Unit
                    {
                        _api.ComboBoxInvoker(comboBox4, true);
                        _api.ComboBoxInvoker(comboBox1, true);
                        _api.ComboBoxInvoker(comboBox2, true);
                    }

                    if (radioButton6.Checked || radioButton7.Checked) //All
                    {
                        _api.ComboBoxInvoker(comboBox3, false);
                        _api.ComboBoxInvoker(comboBox4, false);

                        if (radioButton7.Checked)
                        {
                            _api.GroupBoxInvoker(groupBox2, true);
                            _api.GroupboxReader(groupBox2, API.SetReaderField.AvailableType);
                        }
                    }
                }

                if (radioButton9.Checked) //Book
                {
                    _api.ButtonInvoker(button3, true);
                    _api.ComboBoxInvoker(comboBox2, false);
                    _api.ComboBoxInvoker(comboBox3, true);
                    _api.ComboBoxInvoker(comboBox5, true);
                    _api.ComboBoxInvoker(comboBox1, true);
                    _api.ComboBoxInvoker(comboBox4, true);
                    _api.GroupBoxInvoker(groupBox1, false);
                    _api.GroupBoxInvoker(groupBox2, true);
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
        while (true);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        if (comboBox3.Text != String.Empty && comboBox1.Text != String.Empty && Convert.ToDateTime(comboBox1.Text) >= DateTime.Now && comboBox4.Text != String.Empty && comboBox5.Text != String.Empty)
        {
            _api.Booking();
        }
        else
        {
            MessageBox.Show(@"Incorrect Information!");
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (radioButton6.Checked) //All User
            {
                _api.AdminStatisticsPrint(API.ResourceSort.AllUsers);
            }
            else if (radioButton7.Checked) //All Per Unit (Count)
            {
                _api.AdminStatisticsPrint(API.ResourceSort.AllPerUnit);
            }
            else if (radioButton1.Checked) //Per User
            {
                _api.AdminStatisticsPrint(API.ResourceSort.PerUser);
            }
            else if (radioButton2.Checked) //Per Unit
            {
                _api.AdminStatisticsPrint(API.ResourceSort.PerUnit);
            }
        }
        catch (Exception)
        {
            if (radioButton1.Checked)
            {
                MessageBox.Show($@"User: {_api.AccountUsername}
                                Start Date: {_api.Start}
                                End Date: {_api.End}
                                Unit Type: {_api.UnitType}
                                ARE REQUIRED TO SORT BY USER");
            }
            else if (radioButton2.Checked)
            {
                MessageBox.Show($@"Start Date: {_api.Start}
                                End Date: {_api.End}
                                Unit Type: {_api.UnitType}
                                Unit ID: {_api.UnitID}
                                ARE REQUIRED TO SORT BY UNIT");
            }
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        if (comboBox6.Text == String.Empty)
        {
            MessageBox.Show(@"Select ID");
        }
        else
        {
            _api.CancelReservation();
        }
    }
}