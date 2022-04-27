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
            label5.Text = $"{dal.Username}";
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
                dal.Gridview(dataGridView2, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;", dal.bypassDatatableUpdate);
                dal.Gridview(dataGridView1, "SELECT a.username, h.type, r.Name, hr.start_contract, h.m2, h.rental_price FROM housing_residents hr, residents r, housing h, account a WHERE hr.residents_username  = r.account_username AND hr.housing_id = h.id AND r.account_username = a.username ORDER BY a.username;", dal.bypassDatatableUpdate);
            }
            while(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserCreateWaitlist obj = new UserCreateWaitlist();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dal.SecretaryPrint();
        }
    }
}
