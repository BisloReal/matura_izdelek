using evidenca_krav.NavigationBar;
using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
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
        TeliceRazred Krava;
        TelicaCard kravaCard;
        private DatabaseHelper db;
        public UrediTelicaForm(DatabaseHelper dbHelper, int idKrav, TelicaCard kv)
        {
            InitializeComponent();
            db = dbHelper;
            kravaCard = kv;

            Krava = db.PridobiTelico(idKrav);

            textBoxIme.Text = Krava.Ime;
            textBoxUsSt.Text = Krava.UsesnaSt;
            textBoxPasma.Text = Krava.Pasma;
            dateTimePicker.Value = Krava.DatumRoj;
            textBoxImeMame.Text = Krava.ImeMame;
            textBoxImeOceta.Text = Krava.ImeOceta;


            NaloziMlecneKontrole();
            NaloziOsemenitve();
            NaloziPojatve();
            NaloziTelitve();
            NaloziKorekcije();
            NaloziZdravljenja();
            NaloziComboBoxeKarence();
            NaloziKarence();
        }

        private void NaloziMlecneKontrole()
        {
            flowLayoutPanelMlecneKontrole.Controls.Clear();

            List<MlecneKontroleRazred> mlecneKontrole = db.PridobiMlecneKontrole(Krava.Id);

            foreach (MlecneKontroleRazred mk in mlecneKontrole)
            {
                flowLayoutPanelMlecneKontrole.Controls.Add(new MlecKontrolaCard(db, mk, Krava.Id));
            }
        }

        private void NaloziOsemenitve()
        {
            flowLayoutPanelOs.Controls.Clear();

            List<OsemenitveRazred> osemenitve = db.PridobiOsemenitve(Krava.Id);

            foreach (OsemenitveRazred o in osemenitve)
            {
                flowLayoutPanelOs.Controls.Add(new OsemenitevCard(db, o));
            }
        }

        private void NaloziPojatve()
        {
            flowLayoutPanelPojatve.Controls.Clear();

            List<PojatveRazred> pojatve = db.PridobiPojatve(Krava.Id);

            foreach (PojatveRazred p in pojatve)
            {
                flowLayoutPanelPojatve.Controls.Add(new PojatevCard(db, p));
            }
        }

        private void NaloziTelitve()
        {
            flowLayoutPanelTelitve.Controls.Clear();

            List<TelitevRazred> telitve = db.PridobiTelitve(Krava.Id);

            foreach (TelitevRazred t in telitve)
            {
                flowLayoutPanelTelitve.Controls.Add(new TelitevCard(db, t));
            }
        }

        private void NaloziKorekcije()
        {
            flowLayoutPanelKorekcije.Controls.Clear();

            List<KorekcijeParkljevRazred> korekcije = db.PridobiKorekcijeParkljev(Krava.Id);

            foreach (KorekcijeParkljevRazred k in korekcije)
            {
                flowLayoutPanelKorekcije.Controls.Add(new KorekcijaParkljevCard(db, k));
            }
        }

        private void NaloziZdravljenja()
        {
            flowLayoutPanelZdravljenja.Controls.Clear();

            List<ZdravljenjaRazred> zdravljenja = db.PridobiZdravljenja(Krava.Id);

            foreach (ZdravljenjaRazred z in zdravljenja)
            {
                flowLayoutPanelZdravljenja.Controls.Add(new ZdravljenjeCard(db, z));
            }
        }

        private void NaloziOstaleSpecifike()
        {
            flowLayoutPanelOstaleSpecifike.Controls.Clear();

            List<OstaleSpecifikeRazred> specifike = db.PridobiOstaleSpecifike(Krava.Id);

            foreach (OstaleSpecifikeRazred s in specifike)
            {
                flowLayoutPanelOstaleSpecifike.Controls.Add(new SpecifikaCard(db, s));
            }
        }

        private void NaloziKarence()
        {
            List<KarencaRazred> karence = db.PridobiKarence(Krava.Id);

            KarencaRazred mesna = karence.FirstOrDefault(k => k.VrstaKarence == "Mesna");
            KarencaRazred mlecna = karence.FirstOrDefault(k => k.VrstaKarence == "Mlečna");

            if (mesna != null)
            {
                dateTimePickerDatumKoncaMes.Checked = true;
                dateTimePickerDatumKoncaMes.Value = mesna.DatumKonca;

                comboBoxZdravljenjeMesna.SelectedValue = mesna.ZdravljenjeId;
                comboBoxVeterinarMesna.SelectedValue = mesna.VeterinarId;
                richTextBoxOpombeMes.Text = mesna.Opombe;
            }
            else
            {
                dateTimePickerDatumKoncaMes.Checked = false;

                comboBoxZdravljenjeMesna.SelectedIndex = -1;
                comboBoxVeterinarMesna.SelectedIndex = -1;
                richTextBoxOpombeMes.Text = "";
            }

            if (mlecna != null)
            {
                dateTimePickerDatumKoncaMlec.Checked = true;
                dateTimePickerDatumKoncaMlec.Value = mlecna.DatumKonca;

                comboBoxZdravljenjeMlecna.SelectedValue = mlecna.ZdravljenjeId;
                comboBoxVeterinarMlecna.SelectedValue = mlecna.VeterinarId;
                richTextBoxOpombeMlec.Text = mlecna.Opombe;
            }
            else
            {
                dateTimePickerDatumKoncaMlec.Checked = false;

                comboBoxZdravljenjeMlecna.SelectedIndex = -1;
                comboBoxVeterinarMlecna.SelectedIndex = -1;
                richTextBoxOpombeMlec.Text = "";
            }
        }

        private void NaloziComboBoxeKarence()
        {
            List<ZdravljenjaRazred> zdravljenja = db.PridobiZdravljenja(Krava.Id);

            comboBoxZdravljenjeMesna.DataSource = new List<ZdravljenjaRazred>(zdravljenja);
            comboBoxZdravljenjeMesna.DisplayMember = "Vzrok";
            comboBoxZdravljenjeMesna.ValueMember = "Id";
            comboBoxZdravljenjeMesna.SelectedIndex = -1;

            comboBoxZdravljenjeMlecna.DataSource = new List<ZdravljenjaRazred>(zdravljenja);
            comboBoxZdravljenjeMlecna.DisplayMember = "Vzrok";
            comboBoxZdravljenjeMlecna.ValueMember = "Id";
            comboBoxZdravljenjeMlecna.SelectedIndex = -1;

            List<OsebeRazred> veterinarji = db.PridobiVeterinarje();

            comboBoxVeterinarMesna.DataSource = new List<OsebeRazred>(veterinarji);
            comboBoxVeterinarMesna.DisplayMember = "ImePriimek";
            comboBoxVeterinarMesna.ValueMember = "Id";
            comboBoxVeterinarMesna.SelectedIndex = -1;

            comboBoxVeterinarMlecna.DataSource = new List<OsebeRazred>(veterinarji);
            comboBoxVeterinarMlecna.DisplayMember = "ImePriimek";
            comboBoxVeterinarMlecna.ValueMember = "Id";
            comboBoxVeterinarMlecna.SelectedIndex = -1;
        }

        private bool ShraniKarence()
        {
            if (dateTimePickerDatumKoncaMes.Checked)
            {
                bool mesnaZdravljenjeIzbrano = comboBoxZdravljenjeMesna.SelectedIndex != -1;
                bool mesnaVeterinarIzbran = comboBoxVeterinarMesna.SelectedIndex != -1;

                if (!mesnaZdravljenjeIzbrano || !mesnaVeterinarIzbran)
                {
                    MessageBox.Show("Pri mesni karenci izberite zdravljenje in veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int zdravljenjeId = Convert.ToInt32(comboBoxZdravljenjeMesna.SelectedValue);
                int veterinarId = Convert.ToInt32(comboBoxVeterinarMesna.SelectedValue);

                db.ShraniKarenco(
                    "Mesna",
                    zdravljenjeId,
                    veterinarId,
                    dateTimePickerDatumKoncaMes.Value,
                    richTextBoxOpombeMes.Text,
                    Krava.Id
                );
            }
            else
            {
                db.IzbrisiKarenco(Krava.Id, "Mesna");
            }

            if (dateTimePickerDatumKoncaMlec.Checked)
            {
                bool mlecnaZdravljenjeIzbrano = comboBoxZdravljenjeMlecna.SelectedIndex != -1;
                bool mlecnaVeterinarIzbran = comboBoxVeterinarMlecna.SelectedIndex != -1;

                if (!mlecnaZdravljenjeIzbrano || !mlecnaVeterinarIzbran)
                {
                    MessageBox.Show("Pri mlečni karenci izberite zdravljenje in veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int zdravljenjeId = Convert.ToInt32(comboBoxZdravljenjeMlecna.SelectedValue);
                int veterinarId = Convert.ToInt32(comboBoxVeterinarMlecna.SelectedValue);

                db.ShraniKarenco(
                    "Mlečna",
                    zdravljenjeId,
                    veterinarId,
                    dateTimePickerDatumKoncaMlec.Value,
                    richTextBoxOpombeMlec.Text,
                    Krava.Id
                );
            }
            else
            {
                db.IzbrisiKarenco(Krava.Id, "Mlečna");
            }

            return true;
        }

        private void buttonZapri_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxIme.Text) ||
                string.IsNullOrWhiteSpace(textBoxPasma.Text) ||
                string.IsNullOrWhiteSpace(textBoxImeMame.Text) ||
                string.IsNullOrWhiteSpace(textBoxImeOceta.Text))
            {
                MessageBox.Show("Izpolnite vsa polja osnovnih podatkov.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Krava.Ime = textBoxIme.Text.Trim();
                Krava.DatumRoj = dateTimePicker.Value;
                Krava.Pasma = textBoxPasma.Text.Trim();
                Krava.ImeMame = textBoxImeMame.Text.Trim();
                Krava.ImeOceta = textBoxImeOceta.Text.Trim();

                if (string.IsNullOrWhiteSpace(textBoxUsSt.Text))
                    Krava.UsesnaSt = "/";
                else
                    Krava.UsesnaSt = textBoxUsSt.Text.Trim();

                int izvedba = db.UrediTelico(Krava);

                if (izvedba == 0)
                {
                    if (!ShraniKarence())
                    {
                        return;
                    }

                    kravaCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (izvedba == -1)
                {
                    MessageBox.Show("Krava ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju osnovnih podatkov krave: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajKontrolo_Click(object sender, EventArgs e)
        {
            try
            {
                DodajMlecKontrolo dodajMlecnoKontrolo = new DodajMlecKontrolo(db, Krava.Id);
                if (dodajMlecnoKontrolo.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Mlečna kontrola uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                NaloziMlecneKontrole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju mlečne kontrole: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOdhod_Click(object sender, EventArgs e)
        {
            try
            {
                DodajOdhodForm dodajOdhodForm = new DodajOdhodForm(db, Krava.UsesnaSt);
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

        private void buttonDodajOs_Click(object sender, EventArgs e)
        {
            try
            {
                DodajOsemenitevKrave dodajOsemenitevForm = new DodajOsemenitevKrave(db, Krava);
                if (dodajOsemenitevForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Osemenitev uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                NaloziOsemenitve();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju semenitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajPojatev_Click(object sender, EventArgs e)
        {
            try
            {
                DodajPojatevForm dodajPojatevForm = new DodajPojatevForm(db, Krava);

                if (dodajPojatevForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Pojatev uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziPojatve();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju pojatve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajTelitev_Click(object sender, EventArgs e)
        {
            try
            {
                DodajTelitevForm dodajTelitevForm = new DodajTelitevForm(db, Krava);

                if (dodajTelitevForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Telitev uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziTelitve();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajKorekcijo_Click(object sender, EventArgs e)
        {
            try
            {
                DodajKorekcijoParkljevForm dodajKorekcijoForm = new DodajKorekcijoParkljevForm(db, Krava);

                if (dodajKorekcijoForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Korekcija parkljev uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziKorekcije();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju korekcije parkljev: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajZdravljenje_Click(object sender, EventArgs e)
        {
            try
            {
                DodajZdravljenjeForm dodajZdravljenjeForm = new DodajZdravljenjeForm(db, Krava);

                if (dodajZdravljenjeForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Zdravljenje uspešno dodano.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziZdravljenja();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju zdravljenja: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDodajSpecifiko_Click(object sender, EventArgs e)
        {
            try
            {
                DodajSpecifikoForm dodajSpecifikoForm = new DodajSpecifikoForm(db, Krava);

                if (dodajSpecifikoForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Specifika uspešno dodana.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                NaloziOstaleSpecifike();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju specifike: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
