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
        public string pasmaBik { get; set; }
        public string izboljsuje { get; set; }

        public BikiOsRazred(int idBik, string rejec, DateTime datumRoj, string pasmaBik, string izboljsuje)
        {
            this.idBik = idBik;
            this.rejec = rejec;
            this.datumRoj = datumRoj;
            this.pasmaBik = pasmaBik;
            this.izboljsuje = izboljsuje;
        }
    }
}
