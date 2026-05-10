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
    public partial class UrediOsemenitev : Form
    {
        DatabaseHelper db;
        OsemenitveRazred osemenitev;
        OsemenitevCard osemenitevCard;
        public UrediOsemenitev(DatabaseHelper dbHelper, OsemenitveRazred osr, OsemenitevCard osc)
        {
            InitializeComponent();
            db = dbHelper;
            osemenitev = osr;
            osemenitevCard = osc;

            comboBoxBiki.DataSource = db.PridobiBikeOs();
            comboBoxBiki.DisplayMember = "Ime_Bika";
            comboBoxBiki.ValueMember = "IdBik";
            comboBoxBiki.SelectedValue = osemenitev.BikId;

            comboBoxVeterinarji.DataSource = db.PridobiVeterinarje();
            comboBoxVeterinarji.DisplayMember = "ImePriimek";
            comboBoxVeterinarji.ValueMember = "Id";
            comboBoxVeterinarji.SelectedValue = osemenitev.VeterinarId;

            KraveRazred krava = db.PridobiKravo(osemenitev.KravaId);
            labelImeKrave.Text = krava.Ime + " (" + krava.UsesnaSt + ")";

            richTextBox1.Text = osemenitev.Opombe;
            dateTimePicker.Value = osemenitev.Datum_Osemenitve;

            if (osemenitev.Datum_Pregleda.HasValue)
            {
                dateTimePickerDatumPregleda.Checked = true;
                dateTimePickerDatumPregleda.Value = osemenitev.Datum_Pregleda.Value;
            }
            else
            {
                dateTimePickerDatumPregleda.Checked = false;
            }

            textBoxNacinPregleda.Text = osemenitev.Nacin_Pregleda;
            textBoxIzzidPregleda.Text = osemenitev.Izzid_Pregleda;
            richTextBoxOpombePregleda.Text = osemenitev.Opombe_Pregleda;

            comboBoxVeterinarPregleda.DataSource = db.PridobiVeterinarje();
            comboBoxVeterinarPregleda.DisplayMember = "ImePriimek";
            comboBoxVeterinarPregleda.ValueMember = "Id";

            if (osemenitev.VeterinarPregledaId != 0)
            {
                comboBoxVeterinarPregleda.SelectedValue = osemenitev.VeterinarPregledaId;
            }
            else
            {
                comboBoxVeterinarPregleda.SelectedIndex = -1;
            }

            if (osemenitev.Datum_Presusitve.HasValue)
            {
                dateTimePickerDatumPresusitve.Checked = true;
                dateTimePickerDatumPresusitve.Value = osemenitev.Datum_Presusitve.Value;
            }
            else
            {
                dateTimePickerDatumPresusitve.Checked = false;
            }

            richTextBoxOpombePresusitve.Text = osemenitev.Opombe_Presusitve;
            textBoxKondicija.Text = osemenitev.Kondicija_ob_Presusitvi;
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
                if (comboBoxBiki.SelectedItem == null)
                {
                    MessageBox.Show("Izberite bika.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxVeterinarji.SelectedItem == null)
                {
                    MessageBox.Show("Izberite veterinarja.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                osemenitev.BikId = Convert.ToInt32(comboBoxBiki.SelectedValue);
                osemenitev.VeterinarId = Convert.ToInt32(comboBoxVeterinarji.SelectedValue);
                osemenitev.Opombe = richTextBox1.Text.Trim();
                osemenitev.Datum_Osemenitve = dateTimePicker.Value;

                if (string.IsNullOrWhiteSpace(osemenitev.Opombe))
                {
                    osemenitev.Opombe = "Ni opomb.";
                }

                if (dateTimePickerDatumPregleda.Checked)
                {
                    osemenitev.Datum_Pregleda = dateTimePickerDatumPregleda.Value;
                    osemenitev.Nacin_Pregleda = textBoxNacinPregleda.Text.Trim();
                    osemenitev.Izzid_Pregleda = textBoxIzzidPregleda.Text.Trim();
                    osemenitev.Opombe_Pregleda = richTextBoxOpombePregleda.Text.Trim();

                    if (comboBoxVeterinarPregleda.SelectedItem != null)
                    {
                        osemenitev.VeterinarPregledaId = Convert.ToInt32(comboBoxVeterinarPregleda.SelectedValue);
                    }
                    else
                    {
                        osemenitev.VeterinarPregledaId = 0;
                    }

                    if (string.IsNullOrWhiteSpace(osemenitev.Opombe_Pregleda))
                    {
                        osemenitev.Opombe_Pregleda = "Ni opomb.";
                    }
                }
                else
                {
                    osemenitev.Datum_Pregleda = null;
                    osemenitev.Nacin_Pregleda = "";
                    osemenitev.Izzid_Pregleda = "";
                    osemenitev.Opombe_Pregleda = "";
                    osemenitev.VeterinarPregledaId = 0;
                }

                if (dateTimePickerDatumPresusitve.Checked)
                {
                    osemenitev.Datum_Presusitve = dateTimePickerDatumPresusitve.Value;
                    osemenitev.Opombe_Presusitve = richTextBoxOpombePresusitve.Text.Trim();
                    osemenitev.Kondicija_ob_Presusitvi = textBoxKondicija.Text.Trim();

                    if (string.IsNullOrWhiteSpace(osemenitev.Opombe_Presusitve))
                    {
                        osemenitev.Opombe_Presusitve = "Ni opomb.";
                    }
                }
                else
                {
                    osemenitev.Datum_Presusitve = null;
                    osemenitev.Opombe_Presusitve = "";
                    osemenitev.Kondicija_ob_Presusitvi = "";
                }

                int uspeh = db.UrediOsemenitev(osemenitev);

                if (uspeh == 0)
                {
                    osemenitevCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Osemenitev ni bila najdena.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju osemenitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
