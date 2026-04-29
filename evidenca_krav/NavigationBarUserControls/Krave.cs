using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
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
    public partial class Krave : UserControl, IRefreshable
    {
        private DatabaseHelper db;

        public Krave(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            naloziKrave();
        }


        private void naloziKrave()
        {
            flowLayoutPanel1.Controls.Clear();
            List<KraveRazred> krave = db.PridobiKrave();
            foreach (KraveRazred k in krave)
            {
                flowLayoutPanel1.Controls.Add(new KravaCard(db, k));
            }
        }

        public void Posodobi()
        {
            naloziKrave();
        }
    }
}
