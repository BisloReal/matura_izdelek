using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajPojatevForm : Form
    {
        DatabaseHelper db;
        TeliceRazred krava;

        public DodajPojatevForm(DatabaseHelper dbHelper, TeliceRazred z)
        {
            InitializeComponent();
            db = dbHelper;
            krava = z;

            comboBoxKrave.DataSource = db.PridobiZivaliBrezTelet();
            comboBoxKrave.DisplayMember = "UsesnaSt";
            comboBoxKrave.ValueMember = "Id";
            comboBoxKrave.SelectedValue = krava.Id;

            numericUpDown1.Value = db.PridobiSteviloPojatve(krava.Id) + 1;
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

                int kravaId = Convert.ToInt32(comboBoxKrave.SelectedValue);
                int zapSt = Convert.ToInt32(numericUpDown1.Value);

                if (db.PogledObstajaStPojatev(kravaId, zapSt))
                {
                    MessageBox.Show("Pojatev s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    richTextBox1.Text = "Ni opomb.";
                }

                DateTime datumPojatve = dateTimePicker.Value;
                string opombe = richTextBox1.Text.Trim();

                int uspeh = db.DodajPojatev(zapSt, datumPojatve, opombe, kravaId);

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Napaka pri dodajanju pojatve.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju pojatve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}