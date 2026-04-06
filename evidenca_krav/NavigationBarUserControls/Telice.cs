using evidenca_krav.Obrazci;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav.NavigationBar
{
    public partial class Telice : UserControl
    {
        private DatabaseHelper db;
        public Telice(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;
        }

        private void buttonDodajTelico_Click(object sender, EventArgs e)
        {
            try
            {
                DodajTelicoForm dodajTelicoForm = new DodajTelicoForm(db);

                if (dodajTelicoForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Telica uspešno dodana!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju telice: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
