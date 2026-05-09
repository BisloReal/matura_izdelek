using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenca_krav.Razredi
{
    public class ZdravilaRazred
    {
        public int Id { get; set; }
        public string Zdravilo { get; set; }

        public ZdravilaRazred(int id, string zdravilo)
        {
            Id = id;
            Zdravilo = zdravilo;
        }
    }
}