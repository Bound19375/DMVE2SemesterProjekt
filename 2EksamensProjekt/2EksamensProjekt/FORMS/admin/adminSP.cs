namespace _2EksamensProjekt.FORMS.admin
{
    using _2EksamensProjekt.FORMS.secretary;
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
            label5.Text = $"{dal.Username}";
            panel1.Controls.Clear();
            Housing myForm = Housing.GetInstance();
            myForm.TopLevel = false;
            myForm.AutoScroll = true;
            panel1.Controls.Add(myForm);
            myForm.Show();
        }

        public static adminSP GetInstance()
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
}
