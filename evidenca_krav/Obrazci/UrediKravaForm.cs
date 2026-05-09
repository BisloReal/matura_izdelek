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
    public partial class UrediKravaForm : Form
    {
        KraveRazred Krava;
        KravaCard kravaCard;
        private DatabaseHelper db;
        public UrediKravaForm(DatabaseHelper dbHelper, int idKrav, KravaCard kv)
        {
            InitializeComponent();
            db = dbHelper;
            kravaCard = kv;

            Krava = db.PridobiKravo(idKrav);

            if (Krava.DatumPregleda != null)
            {
                dateTimePickerDatumPregleda.Checked = true;
                dateTimePickerDatumPregleda.Value = Krava.DatumPregleda.Value;
            }

            if (Krava.DatumPrvegaIztoka != null)
            {
                dateTimePickerPrviIztok.Checked = true;
                dateTimePickerPrviIztok.Value = Krava.DatumPrvegaIztoka.Value;
            }

            if (Krava.DatumDrugegaIztoka != null)
            {
                dateTimePickerDrugiIztok.Checked = true;
                dateTimePickerDrugiIztok.Value = Krava.DatumDrugegaIztoka.Value;
            }

            textBoxIme.Text = Krava.Ime;
            textBoxUsSt.Text = Krava.UsesnaSt;
            textBoxPasma.Text = Krava.Pasma;
            dateTimePicker.Value = Krava.DatumRoj;
            textBoxImeMame.Text = Krava.ImeMame;
            textBoxImeOceta.Text = Krava.ImeOceta;
            textBoxLaktacija.Text = Krava.Laktacija;
            textBoxteza.Text = Krava.Teza.ToString();
            textBoxodsvetovaniBiki.Text = Krava.OdsvetovaniBiki;
            textBoxprimerniBiki.Text = Krava.PrimerniBiki;
            textBoxnajboljPrimerniBiki.Text = Krava.NajboljPrimerniBiki;
            textBoxIztokEna.Text = Krava.IztokMlekaOcena.ToString();
            textBoxIztokDva.Text = Krava.IztokMlekaOcenaDruga.ToString();
            textBoxobsegPrsi.Text = Krava.ObsegPrsi.ToString();
            textBoxvisinaKriza.Text = Krava.VisinaKriza.ToString();
            textBoxglobinaTelesa.Text = Krava.GlobinaTelesa.ToString();
            textBoxsirinaVspredaj.Text = Krava.SirinaVspredaj.ToString();
            textBoxhrbetOcena.Text = Krava.HrbetOcena.ToString();
            textBoxdolzinaKriza.Text = Krava.DolzinaKriza.ToString();
            textBoxsednaSirina.Text = Krava.SednaSirina.ToString();
            textBoxnagibKrizaOcena.Text = Krava.NagibKrizaOcena.ToString();
            textBoxpolozajKolkaOcena.Text = Krava.PolozajKolkaOcena.ToString();
            textBoxskocniSklepOcena.Text = Krava.SkocniSklepOcena.ToString();
            textBoxizrazSkocSklepaOcena.Text = Krava.IzrazSkocSklepaOcena.ToString();
            textBoxbiceljOcena.Text = Krava.BiceljOcena.ToString();
            textBoxparkljiOcena.Text = Krava.ParkljiOcena.ToString();
            textBoxdolzinaVimenaOcena.Text = Krava.DolzinaVimenaOcena.ToString();
            textBoxpripetostVimenaOcena.Text = Krava.PripetostVimenaOcena.ToString();
            textBoxvisinaMlecnegaZrcalaOcena.Text = Krava.VisinaMlecnegaZrcalaOcena.ToString();
            textBoxsirinaMlecnegaZrcalaOcena.Text = Krava.SirinaMlecnegaZrcalaOcena.ToString();
            textBoxglobinaVimenaOcena.Text = Krava.GlobinaVimenaOcena.ToString();
            textBoxdnoVimenaOcena.Text = Krava.DnoVimenaOcena.ToString();
            textBoxglobinaCentVeziOcena.Text = Krava.GlobinaCentVeziOcena.ToString();
            textBoxdolzinaSeskovOcena.Text = Krava.DolzinaSeskovOcena.ToString();
            textBoxdebelinaSeskovOcena.Text = Krava.DebelinaSeskovOcena.ToString();
            textBoxnamenostPrednjihSeskovOcena.Text = Krava.NamenostPrednjihSeskovOcena.ToString();
            textBoxnamenostZadnjihSeskovOcena.Text = Krava.NamenostZadnjihSeskovOcena.ToString();
            textBoxpolozajZadnjihSeskovOcena.Text = Krava.PolozajZadnjihSeskovOcena.ToString();
            textBoxomisicanostOcena.Text = Krava.OmisicanostOcena.ToString();
            textBoxkondicijaOcena.Text = Krava.kondicijaOcena.ToString();
            textBoxvisinaKrizaIzracunOcena.Text = Krava.VisinaKrizaIzracunOcena.ToString();
            textBoxglobinaTelesaIzracunOcena.Text = Krava.GlobinaTelesaIzracunOcena.ToString();
            textBoxdolzinaKrizaIzracunOcena.Text = Krava.DolzinaKrizaIzracunOcena.ToString();
            textBoxsednaSirinaIzracunOcena.Text = Krava.SednaSirinaIzracunOcena.ToString();
            textBoxokvirOcena.Text = Krava.OkvirOcena.ToString();
            textBoxkrizOcena.Text = Krava.KrizOcena.ToString();
            textBoxnogeOcena.Text = Krava.NogeOcena.ToString();
            textBoxvimeOcena.Text = Krava.VimeOcena.ToString();
            textBoxtelesneSposobnostiSkupajOcena.Text = Krava.TelesneSposobnostiSkupajOcena.ToString();


            NaloziMlecneKontrole();
            NaloziOsemenitve();
            NaloziPojatve();
            NaloziTelitve();
            NaloziKorekcije();
            NaloziZdravljenja();
            NaloziComboBoxeKarence();
            NaloziKarence();
            NaloziPotomce();
        }

        private void NaloziPotomce()
        {
            flowLayoutPanelPotomci.Controls.Clear();

            List<TeliceRazred> potomci = db.PridobiPotomceKrave(Krava.Id);

            foreach (TeliceRazred tr in potomci)
            {
                flowLayoutPanelPotomci.Controls.Add(new PotomecCard(db, tr));
            }
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
                string.IsNullOrWhiteSpace(textBoxImeOceta.Text) ||
                string.IsNullOrWhiteSpace(textBoxLaktacija.Text))
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
                Krava.Laktacija = textBoxLaktacija.Text.Trim();
                Krava.OdsvetovaniBiki = textBoxodsvetovaniBiki.Text.Trim();
                Krava.PrimerniBiki = textBoxprimerniBiki.Text.Trim();
                Krava.NajboljPrimerniBiki = textBoxnajboljPrimerniBiki.Text.Trim();

                if (string.IsNullOrWhiteSpace(textBoxUsSt.Text))
                    Krava.UsesnaSt = "/";
                else
                    Krava.UsesnaSt = textBoxUsSt.Text.Trim();

                Krava.Teza = Convert.ToSingle(textBoxteza.Text);
                Krava.ObsegPrsi = Convert.ToSingle(textBoxobsegPrsi.Text);
                Krava.VisinaKriza = Convert.ToSingle(textBoxvisinaKriza.Text);
                Krava.GlobinaTelesa = Convert.ToSingle(textBoxglobinaTelesa.Text);
                Krava.DolzinaKriza = Convert.ToSingle(textBoxdolzinaKriza.Text);
                Krava.SednaSirina = Convert.ToSingle(textBoxsednaSirina.Text);
                
                Krava.SirinaVspredaj = Convert.ToInt32(textBoxsirinaVspredaj.Text);
                Krava.HrbetOcena = Convert.ToInt32(textBoxhrbetOcena.Text);
                Krava.NagibKrizaOcena = Convert.ToInt32(textBoxnagibKrizaOcena.Text);
                Krava.PolozajKolkaOcena = Convert.ToInt32(textBoxpolozajKolkaOcena.Text);
                Krava.SkocniSklepOcena = Convert.ToInt32(textBoxskocniSklepOcena.Text);
                Krava.IzrazSkocSklepaOcena = Convert.ToInt32(textBoxizrazSkocSklepaOcena.Text);
                Krava.BiceljOcena = Convert.ToInt32(textBoxbiceljOcena.Text);
                Krava.ParkljiOcena = Convert.ToInt32(textBoxparkljiOcena.Text);
                Krava.DolzinaVimenaOcena = Convert.ToInt32(textBoxdolzinaVimenaOcena.Text);
                Krava.PripetostVimenaOcena = Convert.ToInt32(textBoxpripetostVimenaOcena.Text);
                Krava.VisinaMlecnegaZrcalaOcena = Convert.ToInt32(textBoxvisinaMlecnegaZrcalaOcena.Text);
                Krava.SirinaMlecnegaZrcalaOcena = Convert.ToInt32(textBoxsirinaMlecnegaZrcalaOcena.Text);
                Krava.GlobinaVimenaOcena = Convert.ToInt32(textBoxglobinaVimenaOcena.Text);
                Krava.DnoVimenaOcena = Convert.ToInt32(textBoxdnoVimenaOcena.Text);
                Krava.GlobinaCentVeziOcena = Convert.ToInt32(textBoxglobinaCentVeziOcena.Text);
                Krava.DolzinaSeskovOcena = Convert.ToInt32(textBoxdolzinaSeskovOcena.Text);
                Krava.DebelinaSeskovOcena = Convert.ToInt32(textBoxdebelinaSeskovOcena.Text);
                Krava.NamenostPrednjihSeskovOcena = Convert.ToInt32(textBoxnamenostPrednjihSeskovOcena.Text);
                Krava.NamenostZadnjihSeskovOcena = Convert.ToInt32(textBoxnamenostZadnjihSeskovOcena.Text);
                Krava.PolozajZadnjihSeskovOcena = Convert.ToInt32(textBoxpolozajZadnjihSeskovOcena.Text);
                Krava.OmisicanostOcena = Convert.ToInt32(textBoxomisicanostOcena.Text);
                Krava.kondicijaOcena = Convert.ToInt32(textBoxkondicijaOcena.Text);
                Krava.VisinaKrizaIzracunOcena = Convert.ToInt32(textBoxvisinaKrizaIzracunOcena.Text);
                Krava.GlobinaTelesaIzracunOcena = Convert.ToInt32(textBoxglobinaTelesaIzracunOcena.Text);
                Krava.DolzinaKrizaIzracunOcena = Convert.ToInt32(textBoxdolzinaKrizaIzracunOcena.Text);
                Krava.SednaSirinaIzracunOcena = Convert.ToInt32(textBoxsednaSirinaIzracunOcena.Text);
                Krava.OkvirOcena = Convert.ToInt32(textBoxokvirOcena.Text);
                Krava.KrizOcena = Convert.ToInt32(textBoxkrizOcena.Text);
                Krava.NogeOcena = Convert.ToInt32(textBoxnogeOcena.Text);
                Krava.VimeOcena = Convert.ToInt32(textBoxvimeOcena.Text);
                Krava.TelesneSposobnostiSkupajOcena = Convert.ToInt32(textBoxtelesneSposobnostiSkupajOcena.Text);

                if (dateTimePickerDatumPregleda.Checked)
                {
                    Krava.DatumPregleda = dateTimePickerDatumPregleda.Value;
                }
                else
                {
                    Krava.DatumPregleda = null;
                }

                Krava.IztokMlekaOcena = Convert.ToInt32(textBoxIztokEna.Text);
                if (dateTimePickerPrviIztok.Checked)
                {
                    Krava.DatumPrvegaIztoka = dateTimePickerPrviIztok.Value;
                }
                else
                {
                    Krava.DatumPrvegaIztoka = null;
                }

                Krava.IztokMlekaOcenaDruga = Convert.ToInt32(textBoxIztokDva.Text);
                if (dateTimePickerDrugiIztok.Checked)
                {
                    Krava.DatumDrugegaIztoka = dateTimePickerDrugiIztok.Value;
                }
                else
                {
                    Krava.DatumDrugegaIztoka = null;
                }

                int izvedba = db.UrediKravo(Krava);

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
