namespace _2EksamensProjekt.FORMS.secretary;

public partial class secretarySP : Form
{
    private static readonly secretarySP Singleton = new();
    private readonly API _api = API.GetInstance();
    private secretarySP()
    {
        InitializeComponent();
        label5.Text = $@"{_api.AccountUsername}";
        Task t2 = new(GridViewTimer);
        t2.Start();
    }

    public static secretarySP GetInstance()
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

    private void GridViewTimer()
    {
        do
        {
            _api.Gridview(dataGridView2, _api.sqlcmds.Waitlist, false);
            _api.Gridview(dataGridView1, _api.sqlcmds.CurrentResidents, false);
                
        }
        while(true);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        UserCreateWaitlist obj = UserCreateWaitlist.GetInstance();
        obj.Show();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        _api.SecretaryPrint();
    }
}