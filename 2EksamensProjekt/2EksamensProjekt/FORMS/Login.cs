namespace _2EksamensProjekt.FORMS;

public partial class Login : Form
{
    private readonly API _api = API.GetInstance();
    private static readonly Login Singleton = new();

    private Login()
    {
        InitializeComponent();
        new Task(Slogan).Start(); //Create an instance of a Task & Start it
    }

    public static Login GetInstance() //Login Form Made Singleton Due To Otherwise Disposion Of Objects --Garbage Collector--.
    {
        return Singleton;
    }

/*
    private void formClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Environment.Exit(0);
    }
*/

    private async void Slogan()
    {
        do
        { 
            if (label1.IsDisposed == false)
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                    {
                        label1.Text = _api.SloganT().Result; //Calling Async Task SloganT Method From Api Class.
                    });
                    await Task.Delay(1000); //Sleeping For X Seconds.
                }
            }
            else
            {
                label1.CreateControl();
            }
        }
        while (true); //Keep Task Running While Login Form Is The ActiveForm
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string result = _api.Login(textBox1.Text, textBox2.Text).Result; //Calling Async Task Login Method From DAL Class.
        switch (result)
        {
            //MessageBox.Show(result);
            case "admin":
                Hide();
                adminSP.GetInstance().Show();
                break;
            case "secretary":
                Hide();
                secretarySP.GetInstance().Show();
                break;
            case "youth":
            case "senior":
            case "normal":
                Hide();
                residentSP.GetInstance().Show();
                break;
            default:
                MessageBox.Show(result);
                break;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        MessageBox.Show(textBox1.Text != String.Empty ? _api.GetPassword(textBox1.Text).Result : @"Enter Username");
    }
}