using evidenca_krav.Obrazci;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBarUserControls
{
    public partial class ZdravilaCard : UserControl
    {
        DatabaseHelper db;
        ZdravilaRazred zdravilo;

        public ZdravilaCard(DatabaseHelper dbHelper, ZdravilaRazred zr)
        {
            InitializeComponent();

            db = dbHelper;
            zdravilo = zr;

            labelZdravilo.Text = zdravilo.Zdravilo;
        }

        public void PosodobiPodatke()
        {
            zdravilo = db.PridobiZdravilo(zdravilo.Id);
            labelZdravilo.Text = zdravilo.Zdravilo;
        }

        private void buttonUrediZdravilo_Click(object sender, EventArgs e)
        {
            try
            {
                UrediZdraviloForm urediZdraviloForm = new UrediZdraviloForm(db, zdravilo, this);

                if (urediZdraviloForm.ShowDialog() == DialogResult.OK)
                {
                    PosodobiPodatke();

                    MessageBox.Show("Zdravilo uspešno posodobljeno!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju zdravila: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}