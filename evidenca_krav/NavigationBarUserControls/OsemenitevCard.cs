using evidenca_krav.Obrazci;
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
        }


        public void PosodobiPodatke()
        {
            osemenitev = db.PridobiOsemenitev(osemenitev.Id);
            labelZapSt.Text = osemenitev.Zaporedna_Stevilka.ToString();
            labelDatum.Text = osemenitev.Datum_Osemenitve.ToString("dd.MM.yyyy");
            labelBik.Text = osemenitev.Bik;
            labelVeterinar.Text = osemenitev.Veterinar;
        }

        private void buttonUrediOs_Click(object sender, EventArgs e)
        {
            try
            {
                /*UrediOsemenitev urediOsemenitev = new UrediOsemenitev(db, osemenitev, this);

                if (urediMlecnoKontrolo.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Mlečna kontrola uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju mlečne kontrole: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
