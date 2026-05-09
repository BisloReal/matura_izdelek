using System;

namespace evidenca_krav.Razredi
{
    public class OstaleSpecifikeRazred
    {
        public int Id { get; set; }
        public string Specifika { get; set; }
        public DateTime? Datum { get; set; }
        public int KravaId { get; set; }

        public OstaleSpecifikeRazred(int id, string specifika, int kravaId)
        {
            Id = id;
            Specifika = specifika;
            KravaId = kravaId;
        }

        public OstaleSpecifikeRazred(int id, string specifika, DateTime? datum, int kravaId)
        {
            Id = id;
            Specifika = specifika;
            Datum = datum;
            KravaId = kravaId;
        }
    }
}