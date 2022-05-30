namespace _2EksamensProjekt.FORMS.secretary;

public partial class secretarySP : Form
{
    private static readonly secretarySP Singleton = new();
    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();

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
            _api.Gridview(dataGridView2, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.Waitlist), false, DataGridViewAutoSizeColumnMode.Fill);
            _api.Gridview(dataGridView1, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.CurrentResidents), false, DataGridViewAutoSizeColumnMode.Fill);
                
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