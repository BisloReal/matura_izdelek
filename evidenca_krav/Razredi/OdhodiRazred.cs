using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class OdhodiRazred
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public string G_mid { get; set; }
        public string Lokacija { get; set; }
        public string Vzrok { get; set; }
        public string Opombe { get; set; }
        public int KravaId { get; set; }
        public string KravaStevilka { get; set; }

        public OdhodiRazred(int id, DateTime datum, string g_mid, string lokacija, string vzrok, string opombe, int kravaId)
        {
            Id = id;
            Datum = datum;
            G_mid = g_mid;
            Lokacija = lokacija;
            Vzrok = vzrok;
            Opombe = opombe;
            KravaId = kravaId;
        }
    }
}
