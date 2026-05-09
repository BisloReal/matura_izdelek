using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class UrediZdravljenjeForm : Form
    {
        DatabaseHelper db;
        ZdravljenjaRazred zdravljenje;
        ZdravljenjeCard zdravljenjeCard;

        public UrediZdravljenjeForm(DatabaseHelper dbHelper, ZdravljenjaRazred zr, ZdravljenjeCard zc)
        {
            InitializeComponent();

            db = dbHelper;
            zdravljenje = zr;
            zdravljenjeCard = zc;

            KraveRazred k = db.PridobiKravo(zdravljenje.KravaId);
            labelKrava.Text = k.Ime + " (" + k.UsesnaSt + ")";

            comboBoxVeterinarji.DataSource = db.PridobiVeterinarje();
            comboBoxVeterinarji.DisplayMember = "ImePriimek";
            comboBoxVeterinarji.ValueMember = "Id";
            comboBoxVeterinarji.SelectedValue = zdravljenje.VeterinarId;

            checkedListBoxZdravila.DataSource = db.PridobiZdravila();
            checkedListBoxZdravila.DisplayMember = "Zdravilo";
            checkedListBoxZdravila.ValueMember = "Id";

            numericUpDown1.Value = zdravljenje.ZaporednaStevilka;
            dateTimePicker.Value = zdravljenje.Datum;
            textBoxVzrok.Text = zdravljenje.Vzrok;

            for (int i = 0; i < checkedListBoxZdravila.Items.Count; i++)
            {
                ZdravilaRazred zdravilo = (ZdravilaRazred)checkedListBoxZdravila.Items[i];

                foreach (ZdravilaRazred izbranoZdravilo in zdravljenje.Zdravila)
                {
                    if (zdravilo.Id == izbranoZdravilo.Id)
                    {
                        checkedListBoxZdravila.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private void buttonPreklici_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBoxVeterinarji.SelectedItem == null)
                {
                    MessageBox.Show("Izberite veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (checkedListBoxZdravila.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Izberite vsaj eno zdravilo.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kravaId = zdravljenje.KravaId;
                int zapSt = Convert.ToInt32(numericUpDown1.Value);

                if (db.PogledObstajaStZdravljenje(kravaId, zapSt) &&
                    (zapSt != zdravljenje.ZaporednaStevilka || kravaId != zdravljenje.KravaId))
                {
                    MessageBox.Show("Zdravljenje s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                zdravljenje.KravaId = kravaId;
                zdravljenje.VeterinarId = Convert.ToInt32(comboBoxVeterinarji.SelectedValue);
                zdravljenje.ZaporednaStevilka = zapSt;
                zdravljenje.Datum = dateTimePicker.Value;
                zdravljenje.Vzrok = textBoxVzrok.Text.Trim();

                if (string.IsNullOrWhiteSpace(zdravljenje.Vzrok))
                {
                    zdravljenje.Vzrok = "/";
                }

                int uspeh = db.UrediZdravljenje(zdravljenje);

                if (uspeh == 0)
                {
                    db.IzbrisiZdravilaZdravljenja(zdravljenje.Id);

                    foreach (ZdravilaRazred zdravilo in checkedListBoxZdravila.CheckedItems)
                    {
                        db.DodajZdraviloZdravljenju(zdravljenje.Id, zdravilo.Id);
                    }

                    zdravljenjeCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Zdravljenje ni bilo najdeno.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju zdravljenja: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}