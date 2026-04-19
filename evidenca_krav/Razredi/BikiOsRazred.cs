using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class BikiOsRazred
    {
        public int idBik { get; set; }
        public string rejec { get; set; }
        public DateTime datumRoj { get; set; }
        public string pasma { get; set; }
        public int pasmaBikId { get; set; }
        public string izboljsuje { get; set; }

        public BikiOsRazred(int idBik, string rejec, DateTime datumRoj, string pasma, string izboljsuje)
        {
            this.idBik = idBik;
            this.rejec = rejec;
            this.datumRoj = datumRoj;
            this.pasma = pasma;
            this.izboljsuje = izboljsuje;
        }

        public BikiOsRazred(int idBik, string rejec, DateTime datumRoj, int pasmaBikId, string izboljsuje)
        {
            this.idBik = idBik;
            this.rejec = rejec;
            this.datumRoj = datumRoj;
            this.pasmaBikId = pasmaBikId;
            this.izboljsuje = izboljsuje;
        }

        public BikiOsRazred(int idBik, string rejec, DateTime datumRoj, int pasmaBikId, string pasma, string izboljsuje)
        {
            this.idBik = idBik;
            this.rejec = rejec;
            this.datumRoj = datumRoj;
            this.pasmaBikId = pasmaBikId;
            this.pasma = pasma;
            this.izboljsuje = izboljsuje;
        }
    }
}
