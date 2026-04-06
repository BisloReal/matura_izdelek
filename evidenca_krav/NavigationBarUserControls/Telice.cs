using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBar
{
    public partial class Telice : UserControl
    {
        private DatabaseHelper db;
        public Telice(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;
        }

        private void buttonDodajTelico_Click(object sender, EventArgs e)
        {

        }
    }
}
