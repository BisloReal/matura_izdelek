using evidenca_krav.Razredi;
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
    public partial class PotomecCard : UserControl
    {
        TeliceRazred zival;
        DatabaseHelper db;
        public PotomecCard(DatabaseHelper dbHelper, TeliceRazred tel)
        {
            InitializeComponent();
            zival = tel;
            db = dbHelper;

            labelIme.Text = zival.Ime;
            labelDatumRoj.Text = zival.DatumRoj.ToString("dd.MM.yyyy");
            labelPasma.Text = zival.Pasma;
            labelImeMame.Text = zival.ImeMame;
            labelImeOceta.Text = zival.ImeOceta;
            labelUsSt.Text = zival.UsesnaSt;
            labelVrsta.Text = db.PridobiVrstoZivali(tel.Id);
        }
    }
}
