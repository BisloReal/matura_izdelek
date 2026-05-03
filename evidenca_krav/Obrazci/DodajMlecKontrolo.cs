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
    public partial class DodajMlecKontrolo : Form
    {
        DatabaseHelper db;
        int kravaId;
        public DodajMlecKontrolo(DatabaseHelper dbHelper, int kId)
        {
            InitializeComponent();
            db = dbHelper;  
            kravaId = kId;

            comboBox1.DataSource = db.PridobiKontrolerje();
            numericUpDown1.Value = db.PridobiSteviloMlecnihKontrol(kravaId) + 1;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.PogledObstajaSt(kravaId, Convert.ToInt32(numericUpDown1.Value)))
                {
                    MessageBox.Show("Mlečna kontrola s tozaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxDelDneva.Text) ||
                    string.IsNullOrWhiteSpace(textBoxMlecnost.Text))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    richTextBox1.Text = "Ni opomb.";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostBel.Text))
                {
                    textBoxVsebnostBel.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostLak.Text))
                {
                    textBoxVsebnostLak.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostMas.Text))
                {
                    textBoxVsebnostMas.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostSec.Text))
                {
                    textBoxVsebnostSec.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxSomatskeCelice.Text))
                {
                    textBoxSomatskeCelice.Text = "/";
                }

                string celoIme = comboBox1.SelectedItem.ToString();
                string[] deli = celoIme.Split(' ');

                string ime = deli[0];

                string priimek = "";
                if (deli.Length > 1)
                {
                    priimek = deli[1];
                }

                int kontrolor = db.PridobiIdKontrolerjaPrekoImena(ime, priimek);

                int uspeh = db.DodajMlecnoKontrolo(
                    kravaId,
                    dateTimePicker.Value,
                    Convert.ToInt32(numericUpDown1.Value),
                    kontrolor,
                    textBoxDelDneva.Text.Trim(),
                    textBoxMlecnost.Text.Trim(),
                    textBoxVsebnostMas.Text.Trim(),
                    textBoxVsebnostBel.Text.Trim(),
                    textBoxVsebnostLak.Text.Trim(),
                    textBoxVsebnostSec.Text.Trim(),
                    textBoxSomatskeCelice.Text.Trim(),
                    richTextBox1.Text.Trim()
                );
                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                if (uspeh == -1)
                {
                    MessageBox.Show("Oseba ne obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju mlečne kontrole: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
