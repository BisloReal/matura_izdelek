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
    public partial class UrediMlecnoKontrolo : Form
    {
        DatabaseHelper db;
        MlecneKontroleRazred mlecneKontroleRazred;
        MlecKontrolaCard kontrolaCard;
        int kravaId;
        public UrediMlecnoKontrolo(DatabaseHelper dbHelper, MlecneKontroleRazred mk, MlecKontrolaCard mkc, int kId)
        {
            InitializeComponent();

            db = dbHelper;
            mlecneKontroleRazred = mk;
            kravaId = kId;
            kontrolaCard = mkc;

            numericUpDown1.Value = mlecneKontroleRazred.Zaporedna_Stevilka;
            textBoxDelDneva.Text = mlecneKontroleRazred.Del_dneva;
            textBoxMlecnost.Text = mlecneKontroleRazred.Mlecnost;
            textBoxVsebnostBel.Text = mlecneKontroleRazred.Vsebnost_Beljakovin;
            textBoxVsebnostLak.Text = mlecneKontroleRazred.Vsebnost_Laktaze;
            textBoxVsebnostMas.Text = mlecneKontroleRazred.Vsebnost_Mascobe;
            textBoxVsebnostSec.Text = mlecneKontroleRazred.Vsebnost_Secnice;

            comboBox1.DataSource = db.PridobiKontrolerje();
            comboBox1.SelectedItem = mlecneKontroleRazred.Ime_Priimek_Osebe;
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
                if (db.PogledObstajaSt(kravaId, Convert.ToInt32(numericUpDown1.Value)) && numericUpDown1.Value != mlecneKontroleRazred.Zaporedna_Stevilka)
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
                    richTextBox1.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostLak.Text))
                {
                    richTextBox1.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostMas.Text))
                {
                    richTextBox1.Text = "/";
                }
                if (string.IsNullOrWhiteSpace(textBoxVsebnostSec.Text))
                {
                    richTextBox1.Text = "/";
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

                int uspeh = db.UrediMlecnoKontrolo(
                    mlecneKontroleRazred.Id,
                    dateTimePicker.Value,
                    Convert.ToInt32(numericUpDown1.Value),
                    kontrolor,
                    textBoxDelDneva.Text.Trim(),
                    textBoxMlecnost.Text.Trim(),
                    textBoxVsebnostBel.Text.Trim(),
                    textBoxVsebnostLak.Text.Trim(),
                    textBoxVsebnostMas.Text.Trim(),
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
                MessageBox.Show("Napaka pri urejanju mlečne kontrole: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
