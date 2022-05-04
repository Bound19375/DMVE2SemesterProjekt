namespace _2EksamensProjekt.FORMS.admin;

public partial class Account_Waitlist_Panel : Form
{
    private static readonly Account_Waitlist_Panel Singleton = new();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();

    private readonly API _api = API.GetInstance();
    private Account_Waitlist_Panel()
    {
        InitializeComponent();
        Task t2 = new(GridViewTimer);
        t2.Start();
    }

    public static Account_Waitlist_Panel GetInstance()
    {
        return Singleton;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }

    private void GridViewTimer()
    {
        do
        {
            _api.Gridview(dataGridView2, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.Waitlist), false);
            _api.Gridview(dataGridView1, _sqlCMDS.SQLCMD(API.SQLCMDS.SELECTSQLQUERY.CurrentResidents), false);
        }
        while (true);
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