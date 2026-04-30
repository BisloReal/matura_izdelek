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
    }
}
