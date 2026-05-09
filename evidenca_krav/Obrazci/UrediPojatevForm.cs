using evidenca_krav.NavigationBarUserControls;
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

namespace evidenca_krav.Obrazci
{
    public partial class UrediPojatevForm : Form
    {
        DatabaseHelper db;
        PojatveRazred pojatev;
        PojatevCard pojatevCard;

        public UrediPojatevForm(DatabaseHelper dbHelper, PojatveRazred pr, PojatevCard pc)
        {
            InitializeComponent();

            db = dbHelper;
            pojatev = pr;
            pojatevCard = pc;

            KraveRazred krava = db.PridobiKravo(pojatev.KravaId);
            labelImeKrave.Text = krava.Ime + " (" + krava.UsesnaSt + ")";

            numericUpDown1.Value = pojatev.ZaporednoStevilo;
            dateTimePicker.Value = pojatev.DatumPojatve;
            richTextBox1.Text = pojatev.Opombe;

            if (pojatev.KonecDatumPojatve.HasValue)
            {
                dateTimePickerKonec.Checked = true;
                dateTimePickerKonec.Value = pojatev.KonecDatumPojatve.Value;
            }
            else
            {
                dateTimePickerKonec.Checked = false;
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
                if (db.PogledObstajaStPojatev(pojatev.KravaId, Convert.ToInt32(numericUpDown1.Value)) &&
                    numericUpDown1.Value != pojatev.ZaporednoStevilo)
                {
                    MessageBox.Show("Pojatev s to zaporedno številko že obstaja.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                pojatev.ZaporednoStevilo = Convert.ToInt32(numericUpDown1.Value);
                pojatev.DatumPojatve = dateTimePicker.Value;
                pojatev.Opombe = richTextBox1.Text.Trim();

                if (string.IsNullOrWhiteSpace(pojatev.Opombe))
                {
                    pojatev.Opombe = "Ni opomb.";
                }

                if (dateTimePickerKonec.Checked)
                {
                    pojatev.KonecDatumPojatve = dateTimePickerKonec.Value;
                }
                else
                {
                    pojatev.KonecDatumPojatve = null;
                }

                int uspeh = db.UrediPojatev(pojatev);

                if (uspeh == 0)
                {
                    pojatevCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Pojatev ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju pojatve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}