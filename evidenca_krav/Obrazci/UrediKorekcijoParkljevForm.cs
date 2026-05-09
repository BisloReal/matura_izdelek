using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class UrediKorekcijoParkljevForm : Form
    {
        DatabaseHelper db;
        KorekcijeParkljevRazred korekcija;
        KorekcijaParkljevCard korekcijaCard;

        public UrediKorekcijoParkljevForm(DatabaseHelper dbHelper, KorekcijeParkljevRazred kpr, KorekcijaParkljevCard kpc)
        {
            InitializeComponent();

            db = dbHelper;
            korekcija = kpr;
            korekcijaCard = kpc;

            string imeKrave = db.PridobiKravo(korekcija.KravaId).Ime;
            string usesnaSt = db.PridobiKravo(korekcija.KravaId).UsesnaSt;
            labelKrava.Text = imeKrave + " (" + usesnaSt + ")";

            comboBoxIzvajalci.DataSource = db.PridobiIzvajalce();
            comboBoxIzvajalci.DisplayMember = "ImePriimek";
            comboBoxIzvajalci.ValueMember = "Id";
            comboBoxIzvajalci.SelectedValue = korekcija.IzvajalecId;

            dateTimePicker.Value = korekcija.Datum;
            textBoxStanje.Text = korekcija.Stanje;
            richTextBoxPripombe.Text = korekcija.Pripombe;
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
                if (comboBoxIzvajalci.SelectedItem == null)
                {
                    MessageBox.Show("Izberite izvajalca.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBoxStanje.Text))
                {
                    MessageBox.Show("Vpišite stanje parkljev.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                korekcija.IzvajalecId = Convert.ToInt32(comboBoxIzvajalci.SelectedValue);
                korekcija.Datum = dateTimePicker.Value;
                korekcija.Stanje = textBoxStanje.Text.Trim();
                korekcija.Pripombe = richTextBoxPripombe.Text.Trim();

                if (string.IsNullOrWhiteSpace(korekcija.Pripombe))
                {
                    korekcija.Pripombe = "Ni pripomb.";
                }

                int uspeh = db.UrediKorekcijoParkljev(korekcija);

                if (uspeh == 0)
                {
                    korekcijaCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Korekcija parkljev ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju korekcije parkljev: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}