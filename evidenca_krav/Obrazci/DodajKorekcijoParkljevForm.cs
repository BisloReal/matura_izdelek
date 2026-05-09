using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajKorekcijoParkljevForm : Form
    {
        DatabaseHelper db;
        KraveRazred krava;

        public DodajKorekcijoParkljevForm(DatabaseHelper dbHelper, KraveRazred k)
        {
            InitializeComponent();

            db = dbHelper;
            krava = k;

            comboBoxKrave.DataSource = db.PridobiKrave();
            comboBoxKrave.DisplayMember = "UsesnaSt";
            comboBoxKrave.ValueMember = "Id";
            comboBoxKrave.SelectedValue = krava.Id;

            comboBoxIzvajalci.DataSource = db.PridobiIzvajalce();
            comboBoxIzvajalci.DisplayMember = "ImePriimek";
            comboBoxIzvajalci.ValueMember = "Id";
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
                if (comboBoxKrave.SelectedItem == null)
                {
                    MessageBox.Show("Izberite kravo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxIzvajalci.SelectedItem == null)
                {
                    MessageBox.Show("Izberite izvajalca.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxStanje.Text))
                {
                    MessageBox.Show("Vpišite stanje parkljev.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kravaId = Convert.ToInt32(comboBoxKrave.SelectedValue);
                int izvajalecId = Convert.ToInt32(comboBoxIzvajalci.SelectedValue);
                DateTime datum = dateTimePicker.Value;
                string stanje = textBoxStanje.Text.Trim();
                string pripombe = richTextBoxPripombe.Text.Trim();

                if (string.IsNullOrWhiteSpace(pripombe))
                {
                    pripombe = "Ni pripomb.";
                }

                int uspeh = db.DodajKorekcijoParkljev(
                    datum,
                    stanje,
                    pripombe,
                    izvajalecId,
                    kravaId
                );

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Napaka pri dodajanju korekcije parkljev.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju korekcije parkljev: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}