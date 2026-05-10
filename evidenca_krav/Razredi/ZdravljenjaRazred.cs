using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class ZdravljenjaRazred
    {
        public int Id { get; set; }
        public int ZaporednaStevilka { get; set; }
        public DateTime Datum { get; set; }

        public string Vzrok { get; set; }

        public int VeterinarId { get; set; }
        public string Veterinar { get; set; }

        public int KravaId { get; set; }
        public string Krava { get; set; }

        public List<ZdravilaRazred> Zdravila { get; set; }

        public string ZdravilaPrikaz
        {
            get
            {
                if (Zdravila == null || Zdravila.Count == 0)
                {
                    return "Ni vpisanih zdravil";
                }

                List<string> imenaZdravil = new List<string>();

                foreach (ZdravilaRazred zdravilo in Zdravila)
                {
                    imenaZdravil.Add(zdravilo.Zdravilo);
                }

                return string.Join(", ", imenaZdravil);
            }
        }

        public ZdravljenjaRazred(int id, int zaporednaStevilka, DateTime datum, string vzrok,
            int veterinarId, string veterinar, int kravaId, string krava)
        {
            Id = id;
            ZaporednaStevilka = zaporednaStevilka;
            Datum = datum;
            Vzrok = vzrok;

            VeterinarId = veterinarId;
            Veterinar = veterinar;

            KravaId = kravaId;
            Krava = krava;

            Zdravila = new List<ZdravilaRazred>();
        }

        public ZdravljenjaRazred(int id, int zaporednaStevilka, DateTime datum, string vzrok,
            int veterinarId, string veterinar, int kravaId, string krava, List<ZdravilaRazred> zdravila)
        {
            Id = id;
            ZaporednaStevilka = zaporednaStevilka;
            Datum = datum;
            Vzrok = vzrok;

            VeterinarId = veterinarId;
            Veterinar = veterinar;

            KravaId = kravaId;
            Krava = krava;

            Zdravila = zdravila;
        }
    }
}