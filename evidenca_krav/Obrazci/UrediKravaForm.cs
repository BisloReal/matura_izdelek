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
    public partial class UrediKravaForm : Form
    {
        KraveRazred Krava;
        KravaCard kravaCard;
        private DatabaseHelper db;
        public UrediKravaForm(DatabaseHelper dbHelper, int idKrav, KravaCard kv)
        {
            InitializeComponent();
            db = dbHelper;
            kravaCard = kv;

            Krava = db.PridobiKravo(idKrav);
            textBoxIme.Text = Krava.ime;
            textBoxUsSt.Text = Krava.usesnaSt;
            textBoxPasma.Text = Krava.pasma;
            textBoxImeMame.Text = Krava.imeMame;
            textBoxImeOceta.Text = Krava.imeOceta;

        }




        private void buttonZapri_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
