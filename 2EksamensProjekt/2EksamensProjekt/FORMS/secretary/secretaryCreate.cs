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
    public partial class SecretaryCreate : Form
    {
        public static string? Username { get; set; }
        public static string? Password { get; set; }
        public static string? Type { get; set; }
        DAL dal = DAL.Getinstance();

        public SecretaryCreate()
        {
            InitializeComponent();
            comboBox1.Text = "normal";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = textBox1.Text;
            Password = textBox2.Text;
            Type = comboBox1.Text;
            dal.SecretaryCreateUser();
            this.Close();
        }
    }
}
