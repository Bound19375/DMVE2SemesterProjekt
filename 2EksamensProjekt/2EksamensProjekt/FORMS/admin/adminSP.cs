namespace _2EksamensProjekt.FORMS.admin;

public partial class adminSP : Form
{
    private readonly API _api = API.GetInstance();

    private static readonly adminSP Singleton = new();

    private adminSP()
    {
        InitializeComponent();
        label5.Text = $@"{_api.AccountUsername}";
        panel1.Controls.Clear();
        Housing myForm = Housing.GetInstance();
        myForm.TopLevel = false;
        myForm.AutoScroll = true;
        panel1.Controls.Add(myForm);
        myForm.Show();
    }

    public static adminSP GetInstance()
    {
        return Singleton;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
        Login obj = Login.GetInstance();
        obj.Show();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        panel1.Controls.Clear();
        Account_Waitlist_Panel myForm = Account_Waitlist_Panel.GetInstance();
        myForm.TopLevel = false;
        myForm.AutoScroll = true;
        panel1.Controls.Add(myForm);
        myForm.Show();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        panel1.Controls.Clear();
        Housing myForm = Housing.GetInstance();
        myForm.TopLevel = false;
        myForm.AutoScroll = true;
        panel1.Controls.Add(myForm);
        myForm.Show();
    }

    private void button3_Click(object sender, EventArgs e)
    {
        panel1.Controls.Clear();
        Resources myForm = Resources.GetInstance();
        myForm.TopLevel = false;
        myForm.AutoScroll = true;
        panel1.Controls.Add(myForm);
        myForm.Show();
    }
}