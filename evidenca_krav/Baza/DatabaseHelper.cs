using evidenca_krav.NavigationBar;
using evidenca_krav.Razredi;
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

        // Telice
        public int DodajTelico(string ime, string datumRojstva, string pasma, string imeMame, string imeOceta)
        {
            int TipZivaliId = -1;

            using (var cmd = new SQLiteCommand("SELECT id FROM tip_zivali WHERE tip = @tipZiv", _connection))
            {
                cmd.Parameters.AddWithValue("@tipZiv", "Telica");

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        TipZivaliId = reader.GetInt32(0); 
                    }
                }
            }

            using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Zivali (ime, datum_roj, pasma, ime_mame, ime_oceta, tip_zivali_id) VALUES (@Ime, @DatumRojstva, @Pasma, @ImeMame, @ImeOceta, @TipZivali)", _connection))
            {
                command.Parameters.AddWithValue("@Ime", ime);
                command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                command.Parameters.AddWithValue("@Pasma", pasma);
                command.Parameters.AddWithValue("@ImeMame", imeMame);
                command.Parameters.AddWithValue("@ImeOceta", imeOceta);
                command.Parameters.AddWithValue("@TipZivali", TipZivaliId);
                command.ExecuteNonQuery();
            }

            return 0; // izvedeno
        }

        public List<TeliceRazred> PridobiTelice()
        {
            List<TeliceRazred> telice = new List<TeliceRazred>();
            using (var cmd = new SQLiteCommand("SELECT z.id, z.ime, z.datum_roj, z.pasma, z.ime_mame, z.ime_oceta " +
                                                "FROM zivali z INNER JOIN tip_zivali tz WHERE tz.tip = @tipZiv", _connection))
            {
                cmd.Parameters.AddWithValue("@tipZiv", "Telica");

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        TeliceRazred te = new TeliceRazred(reader.GetInt32(1), reader.GetString(2), reader.GetDateTime(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                        telice.Add(te);
                    }
                }
            }
            return telice;
        }
    }
}
