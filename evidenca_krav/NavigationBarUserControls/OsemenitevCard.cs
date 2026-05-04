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
    public partial class OsemenitevCard : UserControl
    {
        DatabaseHelper db;
        OsemenitveRazred osemenitev;
        public OsemenitevCard(DatabaseHelper dbHelper, OsemenitveRazred osr)
        {
            InitializeComponent();
            db = dbHelper;
            osemenitev = osr;

            labelZapSt.Text = osemenitev.Zaporedna_Stevilka.ToString();
            labelDatum.Text = osemenitev.Datum_Osemenitve.ToString("dd.MM.yyyy");
            labelBik.Text = osemenitev.Bik;
            labelVeterinar.Text = osemenitev.Veterinar;
            labelKrava.Text = osemenitev.Krava;
        }
    }
}
