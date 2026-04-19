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
    public partial class DodajBikaForm : Form
    {
        private DatabaseHelper db;
        public DodajBikaForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            textBoxIzboljsujeBik.Text = "/";

            db = dbHelper;

            comboBoxPasma.DataSource = db.PridobiPasmeBikov();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBoxRejecBik.Text) ||
                    string.IsNullOrEmpty(textBoxIzboljsujeBik.Text) ||
                    string.IsNullOrEmpty(comboBoxPasma.SelectedItem.ToString()))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string rejec = textBoxRejecBik.Text.Trim();
                string datumRojstva = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string izboljsuje = textBoxIzboljsujeBik.Text.Trim();
                int pasmaId = db.PridobiIdPasmePrekoImena(comboBoxPasma.SelectedItem.ToString());

                db.DodajBikaOs(rejec, datumRojstva, pasmaId, izboljsuje);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju bika: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void buttonPasmePregled_Click(object sender, EventArgs e)
        {
            using (PasmeBikovForm pasmeBikovForm = new PasmeBikovForm(db))
            {
                pasmeBikovForm.ShowDialog();
            }

            comboBoxPasma.DataSource = db.PridobiPasmeBikov();
        }

        private void textBoxIzboljsujeBik_Click(object sender, EventArgs e)
        {
            if (textBoxIzboljsujeBik.Text == "/")
            {
                textBoxIzboljsujeBik.Clear();
            }
        }
    }
}
