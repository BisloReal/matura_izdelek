using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class KorekcijeParkljevRazred
    {
        public int Id { get; set; }
        public int KravaId { get; set; }
        public string Krava { get; set; }
        public DateTime Datum { get; set; }
        public string Stanje { get; set; }
        public string Pripombe { get; set; }
        public int IzvajalecId { get; set; }
        public string Izvajalec { get; set; }

        public KorekcijeParkljevRazred(int id, int kravaId, string krava, DateTime datum, string stanje, string pripombe, int izvajalecId, string izvajalec)
        {
            Id = id;
            KravaId = kravaId;
            Krava = krava;
            Datum = datum;
            Stanje = stanje;
            Pripombe = pripombe;
            IzvajalecId = izvajalecId;
            Izvajalec = izvajalec;
        }
    }
}
