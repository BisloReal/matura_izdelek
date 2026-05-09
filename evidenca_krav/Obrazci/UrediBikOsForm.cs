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

            textBoxIme.Text = Bik.Ime;
            textBoxStevilka.Text = Bik.Stevilka;
            textBoxRejecBik.Text = Bik.Rejec;
            dateTimePicker.Value = Bik.DatumRoj;
            textBoxIzboljsujeBik.Text = Bik.Izboljsuje;
            comboBoxPasma.DataSource = db.PridobiPasmeBikov();
            comboBoxPasma.SelectedItem = Bik.Pasma;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxRejecBik.Text) ||
                    string.IsNullOrEmpty(textBoxIzboljsujeBik.Text) ||
                    string.IsNullOrEmpty(textBoxStevilka.Text) ||
                    string.IsNullOrEmpty(textBoxIme.Text) ||
                    string.IsNullOrEmpty(comboBoxPasma.SelectedItem.ToString()))
            {
                MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (db.PogledObstajaStBika(textBoxStevilka.Text.Trim()) && Bik.Stevilka != textBoxStevilka.Text.Trim())
            {
                MessageBox.Show("Bik z tem številko že obstaja. Izberite drugo številko.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           try
           {
                string ime = textBoxIme.Text.Trim();
                string stevilka = textBoxStevilka.Text.Trim();
                string rejec = textBoxRejecBik.Text.Trim();
                string datumRojstva = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string izboljsuje = textBoxIzboljsujeBik.Text.Trim();
                string pasma = comboBoxPasma.SelectedItem.ToString();
                int pasmaId = db.PridobiIdPasmePrekoImena(pasma);


                int izvedba = db.UrediBikaOs(ime, stevilka, rejec, datumRojstva, pasmaId, izboljsuje, Bik.IdBik);

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
