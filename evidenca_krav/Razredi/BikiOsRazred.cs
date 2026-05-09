using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class BikiOsRazred
    {
        public int IdBik { get; set; }
        public string Ime { get; set; }
        public string Stevilka { get; set; }
        public string Rejec { get; set; }
        public DateTime DatumRoj { get; set; }
        public string Pasma { get; set; }
        public int PasmaBikId { get; set; }
        public string Izboljsuje { get; set; }

        public BikiOsRazred(int idBik, string ime, string stevilka, string rejec, DateTime datumRoj, int pasmaBikId, string pasma, string izboljsuje)
        {
            IdBik = idBik;
            Ime = ime;
            Stevilka = stevilka;
            Rejec = rejec;
            DatumRoj = datumRoj;
            PasmaBikId = pasmaBikId;
            Pasma = pasma;
            Izboljsuje = izboljsuje;
        }
    }
}
