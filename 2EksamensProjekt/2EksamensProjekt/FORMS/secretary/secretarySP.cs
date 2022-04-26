using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2EksamensProjekt.FORMS.secretary
{
    using DAL;
    public partial class secretarySP : Form
    {
        static secretarySP singleton = new secretarySP();
        DAL dal = DAL.Getinstance();
        private secretarySP()
        {
            InitializeComponent();
            textBox1.Text = $"Welcome: {dal.Username}";
            Task t1 = new Task(() => GV());
            t1.Start();
        }

        public static secretarySP GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Login obj = Login.GetInstance();
            obj.Show();
            this.Hide();
        }

        private void GV()
        {
            do
            {
                if (dal.DBUpdateCheck().Result > DateTime.Now.AddMilliseconds(-1000) || dataGridView2.DataSource == null)
                {
                    if (dataGridView2.InvokeRequired)
                    {
                        dataGridView2.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                        {
                            dataGridView2.DataSource = dal.Datatable("SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_id = a.id ORDER BY a.username;").Result; //Calling Async Task SloganT Method From Api Class.
                        });
                    }
                }

                if (dal.DBUpdateCheck().Result > DateTime.Now.AddMilliseconds(-1000) || dataGridView1.DataSource == null)
                {
                    if (dataGridView1.InvokeRequired)
                    {
                        dataGridView1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                        {
                            dataGridView1.DataSource = dal.Datatable("SELECT a.username, h.type, r.Name, hr.start_date, h.m2, h.rental_price FROM housing_residents hr, residents r, housing h, account a WHERE hr.residents_id = r.account_id AND hr.housing_id = h.id AND r.account_id = a.id ORDER BY a.username;").Result; //Calling Async Task SloganT Method From Api Class.
                        });
                    }
                }
                Task.Delay(1000);
            }
            while (true); //Keep Task Running While Login Form Is The ActiveForm
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SecretaryCreate obj = new SecretaryCreate();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dal.SecretaryPrint();
        }
    }
}
