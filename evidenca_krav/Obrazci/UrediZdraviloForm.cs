using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Razredi;
using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class UrediZdraviloForm : Form
    {
        DatabaseHelper db;
        ZdravilaRazred zdravilo;
        ZdravilaCard zdravilaCard;

        public UrediZdraviloForm(DatabaseHelper dbHelper, ZdravilaRazred zr, ZdravilaCard zc)
        {
            InitializeComponent();

            db = dbHelper;
            zdravilo = zr;
            zdravilaCard = zc;

            textBoxZdravilo.Text = zdravilo.Zdravilo;
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
                if (string.IsNullOrWhiteSpace(textBoxZdravilo.Text))
                {
                    MessageBox.Show("Vpišite ime zdravila.", "Opozorilo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                zdravilo.Zdravilo = textBoxZdravilo.Text.Trim();

                int uspeh = db.UrediZdravilo(zdravilo);

                if (uspeh == 0)
                {
                    zdravilaCard.PosodobiPodatke();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Zdravilo ni bilo najdeno.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju zdravila: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}