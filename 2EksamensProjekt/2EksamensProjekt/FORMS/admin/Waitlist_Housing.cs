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

        private int MIN { get; set; }
        private int MAX { get; set; }

        public Waitlist_Housing()
        {
            InitializeComponent();
            Task t1 = new Task(() => gridViewTimer());
            radioButton1.Checked = true;
            MIN = int.MinValue;
            MAX = int.MaxValue;
            
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!radioButton3.Checked)
                {
                    MIN = Convert.ToInt32(textBox3.Text);
                    MAX = Convert.ToInt32(textBox2.Text);
                }
                dal.bypassDatatableUpdate = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void gridViewTimer()
        {
            do
            {
                try
                {
                    string sql = "NONE";
                    if (radioButton3.Checked)
                    {
                         sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id  FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                    }
                    else if (radioButton1.Checked)
                    {
                        sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id  FROM housing_residents hr2) AND h.m2 BETWEEN {MIN} AND {MAX} GROUP BY h.id ORDER BY h.id;";

                    }
                    else if (radioButton2.Checked)
                    {
                        sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id  FROM housing_residents hr2) AND h.rental_price BETWEEN {MIN} AND {MAX} GROUP BY h.id ORDER BY h.id;";
                    }
                    dal.Gridview(dataGridView1, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;", false);
                    dal.Gridview(dataGridView2, sql, dal.bypassDatatableUpdate);
                    dal.Gridview(dataGridView3, "SELECT hr.housing_id, h.`type`, h.m2, h.rental_price, r.name, hr.start_contract, hr.residents_username  FROM housing h, housing_residents hr, residents r WHERE h.id = hr.housing_id and hr.residents_username = r.account_username;", false);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            while (true);
        }
    }
}
