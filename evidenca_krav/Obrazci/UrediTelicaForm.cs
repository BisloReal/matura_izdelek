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
    public partial class UrediTelicaForm : Form
    {
        TeliceRazred Telica;
        TelicaCard telicaCard;
        private DatabaseHelper db;
        public UrediTelicaForm(DatabaseHelper dbHelper, int idTel, TelicaCard tc)
        {
            InitializeComponent();
            db = dbHelper;
            telicaCard = tc;

            Telica = db.PridobiTelico(idTel);
            textBoxImeTel.Text = Telica.Ime;
            dateTimePicker.Value = Telica.DatumRoj;
            textBoxPasmaTel.Text = Telica.Pasma;
            textBoxImeMameTel.Text = Telica.ImeMame;
            textBoxImeOcetaTel.Text = Telica.ImeOceta;
            textBoxUsStTel.Text = Telica.UsesnaSt;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (db.PogledObstajaUsSt(textBoxUsStTel.Text.Trim()) && Telica.UsesnaSt != textBoxUsStTel.Text.Trim())
                {
                    MessageBox.Show("Žival z to uporabniško številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxImeTel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPasmaTel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxImeMameTel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxImeOcetaTel.Text) ||
                    string.IsNullOrWhiteSpace(textBoxUsStTel.Text))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string imeTel = textBoxImeTel.Text.Trim();
                string datumRojstva = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string pasma = textBoxPasmaTel.Text.Trim();
                string imeMame = textBoxImeMameTel.Text.Trim();
                string imeOceta = textBoxImeOcetaTel.Text.Trim();
                string usesna_stevilka = textBoxUsStTel.Text.Trim();

                int izvedba = db.UrediTelico(Telica.Id, imeTel, datumRojstva, pasma, imeMame, imeOceta, usesna_stevilka);

                if (izvedba == 0)
                {
                    telicaCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (izvedba == -1)
                {
                    MessageBox.Show("Telica ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Napaka pri urejanju telice: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void buttonOdhod_Click(object sender, EventArgs e)
        {
            try
            {
                DodajOdhodForm dodajOdhodForm = new DodajOdhodForm(db, Telica.UsesnaSt);
                if (dodajOdhodForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Odhod uspešno dodan.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Abort;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju odhoda: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
