using evidenca_krav.NavigationBar;
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
    public partial class MlecKontrolaCard : UserControl
    {
        DatabaseHelper db;
        MlecneKontroleRazred mlecnaKontrola;
        int kravaId;
        public MlecKontrolaCard(DatabaseHelper dbHelper, MlecneKontroleRazred mk, int kId)
        {
            InitializeComponent();

            db = dbHelper;
            mlecnaKontrola = mk;
            kravaId = kId;

            labelDatum.Text = mlecnaKontrola.Datum.ToString("dd.MM.yyyy");
            labelZapSt.Text = mlecnaKontrola.Zaporedna_Stevilka.ToString();
            labelKontrolor.Text = mlecnaKontrola.Ime_Priimek_Osebe;
            labelDelDneva.Text = mlecnaKontrola.Del_dneva;
            labelMlecnost.Text = mlecnaKontrola.Mlecnost;
        }

        public void PosodobiPodatke()
        {
            mlecnaKontrola = db.PridobiMlecnoKontrolo(mlecnaKontrola.Id);
            labelDatum.Text = mlecnaKontrola.Datum.ToString("dd.MM.yyyy");
            labelZapSt.Text = mlecnaKontrola.Zaporedna_Stevilka.ToString();
            labelKontrolor.Text = mlecnaKontrola.Ime_Priimek_Osebe;
            labelDelDneva.Text = mlecnaKontrola.Del_dneva;
            labelMlecnost.Text = mlecnaKontrola.Mlecnost;
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediMlecnoKontrolo urediMlecnoKontrolo = new UrediMlecnoKontrolo(db, mlecnaKontrola, this, kravaId);

                if (urediMlecnoKontrolo.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Mlečna kontrola uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju mlečne kontrole: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
