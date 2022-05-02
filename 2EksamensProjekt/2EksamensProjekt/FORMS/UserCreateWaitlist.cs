using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace _2EksamensProjekt.FORMS.secretary
{
    public partial class UserCreateWaitlist : Form
    {
        public static string? Username { get; set; }
        public static string? Password { get; set; }
        public static string? Type { get; set; }
        API api = API.Getinstance();

        public UserCreateWaitlist()
        {
            InitializeComponent();
            comboBox1.Text = "normal";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = textBox1.Text;
            Password = textBox2.Text;
            Type = comboBox1.Text;
            api.CreateUser_Waitlist();
            this.Close();
        }
    }
}
