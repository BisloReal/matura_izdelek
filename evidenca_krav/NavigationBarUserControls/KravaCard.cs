using evidenca_krav.NavigationBar;
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

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class KravaCard : UserControl
    {
        KraveRazred Krava;
        private DatabaseHelper db;
        public KravaCard(DatabaseHelper dbHelper, KraveRazred krava)
        {
            InitializeComponent();
            db = dbHelper;
            Krava = krava;

            labelIme.Text = Krava.ime;
            labelDatumRoj.Text = Krava.datumRoj.ToString("dd.MM.yyyy");
            labelUsSt.Text = Krava.usesnaSt;
            labelPasma.Text = Krava.pasma;
            labelImeMame.Text = Krava.imeMame;
            labelImeOceta.Text = Krava.imeOceta;
            if (krava.laktacija == null)
            {
                labelLaktacija.Text = "/";
            }
            else
            {
                labelLaktacija.Text = Krava.laktacija;
            }
        }

        private void buttonUrediKrav_Click(object sender, EventArgs e)
        {
            try
            {
                UrediKravaForm urediKravaForm = new UrediKravaForm(db, Krava.id, this);

                if (urediKravaForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Krava uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju krave: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void PosodobiPodatke()
        {
            Krava = db.PridobiKravo(Krava.id);
            labelIme.Text = Krava.ime;
            labelDatumRoj.Text = Krava.datumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Krava.pasma;
            labelImeMame.Text = Krava.imeMame;
            labelImeOceta.Text = Krava.imeOceta;
            labelUsSt.Text = Krava.usesnaSt;
        }
    }
}
