using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajSpecifikoForm : Form
    {
        DatabaseHelper db;
        TeliceRazred Krava;

        public DodajSpecifikoForm(DatabaseHelper dbHelper, TeliceRazred z)
        {
            InitializeComponent();

            db = dbHelper;
            Krava = z;

            comboBoxKrave.DataSource = db.PridobiZivaliBrezTelet();
            comboBoxKrave.DisplayMember = "UsesnaSt";
            comboBoxKrave.ValueMember = "Id";
            comboBoxKrave.SelectedValue = Krava.Id;

            dateTimePicker.ShowCheckBox = true;
            dateTimePicker.Checked = false;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxKrave.SelectedItem == null)
                {
                    MessageBox.Show("Izberite kravo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxSpecifika.Text))
                {
                    MessageBox.Show("Vnesite specifiko.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kravaId = Convert.ToInt32(comboBoxKrave.SelectedValue);
                string specifika = textBoxSpecifika.Text.Trim();

                DateTime? datum = null;

                if (dateTimePicker.Checked)
                {
                    datum = dateTimePicker.Value;
                }

                int uspeh = db.DodajOstaloSpecifiko(specifika, datum, kravaId);

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Krava ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju specifike: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}