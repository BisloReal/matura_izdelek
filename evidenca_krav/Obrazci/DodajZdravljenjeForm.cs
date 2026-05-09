using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajZdravljenjeForm : Form
    {
        DatabaseHelper db;
        TeliceRazred krava;

        public DodajZdravljenjeForm(DatabaseHelper dbHelper, TeliceRazred z)
        {
            InitializeComponent();

            db = dbHelper;
            krava = z;

            comboBoxKrave.DataSource = db.PridobiZivaliBrezTelet();
            comboBoxKrave.DisplayMember = "UsesnaSt";
            comboBoxKrave.ValueMember = "Id";
            comboBoxKrave.SelectedValue = krava.Id;

            comboBoxVeterinarji.DataSource = db.PridobiVeterinarje();
            comboBoxVeterinarji.DisplayMember = "ImePriimek";
            comboBoxVeterinarji.ValueMember = "Id";

            checkedListBoxZdravila.DataSource = db.PridobiZdravila();
            checkedListBoxZdravila.DisplayMember = "Zdravilo";
            checkedListBoxZdravila.ValueMember = "Id";

            numericUpDown1.Value = db.PridobiSteviloZdravljenj(krava.Id) + 1;
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

                if (comboBoxVeterinarji.SelectedItem == null)
                {
                    MessageBox.Show("Izberite veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (checkedListBoxZdravila.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Izberite vsaj eno zdravilo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kravaId = Convert.ToInt32(comboBoxKrave.SelectedValue);
                int zapSt = Convert.ToInt32(numericUpDown1.Value);

                if (db.PogledObstajaStZdravljenje(kravaId, zapSt))
                {
                    MessageBox.Show("Zdravljenje s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int veterinarId = Convert.ToInt32(comboBoxVeterinarji.SelectedValue);
                DateTime datum = dateTimePicker.Value;
                string vzrok = textBoxVzrok.Text.Trim();

                if (string.IsNullOrWhiteSpace(vzrok))
                {
                    vzrok = "/";
                }

                int zdravljenjeId = db.DodajZdravljenje(
                    zapSt,
                    datum,
                    vzrok,
                    veterinarId,
                    kravaId
                );

                if (zdravljenjeId > 0)
                {
                    foreach (ZdravilaRazred zdravilo in checkedListBoxZdravila.CheckedItems)
                    {
                        db.DodajZdraviloZdravljenju(zdravljenjeId, zdravilo.Id);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Napaka pri dodajanju zdravljenja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju zdravljenja: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}