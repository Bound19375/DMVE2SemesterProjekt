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

    private void Worker()
    {
        do
        {
            _api.ComboBoxReader(comboBox2, API.SetReaderField.CreateAccountUsername);
            _api.ComboBoxReader(comboBox3, API.SetReaderField.Password);
            _api.ComboBoxReader(comboBox1, API.SetReaderField.WaitlistType);
        }
        while (true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        _api.CreateUser_Waitlist();
        Hide();
    }
}