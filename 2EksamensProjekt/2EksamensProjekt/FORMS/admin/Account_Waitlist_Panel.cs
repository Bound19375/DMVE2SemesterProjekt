using _2EksamensProjekt.FORMS.secretary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2EksamensProjekt.FORMS.admin
{
    using DAL;
    public partial class Account_Waitlist_Panel : Form
    {
        private static Account_Waitlist_Panel singleton = new Account_Waitlist_Panel();
        API api = API.Getinstance();
        private Account_Waitlist_Panel()
        {
            InitializeComponent();
            Task t2 = new Task(() => gridViewTimer());
            t2.Start();
        }

        public static Account_Waitlist_Panel GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void gridViewTimer()
        {
            do
            {
                api.Gridview(dataGridView2, api.sqlcmds.Waitlist, false);
                api.Gridview(dataGridView1, api.sqlcmds.CurrentResidents, false);
            }
            while (true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserCreateWaitlist obj = new UserCreateWaitlist();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            api.SecretaryPrint();
        }
    }
}
