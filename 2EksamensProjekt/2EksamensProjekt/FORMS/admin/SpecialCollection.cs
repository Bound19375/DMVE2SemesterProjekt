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
    using API;
    public partial class SpecialCollection : Form
    {
        API api = API.Getinstance();
        public SpecialCollection()
        {
            InitializeComponent();
            api.GridviewCollection(dataGridView1, api.SpecialCollectionSql);
        }
    }
}
