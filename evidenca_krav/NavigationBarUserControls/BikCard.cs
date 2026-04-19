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

            labelRejec.Text = BikOs.rejec;
            labelDatumRoj.Text = BikOs.datumRoj.ToString("dd.MM.yyyy");
            labelIzboljsuje.Text = BikOs.izboljsuje;
            labelPasma.Text = BikOs.pasma;   

        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {

        }

        public void PosodobiPodatke()
        {
            BikOs = db.PridobiBikaOs(BikOs.idBik);
            labelRejec.Text = BikOs.rejec;
            labelDatumRoj.Text = BikOs.datumRoj.ToString("dd.MM.yyyy");
            labelIzboljsuje.Text = BikOs.izboljsuje;
            labelPasma.Text = db.PridobiPasmoPrekoId(BikOs.pasmaBikId);
        }
    }
}
