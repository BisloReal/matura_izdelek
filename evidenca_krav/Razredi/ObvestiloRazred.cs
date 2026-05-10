using System;

namespace evidenca_krav.Razredi
{
    public class ObvestiloRazred
    {
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public DateTime Datum { get; set; }
        public string Tip { get; set; }

        public ObvestiloRazred(string naslov, string opis, DateTime datum, string tip)
        {
            Naslov = naslov;
            Opis = opis;
            Datum = datum;
            Tip = tip;
        }
    }
}