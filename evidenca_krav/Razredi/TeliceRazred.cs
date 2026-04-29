using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class TeliceRazred
    {
        public int id {  get; set; }
        public string ime {  get; set; }
        public string usesnaSt { get; set; }
        public DateTime datumRoj { get; set; }
        public string pasma { get; set; }
        public string imeMame { get; set; }
        public string imeOceta { get; set; }

        public TeliceRazred(int idTel, string imeTel, DateTime datumRoj, string pasmaTel, string imeMameTel, string imeOcetaTel, string usesnaStTel)
        {
            id = idTel;
            ime = imeTel;
            this.datumRoj = datumRoj;
            pasma = pasmaTel;
            imeMame = imeMameTel;
            imeOceta = imeOcetaTel;
            usesnaSt = usesnaStTel;
        }
    }
}
