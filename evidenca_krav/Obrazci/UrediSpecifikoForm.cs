using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class UrediSpecifikoForm : Form
    {
        private DatabaseHelper db;
        private OstaleSpecifikeRazred specifika;
        private SpecifikaCard specifikaCard;

        public UrediSpecifikoForm(DatabaseHelper dbHelper, OstaleSpecifikeRazred os, SpecifikaCard sc)
        {
            InitializeComponent();

            db = dbHelper;
            specifika = os;
            specifikaCard = sc;


            KraveRazred krava = db.PridobiKravo(specifika.KravaId);
            labelKrava.Text = krava.Ime + " (" + krava.UsesnaSt + ")";

            textBoxSpecifika.Text = specifika.Specifika;

            if (specifika.Datum.HasValue)
            {
                dateTimePicker.Checked = true;
                dateTimePicker.Value = specifika.Datum.Value;
            }
            else
            {
                dateTimePicker.Checked = false;
            }
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxSpecifika.Text))
                {
                    MessageBox.Show("Vnesite specifiko.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                specifika.Specifika = textBoxSpecifika.Text.Trim();

                if (dateTimePicker.Checked)
                {
                    specifika.Datum = dateTimePicker.Value;
                }
                else
                {
                    specifika.Datum = null;
                }

                int uspeh = db.UrediOstaloSpecifiko(specifika);

                if (uspeh == 0)
                {
                    specifikaCard.PosodobiPodatke();

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Specifika ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju specifike: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}