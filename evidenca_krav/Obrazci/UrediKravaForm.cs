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
            textBoxIme.Text = Krava.Ime;
            textBoxUsSt.Text = Krava.UsesnaSt;
            textBoxPasma.Text = Krava.Pasma;
            textBoxImeMame.Text = Krava.ImeMame;
            textBoxImeOceta.Text = Krava.ImeOceta;
            textBoxLaktacija.Text = Krava.Laktacija;
            textBoxteza.Text = Krava.Teza.ToString();
            textBoxodsvetovaniBiki.Text = Krava.OdsvetovaniBiki;
            textBoxprimerniBiki.Text = Krava.PrimerniBiki;
            textBoxnajboljPrimerniBiki.Text = Krava.NajboljPrimerniBiki;
            textBoxiztokMlekaOcena.Text = Krava.IztokMlekaOcena.ToString();
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

                Krava.IztokMlekaOcena = Convert.ToInt32(textBoxiztokMlekaOcena.Text);
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

                int izvedba = db.UrediKravo(Krava);

                if (izvedba == 0)
                {
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
    }
}
