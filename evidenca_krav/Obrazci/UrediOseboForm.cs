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
    public partial class UrediOseboForm : Form
    {
        DatabaseHelper db;
        OsebeRazred oseba;
        OsebaCard osebaCard;
        public UrediOseboForm(DatabaseHelper dbHelper, OsebeRazred os, OsebaCard oc)
        {
            InitializeComponent();
            db = dbHelper;
            oseba = os;
            osebaCard = oc;

            textBoxIme.Text = oseba.Ime;
            textBoxPriimek.Text = oseba.Priimek;
            textBoxTelefon.Text = oseba.Telefon;
            textBoxEmail.Text = oseba.Email;
            comboBoxZadolzitev.DataSource = db.PridobiZadolzitve();
            comboBoxZadolzitev.SelectedItem = oseba.Zadolzitev;
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

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIme.Text) ||
                    string.IsNullOrEmpty(textBoxPriimek.Text) ||
                    string.IsNullOrEmpty(textBoxTelefon.Text) ||
                    string.IsNullOrEmpty(textBoxEmail.Text) ||
                    comboBoxZadolzitev.SelectedItem == null)
            {
                MessageBox.Show("Izpolnite vsa polja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ime = textBoxIme.Text.Trim();
                string priimek = textBoxPriimek.Text.Trim();
                string telefon = textBoxTelefon.Text.Trim();
                string email = textBoxEmail.Text.Trim();


                int izvedba = db.UrediOsebo(oseba.Id, ime, priimek, telefon, email, comboBoxZadolzitev.SelectedItem.ToString());

                if (izvedba == 0)
                {
                    osebaCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (izvedba == -1)
                {
                    MessageBox.Show("Oseba ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju osebe: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
