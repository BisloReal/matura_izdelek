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
    public partial class UrediTeleForm : Form
    {
        TeliceRazred Tele;
        TeleCard teleCard;
        private DatabaseHelper db;
        public UrediTeleForm(DatabaseHelper dbHelper, int idTel, TeleCard tc)
        {
            InitializeComponent();

            db = dbHelper;
            teleCard = tc;

            Tele = db.PridobiTelico(idTel);
            textBoxImeTel.Text = Tele.Ime;
            dateTimePicker.Value = Tele.DatumRoj;
            textBoxPasmaTel.Text = Tele.Pasma;
            textBoxImeMameTel.Text = Tele.ImeMame;
            textBoxImeOcetaTel.Text = Tele.ImeOceta;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxImeTel.Text) ||
                string.IsNullOrWhiteSpace(textBoxPasmaTel.Text) ||
                string.IsNullOrWhiteSpace(textBoxImeMameTel.Text) ||
                string.IsNullOrWhiteSpace(textBoxImeOcetaTel.Text))
            {
                MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string imeTel = textBoxImeTel.Text.Trim();
                string datumRojstva = dateTimePicker.Value.ToString("yyyy-MM-dd");
                string pasma = textBoxPasmaTel.Text.Trim();
                string imeMame = textBoxImeMameTel.Text.Trim();
                string imeOceta = textBoxImeOcetaTel.Text.Trim();
                string usesna_stevilka = textBoxUsStTel.Text.Trim();

                if (string.IsNullOrWhiteSpace(usesna_stevilka))
                {
                    usesna_stevilka = "/";
                }

                int izvedba = db.UrediTelico(Tele.Id, imeTel, datumRojstva, pasma, imeMame, imeOceta, usesna_stevilka);

                if (izvedba == 0)
                {
                    teleCard.PosodobiPodatke();
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
    }
}
