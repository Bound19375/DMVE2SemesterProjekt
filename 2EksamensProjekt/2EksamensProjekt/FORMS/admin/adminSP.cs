namespace _2EksamensProjekt.FORMS.admin
{
    using DAL;
    using UnikAPI;
    public partial class adminSP : Form
    {
        DAL dal = DAL.Getinstance();
        API api = API.Getinstance();

        private static adminSP singleton = new adminSP();
        private adminSP()
        {
            InitializeComponent();
            textBox1.Text = $"Welcome: {dal.Username}";
        }

        public static adminSP GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Login obj = Login.GetInstance();
            obj.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login.GetInstance().Show();

        }
    }
}
