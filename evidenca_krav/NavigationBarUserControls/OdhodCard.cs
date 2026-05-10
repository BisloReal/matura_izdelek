using evidenca_krav.NavigationBar;
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
    public partial class OdhodCard : UserControl
    {
        DatabaseHelper db;
        OdhodiRazred odhod;
        Odhodi odhodUC;
        public OdhodCard(DatabaseHelper dbHelper, OdhodiRazred oc, Odhodi odh)
        {
            InitializeComponent();

            db = dbHelper;
            odhod = oc;
            odhodUC = odh;

            labelDatum.Text = odhod.Datum.ToString("dd.MM.yyyy");
            labelGMid.Text = odhod.G_mid.ToString();
            labelLokacija.Text = odhod.Lokacija;
            labelVzrok.Text = odhod.Vzrok;
            labelKrava.Text = db.PridobiUsStKrave(oc.KravaId);
        }

        public void PosodobiPodatke()
        {
            odhod = db.PridobiOdhod(odhod.Id);
            labelDatum.Text = odhod.Datum.ToString("dd.MM.yyyy");
            labelGMid.Text = odhod.G_mid.ToString();
            labelLokacija.Text = odhod.Lokacija;
            labelVzrok.Text = odhod.Vzrok;
            labelKrava.Text = db.PridobiUsStKrave(odhod.KravaId);
        }

        private void buttonUrediTel_Click(object sender, EventArgs e)
        {
            try
            {
                UrediOdhodForm urediOdhodForm = new UrediOdhodForm(db, odhod, this);

                if (urediOdhodForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Odhod uspešno posodobljen!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju Odhoda: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonIzbrisi_Click(object sender, EventArgs e)
        {
            DialogResult rezultat = MessageBox.Show(
                "Ali ste prepričani, da želite izbrisati ta zapis?",
                "Potrditev brisanja",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (rezultat == DialogResult.Yes)
            {
                db.IzbrisiOdhod(odhod.Id);
                odhodUC.Posodobi();

                MessageBox.Show("Zapis je bil uspešno izbrisan.");
            }
        }

        private void buttonPogledZivali_Click(object sender, EventArgs e)
        {
            try
            {
                string tip = db.PridobiTipZivali(odhod.KravaId);

                if (tip == "Krava")
                {
                    UrediKravaForm urediKravaForm = new UrediKravaForm(db, odhod.KravaId, null);
                    if (urediKravaForm.ShowDialog() == DialogResult.OK)
                    {
                        db.PosodobiStanja();
                        PosodobiPodatke();

                        MessageBox.Show("Krava uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (tip == "Telica")
                {
                    TeliceRazred telica = db.PridobiTelico(odhod.KravaId);

                    UrediTelicaForm urediTelicaForm = new UrediTelicaForm(db, telica.Id, null);
                    if (urediTelicaForm.ShowDialog() == DialogResult.OK)
                    {
                        db.PosodobiStanja();
                        PosodobiPodatke();

                        MessageBox.Show("Telica uspešno posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (tip == "Tele")
                {
                    TeliceRazred tele = db.PridobiTelico(odhod.KravaId);

                    UrediTeleForm urediTeleForm = new UrediTeleForm(db, tele.Id, null);
                    if (urediTeleForm.ShowDialog() == DialogResult.OK)
                    {
                        db.PosodobiStanja();
                        PosodobiPodatke();

                        MessageBox.Show("Tele uspešno posodobljeno!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tip živali ni bil najden.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri odpiranju živali: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
