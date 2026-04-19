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

namespace evidenca_krav.NavigationBar
{
    public partial class Biki : UserControl
    {
        private DatabaseHelper db;

        public Biki(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            NaloziBike();
        }

        private void buttonDodajBika_Click(object sender, EventArgs e)
        {
            try
            {
                DodajBikaForm dodajBikaForm = new DodajBikaForm(db);
                if (dodajBikaForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Bik uspešno dodan.", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                NaloziBike();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri dodajanju bika: " + ex.Message, "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void NaloziBike()
        {
            flowLayoutPanelBiki.Controls.Clear();

            List<BikiOsRazred> biki = db.PridobiBikeOs();

            foreach (BikiOsRazred b in biki)
            {
                flowLayoutPanelBiki.Controls.Add(new BikCard(db, b));
            }
        }
    }
}
