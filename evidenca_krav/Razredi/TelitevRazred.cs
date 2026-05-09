using System;

namespace evidenca_krav.Razredi
{
    public class TelitevRazred
    {
        public int Id { get; set; }
        public int ZaporednoStevilo { get; set; }

        public string Potek { get; set; }
        public string Rojstvo { get; set; }
        public string KakovostMleziva { get; set; }

        public int TeleId { get; set; }
        public string Tele { get; set; }

        public int BikId { get; set; }
        public string Bik { get; set; }

        public int KravaMamaId { get; set; }
        public string KravaMama { get; set; }

        public DateTime Datum { get; set; }

        public TelitevRazred(int id, int zaporednoStevilo, string potek, string rojstvo, string kakovostMleziva,
            int teleId, string tele, int bikId, string bik, int kravaMamaId, string kravaMama, DateTime datum)
        {
            Id = id;
            ZaporednoStevilo = zaporednoStevilo;
            Potek = potek;
            Rojstvo = rojstvo;
            KakovostMleziva = kakovostMleziva;

            TeleId = teleId;
            Tele = tele;

            BikId = bikId;
            Bik = bik;

            KravaMamaId = kravaMamaId;
            KravaMama = kravaMama;

            Datum = datum;
        }
    }
}