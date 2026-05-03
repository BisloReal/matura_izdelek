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
    public partial class Odhodi : UserControl, IRefreshable
    {
        private DatabaseHelper db;
        public Odhodi(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;
            naloziOdhode();
        }

        public void naloziOdhode()
        {
            flowLayoutPanel1.Controls.Clear();
            List<OdhodiRazred> odhodi = db.PridobiOdhode();
            foreach (OdhodiRazred o in odhodi)
            {
                flowLayoutPanel1.Controls.Add(new OdhodCard(db, o, this));
            }
        }


        public void Posodobi()
        {
            naloziOdhode();
        }

        private void buttonDodajOdhod_Click(object sender, EventArgs e)
        {
            try
            {
                DodajOdhodForm dodajOdhodForm = new DodajOdhodForm(db, null);
                if (dodajOdhodForm.ShowDialog() == DialogResult.OK)
                {
                    naloziOdhode();
                    MessageBox.Show("Odhod uspešno dodan.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju odhoda: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
