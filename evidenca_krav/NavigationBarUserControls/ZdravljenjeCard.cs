using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class ZdravljenjeCard : UserControl
    {
        DatabaseHelper db;
        ZdravljenjaRazred zdravljenje;

        public ZdravljenjeCard(DatabaseHelper dbHelper, ZdravljenjaRazred zr)
        {
            InitializeComponent();

            db = dbHelper;
            zdravljenje = zr;

            NastaviPodatke();
        }

        private void NastaviPodatke()
        {
            labelZapSt.Text = zdravljenje.ZaporednaStevilka.ToString();
            labelDatum.Text = zdravljenje.Datum.ToString("dd.MM.yyyy");
            labelVeterinar.Text = zdravljenje.Veterinar;

            if (string.IsNullOrWhiteSpace(zdravljenje.Vzrok))
            {
                labelVzrok.Text = "/";
            }
            else
            {
                labelVzrok.Text = zdravljenje.Vzrok;
            }
        }

        public void PosodobiPodatke()
        {
            zdravljenje = db.PridobiZdravljenje(zdravljenje.Id);
            NastaviPodatke();
        }

        private void buttonUrediZdravljenje_Click(object sender, EventArgs e)
        {
            try
            {
                UrediZdravljenjeForm urediZdravljenjeForm = new UrediZdravljenjeForm(db, zdravljenje, this);

                if (urediZdravljenjeForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Zdravljenje uspešno posodobljeno!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju zdravljenja: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}