namespace _2EksamensProjekt.FORMS.admin
{
    public partial class Housing : Form
    {
        API api = API.Getinstance();
        private static Housing singleton = new Housing();

        public Housing()
        {
            InitializeComponent();
            Task t1 = new Task(() => Worker());
            radioButton3.Checked = true;
            t1.Start();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                MessageBox.Show("Input Values Into Min & Max");
            }
        }

        private void Worker()
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
                    api.Gridview(dataGridView1, "SELECT a.username, w.type FROM waitlist w, account a WHERE w.account_username = a.username ORDER BY a.username;", false);
                    api.Gridview(dataGridView2, sql, true);
                    api.Gridview(dataGridView3, "SELECT hr.housing_id, h.`type`, h.m2, h.rental_price, r.name, hr.start_contract, hr.residents_username  FROM housing h, housing_residents hr, residents r WHERE h.id = hr.housing_id and hr.residents_username = r.account_username;", false);


                    api.ComboBoxFill(comboBox1, "SELECT h.id FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;");
                    api.ComboBoxFill(comboBox2, "SELECT w.account_username FROM waitlist w");

                    if (!radioButton3.Checked)
                    {
                        api.TextboxReader(textBox3, "MIN");
                        api.TextboxReader(textBox2, "MAX");
                    }
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
            api.AccountUsername = comboBox2.Text;
            api.HouseID = comboBox1.Text;
            api.AccountName = textBox1.Text;
            api.GrantHousing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminCreateHouse obj = new AdminCreateHouse();
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            api.HouseID = comboBox1.Text;
            api.DeleteHouse();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (radioButton3.Checked)
                {
                    api.SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                }
                else if (radioButton1.Checked)
                {
                    api.SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.m2 BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";

                }
                else if (radioButton2.Checked)
                {
                    api.SpecialCollectionSql = $"SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) AND h.rental_price BETWEEN @min AND @max GROUP BY h.id ORDER BY h.id;";
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
