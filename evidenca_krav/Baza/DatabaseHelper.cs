using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace evidenca_krav
{
    public class DatabaseHelper
    {
        private SQLiteConnection _connection;

        public DatabaseHelper()
        {
            _connection = new SQLiteConnection("Data Source=evidenca_krav.db");
        }

        public void Odpri()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Povezava do baze ni bila uspešna: " + ex.Message);
            }
        }

        public void Zapri()
        {
            try
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zaprejte povezave do baze ni bilo uspešno: " + ex.Message);
            }
        }
    }
}
