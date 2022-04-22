namespace _2EksamensProjekt
{
    using DAL;
    public partial class Login : Form
    {
        DAL dal = DAL.Getinstance();

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = dal.Login(textBox1.Text, textBox2.Text).Result;
            MessageBox.Show(result);


            //this.Close();
        }
    }
}