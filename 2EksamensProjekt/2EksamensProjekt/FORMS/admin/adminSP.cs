namespace _2EksamensProjekt.FORMS.admin
{
    using DAL;
    using UnikAPI;
    public partial class adminSP : Form
    {
        DAL dal = DAL.Getinstance();
        API api = API.Getinstance();
        public adminSP()
        {
            InitializeComponent();
            textBox1.Text = $"Welcome: {dal.Username}";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Login.GetInstance().Show();

        }
    }
}
