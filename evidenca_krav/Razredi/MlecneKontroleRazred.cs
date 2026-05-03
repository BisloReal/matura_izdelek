using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class MlecneKontroleRazred
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int Zaporedna_Stevilka { get; set; }
        public string Ime_Priimek_Osebe { get; set; }
        public string Del_dneva { get; set; }
        public string Mlecnost { get; set; }
        public string Vsebnost_Mascobe { get; set; }
        public string Vsebnost_Beljakovin { get; set; }
        public string Vsebnost_Laktaze { get; set; }
        public string Vsebnost_Secnice { get; set; }
        public string Somatske_Celice { get; set; }
        public string Opombe { get; set; }
        public string UsStKrave { get; set; }

        public MlecneKontroleRazred(int id, DateTime datum, int zaporednaStevilka, string imePriimekOsebe, string delDneva, string mlecnost, string vsebnostMascobe, string vsebnostBeljakovin, string vsebnostLaktaze, string vsebnostSecnice, string somatskeCelice, string opombe, string usStKrave)
        {
            Id = id;
            Datum = datum;
            Zaporedna_Stevilka = zaporednaStevilka;
            Ime_Priimek_Osebe = imePriimekOsebe;
            Del_dneva = delDneva;
            Mlecnost = mlecnost;
            Vsebnost_Mascobe = vsebnostMascobe;
            Vsebnost_Beljakovin = vsebnostBeljakovin;
            Vsebnost_Laktaze = vsebnostLaktaze;
            Vsebnost_Secnice = vsebnostSecnice;
            Somatske_Celice = somatskeCelice;
            Opombe = opombe;
            UsStKrave = usStKrave;
        }
    }
}
