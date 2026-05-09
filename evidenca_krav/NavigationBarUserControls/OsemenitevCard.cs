using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class OsemenitevCard : UserControl
    {
        DatabaseHelper db;
        OsemenitveRazred osemenitev;

        public OsemenitevCard(DatabaseHelper dbHelper, OsemenitveRazred osr)
        {
            InitializeComponent();
            db = dbHelper;
            osemenitev = osr;

            NastaviPodatke();
        }

        private void NastaviPodatke()
        {
            labelZapSt.Text = osemenitev.Zaporedna_Stevilka.ToString();
            labelDatum.Text = osemenitev.Datum_Osemenitve.ToString("dd.MM.yyyy");
            labelBik.Text = osemenitev.Bik;
            labelVeterinar.Text = osemenitev.Veterinar;

            if (osemenitev.Datum_Pregleda.HasValue)
            {
                string izzid = osemenitev.Izzid_Pregleda;

                if (string.IsNullOrWhiteSpace(izzid))
                {
                    izzid = "/";
                }

                labelPregled.Text =  osemenitev.Datum_Pregleda.Value.ToString("dd.MM.yyyy") + " - " + izzid;
            }
            else
            {
                labelPregled.Text = "ni vpisan";
            }

            if (osemenitev.Datum_Presusitve.HasValue)
            {
                labelPresusitev.Text =  osemenitev.Datum_Presusitve.Value.ToString("dd.MM.yyyy");
            }
            else
            {
                labelPresusitev.Text = "ni vpisana";
            }
        }

        public void PosodobiPodatke()
        {
            osemenitev = db.PridobiOsemenitev(osemenitev.Id);
            NastaviPodatke();
        }

        private void buttonUrediOs_Click(object sender, EventArgs e)
        {
            try
            {
                UrediOsemenitev urediOsemenitev = new UrediOsemenitev(db, osemenitev, this);

                if (urediOsemenitev.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Osemenitev uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju osemenitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}