namespace _2EksamensProjekt.FORMS.secretary
{
    public partial class secretarySP : Form
    {
        static secretarySP singleton = new secretarySP();
        API api = API.Getinstance();
        private secretarySP()
        {
            InitializeComponent();
            label5.Text = $"{api.AccountUsername}";
            Task t2 = new Task(() => gridViewTimer());
            t2.Start();
        }

        public static secretarySP GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Login obj = Login.GetInstance();
            obj.Show();
        }

        private void gridViewTimer()
        {
            do
            {
                api.Gridview(dataGridView2, api.sqlcmds.Waitlist, false);
                api.Gridview(dataGridView1, api.sqlcmds.CurrentResidents, false);
                
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
            api.SecretaryPrint();
        }
    }
}
