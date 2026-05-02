using evidenca_krav.NavigationBar;
using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
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
    public partial class UrediBikOsForm : Form
    {
        BikiOsRazred Bik;
        BikCard bikCard;
        DatabaseHelper db;
        public UrediBikOsForm(DatabaseHelper dbHelper, BikiOsRazred bik, BikCard bc)
        {
            InitializeComponent();
            db = dbHelper;
            Bik = bik;
            bikCard = bc;

            textBoxRejecBik.Text = Bik.Rejec;
            dateTimePicker.Value = Bik.DatumRoj;
            comboBoxPasma.DataSource = db.PridobiPasmeBikov();
            comboBoxPasma.SelectedItem = Bik.Pasma;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRejecBik.Text) ||
                    comboBoxPasma.SelectedItem == null)
            {
                MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBoxIzboljsujeBik.Text))
            {
                textBoxIzboljsujeBik.Text = "/";
            }

           try
           {
                string rejec = textBoxRejecBik.Text.Trim();
                string datumRojstva = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string izboljsuje = textBoxIzboljsujeBik.Text.Trim();
                string pasma = comboBoxPasma.SelectedItem.ToString();
                int pasmaId = db.PridobiIdPasmePrekoImena(pasma);


                int izvedba = db.UrediBikaOs(rejec, datumRojstva, pasmaId, izboljsuje, Bik.IdBik);

                if (izvedba == 0)
                {
                    bikCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (izvedba == -1)
                {
                    MessageBox.Show("Bik ni bila najden.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           }
           catch (Exception ex)
           {
                MessageBox.Show("Napaka pri urejanju bika: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
