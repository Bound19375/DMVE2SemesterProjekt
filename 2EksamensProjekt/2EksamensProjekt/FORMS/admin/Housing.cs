namespace _2EksamensProjekt.FORMS.admin;

public partial class Housing : Form
{
    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();
    private readonly static Housing Singleton = new();

    private Housing()
    {
        InitializeComponent();
        Task t1 = new(Worker);
        radioButton3.Checked = true;
        t1.Start();
    }

    public static Housing GetInstance()
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
                string sql = "NONE";
                if (radioButton3.Checked)
                {
                    sql = _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableHouseSortAll);
                }
                else if (radioButton1.Checked)
                {
                    sql = _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableHouseSortByM2);

                }
                else if (radioButton2.Checked)
                {
                    sql = _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableHouseSortByPrice);

                }
                _api.Gridview(dataGridView1, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.Waitlist), false);
                _api.Gridview(dataGridView2, sql, true);
                _api.Gridview(dataGridView3, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.CurrentResidents), false);


                _api.ComboBoxFill(comboBox1, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.AvailableHouseIDs));
                _api.ComboBoxFill(comboBox2, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.UsernamesOnWaitinglist));
                _api.ComboBoxFill(comboBox4, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.CurrentResidentsUsername));

                if (!radioButton3.Checked)
                {
                    _api.TextboxReader(textBox3, API.SetReaderField.Min);
                    _api.TextboxReader(textBox2, API.SetReaderField.Max);
                }
                _api.ComboBoxReader(comboBox1, API.SetReaderField.HouseID);
                _api.ComboBoxReader(comboBox2, API.SetReaderField.AccountUsername);
                _api.ComboBoxReader(comboBox3, API.SetReaderField.AccountName);
                _api.ComboBoxReader(comboBox4, API.SetReaderField.DeleteFromSystemUsername);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
        while (true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        _api.GrantHousing();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        AdminCreateHouse obj = AdminCreateHouse.GetInstance();
        obj.Show();
    }

    private void button4_Click(object sender, EventArgs e)
    {
        _api.DeleteHouse();
    }

    private void button5_Click(object sender, EventArgs e)
    {
        try
        {

            if (radioButton3.Checked)
            {
                SpecialCollection obj = new();
                obj.ShowSpecialCollection(obj.dataGridView1, API.SPECIALCOLLECTION.SortByAll);
                obj.Show();
            }
            else if (radioButton1.Checked)
            {
                SpecialCollection obj = new();
                obj.ShowSpecialCollection(obj.dataGridView1, API.SPECIALCOLLECTION.SortByM2);
                obj.Show();

            }
            else if (radioButton2.Checked)
            {
                SpecialCollection obj = new();
                obj.ShowSpecialCollection(obj.dataGridView1, API.SPECIALCOLLECTION.SortByPrice);
                obj.Show();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(!radioButton3.Checked
                ? $@"Input Values Into Min & Max:
{ex}"
                : ex.Message);
        }
    }

    private void button2_Click_1(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(comboBox2.Text))
            {
                _api.DeleteWaitlistAccount();
                comboBox2.Items.Clear();
            }
            else
            {
                MessageBox.Show(@"Specify Username");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void button6_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(comboBox4.Text))
            {
                _api.DeleteResidentAccount();
                comboBox4.Items.Clear();
            }
            else
            {
                MessageBox.Show(@"Specify Username");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}