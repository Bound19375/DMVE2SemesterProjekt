using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2EksamensProjekt
{
    using DAL;
    public partial class AdminCreateHouse : Form
    {
        public static string HouseType { get; set; } = "NONE";
        public static int M2 { get; set; } = 0;
        public static int Price { get; set; } = 0;

        DAL dal = DAL.Getinstance();
        public AdminCreateHouse()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HouseType = comboBox1.Text;
            M2 = Convert.ToInt32(textBox1.Text);
            Price = Convert.ToInt32(textBox2.Text);
            dal.CreateNewHouse();
            this.Close();
        }
    }
}
