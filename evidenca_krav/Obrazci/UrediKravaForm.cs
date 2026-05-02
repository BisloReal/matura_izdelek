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
            textBoxIme.Text = Krava.ime;
            textBoxUsSt.Text = Krava.usesnaSt;
            textBoxPasma.Text = Krava.pasma;
            textBoxImeMame.Text = Krava.imeMame;
            textBoxImeOceta.Text = Krava.imeOceta;
            textBoxLaktacija.Text = Krava.laktacija;
            textBoxteza.Text = Krava.teza.ToString();
            textBoxodsvetovaniBiki.Text = Krava.odsvetovaniBiki;
            textBoxprimerniBiki.Text = Krava.primerniBiki;
            textBoxnajboljPrimerniBiki.Text = Krava.najboljPrimerniBiki;
            textBoxiztokMlekaOcena.Text = Krava.iztokMlekaOcena.ToString();
            textBoxobsegPrsi.Text = Krava.obsegPrsi.ToString();
            textBoxvisinaKriza.Text = Krava.visinaKriza.ToString();
            textBoxglobinaTelesa.Text = Krava.globinaTelesa.ToString();
            textBoxsirinaVspredaj.Text = Krava.sirinaVspredaj.ToString();
            textBoxhrbetOcena.Text = Krava.hrbetOcena.ToString();
            textBoxdolzinaKriza.Text = Krava.dolzinaKriza.ToString();
            textBoxsednaSirina.Text = Krava.sednaSirina.ToString();
            textBoxnagibKrizaOcena.Text = Krava.nagibKrizaOcena.ToString();
            textBoxpolozajKolkaOcena.Text = Krava.polozajKolkaOcena.ToString();
            textBoxskocniSklepOcena.Text = Krava.skocniSklepOcena.ToString();
            textBoxizrazSkocSklepaOcena.Text = Krava.izrazSkocSklepaOcena.ToString();
            textBoxbiceljOcena.Text = Krava.biceljOcena.ToString();
            textBoxparkljiOcena.Text = Krava.parkljiOcena.ToString();
            textBoxdolzinaVimenaOcena.Text = Krava.dolzinaVimenaOcena.ToString();
            textBoxpripetostVimenaOcena.Text = Krava.pripetostVimenaOcena.ToString();
            textBoxvisinaMlecnegaZrcalaOcena.Text = Krava.visinaMlecnegaZrcalaOcena.ToString();
            textBoxsirinaMlecnegaZrcalaOcena.Text = Krava.sirinaMlecnegaZrcalaOcena.ToString();
            textBoxglobinaVimenaOcena.Text = Krava.globinaVimenaOcena.ToString();
            textBoxdnoVimenaOcena.Text = Krava.dnoVimenaOcena.ToString();
            textBoxglobinaCentVeziOcena.Text = Krava.globinaCentVeziOcena.ToString();
            textBoxdolzinaSeskovOcena.Text = Krava.dolzinaSeskovOcena.ToString();
            textBoxdebelinaSeskovOcena.Text = Krava.debelinaSeskovOcena.ToString();
            textBoxnamenostPrednjihSeskovOcena.Text = Krava.namenostPrednjihSeskovOcena.ToString();
            textBoxnamenostZadnjihSeskovOcena.Text = Krava.namenostZadnjihSeskovOcena.ToString();
            textBoxpolozajZadnjihSeskovOcena.Text = Krava.polozajZadnjihSeskovOcena.ToString();
            textBoxomisicanostOcena.Text = Krava.omisicanostOcena.ToString();
            textBoxkondicijaOcena.Text = Krava.kondicijaOcena.ToString();
            textBoxvisinaKrizaIzracunOcena.Text = Krava.visinaKrizaIzracunOcena.ToString();
            textBoxglobinaTelesaIzracunOcena.Text = Krava.globinaTelesaIzracunOcena.ToString();
            textBoxdolzinaKrizaIzracunOcena.Text = Krava.dolzinaKrizaIzracunOcena.ToString();
            textBoxsednaSirinaIzracunOcena.Text = Krava.sednaSirinaIzracunOcena.ToString();
            textBoxokvirOcena.Text = Krava.okvirOcena.ToString();
            textBoxkrizOcena.Text = Krava.krizOcena.ToString();
            textBoxnogeOcena.Text = Krava.nogeOcena.ToString();
            textBoxvimeOcena.Text = Krava.vimeOcena.ToString();
            textBoxtelesneSposobnostiSkupajOcena.Text = Krava.telesneSposobnostiSkupajOcena.ToString();
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
                Krava.ime = textBoxIme.Text.Trim();
                Krava.datumRoj = dateTimePicker.Value;
                Krava.pasma = textBoxPasma.Text.Trim();
                Krava.imeMame = textBoxImeMame.Text.Trim();
                Krava.imeOceta = textBoxImeOceta.Text.Trim();
                Krava.laktacija = textBoxLaktacija.Text.Trim();
                Krava.odsvetovaniBiki = textBoxodsvetovaniBiki.Text.Trim();
                Krava.primerniBiki = textBoxprimerniBiki.Text.Trim();
                Krava.najboljPrimerniBiki = textBoxnajboljPrimerniBiki.Text.Trim();

                if (string.IsNullOrWhiteSpace(textBoxUsSt.Text))
                    Krava.usesnaSt = "/";
                else
                    Krava.usesnaSt = textBoxUsSt.Text.Trim();

                Krava.teza = Convert.ToSingle(textBoxteza.Text);
                Krava.obsegPrsi = Convert.ToSingle(textBoxobsegPrsi.Text);
                Krava.visinaKriza = Convert.ToSingle(textBoxvisinaKriza.Text);
                Krava.globinaTelesa = Convert.ToSingle(textBoxglobinaTelesa.Text);
                Krava.dolzinaKriza = Convert.ToSingle(textBoxdolzinaKriza.Text);
                Krava.sednaSirina = Convert.ToSingle(textBoxsednaSirina.Text);

                Krava.iztokMlekaOcena = Convert.ToInt32(textBoxiztokMlekaOcena.Text);
                Krava.sirinaVspredaj = Convert.ToInt32(textBoxsirinaVspredaj.Text);
                Krava.hrbetOcena = Convert.ToInt32(textBoxhrbetOcena.Text);
                Krava.nagibKrizaOcena = Convert.ToInt32(textBoxnagibKrizaOcena.Text);
                Krava.polozajKolkaOcena = Convert.ToInt32(textBoxpolozajKolkaOcena.Text);
                Krava.skocniSklepOcena = Convert.ToInt32(textBoxskocniSklepOcena.Text);
                Krava.izrazSkocSklepaOcena = Convert.ToInt32(textBoxizrazSkocSklepaOcena.Text);
                Krava.biceljOcena = Convert.ToInt32(textBoxbiceljOcena.Text);
                Krava.parkljiOcena = Convert.ToInt32(textBoxparkljiOcena.Text);
                Krava.dolzinaVimenaOcena = Convert.ToInt32(textBoxdolzinaVimenaOcena.Text);
                Krava.pripetostVimenaOcena = Convert.ToInt32(textBoxpripetostVimenaOcena.Text);
                Krava.visinaMlecnegaZrcalaOcena = Convert.ToInt32(textBoxvisinaMlecnegaZrcalaOcena.Text);
                Krava.sirinaMlecnegaZrcalaOcena = Convert.ToInt32(textBoxsirinaMlecnegaZrcalaOcena.Text);
                Krava.globinaVimenaOcena = Convert.ToInt32(textBoxglobinaVimenaOcena.Text);
                Krava.dnoVimenaOcena = Convert.ToInt32(textBoxdnoVimenaOcena.Text);
                Krava.globinaCentVeziOcena = Convert.ToInt32(textBoxglobinaCentVeziOcena.Text);
                Krava.dolzinaSeskovOcena = Convert.ToInt32(textBoxdolzinaSeskovOcena.Text);
                Krava.debelinaSeskovOcena = Convert.ToInt32(textBoxdebelinaSeskovOcena.Text);
                Krava.namenostPrednjihSeskovOcena = Convert.ToInt32(textBoxnamenostPrednjihSeskovOcena.Text);
                Krava.namenostZadnjihSeskovOcena = Convert.ToInt32(textBoxnamenostZadnjihSeskovOcena.Text);
                Krava.polozajZadnjihSeskovOcena = Convert.ToInt32(textBoxpolozajZadnjihSeskovOcena.Text);
                Krava.omisicanostOcena = Convert.ToInt32(textBoxomisicanostOcena.Text);
                Krava.kondicijaOcena = Convert.ToInt32(textBoxkondicijaOcena.Text);
                Krava.visinaKrizaIzracunOcena = Convert.ToInt32(textBoxvisinaKrizaIzracunOcena.Text);
                Krava.globinaTelesaIzracunOcena = Convert.ToInt32(textBoxglobinaTelesaIzracunOcena.Text);
                Krava.dolzinaKrizaIzracunOcena = Convert.ToInt32(textBoxdolzinaKrizaIzracunOcena.Text);
                Krava.sednaSirinaIzracunOcena = Convert.ToInt32(textBoxsednaSirinaIzracunOcena.Text);
                Krava.okvirOcena = Convert.ToInt32(textBoxokvirOcena.Text);
                Krava.krizOcena = Convert.ToInt32(textBoxkrizOcena.Text);
                Krava.nogeOcena = Convert.ToInt32(textBoxnogeOcena.Text);
                Krava.vimeOcena = Convert.ToInt32(textBoxvimeOcena.Text);
                Krava.telesneSposobnostiSkupajOcena = Convert.ToInt32(textBoxtelesneSposobnostiSkupajOcena.Text);

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


    }
}
