namespace _2EksamensProjekt.FORMS;

public partial class UserCreateWaitlist : Form
{
    private readonly API _api = API.GetInstance();

    private static readonly UserCreateWaitlist Singleton = new(); 
    private UserCreateWaitlist()
    {
        InitializeComponent();
        comboBox1.Text = @"normal";
        Task t1 = new(Worker);
        t1.Start();
    }

    public static UserCreateWaitlist GetInstance()
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
            _api.ComboBoxReader(comboBox2, API.SetReaderField.CreateAccountUsername);
            _api.ComboBoxReader(comboBox3, API.SetReaderField.Password);
            _api.ComboBoxReader(comboBox1, API.SetReaderField.WaitlistType);
            _api.ComboBoxReader(comboBox4, API.SetReaderField.AccountPhoneNumer);
            _api.ComboBoxReader(comboBox5, API.SetReaderField.AccountName);
            _api.ComboBoxReader(comboBox6, API.SetReaderField.AccountSurname);

        }
        while (true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Regex regex = new(@"^[a-zA-Z0-9\sæøåÆØÅ]+$"); //Input Validation
        if (regex.IsMatch(comboBox1.Text) && regex.IsMatch(comboBox2.Text) && regex.IsMatch(comboBox3.Text) && regex.IsMatch(comboBox4.Text) && regex.IsMatch(comboBox5.Text) && regex.IsMatch(comboBox6.Text))
        {
            _api.CreateUserWaitlist();
            Hide();
        }
        else
        {
            MessageBox.Show("Oh you naughty sql injection tryharder!");
        }
    }
}