using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class TeliceRazred
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string UsesnaSt { get; set; }
        public DateTime DatumRoj { get; set; }
        public string Pasma { get; set; }
        public string ImeMame { get; set; }
        public string ImeOceta { get; set; }

        public TeliceRazred(int idTel, string imeTel, DateTime datumRoj, string pasmaTel, string imeMameTel, string imeOcetaTel, string usesnaStTel)
        {
            Id = idTel;
            Ime = imeTel;
            DatumRoj = datumRoj;
            Pasma = pasmaTel;
            ImeMame = imeMameTel;
            ImeOceta = imeOcetaTel;
            UsesnaSt = usesnaStTel;
        }
    }
}
