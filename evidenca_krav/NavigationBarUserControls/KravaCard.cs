using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class KravaCard : UserControl
    {
        KraveRazred Krava;
        private DatabaseHelper db;
        public KravaCard(DatabaseHelper dbHelper, KraveRazred krava)
        {
            InitializeComponent();
            db = dbHelper;
            Krava = krava;

            labelIme.Text = Krava.ime;
        }
    }
}
