namespace _2EksamensProjekt.FORMS.resident
{
    using DAL;
    public partial class residentSP : Form
    {
        DAL dal = DAL.Getinstance();
        private static residentSP singleton = new residentSP();
        private residentSP()
        {
            InitializeComponent();
            label5.Text = $"{dal.Username}";
        }

        public static residentSP GetInstance()
        {
            return singleton;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Login obj = Login.GetInstance();
            obj.Show();
            this.Hide();
        }
    }
}
