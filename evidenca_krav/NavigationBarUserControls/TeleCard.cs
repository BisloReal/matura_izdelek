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

            labelIme.Text = Tele.ime;
            labelDatumRoj.Text = Tele.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Tele.pasma;
            labelImeMame.Text = Tele.imeMame;
            labelImeOceta.Text = Tele.imeOceta;
            labelUsSt.Text = Tele.usesnaSt;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediTeleForm dodajTeleForm = new UrediTeleForm(db, Tele.id, this);

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
            Tele = db.PridobiTelico(Tele.id);
            labelIme.Text = Tele.ime;
            labelDatumRoj.Text = Tele.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Tele.pasma;
            labelImeMame.Text = Tele.imeMame;
            labelImeOceta.Text = Tele.imeOceta;
            labelUsSt.Text = Tele.usesnaSt;
        }
    }
}
