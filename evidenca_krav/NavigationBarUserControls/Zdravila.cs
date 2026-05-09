using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class Zdravila : UserControl
    {
        private DatabaseHelper db;

        public Zdravila(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            NaloziZdravila();
        }

        private void buttonDodajZdravilo_Click(object sender, EventArgs e)
        {
            try
            {
                DodajZdraviloForm dodajZdraviloForm = new DodajZdraviloForm(db);

                if (dodajZdraviloForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Zdravilo uspešno dodano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziZdravila();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju zdravila: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NaloziZdravila()
        {
            flowLayoutPanelZdravila.Controls.Clear();

            List<ZdravilaRazred> zdravila = db.PridobiZdravila();

            foreach (ZdravilaRazred z in zdravila)
            {
                flowLayoutPanelZdravila.Controls.Add(new ZdravilaCard(db, z));
            }
        }
    }
}