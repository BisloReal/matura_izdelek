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

            labelIme.Text = Telica.Ime;
            labelDatumRoj.Text = Telica.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.Pasma;
            labelImeMame.Text = Telica.ImeMame;
            labelImeOceta.Text = Telica.ImeOceta;
            labelUsSt.Text = Telica.UsesnaSt;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediTelicaForm urediTelicoForm = new UrediTelicaForm(db, Telica.Id, this);

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
            Telica = db.PridobiTelico(Telica.Id);
            labelIme.Text = Telica.Ime;
            labelDatumRoj.Text = Telica.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.Pasma;
            labelImeMame.Text = Telica.ImeMame;
            labelImeOceta.Text = Telica.ImeOceta;
            labelUsSt.Text =  Telica.UsesnaSt;
        }
    }
}
