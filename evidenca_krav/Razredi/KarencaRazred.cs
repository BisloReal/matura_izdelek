using System;

namespace evidenca_krav.Razredi
{
    public class KarencaRazred
    {
        public int Id { get; set; }
        public string VrstaKarence { get; set; }
        public int ZdravljenjeId { get; set; }
        public int VeterinarId { get; set; }
        public DateTime DatumKonca { get; set; }
        public string Opombe { get; set; }
        public int ZivaliId { get; set; }

        public KarencaRazred(int id, string vrstaKarence, int zdravljenjeId, int veterinarId, DateTime datumKonca, string opombe, int zivaliId)
        {
            Id = id;
            VrstaKarence = vrstaKarence;
            ZdravljenjeId = zdravljenjeId;
            VeterinarId = veterinarId;
            DatumKonca = datumKonca;
            Opombe = opombe;
            ZivaliId = zivaliId;
        }
    }
}