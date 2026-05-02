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
    public partial class ZadolzitveOsebForm : Form
    {
        private DatabaseHelper db;
        public ZadolzitveOsebForm(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            listBoxZadolzitve.DataSource = db.PridobiZadolzitve();
        }

        private void listBoxZadolzitve_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxZadolzitev.Text = listBoxZadolzitve.SelectedItem.ToString();
        }

        private void buttonPotrdi_Click(object sender, EventArgs e)
        {
            try
            {
                db.DodajZadolzitev(textBoxZadolzitev.Text);
                listBoxZadolzitve.DataSource = db.PridobiZadolzitve();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju zadolžitve: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonUrediIzbrano_Click(object sender, EventArgs e)
        {
            try
            {
                db.UrediZadolzitev(db.PridobiIdZadolzitvePrekoImena(listBoxZadolzitve.SelectedItem.ToString()), textBoxZadolzitev.Text);
                listBoxZadolzitve.DataSource = db.PridobiZadolzitve();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri urejanju pasme: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonZapri_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
