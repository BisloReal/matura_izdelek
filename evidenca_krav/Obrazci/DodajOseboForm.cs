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
    public partial class DodajOseboForm : Form
    {
        DatabaseHelper db;
        public DodajOseboForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;

            comboBoxZadolzitev.DataSource = db.PridobiZadolzitve();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxIme.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPriimek.Text) ||
                    string.IsNullOrWhiteSpace(textBoxTelefon.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text))
                {
                    MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string ime = textBoxIme.Text.Trim();
                string priimek = textBoxPriimek.Text.Trim();
                string telefon = textBoxTelefon.Text.Trim();
                string email = textBoxEmail.Text.Trim();
                string zadolzitev = comboBoxZadolzitev.SelectedItem.ToString();

                int uspeh = db.DodajOsebo(ime, priimek, telefon, email, zadolzitev);
                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                if (uspeh == -1)
                {
                    MessageBox.Show("Zadolžitev ne obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju osebe: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonZadolzitvePregled_Click(object sender, EventArgs e)
        {
            using (ZadolzitveOsebForm zadolzitveOsebForm = new ZadolzitveOsebForm(db))
            {
                zadolzitveOsebForm.ShowDialog();
            }

            comboBoxZadolzitev.DataSource = db.PridobiZadolzitve();
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
