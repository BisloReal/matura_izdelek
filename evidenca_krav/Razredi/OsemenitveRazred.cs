using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class OsemenitveRazred
    {
        public int Id { get; set; }
        public int Zaporedna_Stevilka { get; set; }
        public int KravaId { get; set; }
        public int BikId { get; set; }
        public int VeterinarId { get; set; }
        public int VeterinarPregledaId { get; set; }

        public DateTime Datum_Osemenitve { get; set; }

        public string Veterinar { get; set; }
        public string Opombe { get; set; }
        public string Bik { get; set; }
        public string Krava { get; set; }

        public DateTime? Datum_Pregleda { get; set; }
        public string Izzid_Pregleda { get; set; }
        public string Nacin_Pregleda { get; set; }
        public string Opombe_Pregleda { get; set; }
        public string Veterinar_Pregleda { get; set; }

        public DateTime? Datum_Presusitve { get; set; }
        public string Opombe_Presusitve { get; set; }
        public string Kondicija_ob_Presusitvi { get; set; }

        public OsemenitveRazred(int id, int zapSt, int kravaId, int bikId, DateTime datumOsemenitve, string veterinar, string opombe, string bik, string krava)
        {
            Id = id;
            Zaporedna_Stevilka = zapSt;
            KravaId = kravaId;
            BikId = bikId;
            Datum_Osemenitve = datumOsemenitve;
            Veterinar = veterinar;
            Opombe = opombe;
            Bik = bik;
            Krava = krava;

            Datum_Pregleda = null;
            Izzid_Pregleda = "";
            Nacin_Pregleda = "";
            Opombe_Pregleda = "";
            Veterinar_Pregleda = "";

            Datum_Presusitve = null;
            Opombe_Presusitve = "";
            Kondicija_ob_Presusitvi = "";
        }

        public OsemenitveRazred(int id, int zapSt, int kravaId, int bikId, DateTime datumOsemenitve, string veterinar, string opombe, string bik, string krava,
            DateTime? datumPregleda, string izzidPregleda, string nacinPregleda, string opombePregleda, string veterinarPregleda,
            DateTime? datumPresusitve, string opombePresusitve, string kondicijaObPresusitvi)
        {
            Id = id;
            Zaporedna_Stevilka = zapSt;
            KravaId = kravaId;
            BikId = bikId;
            Datum_Osemenitve = datumOsemenitve;
            Veterinar = veterinar;
            Opombe = opombe;
            Bik = bik;
            Krava = krava;

            Datum_Pregleda = datumPregleda;
            Izzid_Pregleda = izzidPregleda;
            Nacin_Pregleda = nacinPregleda;
            Opombe_Pregleda = opombePregleda;
            Veterinar_Pregleda = veterinarPregleda;

            Datum_Presusitve = datumPresusitve;
            Opombe_Presusitve = opombePresusitve;
            Kondicija_ob_Presusitvi = kondicijaObPresusitvi;
        }
    }
}