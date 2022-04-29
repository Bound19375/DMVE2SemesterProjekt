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
    public partial class Housing : Form
    {
        DAL dal = DAL.Getinstance();
        private static Housing singleton = new Housing();

        public static int MIN { get; set; } = int.MinValue;
        public static int MAX { get; set; } = int.MaxValue;
        public static string AccountUsername { get; set; } = String.Empty;
        public static string HouseID { get; set; } = string.Empty;
        public static string AccountName { get; set; } = String.Empty;
        public static string SpecialCollectionSql { get; set; } = String.Empty;

        public Housing()
        {
            InitializeComponent();
            Task t1 = new Task(() => gridViewTimer());
            Task t2 = new Task(() => UpdateComboBox());
            radioButton3.Checked = true;
            t1.Start();
            t2.Start();
        }

        public static Housing GetInstance()
        {
            return singleton;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void UpdateComboBox()
        {
            do
            {
                dal.ComboBoxFill(comboBox1, "SELECT h.id FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;");
                dal.ComboBoxFill(comboBox2, "SELECT w.account_username FROM waitlist w");
            }
            while (true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!radioButton3.Checked)
                {
                    MIN = Convert.ToInt32(textBox3.Text);
                    MAX = Convert.ToInt32(textBox2.Text);
                    dal.bypassDatatableUpdate = true;
                }
                dal.bypassDatatableUpdate = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Input Values Into Min & Max");
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
                        sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                    }
                    else if (radioButton1.Checked)
                    {
                        sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.m2 BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";

                    }
                    else if (radioButton2.Checked)
                    {
                        sql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.rental_price BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";
                    }
                    dal.Gridview(dataGridView1, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;", false);
                    dal.Gridview(dataGridView2, sql, dal.bypassDatatableUpdate);
                    dal.Gridview(dataGridView3, "SELECT hr.housing_id, h.`type`, h.m2, h.rental_price, r.name, hr.start_contract, hr.residents_username  FROM housing h, housing_residents hr, residents r WHERE h.id = hr.housing_id and hr.residents_username = r.account_username;", false);
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountUsername = comboBox2.Text;
            HouseID = comboBox1.Text;
            AccountName = textBox1.Text;
            dal.GrantHousing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminCreateHouse obj = new AdminCreateHouse();
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HouseID = comboBox1.Text;
            dal.DeleteHouse();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!radioButton3.Checked)
                {
                    MIN = Convert.ToInt32(textBox3.Text);
                    MAX = Convert.ToInt32(textBox2.Text);
                    dal.bypassDatatableUpdate = true;
                }

                if (radioButton3.Checked)
                {
                    SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                }
                else if (radioButton1.Checked)
                {
                    SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.m2 BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";

                }
                else if (radioButton2.Checked)
                {
                    SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.rental_price BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";
                }
                SpecialCollection obj = new SpecialCollection();
                obj.Show();
            }
            catch (Exception ex)
            {
                if (!radioButton3.Checked)
                {
                    MessageBox.Show($"Input Values Into Min & Max:\n{ex}");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
