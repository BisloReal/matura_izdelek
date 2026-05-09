using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class SpecifikaCard : UserControl
    {
        private DatabaseHelper db;
        private OstaleSpecifikeRazred specifika;

        public SpecifikaCard(DatabaseHelper dbHelper, OstaleSpecifikeRazred ostalaSpecifika)
        {
            InitializeComponent();

            db = dbHelper;
            specifika = ostalaSpecifika;

            labelSpecifika.Text = specifika.Specifika;

            if (specifika.Datum.HasValue)
            {
                labelDatum.Text = specifika.Datum.Value.ToString("dd. MM. yyyy");
            }
            else
            {
                labelDatum.Text = "/";
            }
        }

        public void PosodobiPodatke()
        {
            specifika = db.PridobiOstaloSpecifiko(specifika.Id);
            labelSpecifika.Text = specifika.Specifika;

            if (specifika.Datum.HasValue)
            {
                labelDatum.Text = specifika.Datum.Value.ToString("dd. MM. yyyy");
            }
            else
            {
                labelDatum.Text = "/";
            }
        }

        private void buttonUrediSpec_Click(object sender, EventArgs e)
        {
            try
            {
                UrediSpecifikoForm urediSpecifikaForm = new UrediSpecifikoForm(db, specifika, this);

                if (urediSpecifikaForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Specifika uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju specifike: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}