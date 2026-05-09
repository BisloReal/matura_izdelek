using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class PojatevCard : UserControl
    {
        DatabaseHelper db;
        PojatveRazred pojatev;

        public PojatevCard(DatabaseHelper dbHelper, PojatveRazred pr)
        {
            InitializeComponent();
            db = dbHelper;
            pojatev = pr;

            NastaviPodatke();
        }

        private void NastaviPodatke()
        {
            labelZapSt.Text = pojatev.ZaporednoStevilo.ToString();
            labelDatum.Text = pojatev.DatumPojatve.ToString("dd.MM.yyyy");
            labelKrava.Text = pojatev.KravaIme;

            if (pojatev.KonecDatumPojatve.HasValue)
            {
                labelKonecDatum.Text = pojatev.KonecDatumPojatve.Value.ToString("dd.MM.yyyy");
            }
            else
            {
                labelKonecDatum.Text = "ni vpisan";
            }
        }

        public void PosodobiPodatke()
        {
            pojatev = db.PridobiPojatev(pojatev.Id);
            NastaviPodatke();
        }

        private void buttonUrediPojatev_Click(object sender, EventArgs e)
        {
            try
            {
                UrediPojatevForm urediPojatevForm = new UrediPojatevForm(db, pojatev, this);

                if (urediPojatevForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Pojatev uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju pojatve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}