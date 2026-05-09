using System;
using System.Windows.Forms;

namespace evidenca_krav.Obrazci
{
    public partial class DodajZdraviloForm : Form
    {
        DatabaseHelper db;

        public DodajZdraviloForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();

            db = dbHelper;
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

                string zdravilo = textBoxZdravilo.Text.Trim();

                int uspeh = db.DodajZdravilo(zdravilo);

                if (uspeh == 0)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else if (uspeh == -1)
                {
                    MessageBox.Show("Napaka pri dodajanju zdravila.", "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju zdravila: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}