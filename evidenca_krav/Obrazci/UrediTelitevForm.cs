using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class UrediTelitevForm : Form
    {
        DatabaseHelper db;
        TelitevRazred telitev;
        TelitevCard telitevCard;

        public UrediTelitevForm(DatabaseHelper dbHelper, TelitevRazred tr, TelitevCard tc)
        {
            InitializeComponent();

            db = dbHelper;
            telitev = tr;
            telitevCard = tc;

            comboBoxMama.DataSource = db.PridobiKrave();
            comboBoxMama.DisplayMember = "UsesnaSt";
            comboBoxMama.ValueMember = "Id";
            comboBoxMama.SelectedValue = telitev.KravaMamaId;

            comboBoxTele.DataSource = db.PridobiZivaliBrezTelitveAliTrenutno(telitev.TeleId);
            comboBoxTele.DisplayMember = "UsesnaSt";
            comboBoxTele.ValueMember = "Id";
            comboBoxTele.SelectedValue = telitev.TeleId;

            comboBoxBiki.DataSource = db.PridobiBikeOs();
            comboBoxBiki.DisplayMember = "Ime_Bika";
            comboBoxBiki.ValueMember = "IdBik";
            comboBoxBiki.SelectedValue = telitev.BikId;

            numericUpDown1.Value = telitev.ZaporednoStevilo;
            dateTimePicker.Value = telitev.Datum;

            textBoxPotek.Text = telitev.Potek;
            textBoxRojstvo.Text = telitev.Rojstvo;
            textBoxKakovostMleziva.Text = telitev.KakovostMleziva;
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

                if (db.PogledObstajaStTelitev(kravaMamaId, zapTelitev) &&
                    (zapTelitev != telitev.ZaporednoStevilo || kravaMamaId != telitev.KravaMamaId))
                {
                    MessageBox.Show("Telitev s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (kravaMamaId == teleId)
                {
                    MessageBox.Show("Krava mama in tele ne smeta biti ista žival.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                telitev.ZaporednoStevilo = zapTelitev;
                telitev.Potek = textBoxPotek.Text.Trim();
                telitev.Rojstvo = textBoxRojstvo.Text.Trim();
                telitev.KakovostMleziva = textBoxKakovostMleziva.Text.Trim();
                telitev.Datum = dateTimePicker.Value;
                telitev.KravaMamaId = kravaMamaId;
                telitev.TeleId = teleId;
                telitev.BikId = bikId;

                int uspeh = db.UrediTelitev(telitev);

                if (uspeh == 0)
                {
                    telitevCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Telitev ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju telitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}