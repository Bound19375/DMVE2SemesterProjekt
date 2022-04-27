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
    public partial class Waitlist_Housing : Form
    {
        DAL dal = DAL.Getinstance();

        private static Waitlist_Housing singleton = new Waitlist_Housing();
        public Waitlist_Housing()
        {
            InitializeComponent();
            Task t1 = new Task(() => gridViewTimer());
            t1.Start();
        }

        public static Waitlist_Housing GetInstance()
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
                dal.Gridview(dataGridView1, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;");
            }
            while (true);
        }
    }
}
