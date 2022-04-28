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
    public partial class Resources : Form
    {
        DAL dal = DAL.Getinstance();
        private static Resources singleton = new Resources();

        public static string User { get; set; } = string.Empty;
        public static string Start { get; set; } = string.Empty;
        public static string End { get; set; } = string.Empty;
        public static string UnitID { get; set; } = string.Empty;
  
        public Resources()
        {
            InitializeComponent();  
            Task t1 = new Task(() => gridViewTimer());
            Task t2 = new Task(() => UpdateComboBox());
            t1.Start();
            t2.Start();
        }

        public static Resources GetInstance()
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
                string activeRButton = "NONE";

                dal.ComboBoxFill(comboBox3, "SELECT r.account_username FROM residents r ORDER BY r.account_username;", false);
                dal.ComboBoxFill(comboBox1, "SELECT DISTINCT CAST(rrr.start_timestamp AS DATE) FROM resident_resource_reservations rrr ORDER BY rrr.start_timestamp;", false);
                if (radioButton3.Checked)
                {
                    if (activeRButton != "radioButton3")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radiobutton3";
                    }
                    dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'washingmachine';", dal.bypassComboBoxFillCount);
                }
                else if (radioButton4.Checked)
                {
                    if(activeRButton != "radioButton4")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radiobutton4";
                    }
                    dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'partyhall';", dal.bypassComboBoxFillCount);
                }
                else if (radioButton5.Checked)
                {
                    if (activeRButton != "radioButton5")
                    {
                        dal.bypassComboBoxFillCount = true;
                        activeRButton = "radiobutton5";
                    }
                    dal.ComboBoxFill(comboBox4, "SELECT r.id FROM resource r WHERE r.type = 'parkingspace';", dal.bypassComboBoxFillCount);
                }
            }
            while (true);
        }

        private void gridViewTimer()
        {
            do
            {
                try
                {
                    dal.Gridview(dataGridView4, "SELECT a.username, r.Name, r2.type, r2.id, rrr.start_timestamp, rrr.duration_in_minutes FROM resident_resource_reservations rrr, residents r, account a, resource r2 WHERE rrr.residents_username = r.account_username AND rrr.resource_id = r2.id AND r.account_username = a.username AND rrr.start_timestamp > DATE_SUB(NOW(), INTERVAL rrr.duration_in_minutes MINUTE) ORDER BY rrr.start_timestamp;", false);
                    
                    if (radioButton1.Checked) //Per User
                    {
                        dal.Gridview(dataGridView5, "", false);
                    }
                    if (radioButton2.Checked) //Per Unit
                    {
                        if (radioButton3.Checked) //Per Unit Washing Machine
                        {

                        }
                        if (radioButton4.Checked) //Per Unit Party Hall
                        {

                        }
                        if (radioButton5.Checked) //Per Unit Parking Space
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Start = comboBox1.Text;
                End = comboBox2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input Values Into Start & End");
            }
            
        }
    }
}
