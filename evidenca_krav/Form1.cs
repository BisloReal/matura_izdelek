using evidenca_krav.NavigationBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav
{
    public partial class Form1 : Form
    {
        NavigationNadzor navNadzor;
        private DatabaseHelper db;
        public Form1()
        {
            InitializeComponent();

            db = new DatabaseHelper();
            db.Odpri();

            Initializacija();
        }

        private void Initializacija()
        {
            List<UserControl> userControlList = new List<UserControl>()
            {
                new Glavno_okno(db),
                new Krave(db),
                new Biki(db),
                new Telice(db)
            };

            navNadzor = new NavigationNadzor(userControlList, panelPrikaz);
            navNadzor.Prikaz(0);
        }
        private void buttonGlavnoOkno_Click(object sender, EventArgs e)
        {
            navNadzor.Prikaz(0);
        }

        private void buttonKrave_Click(object sender, EventArgs e)
        {
            navNadzor.Prikaz(1);
        }

        private void buttonBiki_Click(object sender, EventArgs e)
        {
            navNadzor.Prikaz(2);
        }

        private void buttonTelice_Click(object sender, EventArgs e)
        {
            navNadzor.Prikaz(3);
        }
    }
}
