using evidenca_krav.NavigationBarUserControls;
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

namespace evidenca_krav.NavigationBar
{
    public partial class Glavno_okno : UserControl
    {
        private DatabaseHelper db;
        public Glavno_okno(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;
            NaloziObvestila();
        }

        private void buttonPosodobi_Click(object sender, EventArgs e)
        {
            db.PosodobiStanja();
            NaloziObvestila();
        }

        private void NaloziObvestila()
        {
            flowLayoutPanelObvestila.Controls.Clear();

            List<ObvestiloRazred> obvestila = db.PridobiObvestila();

            foreach (ObvestiloRazred ob in obvestila)
            {
                flowLayoutPanelObvestila.Controls.Add(new ObvestiloCard(ob));
            }
        }
    }
}
