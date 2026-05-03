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

namespace evidenca_krav.NavigationBar
{
    public partial class Telice : UserControl, IRefreshable
    {
        private DatabaseHelper db;
        public Telice(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;
            NaloziTelice();
        }

        private void buttonDodajTelico_Click(object sender, EventArgs e)
        {
            try
            {
                DodajTelicoForm dodajTelicoForm = new DodajTelicoForm(db);

                if (dodajTelicoForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    NaloziTelice();
                    MessageBox.Show("Telica uspešno dodana!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telice: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NaloziTelice()
        {
            flowLayoutPanel1.Controls.Clear();

            List<TeliceRazred> telice = db.PridobiTelice();

            foreach (TeliceRazred t in telice)
            {
                flowLayoutPanel1.Controls.Add(new TelicaCard(db, t));
            }
        }

        public void Posodobi()
        {
            NaloziTelice();
        }
    }
}
