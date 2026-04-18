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

            labelIme.Text = Telica.imeTel;
            labelDatumRoj.Text = Telica.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.pasmaTel;
            labelImeMame.Text = Telica.imeMameTel;
            labelImeOceta.Text = Telica.imeOcetaTel;
            labelUsSt.Text = Telica.usesnaStTel;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediTelicaForm dodajTelicoForm = new UrediTelicaForm(db, Telica.idTel, this);

                if (dodajTelicoForm.ShowDialog() == DialogResult.OK)
                {
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
            Telica = db.PridobiTelico(Telica.idTel);
            labelIme.Text = Telica.imeTel;
            labelDatumRoj.Text = Telica.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Telica.pasmaTel;
            labelImeMame.Text = Telica.imeMameTel;
            labelImeOceta.Text = Telica.imeOcetaTel;
            labelUsSt.Text =  Telica.usesnaStTel;
        }
    }
}
