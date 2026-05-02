using evidenca_krav.NavigationBar;
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
    public partial class TeleCard : UserControl
    {
        TeliceRazred Tele;
        private DatabaseHelper db;
        public TeleCard(DatabaseHelper dbHelper, TeliceRazred tele)
        {
            InitializeComponent();
            db = dbHelper;
            Tele = tele;

            labelIme.Text = Tele.Ime;
            labelDatumRoj.Text = Tele.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Tele.Pasma;
            labelImeMame.Text = Tele.ImeMame;
            labelImeOceta.Text = Tele.ImeOceta;
            labelUsSt.Text = Tele.UsesnaSt;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediTeleForm dodajTeleForm = new UrediTeleForm(db, Tele.Id, this);

                if (dodajTeleForm.ShowDialog() == DialogResult.OK)
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
            Tele = db.PridobiTelico(Tele.Id);
            labelIme.Text = Tele.Ime;
            labelDatumRoj.Text = Tele.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Tele.Pasma;
            labelImeMame.Text = Tele.ImeMame;
            labelImeOceta.Text = Tele.ImeOceta;
            labelUsSt.Text = Tele.UsesnaSt;
        }
    }
}
