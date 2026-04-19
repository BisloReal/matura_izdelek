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
    public partial class PasmeBikovForm : Form
    {
        private DatabaseHelper db;
        public PasmeBikovForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            listBoxPasme.DataSource = db.PridobiPasmeBikov();
        }

        private void listBoxPasme_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxPasma.Text = listBoxPasme.SelectedItem.ToString();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                db.DodajPasmoBikov(textBoxPasma.Text);
                listBoxPasme.DataSource = db.PridobiPasmeBikov();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju pasme: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonZapri_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonUrediIzbrano_Click(object sender, EventArgs e)
        {
            try
            {
                db.UrediPasmoBikov(db.PridobiIdPasmePrekoImena(listBoxPasme.SelectedItem.ToString()), textBoxPasma.Text);
                listBoxPasme.DataSource = db.PridobiPasmeBikov();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju pasme: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
