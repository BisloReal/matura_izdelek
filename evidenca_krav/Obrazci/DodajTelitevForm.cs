using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajTelitevForm : Form
    {
        DatabaseHelper db;
        TeliceRazred krava;

        public DodajTelitevForm(DatabaseHelper dbHelper, TeliceRazred z)
        {
            InitializeComponent();
            db = dbHelper;
            krava = z;

            comboBoxMama.DataSource = db.PridobiZivaliBrezTelet();
            comboBoxMama.DisplayMember = "UsesnaSt";
            comboBoxMama.ValueMember = "Id";
            comboBoxMama.SelectedValue = krava.Id;

            comboBoxTele.DataSource = db.PridobiZivaliBrezTelitve();
            comboBoxTele.DisplayMember = "UsesnaSt";
            comboBoxTele.ValueMember = "Id";

            comboBoxBiki.DataSource = db.PridobiBikeOs();
            comboBoxBiki.DisplayMember = "Ime_Bika";
            comboBoxBiki.ValueMember = "IdBik";

            numericUpDown1.Value = db.PridobiSteviloTelitev(krava.Id) + 1;
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
                if (comboBoxMama.SelectedItem == null)
                {
                    MessageBox.Show("Izberite kravo mamo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxTele.SelectedItem == null)
                {
                    MessageBox.Show("Izberite tele.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxBiki.SelectedItem == null)
                {
                    MessageBox.Show("Izberite bika.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxPotek.Text) ||
                    string.IsNullOrWhiteSpace(textBoxRojstvo.Text) ||
                    string.IsNullOrWhiteSpace(textBoxKakovostMleziva.Text))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kravaMamaId = Convert.ToInt32(comboBoxMama.SelectedValue);
                int teleId = Convert.ToInt32(comboBoxTele.SelectedValue);
                int bikId = Convert.ToInt32(comboBoxBiki.SelectedValue);
                int zapTelitev = Convert.ToInt32(numericUpDown1.Value);

                if (db.PogledObstajaStTelitev(kravaMamaId, zapTelitev))
                {
                    MessageBox.Show("Telitev s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (kravaMamaId == teleId)
                {
                    MessageBox.Show("Krava mama in tele ne smeta biti ista žival.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string potek = textBoxPotek.Text.Trim();
                string rojstvo = textBoxRojstvo.Text.Trim();
                string kakovostMleziva = textBoxKakovostMleziva.Text.Trim();
                DateTime datum = dateTimePicker.Value;

                int uspeh = db.DodajTelitev(
                    zapTelitev,
                    potek,
                    rojstvo,
                    kakovostMleziva,
                    datum,
                    kravaMamaId,
                    teleId,
                    bikId
                );

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Napaka pri dodajanju telitve.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}