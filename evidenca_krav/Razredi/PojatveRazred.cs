using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class PojatveRazred
    {
        public int Id { get; set; }
        public int ZaporednoStevilo { get; set; }
        public int KravaId { get; set; }
        public string KravaIme { get; set; }
        public DateTime DatumPojatve { get; set; }
        public DateTime? KonecDatumPojatve { get; set; }
        public string Opombe { get; set; }

        public PojatveRazred(int id, int zaporednoStevilo, int kravaId, string kravaIme, DateTime datumPojatve, string opombe)
        {
            Id = id;
            ZaporednoStevilo = zaporednoStevilo;
            KravaId = kravaId;
            KravaIme = kravaIme;
            DatumPojatve = datumPojatve;
            Opombe = opombe;
        }

        public PojatveRazred(int id, int zaporednoStevilo, int kravaId, string kravaIme, DateTime datumPojatve, DateTime konecPojatve, string opombe)
        {
            Id = id;
            ZaporednoStevilo = zaporednoStevilo;
            KravaId = kravaId;
            KravaIme = kravaIme;
            DatumPojatve = datumPojatve;
            KonecDatumPojatve = konecPojatve;
            Opombe = opombe;
        }
    }
}
