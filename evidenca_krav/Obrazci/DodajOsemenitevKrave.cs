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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace evidenca_krav.Obrazci
{
    public partial class DodajOsemenitevKrave : Form
    {
        private DatabaseHelper db;
        TeliceRazred Krava;

        public DodajOsemenitevKrave(DatabaseHelper dbHelper, TeliceRazred z)
        {
            InitializeComponent();
            db = dbHelper;
            Krava = z;

            comboBoxBiki.DataSource = db.PridobiBikeOs();
            comboBoxBiki.DisplayMember = "Ime_Bika";
            comboBoxBiki.ValueMember = "IdBik";

            comboBoxKrave.DataSource = db.PridobiZivaliBrezTelet();
            comboBoxKrave.DisplayMember = "UsesnaSt";
            comboBoxKrave.ValueMember = "Id";
            comboBoxKrave.SelectedValue = Krava.Id;

            comboBoxVeterinarji.DataSource = db.PridobiVeterinarje();
            comboBoxVeterinarji.DisplayMember = "ImePriimek";
            comboBoxVeterinarji.ValueMember = "Id";
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                int kravaId = Convert.ToInt32(comboBoxKrave.SelectedValue);
                if (db.PogledObstajaStOsemenitev(kravaId, Convert.ToInt32(numericUpDown1.Value)))
                {
                    MessageBox.Show("Osemenitev s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBoxVeterinarji.SelectedItem == null)
                {
                    MessageBox.Show("Izberite veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxKrave.SelectedItem == null)
                {
                    MessageBox.Show("Izberite kravo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxBiki.SelectedItem == null)
                {
                    MessageBox.Show("Izberite bika.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    richTextBox1.Text = "Ni opomb.";
                }

                int zapOs = Convert.ToInt32(numericUpDown1.Value);
                int veterinarId = Convert.ToInt32(comboBoxVeterinarji.SelectedValue);
                int bikId = Convert.ToInt32(comboBoxBiki.SelectedValue);
                DateTime datumOs = dateTimePicker.Value;
                string opombe = richTextBox1.Text;


                int uspeh = db.DodajOsemenitev(zapOs, datumOs, veterinarId, opombe, kravaId, bikId);

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Oseba ne obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju osemenitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
