using evidenca_krav.NavigationBar;
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
    public partial class BikCard : UserControl
    {
        BikiOsRazred BikOs;
        private DatabaseHelper db;

        public BikCard(DatabaseHelper dbHelper, BikiOsRazred bik)
        {
            InitializeComponent();
            db = dbHelper;
            BikOs = bik;    

            labelRejec.Text = BikOs.Rejec;
            labelDatumRoj.Text = BikOs.DatumRoj.ToString("dd.MM.yyyy");
            labelIzboljsuje.Text = BikOs.Izboljsuje;
            labelPasma.Text = BikOs.Pasma;   

        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {

        }

        public void PosodobiPodatke()
        {
            BikOs = db.PridobiBikaOs(BikOs.IdBik);
            labelRejec.Text = BikOs.Rejec;
            labelDatumRoj.Text = BikOs.DatumRoj.ToString("dd.MM.yyyy");
            labelIzboljsuje.Text = BikOs.Izboljsuje;
            labelPasma.Text = db.PridobiPasmoPrekoId(BikOs.PasmaBikId);
        }
    }
}
