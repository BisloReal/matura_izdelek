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
    public partial class DodajOdhodForm : Form
    {
        DatabaseHelper db;
        public DodajOdhodForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            comboBoxZival.DataSource = db.PridobiUsStVsehZivali();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxGmid.Text) ||
                    string.IsNullOrWhiteSpace(textBoxLokacija.Text) ||
                    string.IsNullOrWhiteSpace(textBoxVzrok.Text))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(richTextBoxOpombe.Text))
                {
                    richTextBoxOpombe.Text = "Ni opomb.";
                }

                DateTime datum = dateTimePicker.Value;
                string gmid = textBoxGmid.Text.Trim();
                string lokacija = textBoxLokacija.Text.Trim();
                string vzrok = textBoxVzrok.Text.Trim();
                string opombe = richTextBoxOpombe.Text.Trim();

                int zivalId = db.PridobiIdKravePrekoUsSt(comboBoxZival.SelectedItem.ToString());

                int uspeh = db.DodajOdhod(datum, gmid, lokacija, vzrok, opombe, zivalId);
                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                if (uspeh == -1)
                {
                    MessageBox.Show("Id živali ne obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju odhoda: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
