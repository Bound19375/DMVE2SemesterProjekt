namespace _2EksamensProjekt.FORMS;

public partial class UserCreateWaitlist : Form
{        
    API api = API.Getinstance();

    private static UserCreateWaitlist singleton = new UserCreateWaitlist(); 
    private UserCreateWaitlist()
    {
        InitializeComponent();
        comboBox1.Text = "normal";
        Task t1 = new Task(() => worker());
        t1.Start();
    }

    public static UserCreateWaitlist GetInstance()
    {
        return singleton;
    }

    private void worker()
    {
        do
        {
            api.ComboBoxReader(comboBox2, "CreateAccountUsername");
            api.ComboBoxReader(comboBox3, "Password");
            api.ComboBoxReader(comboBox1, "WaitlistType");
        }
        while (true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        api.CreateUser_Waitlist();
        this.Hide();
    }
}