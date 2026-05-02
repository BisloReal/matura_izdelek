using evidenca_krav.NavigationBar;
using evidenca_krav.NavigationBarUserControls;
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
    public partial class OsebaCard : UserControl
    {
        private DatabaseHelper db;
        private OsebeRazred oseba;

        public OsebaCard(DatabaseHelper dbHelper, OsebeRazred osebaR)
        {
            InitializeComponent();
            db = dbHelper;
            oseba = osebaR;

            labelIme.Text = oseba.Ime;
            labelPriimek.Text = oseba.Priimek;
            labelTelefon.Text = oseba.Telefon;
            labelEmail.Text = oseba.Email;
            labelZadolzitev.Text = oseba.Zadolzitev;
        }
        
        private void buttonUrediOsebo_Click(object sender, EventArgs e)
        {
            try
            {
                UrediOseboForm urediOseboForm = new UrediOseboForm(db, oseba, this);

                if (urediOseboForm.ShowDialog() == DialogResult.OK)
                {
                    db.PosodobiStanja();
                    PosodobiPodatke();

                    MessageBox.Show("Oseba uspešno Posodobljena!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju osebe: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 


        public void PosodobiPodatke()
        {
            oseba = db.PridobiOsebo(oseba.Id);
            labelIme.Text = oseba.Ime;
            labelPriimek.Text = oseba.Priimek;
            labelTelefon.Text = oseba.Telefon;
            labelEmail.Text = oseba.Email;
            labelZadolzitev.Text = oseba.Zadolzitev;
        }
    }
}
