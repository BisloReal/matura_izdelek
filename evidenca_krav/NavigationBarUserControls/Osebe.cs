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
    public partial class Osebe : UserControl, IRefreshable
    {
        private DatabaseHelper db;
        public Osebe(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            NaloziOsebe();
        }


        public void Posodobi()
        {
            NaloziOsebe();
        }

        public void NaloziOsebe()
        {
            flowLayoutPanel1.Controls.Clear();
            List<OsebeRazred> osebe = db.PridobiOsebe();
            foreach (OsebeRazred o in osebe)
            {
                flowLayoutPanel1.Controls.Add(new OsebaCard(db, o));
            }
        }

        private void buttonDodajOsebo_Click(object sender, EventArgs e)
        {

        }
    }
}
