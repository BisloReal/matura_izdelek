using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class OsebeRazred
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Zadolzitev { get; set; }

        public string ImePriimek
        {
            get { return Ime + " " + Priimek; }
        }

        public OsebeRazred(int id, string ime, string priimek, string telefon, string email, string zadolzitev)
        {
            Id = id;
            Ime = ime;
            Priimek = priimek;
            Telefon = telefon;
            Email = email;
            Zadolzitev = zadolzitev;
        }
    }
}
