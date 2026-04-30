using evidenca_krav.Razredi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.RazredSi
{
    public class KraveRazred : TeliceRazred
    {
        public string laktacija { get; set; }
        public string odsvetovaniBiki { get; set; }
        public string primerniBiki { get; set; }
        public string najboljPrimerniBiki { get; set; }
        public float teza { get; set; }
        public int iztokMlekaOcena { get; set; }
        public float obsegPrsi { get; set; }
        public float visinaKriza { get; set; }
        public float globinaTelesa { get; set; }
        public int sirinaVspredaj { get; set; }
        public int hrbetOcena { get; set; }
        public float dolzinaKriza { get; set; }
        public float sednaSirina { get; set; }
        public int nagibKrizaOcena { get; set; }
        public int polozajKolkaOcena { get; set; }
        public int skocniSklepOcena { get; set; }
        public int izrazSkocSklepaOcena { get; set; }
        public int biceljOcena { get; set; }
        public int parkljiOcena { get; set; }
        public int dolzinaVimenaOcena { get; set; }
        public int pripetostVimenaOcena { get; set; }
        public int visinaMlecnegaZrcalaOcena { get; set; }
        public int sirinaMlecnegaZrcalaOcena { get; set; }
        public int globinaVimenaOcena { get; set; }
        public int dnoVimenaOcena { get; set; }
        public int globinaCentVeziOcena { get; set; }
        public int dolzinaSeskovOcena { get; set; }
        public int debelinaSeskovOcena { get; set; }   
        public int namenostPrednjihSeskovOcena { get; set; }    
        public int namenostZadnjihSeskovOcena { get; set; }
        public int polozajZadnjihSeskovOcena { get; set; }
        public int omisicanostOcena { get; set; }
        public int kondicijaOcena   { get; set; }
        public int visinaKrizaIzracunOcena { get; set; }
        public int globinaTelesaIzracunOcena { get; set; }
        public int dolzinaKrizaIzracunOcena { get; set; }
        public int sednaSirinaIzracunOcena { get; set; }
        public int okvirOcena { get; set; }
        public int krizOcena { get; set; }
        public int nogeOcena { get; set; }
        public int vimeOcena { get; set; }
        public int telesneSposobnostiSkupajOcena { get; set; }


        public KraveRazred(int idTel, string imeTel, DateTime datumRoj, string pasmaTel, string imeMameTel, string imeOcetaTel, string usesnaStTel, string laktacija) : base(idTel, imeTel, datumRoj, pasmaTel, imeMameTel, imeOcetaTel, usesnaStTel) { }
    }
}
