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
        public Form1()
        {
            InitializeComponent();
            Initializacija();
        }

        private void Initializacija()
        {
            List<UserControl> userControlList = new List<UserControl>()
            { new Glavno_okno(), new Krave(), new Biki(), new Telice()};
            
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
