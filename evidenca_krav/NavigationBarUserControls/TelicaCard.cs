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
    public partial class TelicaCard : UserControl
    {
        TeliceRazred Telica;
        private DatabaseHelper db;
        public TelicaCard(DatabaseHelper dbHelper, TeliceRazred tel)
        {
            InitializeComponent();
            Telica = tel;
            db = dbHelper;

            labelIme.Text = Telica.ime;
            labelDatumRoj.Text = Telica.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.pasma;
            labelImeMame.Text = Telica.imeMame;
            labelImeOceta.Text = Telica.imeOceta;
            labelUsSt.Text = Telica.usesnaSt;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediTelicaForm urediTelicoForm = new UrediTelicaForm(db, Telica.id, this);

                if (urediTelicoForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();
                    
                    MessageBox.Show("Telica uspešno Posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telice: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PosodobiPodatke()
        {
            Telica = db.PridobiTelico(Telica.id);
            labelIme.Text = Telica.ime;
            labelDatumRoj.Text = Telica.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.pasma;
            labelImeMame.Text = Telica.imeMame;
            labelImeOceta.Text = Telica.imeOceta;
            labelUsSt.Text =  Telica.usesnaSt;
        }
    }
}
