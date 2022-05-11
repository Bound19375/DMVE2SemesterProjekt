namespace _2EksamensProjekt.FORMS.admin;

public partial class AdminCreateHouse : Form
{
    

    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();

    private static AdminCreateHouse singleton = new AdminCreateHouse();

    private AdminCreateHouse()
    {
        InitializeComponent();
        Task t1 = new(Worker);
        t1.Start();
    }

    public static AdminCreateHouse GetInstance()
    {
        return singleton;
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
            //Reader
            _api.ComboBoxReader(comboBox5, API.SetReaderField.Address);
            _api.ComboBoxReader(comboBox6, API.SetReaderField.Zipcode);
            _api.ComboBoxReader(comboBox1, API.SetReaderField.HouseType);
            _api.ComboBoxReader(comboBox2, API.SetReaderField.M2);
            _api.ComboBoxReader(comboBox3, API.SetReaderField.Price);


            //Filler
            _api.ComboBoxFill(comboBox6, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.Zipcode));
        }
        while (true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Regex regex = new(@"^[a-zA-Z0-9\sæøåÆØÅ]+$"); //Input Validation
        if (regex.IsMatch(comboBox1.Text) && regex.IsMatch(comboBox2.Text) && regex.IsMatch(comboBox3.Text) && regex.IsMatch(comboBox5.Text) && regex.IsMatch(comboBox6.Text))
        {
            if (comboBox6.Items.Contains(comboBox6.Text))
            {
                _api.CreateNewHouse();
                Hide();
            }
            else
            {
                MessageBox.Show("Ukorrekt PostNr!");
            }
        }
        else
        {
            MessageBox.Show("Oh you naughty sql injection tryharder!");
        }
    }
}