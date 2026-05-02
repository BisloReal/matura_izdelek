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

            labelIme.Text = Krava.Ime;
            labelDatumRoj.Text = Krava.DatumRoj.ToString("dd.MM.yyyy");
            labelUsSt.Text = Krava.UsesnaSt;
            labelPasma.Text = Krava.Pasma;
            labelImeMame.Text = Krava.ImeMame;
            labelImeOceta.Text = Krava.ImeOceta;
            if (krava.Laktacija == null)
            {
                labelLaktacija.Text = "/";
            }
            else
            {
                labelLaktacija.Text = Krava.Laktacija;
            }
        }

        private void buttonUrediKrav_Click(object sender, EventArgs e)
        {
            try
            {
                UrediKravaForm urediKravaForm = new UrediKravaForm(db, Krava.Id, this);

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
            Krava = db.PridobiKravo(Krava.Id);
            labelIme.Text = Krava.Ime;
            labelDatumRoj.Text = Krava.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = Krava.Pasma;
            labelImeMame.Text = Krava.ImeMame;
            labelImeOceta.Text = Krava.ImeOceta;
            labelUsSt.Text = Krava.UsesnaSt;
        }
    }
}
