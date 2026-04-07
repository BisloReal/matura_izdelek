using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class TeliceRazred
    {
        public int idTel {  get; set; }
        public string imeTel {  get; set; }
        public DateTime datumRoj { get; set; }
        public string pasmaTel { get; set; }
        public string imeMameTel { get; set; }
        public string imeOcetaTel { get; set; }

        public TeliceRazred(int idTel, string imeTel, DateTime datumRoj, string pasmaTel, string imeMameTel, string imeOcetaTel)
        {
            this.idTel = idTel;
            this.imeTel = imeTel;
            this.datumRoj = datumRoj;
            this.pasmaTel = pasmaTel;
            this.imeMameTel = imeMameTel;
            this.imeOcetaTel = imeOcetaTel;
        }
    }
}
