namespace _2EksamensProjekt.FORMS.admin;

public partial class Account_Waitlist_Panel : Form
{
    private static readonly Account_Waitlist_Panel Singleton = new();
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
            _api.Gridview(dataGridView2, _api.sqlcmds.Waitlist, false);
            _api.Gridview(dataGridView1, _api.sqlcmds.CurrentResidents, false);
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