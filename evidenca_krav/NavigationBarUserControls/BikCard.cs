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
            try
            {
                UrediBikOsForm urediBikOsForm = new UrediBikOsForm(db, BikOs, this);

                if (urediBikOsForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Bik uspešno Posodobljen!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju bika: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
