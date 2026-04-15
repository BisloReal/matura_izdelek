using evidenca_krav.NavigationBar;
using evidenca_krav.Razredi;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace evidenca_krav
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=evidenca_krav.db";

        // TELICE
        public int DodajTelico(string ime, string datumRojstva, string pasma, string imeMame, string imeOceta)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                int TipZivaliId = -1;

                using (var cmd = new SQLiteCommand("SELECT id FROM tip_zivali WHERE tip = @tipZiv", conn))
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

                if (TipZivaliId == -1)
                    return -1;

                using (var command = new SQLiteCommand(
                    "INSERT INTO zivali (ime, datum_roj, pasma, ime_mame, ime_oceta, tip_zivali_id) " +
                    "VALUES (@Ime, @DatumRojstva, @Pasma, @ImeMame, @ImeOceta, @TipZivali)", conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@Pasma", pasma);
                    command.Parameters.AddWithValue("@ImeMame", imeMame);
                    command.Parameters.AddWithValue("@ImeOceta", imeOceta);
                    command.Parameters.AddWithValue("@TipZivali", TipZivaliId);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public List<TeliceRazred> PridobiTelice()
        {
            List<TeliceRazred> telice = new List<TeliceRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT z.id, z.ime, z.datum_roj, z.pasma, z.ime_mame, z.ime_oceta " +
                    "FROM zivali z " +
                    "INNER JOIN tip_zivali tz ON z.tip_zivali_id = tz.id " +
                    "LEFT JOIN odhodi_krav ok ON z.id = ok.krava_id " +
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL", conn))
                {
                    cmd.Parameters.AddWithValue("@tipZiv", "Telica");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            telice.Add(new TeliceRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            ));
                        }
                    }
                }
            }

            return telice;
        }

        public TeliceRazred PridobiTelico(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT id, ime, datum_roj, pasma, ime_mame, ime_oceta " +
                    "FROM zivali WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TeliceRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            );
                        }
                    }
                }
            }

            return null;
        }

        public int UrediTelico(int id, string ime, string datumRojstva, string pasma, string imeMame, string imeOceta)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE zivali SET ime = @Ime, datum_roj = @DatumRojstva, pasma = @Pasma, ime_mame = @ImeMame, ime_oceta = @ImeOceta WHERE id = @Id",
                    conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@Pasma", pasma);
                    command.Parameters.AddWithValue("@ImeMame", imeMame);
                    command.Parameters.AddWithValue("@ImeOceta", imeOceta);
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery() > 0 ? 0 : -1;
                }
            }
        }

        // BIKI OS
        public List<BikiOsRazred> PridobiBikeOs()
        {
            List<BikiOsRazred> biki = new List<BikiOsRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT b.id, b.rejec, b.datum_roj, b.izboljsuje, bp.pasma" +
                    "FROM biki_os b " +
                    "INNER JOIN biki_pasme tz bp b.biki_pasma_id = bp.id", conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            biki.Add(new BikiOsRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(3),
                                reader.GetString(4)
                            ));
                        }
                    }
                }
            }

            return biki;
        }


    }
}