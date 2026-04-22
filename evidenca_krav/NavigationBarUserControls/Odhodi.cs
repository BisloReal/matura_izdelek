using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class Odhodi : UserControl
    {
        private DatabaseHelper db;
        public Odhodi(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;
        }
    }
}
