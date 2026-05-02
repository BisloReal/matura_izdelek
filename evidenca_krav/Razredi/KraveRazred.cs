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
        public string Laktacija { get; set; }
        public string OdsvetovaniBiki { get; set; }
        public string PrimerniBiki { get; set; }
        public string NajboljPrimerniBiki { get; set; }
        public float Teza { get; set; }
        public int IztokMlekaOcena { get; set; }
        public float ObsegPrsi { get; set; }
        public float VisinaKriza { get; set; }
        public float GlobinaTelesa { get; set; }
        public int SirinaVspredaj { get; set; }
        public int HrbetOcena { get; set; }
        public float DolzinaKriza { get; set; }
        public float SednaSirina { get; set; }
        public int NagibKrizaOcena { get; set; }
        public int PolozajKolkaOcena { get; set; }
        public int SkocniSklepOcena { get; set; }
        public int IzrazSkocSklepaOcena { get; set; }
        public int BiceljOcena { get; set; }
        public int ParkljiOcena { get; set; }
        public int DolzinaVimenaOcena { get; set; }
        public int PripetostVimenaOcena { get; set; }
        public int VisinaMlecnegaZrcalaOcena { get; set; }
        public int SirinaMlecnegaZrcalaOcena { get; set; }
        public int GlobinaVimenaOcena { get; set; }
        public int DnoVimenaOcena { get; set; }
        public int GlobinaCentVeziOcena { get; set; }
        public int DolzinaSeskovOcena { get; set; }
        public int DebelinaSeskovOcena { get; set; }
        public int NamenostPrednjihSeskovOcena { get; set; }
        public int NamenostZadnjihSeskovOcena { get; set; }
        public int PolozajZadnjihSeskovOcena { get; set; }
        public int OmisicanostOcena { get; set; }
        public int kondicijaOcena { get; set; }
        public int VisinaKrizaIzracunOcena { get; set; }
        public int GlobinaTelesaIzracunOcena { get; set; }
        public int DolzinaKrizaIzracunOcena { get; set; }
        public int SednaSirinaIzracunOcena { get; set; }
        public int OkvirOcena { get; set; }
        public int KrizOcena { get; set; }
        public int NogeOcena { get; set; }
        public int VimeOcena { get; set; }
        public int TelesneSposobnostiSkupajOcena { get; set; }


        public KraveRazred(int idTel, string imeTel, DateTime datumRoj, string pasmaTel, string imeMameTel, string imeOcetaTel, string usesnaStTel, string laktacija) : base(idTel, imeTel, datumRoj, pasmaTel, imeMameTel, imeOcetaTel, usesnaStTel) 
        { 
            Laktacija = laktacija;
        }
    }
}
