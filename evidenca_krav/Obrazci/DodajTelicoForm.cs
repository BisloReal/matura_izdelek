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
    public partial class DodajTelicoForm : Form
    {
        private DatabaseHelper db;
        public DodajTelicoForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
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
                string UsSt = textBoxUsStTel.Text.Trim();

                int uspeh = db.DodajTelico(imeTel, datumRojstva, pasma, imeMame, imeOceta, UsSt);
                if (uspeh == 0) 
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                if (uspeh == -1)
                {
                    MessageBox.Show("Tip živali ne obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telice: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
