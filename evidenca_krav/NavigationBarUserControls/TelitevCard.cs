using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class TelitevCard : UserControl
    {
        DatabaseHelper db;
        TelitevRazred telitev;

        public TelitevCard(DatabaseHelper dbHelper, TelitevRazred tr)
        {
            InitializeComponent();
            db = dbHelper;
            telitev = tr;

            NastaviPodatke();
        }

        private void NastaviPodatke()
        {
            labelZapSt.Text = telitev.ZaporednoStevilo.ToString();
            labelDatum.Text = telitev.Datum.ToString("dd.MM.yyyy");

            labelTele.Text = telitev.Tele;
            labelKrava.Text = telitev.KravaMama;
            labelBik.Text = telitev.Bik;

            if (string.IsNullOrWhiteSpace(telitev.Potek))
            {
                labelPotek.Text = "/";
            }
            else
            {
                labelPotek.Text = telitev.Potek;
            }

            if (string.IsNullOrWhiteSpace(telitev.Rojstvo))
            {
                labelRojstvo.Text = "/";
            }
            else
            {
                labelRojstvo.Text = telitev.Rojstvo;
            }

            if (string.IsNullOrWhiteSpace(telitev.KakovostMleziva))
            {
                labelKakovostMleziva.Text = "/";
            }
            else
            {
                labelKakovostMleziva.Text = telitev.KakovostMleziva;
            }
        }

        public void PosodobiPodatke()
        {
            telitev = db.PridobiTelitev(telitev.Id);
            NastaviPodatke();
        }

        private void buttonUrediTelitev_Click(object sender, EventArgs e)
        {

        }
    }
}