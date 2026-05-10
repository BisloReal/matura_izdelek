using evidenca_krav.NavigationBar;
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
    public partial class ObvestiloCard : UserControl
    {
        public ObvestiloCard(ObvestiloRazred or)
        {
            InitializeComponent();

            labelObvestilo.Text = or.Naslov;
            labelDatum.Text = or.Datum.ToString("dd.MM.yyyy");
            labelTip.Text = or.Tip;
            richTextBoxOpis.Text = or.Opis;
        }
    }
}
