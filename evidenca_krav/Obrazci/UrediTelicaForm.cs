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
            textBoxImeTel.Text = Telica.imeTel;
            dateTimePicker.Value = Telica.datumRoj;
            textBoxPasmaTel.Text = Telica.pasmaTel;
            textBoxImeMameTel.Text = Telica.imeMameTel;
            textBoxImeOcetaTel.Text = Telica.imeOcetaTel;
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

                int izvedba = db.UrediTelico(Telica.idTel, imeTel, datumRojstva, pasma, imeMame, imeOceta);

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
    }
}
