namespace _2EksamensProjekt
{
    using DAL;
    using UnikAPI;
    public partial class Login : Form
    {
        DAL dal = DAL.Getinstance(); 
        API api = API.Getinstance();

        public Login()
        {
            InitializeComponent();
            Task slogan = new Task(() => Slogan());
            slogan.Start(); //Create an instance of a Task & Start it
        }

        private async void Slogan()
        {
            do
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                    {
                        label1.Text = api.SloganT().Result; //Calling Async Task SloganT Method From Api Class.
                    });
                    await Task.Delay(1000); //Sleeping For X Seconds.
                }
            }
            while (Form.ActiveForm == Login.ActiveForm); //Keep Task Running While Login Form Is The ActiveForm
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = dal.Login(textBox1.Text, textBox2.Text).Result; //Calling Async Task Login Method From DAL Class.
            MessageBox.Show(result);


            //this.Close();
        }

    }
}