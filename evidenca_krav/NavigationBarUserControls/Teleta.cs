using evidenca_krav.NavigationBar;
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
    public partial class Teleta : UserControl, IRefreshable
    {
        DatabaseHelper db;
        public Teleta(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;
            NaloziTeleta();
        }

        private void NaloziTeleta()
        {
            flowLayoutPanel1.Controls.Clear();
            List<TeliceRazred> teleta = db.PridobiTeleta();
            foreach (TeliceRazred t in teleta)
            {
                flowLayoutPanel1.Controls.Add(new TeleCard(db, t));
            }
        }

        public void Posodobi()
        {
            NaloziTeleta();
        }
    }
}
