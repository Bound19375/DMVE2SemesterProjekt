namespace _2EksamensProjekt.FORMS.admin;

public partial class Housing : Form
{
    private readonly API _api = API.GetInstance();
    private static readonly Housing Singleton = new();

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
                    sql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                }
                else if (radioButton1.Checked)
                {
                    sql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.m2 BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";

                }
                else if (radioButton2.Checked)
                {
                    sql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.rental_price BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";
                }
                _api.Gridview(dataGridView1, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;", false);
                _api.Gridview(dataGridView2, sql, true);
                _api.Gridview(dataGridView3, "SELECT hr.housing_id, h.`type`, h.m2, h.rental_price, r.name, hr.start_contract, hr.residents_username  FROM housing h, housing_residents hr, residents r WHERE h.id = hr.housing_id and hr.residents_username = r.account_username;", false);


                _api.ComboBoxFill(comboBox1, "SELECT h.id FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;");
                _api.ComboBoxFill(comboBox2, "SELECT w.account_username FROM waitlist w");
                _api.ComboBoxFill(comboBox4, _api.sqlcmds.CurrentResidentsUsername);

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
        AdminCreateHouse obj = new();
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
                _api.SpecialCollectionSql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
            }
            else if (radioButton1.Checked)
            {
                _api.SpecialCollectionSql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.m2 BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";

            }
            else if (radioButton2.Checked)
            {
                _api.SpecialCollectionSql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.rental_price BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";
            }
            SpecialCollection obj = new();
            obj.Show();
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