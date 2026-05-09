using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class KorekcijaParkljevCard : UserControl
    {
        DatabaseHelper db;
        KorekcijeParkljevRazred korekcija;

        public KorekcijaParkljevCard(DatabaseHelper dbHelper, KorekcijeParkljevRazred kpr)
        {
            InitializeComponent();
            db = dbHelper;
            korekcija = kpr;

            NastaviPodatke();
        }

        private void NastaviPodatke()
        {
            labelDatum.Text = korekcija.Datum.ToString("dd.MM.yyyy");
            labelStanje.Text = korekcija.Stanje;
            labelIzvajalec.Text = korekcija.Izvajalec;
        }

        public void PosodobiPodatke()
        {
            korekcija = db.PridobiKorekcijoParkljev(korekcija.Id);
            NastaviPodatke();
        }

        private void buttonUrediKorekcijo_Click(object sender, EventArgs e)
        {
            try
            {
                UrediKorekcijoParkljevForm urediKorekcijoForm = new UrediKorekcijoParkljevForm(db, korekcija, this);

                if (urediKorekcijoForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Korekcija parkljev uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju korekcije parkljev: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}