using evidenca_krav.NavigationBarUserControls;
using evidenca_krav.Obrazci;
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

namespace evidenca_krav.NavigationBar
{
    public partial class Krave : UserControl, IRefreshable
    {
        private DatabaseHelper db;
        private List<KraveRazred> vseKrave = new List<KraveRazred>();

        public Krave(DatabaseHelper dbHelper)
        {
            InitializeComponent();
            db = dbHelper;

            naloziKrave();
            comboBox1.SelectedIndex = 0;
        }

        private void naloziKrave()
        {
            vseKrave = db.PridobiKrave();
            prikaziKrave(vseKrave);
        }

        private void prikaziKrave(List<KraveRazred> krave)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (KraveRazred k in krave)
            {
                flowLayoutPanel1.Controls.Add(new KravaCard(db, k));
            }
        }

        public void Posodobi()
        {
            naloziKrave();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filtriraj();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (comboBox1.SelectedItem.ToString() == "Datum rojstva")
            {
                textBox1.Visible = false;
                dateTimePicker1.Visible = true;
            }
            else
            {
                textBox1.Visible = true;
                dateTimePicker1.Visible = false;
            }

            filtriraj();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filtriraj();
        }

        private void filtriraj()
        {
            string kriterij = comboBox1.SelectedItem.ToString();

            if (kriterij == "Brez filtra")
            {
                textBox1.Visible = true;
                dateTimePicker1.Visible = false;
                prikaziKrave(vseKrave);
                return;
            }

            List<KraveRazred> filtrirane = new List<KraveRazred>();

            foreach (KraveRazred k in vseKrave)
            {
                if (kriterij == "Ime")
                {
                    if (k.Ime != null && k.Ime.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        filtrirane.Add(k);
                    }
                }
                else if (kriterij == "Ušesna številka")
                {
                    if (k.UsesnaSt != null && k.UsesnaSt.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        filtrirane.Add(k);
                    }
                }
                else if (kriterij == "Pasma")
                {
                    if (k.Pasma != null && k.Pasma.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        filtrirane.Add(k);
                    }
                }
                else if (kriterij == "Ime mame")
                {
                    if (k.ImeMame != null && k.ImeMame.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        filtrirane.Add(k);
                    }
                }
                else if (kriterij == "Ime očeta")
                {
                    if (k.ImeOceta != null && k.ImeOceta.ToLower().Contains(textBox1.Text.Trim().ToLower()))
                    {
                        filtrirane.Add(k);
                    }
                }
                else if (kriterij == "Datum rojstva")
                {
                    if (k.DatumRoj.Date == dateTimePicker1.Value.Date)
                    {
                        filtrirane.Add(k);
                    }
                }
            }

            if (kriterij != "Datum rojstva" && string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                prikaziKrave(vseKrave);
            }
            else
            {
                prikaziKrave(filtrirane);
            }
        }
    }
}
