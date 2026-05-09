using evidenca_krav.NavigationBar;
using evidenca_krav.Razredi;
using evidenca_krav.RazredSi;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Policy;
using System.Windows.Forms;

namespace evidenca_krav
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=evidenca_krav.db";

        public void PosodobiStanja()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // KRAVE
                using (var cmd = new SQLiteCommand(
                    "UPDATE zivali " +
                    "SET tip_zivali_id = (SELECT id FROM tip_zivali WHERE tip = 'Krava' LIMIT 1) " +
                    "WHERE id IN (SELECT krava_mama_id FROM telitve)", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // TELICE
                using (var cmd = new SQLiteCommand(
                    "UPDATE zivali " +
                    "SET tip_zivali_id = (SELECT id FROM tip_zivali WHERE tip = 'Telica' LIMIT 1) " +
                    "WHERE datum_roj <= date('now', '-12 months') " +
                    "AND id NOT IN (SELECT krava_mama_id FROM telitve)", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // TELE
                using (var cmd = new SQLiteCommand(
                    "UPDATE zivali " +
                    "SET tip_zivali_id = (SELECT id FROM tip_zivali WHERE tip = 'Tele' LIMIT 1) " +
                    "WHERE id NOT IN (SELECT krava_mama_id FROM telitve) " +
                    "AND datum_roj > date('now', '-12 months')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> PridobiUsStVsehZivali()
        {
            List<string> usStZivali = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT usesna_stevilka FROM zivali WHERE usesna_stevilka IS NOT NULL", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usStZivali.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return usStZivali;
        }

        // TELICE
        public int DodajTelico(string ime, string datumRojstva, string pasma, string imeMame, string imeOceta, string UsesnaStevilka)
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
                {
                    return -1;
                }

                using (var command = new SQLiteCommand(
                    "INSERT INTO zivali (ime, datum_roj, usesna_stevilka, pasma, ime_mame, ime_oceta, tip_zivali_id) " +
                    "VALUES (@Ime, @DatumRojstva, @UsesnaStevilka, @Pasma, @ImeMame, @ImeOceta, @TipZivali)", conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@UsesnaStevilka", UsesnaStevilka);
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
                    "SELECT z.id, z.ime, z.datum_roj, z.pasma, z.ime_mame, z.ime_oceta, z.usesna_stevilka " +
                    "FROM zivali z " +
                    "INNER JOIN tip_zivali tz ON z.tip_zivali_id = tz.id " +
                    "LEFT JOIN odhodi_krav ok ON z.id = ok.krava_id " +
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL " +
                    "ORDER BY z.id DESC", conn))
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
                                reader.GetString(5),
                                reader.GetString(6)
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
                    "SELECT id, ime, datum_roj, pasma, ime_mame, ime_oceta, usesna_stevilka " +
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
                                reader.GetString(5),
                                reader.GetString(6)
                            );
                        }
                    }
                }
            }

            return null;
        }

        public int UrediTelico(int id, string ime, string datumRojstva, string pasma, string imeMame, string imeOceta, string usesna_stevilka)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE zivali SET ime = @Ime, datum_roj = @DatumRojstva, pasma = @Pasma, ime_mame = @ImeMame, ime_oceta = @ImeOceta, usesna_stevilka = @UsesnaStevilka WHERE id = @Id",
                    conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@Pasma", pasma);
                    command.Parameters.AddWithValue("@ImeMame", imeMame);
                    command.Parameters.AddWithValue("@ImeOceta", imeOceta);
                    command.Parameters.AddWithValue("@UsesnaStevilka", usesna_stevilka);
                    command.Parameters.AddWithValue("@Id", id);

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public bool PogledObstajaUsSt(string usSt)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM zivali  WHERE usesna_stevilka = @UsSt", conn))
                {
                    cmd.Parameters.AddWithValue("@UsSt", usSt);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        // TELE
        public List<TeliceRazred> PridobiTeleta()
        {
            List<TeliceRazred> tele = new List<TeliceRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT z.id, z.ime, z.datum_roj, z.pasma, z.ime_mame, z.ime_oceta, z.usesna_stevilka " +
                    "FROM zivali z " +
                    "INNER JOIN tip_zivali tz ON z.tip_zivali_id = tz.id " +
                    "LEFT JOIN odhodi_krav ok ON z.id = ok.krava_id " +
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL " +
                    "ORDER BY z.id DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@tipZiv", "Tele");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tele.Add(new TeliceRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6)
                            ));
                        }
                    }
                }
            }

            return tele;
        }


        // BIKI OS
        public List<BikiOsRazred> PridobiBikeOs()
        {
            List<BikiOsRazred> biki = new List<BikiOsRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT  b.id, b.ime, b.stevilka, b.rejec, b.datum_roj, bp.id, bp.pasma, b.izboljsuje " +
                    "FROM biki_os b " +
                    "INNER JOIN biki_pasme bp ON b.biki_pasma_id = bp.id " +
                    "ORDER BY b.id DESC", conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            biki.Add(new BikiOsRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetDateTime(4),
                                reader.GetInt32(5),
                                reader.GetString(6),
                                reader.GetString(7)
                            ));
                        }
                    }
                }
            }

            return biki;
        }

        public BikiOsRazred PridobiBikaOs(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT b.id, b.ime, b.stevilka, b.rejec, b.datum_roj, bp.id, bp.pasma, b.izboljsuje " +
                    "FROM biki_os b " +
                    "INNER JOIN biki_pasme bp ON b.biki_pasma_id = bp.id WHERE b.id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BikiOsRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetDateTime(4),
                                reader.GetInt32(5),
                                reader.GetString(6),
                                reader.GetString(7)
                            );
                        }
                    }
                }
            }
            return null;
        }

        public string PridobiPasmoPrekoId(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT pasma FROM biki_pasme WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }
            return null;
        }

        public int PridobiIdPasmePrekoImena(string imePasme)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT id FROM biki_pasme WHERE pasma = @ImePasme", conn))
                {
                    cmd.Parameters.AddWithValue("@ImePasme", imePasme);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return -1;
        }

        public void DodajPasmoBikov(string imePasme)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO biki_pasme (pasma) VALUES (@ImePasme)", conn))
                {
                    command.Parameters.AddWithValue("@ImePasme", imePasme);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UrediPasmoBikov(int id, string imePasme)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var command = new SQLiteCommand(
                    "UPDATE biki_pasme SET pasma = @ImePasme WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@ImePasme", imePasme);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> PridobiPasmeBikov()
        {
            List<string> pasme = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT pasma FROM biki_pasme ORDER BY pasma", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pasme.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return pasme;
        }

        public void DodajBikaOs(string ime, string stevilka, string rejec, string datumRojstva, int pasmaBikId, string izboljsuje)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO biki_os (ime, stevilka, rejec, datum_roj, biki_pasma_id, izboljsuje) " +
                    "VALUES (@Ime, @Stevilka, @Rejec, @DatumRojstva, @PasmaBikId, @Izboljsuje)", conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@Stevilka", stevilka);
                    command.Parameters.AddWithValue("@Rejec", rejec);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@PasmaBikId", pasmaBikId);
                    command.Parameters.AddWithValue("@Izboljsuje", izboljsuje);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int UrediBikaOs(string ime, string stevilka, string rejec, string datumRojstva, int pasmaBikId, string izboljsuje, int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE biki_os SET ime = @Ime, stevilka = @Stevilka, rejec = @Rejec, datum_roj = @DatumRojstva, biki_pasma_id = @PasmaBikId, izboljsuje = @Izboljsuje WHERE id = @Id",
                    conn))
                {
                    command.Parameters.AddWithValue("@Ime", ime);
                    command.Parameters.AddWithValue("@Stevilka", stevilka);
                    command.Parameters.AddWithValue("@Rejec", rejec);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@PasmaBikId", pasmaBikId);
                    command.Parameters.AddWithValue("@Izboljsuje", izboljsuje);
                    command.Parameters.AddWithValue("@Id", id);

                    Console.WriteLine(command.CommandText);
                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public bool PogledObstajaStBika(string st)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM biki_os WHERE stevilka = @St", conn))
                {
                    cmd.Parameters.AddWithValue("@St", st);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        // KRAVE
        public List<KraveRazred> PridobiKrave()
        {
            List<KraveRazred> krave = new List<KraveRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT z.* " +
                    "FROM zivali z " +
                    "INNER JOIN tip_zivali tz ON z.tip_zivali_id = tz.id " +
                    "LEFT JOIN odhodi_krav ok ON z.id = ok.krava_id " +
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL " +
                    "ORDER BY z.id DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@tipZiv", "Krava");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string ime = reader.GetString(reader.GetOrdinal("ime"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum_roj"));
                            string pasma = reader.GetString(reader.GetOrdinal("pasma"));

                            string imeMame = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_mame")))
                            {
                                imeMame = reader.GetString(reader.GetOrdinal("ime_mame"));
                            }

                            string imeOceta = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_oceta")))
                            {
                                imeOceta = reader.GetString(reader.GetOrdinal("ime_oceta"));
                            }

                            string usesna = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("usesna_stevilka")))
                            {
                                usesna = reader.GetString(reader.GetOrdinal("usesna_stevilka"));
                            }

                            string laktacija = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("laktacija")))
                            {
                                laktacija = reader.GetString(reader.GetOrdinal("laktacija"));
                            }

                            KraveRazred k = new KraveRazred(id, ime, datum, pasma, imeMame, imeOceta, usesna, laktacija);

                            if (!reader.IsDBNull(reader.GetOrdinal("datum_pregleda")))
                            {
                                k.DatumPregleda = reader.GetDateTime(reader.GetOrdinal("datum_pregleda"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_datum_prvi")))
                            {
                                k.DatumPrvegaIztoka = reader.GetDateTime(reader.GetOrdinal("iztok_mleka_datum_prvi"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena_druga")))
                            {
                                k.IztokMlekaOcenaDruga = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena_druga"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_datum_drugi")))
                            {
                                k.DatumDrugegaIztoka = reader.GetDateTime(reader.GetOrdinal("iztok_mleka_datum_drugi"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("odsvetovani_biki")))
                            {
                                k.OdsvetovaniBiki = reader.GetString(reader.GetOrdinal("odsvetovani_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("primerni_biki")))
                            {
                                k.PrimerniBiki = reader.GetString(reader.GetOrdinal("primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("najbolj_primerni_biki")))
                            {
                                k.NajboljPrimerniBiki = reader.GetString(reader.GetOrdinal("najbolj_primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("teza")))
                            {
                                k.Teza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("teza")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena")))
                            {
                                k.IztokMlekaOcena = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("obseg_prsi_cm")))
                            {
                                k.ObsegPrsi = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("obseg_prsi_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_cm")))
                            {
                                k.VisinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("visina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_cm")))
                            {
                                k.GlobinaTelesa = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("globina_telesa_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_spredaj_ocena")))
                            {
                                k.SirinaVspredaj = reader.GetInt32(reader.GetOrdinal("sirina_spredaj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("hrbet_ocena")))
                            {
                                k.HrbetOcena = reader.GetInt32(reader.GetOrdinal("hrbet_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_cm")))
                            {
                                k.DolzinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("dolzina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_cm")))
                            {
                                k.SednaSirina = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("sedna_sirina_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("nagib_kriza_ocena")))
                            {
                                k.NagibKrizaOcena = reader.GetInt32(reader.GetOrdinal("nagib_kriza_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_kolka_ocena")))
                            {
                                k.PolozajKolkaOcena = reader.GetInt32(reader.GetOrdinal("polozaj_kolka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("skocni_sklep_ocena")))
                            {
                                k.SkocniSklepOcena = reader.GetInt32(reader.GetOrdinal("skocni_sklep_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("izraz_skoc_sklepa_ocena")))
                            {
                                k.IzrazSkocSklepaOcena = reader.GetInt32(reader.GetOrdinal("izraz_skoc_sklepa_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("bicelj_ocena")))
                            {
                                k.BiceljOcena = reader.GetInt32(reader.GetOrdinal("bicelj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("parklji_ocena")))
                            {
                                k.ParkljiOcena = reader.GetInt32(reader.GetOrdinal("parklji_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_vimena_ocena")))
                            {
                                k.DolzinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("dolzina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("pripetost_vimena_ocena")))
                            {
                                k.PripetostVimenaOcena = reader.GetInt32(reader.GetOrdinal("pripetost_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_mlecnega_zrcala_ocena")))
                            {
                                k.VisinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("visina_mlecnega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_mlenega_zrcala_ocena")))
                            {
                                k.SirinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("sirina_mlenega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_vimena_ocena")))
                            {
                                k.GlobinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("globina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dno_vimena_ocena")))
                            {
                                k.DnoVimenaOcena = reader.GetInt32(reader.GetOrdinal("dno_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_cent_vezi_ocena")))
                            {
                                k.GlobinaCentVeziOcena = reader.GetInt32(reader.GetOrdinal("globina_cent_vezi_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_seskov_ocena")))
                            {
                                k.DolzinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("dolzina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("debelina_seskov_ocena")))
                            {
                                k.DebelinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("debelina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenost_prednjih_seskov_ocena")))
                            {
                                k.NamenostPrednjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenost_prednjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena")))
                            {
                                k.NamenostZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena")))
                            {
                                k.PolozajZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("omisicanost_ocena")))
                            {
                                k.OmisicanostOcena = reader.GetInt32(reader.GetOrdinal("omisicanost_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kondicija_ocena")))
                            {
                                k.kondicijaOcena = reader.GetInt32(reader.GetOrdinal("kondicija_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_izracun_ocena")))
                            {
                                k.VisinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("visina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_izracun_ocena")))
                            {
                                k.GlobinaTelesaIzracunOcena = reader.GetInt32(reader.GetOrdinal("globina_telesa_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_izracun_ocena")))
                            {
                                k.DolzinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("dolzina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_izracun_ocena")))
                            {
                                k.SednaSirinaIzracunOcena = reader.GetInt32(reader.GetOrdinal("sedna_sirina_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("okvir_ocena")))
                            {
                                k.OkvirOcena = reader.GetInt32(reader.GetOrdinal("okvir_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kriz_ocena")))
                            {
                                k.KrizOcena = reader.GetInt32(reader.GetOrdinal("kriz_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("noge_ocena")))
                            {
                                k.NogeOcena = reader.GetInt32(reader.GetOrdinal("noge_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("vime_ocena")))
                            {
                                k.VimeOcena = reader.GetInt32(reader.GetOrdinal("vime_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena")))
                            {
                                k.TelesneSposobnostiSkupajOcena = reader.GetInt32(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena"));
                            }

                            krave.Add(k);
                        }
                    }
                }
            }

            return krave;
        }

        public KraveRazred PridobiKravo(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT z.* " +
                    "FROM zivali z " +
                    "WHERE z.id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string ime = reader.GetString(reader.GetOrdinal("ime"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum_roj"));
                            string pasma = reader.GetString(reader.GetOrdinal("pasma"));

                            string imeMame = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_mame")))
                            {
                                imeMame = reader.GetString(reader.GetOrdinal("ime_mame"));
                            }

                            string imeOceta = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_oceta")))
                            {
                                imeOceta = reader.GetString(reader.GetOrdinal("ime_oceta"));
                            }

                            string usesna = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("usesna_stevilka")))
                            {
                                usesna = reader.GetString(reader.GetOrdinal("usesna_stevilka"));
                            }

                            string laktacija = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("laktacija")))
                            {
                                laktacija = reader.GetString(reader.GetOrdinal("laktacija"));
                            }

                            KraveRazred k = new KraveRazred(id, ime, datum, pasma, imeMame, imeOceta, usesna, laktacija);

                            if (!reader.IsDBNull(reader.GetOrdinal("datum_pregleda")))
                            {
                                k.DatumPregleda = reader.GetDateTime(reader.GetOrdinal("datum_pregleda"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_datum_prvi")))
                            {
                                k.DatumPrvegaIztoka = reader.GetDateTime(reader.GetOrdinal("iztok_mleka_datum_prvi"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena_druga")))
                            {
                                k.IztokMlekaOcenaDruga = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena_druga"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_datum_drugi")))
                            {
                                k.DatumDrugegaIztoka = reader.GetDateTime(reader.GetOrdinal("iztok_mleka_datum_drugi"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("odsvetovani_biki")))
                            {
                                k.OdsvetovaniBiki = reader.GetString(reader.GetOrdinal("odsvetovani_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("primerni_biki")))
                            {
                                k.PrimerniBiki = reader.GetString(reader.GetOrdinal("primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("najbolj_primerni_biki")))
                            {
                                k.NajboljPrimerniBiki = reader.GetString(reader.GetOrdinal("najbolj_primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("teza")))
                            {
                                k.Teza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("teza")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena")))
                            {
                                k.IztokMlekaOcena = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("obseg_prsi_cm")))
                            {
                                k.ObsegPrsi = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("obseg_prsi_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_cm")))
                            {
                                k.VisinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("visina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_cm")))
                            {
                                k.GlobinaTelesa = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("globina_telesa_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_spredaj_ocena")))
                            {
                                k.SirinaVspredaj = reader.GetInt32(reader.GetOrdinal("sirina_spredaj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("hrbet_ocena")))
                            {
                                k.HrbetOcena = reader.GetInt32(reader.GetOrdinal("hrbet_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_cm")))
                            {
                                k.DolzinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("dolzina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_cm")))
                            {
                                k.SednaSirina = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("sedna_sirina_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("nagib_kriza_ocena")))
                            {
                                k.NagibKrizaOcena = reader.GetInt32(reader.GetOrdinal("nagib_kriza_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_kolka_ocena")))
                            {
                                k.PolozajKolkaOcena = reader.GetInt32(reader.GetOrdinal("polozaj_kolka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("skocni_sklep_ocena")))
                            {
                                k.SkocniSklepOcena = reader.GetInt32(reader.GetOrdinal("skocni_sklep_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("izraz_skoc_sklepa_ocena")))
                            {
                                k.IzrazSkocSklepaOcena = reader.GetInt32(reader.GetOrdinal("izraz_skoc_sklepa_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("bicelj_ocena")))
                            {
                                k.BiceljOcena = reader.GetInt32(reader.GetOrdinal("bicelj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("parklji_ocena")))
                            {
                                k.ParkljiOcena = reader.GetInt32(reader.GetOrdinal("parklji_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_vimena_ocena")))
                            {
                                k.DolzinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("dolzina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("pripetost_vimena_ocena")))
                            {
                                k.PripetostVimenaOcena = reader.GetInt32(reader.GetOrdinal("pripetost_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_mlecnega_zrcala_ocena")))
                            {
                                k.VisinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("visina_mlecnega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_mlenega_zrcala_ocena")))
                            {
                                k.SirinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("sirina_mlenega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_vimena_ocena")))
                            {
                                k.GlobinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("globina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dno_vimena_ocena")))
                            {
                                k.DnoVimenaOcena = reader.GetInt32(reader.GetOrdinal("dno_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_cent_vezi_ocena")))
                            {
                                k.GlobinaCentVeziOcena = reader.GetInt32(reader.GetOrdinal("globina_cent_vezi_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_seskov_ocena")))
                            {
                                k.DolzinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("dolzina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("debelina_seskov_ocena")))
                            {
                                k.DebelinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("debelina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenost_prednjih_seskov_ocena")))
                            {
                                k.NamenostPrednjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenost_prednjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena")))
                            {
                                k.NamenostZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena")))
                            {
                                k.PolozajZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("omisicanost_ocena")))
                            {
                                k.OmisicanostOcena = reader.GetInt32(reader.GetOrdinal("omisicanost_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kondicija_ocena")))
                            {
                                k.kondicijaOcena = reader.GetInt32(reader.GetOrdinal("kondicija_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_izracun_ocena")))
                            {
                                k.VisinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("visina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_izracun_ocena")))
                            {
                                k.GlobinaTelesaIzracunOcena = reader.GetInt32(reader.GetOrdinal("globina_telesa_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_izracun_ocena")))
                            {
                                k.DolzinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("dolzina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_izracun_ocena")))
                            {
                                k.SednaSirinaIzracunOcena = reader.GetInt32(reader.GetOrdinal("sedna_sirina_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("okvir_ocena")))
                            {
                                k.OkvirOcena = reader.GetInt32(reader.GetOrdinal("okvir_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kriz_ocena")))
                            {
                                k.KrizOcena = reader.GetInt32(reader.GetOrdinal("kriz_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("noge_ocena")))
                            {
                                k.NogeOcena = reader.GetInt32(reader.GetOrdinal("noge_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("vime_ocena")))
                            {
                                k.VimeOcena = reader.GetInt32(reader.GetOrdinal("vime_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena")))
                            {
                                k.TelesneSposobnostiSkupajOcena = reader.GetInt32(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena"));
                            }

                            return k;
                        }
                    }
                }
            }
            return null;
        }

        public int UrediKravo(KraveRazred k)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(@"
            UPDATE zivali SET
                ime = @Ime,
                datum_roj = @DatumRojstva,
                pasma = @Pasma,
                ime_mame = @ImeMame,
                ime_oceta = @ImeOceta,
                usesna_stevilka = @UsesnaStevilka,
                laktacija = @Laktacija,

                odsvetovani_biki = @OdsvetovaniBiki,
                primerni_biki = @PrimerniBiki,
                najbolj_primerni_biki = @NajboljPrimerniBiki,
                teza = @Teza,
                obseg_prsi_cm = @ObsegPrsi,
                visina_kriza_cm = @VisinaKriza,
                globina_telesa_cm = @GlobinaTelesa,
                sirina_spredaj_ocena = @SirinaVspredaj,
                hrbet_ocena = @HrbetOcena,
                dolzina_kriza_cm = @DolzinaKriza,
                sedna_sirina_cm = @SednaSirina,
                nagib_kriza_ocena = @NagibKrizaOcena,
                polozaj_kolka_ocena = @PolozajKolkaOcena,
                skocni_sklep_ocena = @SkocniSklepOcena,
                izraz_skoc_sklepa_ocena = @IzrazSkocSklepaOcena,

                bicelj_ocena = @BiceljOcena,
                parklji_ocena = @ParkljiOcena,
                dolzina_vimena_ocena = @DolzinaVimenaOcena,
                pripetost_vimena_ocena = @PripetostVimenaOcena,
                visina_mlecnega_zrcala_ocena = @VisinaMlecnegaZrcalaOcena,
                sirina_mlenega_zrcala_ocena = @SirinaMlecnegaZrcalaOcena,
                globina_vimena_ocena = @GlobinaVimenaOcena,
                dno_vimena_ocena = @DnoVimenaOcena,
                globina_cent_vezi_ocena = @GlobinaCentVeziOcena,
                dolzina_seskov_ocena = @DolzinaSeskovOcena,
                debelina_seskov_ocena = @DebelinaSeskovOcena,
                namenost_prednjih_seskov_ocena = @NamenostPrednjihSeskovOcena,
                namenbnost_zadnjih_seskov_ocena = @NamenostZadnjihSeskovOcena,
                polozaj_zadnjih_seskov_ocena = @PolozajZadnjihSeskovOcena,
                omisicanost_ocena = @OmisicanostOcena,
                kondicija_ocena = @KondicijaOcena,

                visina_kriza_izracun_ocena = @VisinaKrizaIzracunOcena,
                globina_telesa_izracun_ocena = @GlobinaTelesaIzracunOcena,
                dolzina_kriza_izracun_ocena = @DolzinaKrizaIzracunOcena,
                sedna_sirina_izracun_ocena = @SednaSirinaIzracunOcena,
                okvir_ocena = @OkvirOcena,
                kriz_ocena = @KrizOcena,
                noge_ocena = @NogeOcena,
                vime_ocena = @VimeOcena,
                telesne_lastnosti_skupaj_ocena = @TelesneSposobnostiSkupajOcena,

                datum_pregleda = @DatumPregleda,
                iztok_mleka_ocena = @IztokMlekaOcena,
                iztok_mleka_datum_prvi = @DatumPrvegaIztoka,
                iztok_mleka_ocena_druga = @IztokMlekaOcenaDruga,
                iztok_mleka_datum_drugi = @DatumDrugegaIztoka

            WHERE id = @Id
        ", conn))
                {
                    command.Parameters.AddWithValue("@Id", k.Id);
                    command.Parameters.AddWithValue("@Ime", k.Ime);
                    command.Parameters.AddWithValue("@DatumRojstva", k.DatumRoj);
                    command.Parameters.AddWithValue("@Pasma", k.Pasma);
                    command.Parameters.AddWithValue("@ImeMame", k.ImeMame);
                    command.Parameters.AddWithValue("@ImeOceta", k.ImeOceta);
                    command.Parameters.AddWithValue("@UsesnaStevilka", k.UsesnaSt);
                    command.Parameters.AddWithValue("@Laktacija", k.Laktacija);

                    command.Parameters.AddWithValue("@OdsvetovaniBiki", k.OdsvetovaniBiki);
                    command.Parameters.AddWithValue("@PrimerniBiki", k.PrimerniBiki);
                    command.Parameters.AddWithValue("@NajboljPrimerniBiki", k.NajboljPrimerniBiki);
                    command.Parameters.AddWithValue("@Teza", k.Teza);
                    command.Parameters.AddWithValue("@ObsegPrsi", k.ObsegPrsi);
                    command.Parameters.AddWithValue("@VisinaKriza", k.VisinaKriza);
                    command.Parameters.AddWithValue("@GlobinaTelesa", k.GlobinaTelesa);
                    command.Parameters.AddWithValue("@SirinaVspredaj", k.SirinaVspredaj);
                    command.Parameters.AddWithValue("@HrbetOcena", k.HrbetOcena);
                    command.Parameters.AddWithValue("@DolzinaKriza", k.DolzinaKriza);
                    command.Parameters.AddWithValue("@SednaSirina", k.SednaSirina);
                    command.Parameters.AddWithValue("@NagibKrizaOcena", k.NagibKrizaOcena);
                    command.Parameters.AddWithValue("@PolozajKolkaOcena", k.PolozajKolkaOcena);
                    command.Parameters.AddWithValue("@SkocniSklepOcena", k.SkocniSklepOcena);
                    command.Parameters.AddWithValue("@IzrazSkocSklepaOcena", k.IzrazSkocSklepaOcena);

                    command.Parameters.AddWithValue("@BiceljOcena", k.BiceljOcena);
                    command.Parameters.AddWithValue("@ParkljiOcena", k.ParkljiOcena);
                    command.Parameters.AddWithValue("@DolzinaVimenaOcena", k.DolzinaVimenaOcena);
                    command.Parameters.AddWithValue("@PripetostVimenaOcena", k.PripetostVimenaOcena);
                    command.Parameters.AddWithValue("@VisinaMlecnegaZrcalaOcena", k.VisinaMlecnegaZrcalaOcena);
                    command.Parameters.AddWithValue("@SirinaMlecnegaZrcalaOcena", k.SirinaMlecnegaZrcalaOcena);
                    command.Parameters.AddWithValue("@GlobinaVimenaOcena", k.GlobinaVimenaOcena);
                    command.Parameters.AddWithValue("@DnoVimenaOcena", k.DnoVimenaOcena);
                    command.Parameters.AddWithValue("@GlobinaCentVeziOcena", k.GlobinaCentVeziOcena);
                    command.Parameters.AddWithValue("@DolzinaSeskovOcena", k.DolzinaSeskovOcena);
                    command.Parameters.AddWithValue("@DebelinaSeskovOcena", k.DebelinaSeskovOcena);
                    command.Parameters.AddWithValue("@NamenostPrednjihSeskovOcena", k.NamenostPrednjihSeskovOcena);
                    command.Parameters.AddWithValue("@NamenostZadnjihSeskovOcena", k.NamenostZadnjihSeskovOcena);
                    command.Parameters.AddWithValue("@PolozajZadnjihSeskovOcena", k.PolozajZadnjihSeskovOcena);
                    command.Parameters.AddWithValue("@OmisicanostOcena", k.OmisicanostOcena);
                    command.Parameters.AddWithValue("@KondicijaOcena", k.kondicijaOcena);

                    command.Parameters.AddWithValue("@VisinaKrizaIzracunOcena", k.VisinaKrizaIzracunOcena);
                    command.Parameters.AddWithValue("@GlobinaTelesaIzracunOcena", k.GlobinaTelesaIzracunOcena);
                    command.Parameters.AddWithValue("@DolzinaKrizaIzracunOcena", k.DolzinaKrizaIzracunOcena);
                    command.Parameters.AddWithValue("@SednaSirinaIzracunOcena", k.SednaSirinaIzracunOcena);
                    command.Parameters.AddWithValue("@OkvirOcena", k.OkvirOcena);
                    command.Parameters.AddWithValue("@KrizOcena", k.KrizOcena);
                    command.Parameters.AddWithValue("@NogeOcena", k.NogeOcena);
                    command.Parameters.AddWithValue("@VimeOcena", k.VimeOcena);
                    command.Parameters.AddWithValue("@TelesneSposobnostiSkupajOcena", k.TelesneSposobnostiSkupajOcena);

                    if (k.DatumPregleda.HasValue)
                    {
                        command.Parameters.AddWithValue("@DatumPregleda", k.DatumPregleda.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DatumPregleda", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@IztokMlekaOcena", k.IztokMlekaOcena);
                    if (k.DatumPrvegaIztoka.HasValue)
                    {
                        command.Parameters.AddWithValue("@DatumPrvegaIztoka", k.DatumPrvegaIztoka.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DatumPrvegaIztoka", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@IztokMlekaOcenaDruga", k.IztokMlekaOcenaDruga);
                    if (k.DatumDrugegaIztoka.HasValue)
                    {
                        command.Parameters.AddWithValue("@DatumDrugegaIztoka", k.DatumDrugegaIztoka.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DatumDrugegaIztoka", DBNull.Value);
                    }

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public string PridobiUsStKrave(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT usesna_stevilka FROM zivali WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                }
            }
            return null;
        }

        public int PridobiIdKravePrekoUsSt(string usSt)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT id FROM zivali WHERE usesna_stevilka = @UsSt", conn))
                {
                    cmd.Parameters.AddWithValue("@UsSt", usSt);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                    }
                }
            }
            return -1;
        }


        // Osebe
        public List<OsebeRazred> PridobiOsebe()
        {
            List<OsebeRazred> osebe = new List<OsebeRazred>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.ime, o.priimek, o.tel, o.email, z.zadolzitev " +
                    "FROM osebe o INNER JOIN zadolzitve_oseb z ON o.zadolzitev_id = z.id " +
                    "ORDER BY o.id DESC", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            osebe.Add(new OsebeRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            ));
                        }
                    }
                }
            }
            return osebe;
        }

        public OsebeRazred PridobiOsebo(int idOsebe)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.ime, o.priimek, o.tel, o.email, z.zadolzitev " +
                    "FROM osebe o INNER JOIN zadolzitve_oseb z ON o.zadolzitev_id = z.id " +
                    "WHERE o.id = @idOsebe", conn))
                {
                    cmd.Parameters.AddWithValue("@idOsebe", idOsebe);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OsebeRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
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

        public List<string> PridobiZadolzitve()
        {
            List<string> zadolzitve = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT zadolzitev FROM zadolzitve_oseb ORDER BY zadolzitev", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zadolzitve.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return zadolzitve;
        }

        public int DodajOsebo(string ime, string priimek, string tel, string email, string zadolzitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO osebe (ime, priimek, tel, email, zadolzitev_id) " +
                    "VALUES (@Ime, @Priimek, @Tel, @Email, (SELECT id FROM zadolzitve_oseb WHERE zadolzitev = @Zadolzitev))", conn))
                {
                    cmd.Parameters.AddWithValue("@Ime", ime);
                    cmd.Parameters.AddWithValue("@Priimek", priimek);
                    cmd.Parameters.AddWithValue("@Tel", tel);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Zadolzitev", zadolzitev);
                    int rezultat = cmd.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public void DodajZadolzitev(string zadolzitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO zadolzitve_oseb (zadolzitev) VALUES (@Zadolzitev)", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", zadolzitev);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int PridobiIdZadolzitvePrekoImena(string zadolzitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT id FROM zadolzitve_oseb WHERE zadolzitev = @Zadolzitev", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", zadolzitev);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1;
        }

        public void UrediZadolzitev(int id, string zadolzitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE zadolzitve_oseb SET zadolzitev = @Zadolzitev WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", zadolzitev);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int UrediOsebo(int id, string ime, string priimek, string tel, string email, string zadolzitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE osebe SET ime = @Ime, priimek = @Priimek, tel = @Tel, email = @Email, zadolzitev_id = (SELECT id FROM zadolzitve_oseb WHERE zadolzitev = @Zadolzitev) " +
                    "WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Ime", ime);
                    cmd.Parameters.AddWithValue("@Priimek", priimek);
                    cmd.Parameters.AddWithValue("@Tel", tel);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Zadolzitev", zadolzitev);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public List<OsebeRazred> PridobiKontrolerje()
        {
            List<OsebeRazred> kontrolerji = new List<OsebeRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.ime, o.priimek, o.tel, o.email, zo.zadolzitev " +
                    "FROM osebe o " +
                    "INNER JOIN zadolzitve_oseb zo ON o.zadolzitev_id = zo.id " +
                    "WHERE zo.zadolzitev = @Zadolzitev " +
                    "ORDER BY o.ime, o.priimek", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", "Kontrolor");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kontrolerji.Add(new OsebeRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            ));
                        }
                    }
                }
            }

            return kontrolerji;
        }

        public List<OsebeRazred> PridobiVeterinarje()
        {
            List<OsebeRazred> veterinarji = new List<OsebeRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.ime, o.priimek, o.tel, o.email, zo.zadolzitev " +
                    "FROM osebe o " +
                    "INNER JOIN zadolzitve_oseb zo ON o.zadolzitev_id = zo.id " +
                    "WHERE zo.zadolzitev = @Zadolzitev " +
                    "ORDER BY o.ime, o.priimek", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", "Veterinar");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            veterinarji.Add(new OsebeRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            ));
                        }
                    }
                }
            }

            return veterinarji;
        }

        public List<OsebeRazred> PridobiIzvajalce()
        {
            List<OsebeRazred> izvajalci = new List<OsebeRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.ime, o.priimek, o.tel, o.email, zo.zadolzitev " +
                    "FROM osebe o " +
                    "INNER JOIN zadolzitve_oseb zo ON o.zadolzitev_id = zo.id " +
                    "WHERE zo.zadolzitev = @Zadolzitev " +
                    "ORDER BY o.ime, o.priimek", conn))
                {
                    cmd.Parameters.AddWithValue("@Zadolzitev", "Korektor parkljev");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            izvajalci.Add(new OsebeRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5)
                            ));
                        }
                    }
                }
            }

            return izvajalci;
        }

        // Odhodi

        public List<OdhodiRazred> PridobiOdhode()
        {
            List<OdhodiRazred> odhodi = new List<OdhodiRazred>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT o.id, o.datum, o.[g-mid], o.lokacija, o.vzrok, o.opombe, o.krava_id " +
                    "FROM odhodi_krav o " +
                    "ORDER BY o.id DESC", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            odhodi.Add(new OdhodiRazred(
                                reader.GetInt32(0),
                                reader.GetDateTime(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetInt32(6)
                            ));
                        }
                    }
                }
            }
            return odhodi;
        }

        public int DodajOdhod(DateTime datum, string g_mid, string lokacija, string vzrok, string opombe, int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO odhodi_krav (datum, [g-mid], lokacija, vzrok, opombe, krava_id) " +
                    "VALUES (@Datum, @GMid, @Lokacija, @Vzrok, @Opombe, @KravaId)", conn))
                {
                    cmd.Parameters.AddWithValue("@Datum", datum);
                    cmd.Parameters.AddWithValue("@GMid", g_mid);
                    cmd.Parameters.AddWithValue("@Lokacija", lokacija);
                    cmd.Parameters.AddWithValue("@Vzrok", vzrok);
                    cmd.Parameters.AddWithValue("@Opombe", opombe);
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public int UrediOdhod(int id, DateTime datum, string g_mid, string lokacija, string vzrok, string opombe, int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE odhodi_krav SET datum = @Datum, [g-mid] = @GMid, lokacija = @Lokacija, vzrok = @Vzrok, opombe = @Opombe, krava_id = @KravaId " +
                    "WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Datum", datum);
                    cmd.Parameters.AddWithValue("@GMid", g_mid);
                    cmd.Parameters.AddWithValue("@Lokacija", lokacija);
                    cmd.Parameters.AddWithValue("@Vzrok", vzrok);
                    cmd.Parameters.AddWithValue("@Opombe", opombe);
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public OdhodiRazred PridobiOdhod(int idOdhoda)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT id, datum, [g-mid], lokacija, vzrok, opombe, krava_id " +
                    "FROM odhodi_krav WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", idOdhoda);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new OdhodiRazred(
                                reader.GetInt32(0),
                                reader.GetDateTime(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetInt32(6)
                            );
                        }
                    }
                }
            }
            return null;
        }

        public int IzbrisiOdhod(int idOdhoda)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM odhodi_krav WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", idOdhoda);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }


        // Mlečne kontrole
        public List<MlecneKontroleRazred> PridobiMlecneKontrole(int idKrave)
        {
            List<MlecneKontroleRazred> kontrole = new List<MlecneKontroleRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT mk.*, o.ime, o.priimek, z.usesna_stevilka
                    FROM mlecne_kontrole mk
                    JOIN osebe o ON mk.kontrolor_id = o.id
                    JOIN zivali z ON mk.krava_id = z.id
                    WHERE mk.krava_id = @IdKrave
                    ORDER BY mk.id DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKrave", idKrave);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MlecneKontroleRazred kontrola = new MlecneKontroleRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader.GetInt32(reader.GetOrdinal("zap_st")),
                                reader["ime"].ToString() + " " + reader["priimek"].ToString(),
                                reader["del_dneva"].ToString(),
                                reader["mlecnost"].ToString(),
                                reader["vsebnost_mascobe"].ToString(),
                                reader["vsebnost_beljakovin"].ToString(),
                                reader["vsebnost_laktoze"].ToString(),
                                reader["vsebnost_secnine"].ToString(),
                                reader["somatske_celice"]?.ToString(),
                                reader["opombe"].ToString(),
                                reader["usesna_stevilka"].ToString()
                            );

                            kontrole.Add(kontrola);
                        }
                    }
                }
            }

            return kontrole;
        }

        public MlecneKontroleRazred PridobiMlecnoKontrolo(int idKontrole)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT mk.*, o.ime, o.priimek, z.usesna_stevilka
                    FROM mlecne_kontrole mk
                    JOIN osebe o ON mk.kontrolor_id = o.id
                    JOIN zivali z ON mk.krava_id = z.id
                    WHERE mk.id = @IdKontrole";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKontrole", idKontrole);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MlecneKontroleRazred k = new MlecneKontroleRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader.GetInt32(reader.GetOrdinal("zap_st")),
                                reader["ime"].ToString() + " " + reader["priimek"].ToString(),
                                reader["del_dneva"].ToString(),
                                reader["mlecnost"].ToString(),
                                reader["vsebnost_mascobe"].ToString(),
                                reader["vsebnost_beljakovin"].ToString(),
                                reader["vsebnost_laktoze"].ToString(),
                                reader["vsebnost_secnine"].ToString(),
                                reader["somatske_celice"]?.ToString(),
                                reader["opombe"].ToString(),
                                reader["usesna_stevilka"].ToString()
                            );

                            return k;
                        }
                    }
                }
            }
            return null;
        }

        public int DodajMlecnoKontrolo(int kravaId, DateTime datum, int zapSt, int kontrolorId, string delDneva, string mlecnost, string vsebnostMascobe, string vsebnostBeljakovin, string vsebnostLaktoze, string vsebnostSecnine, string somatskeCelice, string opombe)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO mlecne_kontrole (krava_id, datum, zap_st, kontrolor_id, del_dneva, mlecnost, vsebnost_mascobe, vsebnost_beljakovin, vsebnost_laktoze, vsebnost_secnine, somatske_celice, opombe) " +
                    "VALUES (@KravaId, @Datum, @ZapSt, @KontrolorId, @DelDneva, @Mlecnost, @VsebnostMascobe, @VsebnostBeljakovin, @VsebnostLaktoze, @VsebnostSecnine, @SomatskeCelice, @Opombe)", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    cmd.Parameters.AddWithValue("@Datum", datum);
                    cmd.Parameters.AddWithValue("@ZapSt", zapSt);
                    cmd.Parameters.AddWithValue("@KontrolorId", kontrolorId);
                    cmd.Parameters.AddWithValue("@DelDneva", delDneva);
                    cmd.Parameters.AddWithValue("@Mlecnost", mlecnost);
                    cmd.Parameters.AddWithValue("@VsebnostMascobe", vsebnostMascobe);
                    cmd.Parameters.AddWithValue("@VsebnostBeljakovin", vsebnostBeljakovin);
                    cmd.Parameters.AddWithValue("@VsebnostLaktoze", vsebnostLaktoze);
                    cmd.Parameters.AddWithValue("@VsebnostSecnine", vsebnostSecnine);
                    cmd.Parameters.AddWithValue("@SomatskeCelice", somatskeCelice);
                    cmd.Parameters.AddWithValue("@Opombe", opombe);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public int UrediMlecnoKontrolo(int id, DateTime datum, int zapSt, int kontrolorId, string delDneva, string mlecnost, string vsebnostMascobe, string vsebnostBeljakovin, string vsebnostLaktoze, string vsebnostSecnine, string somatskeCelice, string opombe)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE mlecne_kontrole SET datum = @Datum, zap_st = @ZapSt, kontrolor_id = @KontrolorId, del_dneva = @DelDneva, mlecnost = @Mlecnost, vsebnost_mascobe = @VsebnostMascobe, vsebnost_beljakovin = @VsebnostBeljakovin, vsebnost_laktoze = @VsebnostLaktoze, vsebnost_secnine = @VsebnostSecnine, somatske_celice = @SomatskeCelice, opombe = @Opombe " +
                    "WHERE id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Datum", datum);
                    cmd.Parameters.AddWithValue("@ZapSt", zapSt);
                    cmd.Parameters.AddWithValue("@KontrolorId", kontrolorId);
                    cmd.Parameters.AddWithValue("@DelDneva", delDneva);
                    cmd.Parameters.AddWithValue("@Mlecnost", mlecnost);
                    cmd.Parameters.AddWithValue("@VsebnostMascobe", vsebnostMascobe);
                    cmd.Parameters.AddWithValue("@VsebnostBeljakovin", vsebnostBeljakovin);
                    cmd.Parameters.AddWithValue("@VsebnostLaktoze", vsebnostLaktoze);
                    cmd.Parameters.AddWithValue("@VsebnostSecnine", vsebnostSecnine);
                    cmd.Parameters.AddWithValue("@SomatskeCelice", somatskeCelice);
                    cmd.Parameters.AddWithValue("@Opombe", opombe);
                    int rezultat = cmd.ExecuteNonQuery();
                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        public int PridobiSteviloMlecnihKontrol(int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM mlecne_kontrole WHERE krava_id = @KravaId", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return 0;
        }

        public bool PogledObstajaSt(int kravaId, int st)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM mlecne_kontrole WHERE krava_id = @KravaId AND zap_st = @ZapSt", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    cmd.Parameters.AddWithValue("@ZapSt", st);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        // Osemenitve
        public List<OsemenitveRazred> PridobiOsemenitve(int idKrave)
        {
            List<OsemenitveRazred> osemenitve = new List<OsemenitveRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT o.id, o.zap_os, o.datum, o.veterinar_id, o.opombe, o.krava_id, o.bik_id, " +
                    "o.datum_pregleda, o.izzid_pregleda, o.nacin_pregleda, o.opombe_pregleda, o.veterinar_pregleda_id, " +
                    "o.datum_presusitve, o.opombe_presusitve, o.kondicija_ob_presusitvi, " +
                    "v.ime AS veterinar_ime, v.priimek AS veterinar_priimek, " +
                    "k.usesna_stevilka AS krava_usesna, k.ime AS krava_ime, " +
                    "b.ime AS bik_ime, b.stevilka AS bik_stevilka, " +
                    "vp.ime AS veterinar_pregleda_ime, vp.priimek AS veterinar_pregleda_priimek " +
                    "FROM osemenitve o " +
                    "INNER JOIN osebe v ON o.veterinar_id = v.id " +
                    "INNER JOIN zivali k ON o.krava_id = k.id " +
                    "INNER JOIN biki_os b ON o.bik_id = b.id " +
                    "LEFT JOIN osebe vp ON o.veterinar_pregleda_id = vp.id " +
                    "WHERE o.krava_id = @IdKrave " +
                    "ORDER BY o.id DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKrave", idKrave);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime? datumPregleda = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("datum_pregleda")))
                            {
                                datumPregleda = reader.GetDateTime(reader.GetOrdinal("datum_pregleda"));
                            }

                            DateTime? datumPresusitve = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("datum_presusitve")))
                            {
                                datumPresusitve = reader.GetDateTime(reader.GetOrdinal("datum_presusitve"));
                            }

                            string veterinarPregleda = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_ime")) &&
                                !reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_priimek")))
                            {
                                veterinarPregleda =
                                    reader["veterinar_pregleda_ime"].ToString() + " " +
                                    reader["veterinar_pregleda_priimek"].ToString();
                            }

                            string bik = reader["bik_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("bik_stevilka")) &&
                                !string.IsNullOrWhiteSpace(reader["bik_stevilka"].ToString()))
                            {
                                bik += " (" + reader["bik_stevilka"].ToString() + ")";
                            }

                            string krava = reader["krava_usesna"].ToString();

                            if (string.IsNullOrWhiteSpace(krava))
                            {
                                krava = reader["krava_ime"].ToString();
                            }

                            OsemenitveRazred osemenitev = new OsemenitveRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("zap_os")),
                                reader.GetInt32(reader.GetOrdinal("krava_id")),
                                reader.GetInt32(reader.GetOrdinal("bik_id")),
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader["veterinar_ime"].ToString() + " " + reader["veterinar_priimek"].ToString(),
                                reader["opombe"].ToString(),
                                bik,
                                krava,

                                datumPregleda,
                                reader["izzid_pregleda"].ToString(),
                                reader["nacin_pregleda"].ToString(),
                                reader["opombe_pregleda"].ToString(),
                                veterinarPregleda,

                                datumPresusitve,
                                reader["opombe_presusitve"].ToString(),
                                reader["kondicija_ob_presusitvi"].ToString()
                            );

                            osemenitev.VeterinarId = reader.GetInt32(reader.GetOrdinal("veterinar_id"));

                            if (!reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_id")))
                            {
                                osemenitev.VeterinarPregledaId = reader.GetInt32(reader.GetOrdinal("veterinar_pregleda_id"));
                            }
                            else
                            {
                                osemenitev.VeterinarPregledaId = 0;
                            }

                            osemenitve.Add(osemenitev);
                        }
                    }
                }
            }

            return osemenitve;
        }

        public OsemenitveRazred PridobiOsemenitev(int IdOsemenitve)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT o.*, " +
                    "v.ime AS veterinar_ime, v.priimek AS veterinar_priimek, " +
                    "k.usesna_stevilka AS krava_usesna, k.ime AS krava_ime, " +
                    "b.ime AS bik_ime, b.stevilka AS bik_stevilka, " +
                    "vp.ime AS veterinar_pregleda_ime, vp.priimek AS veterinar_pregleda_priimek " +
                    "FROM osemenitve o " +
                    "JOIN osebe v ON o.veterinar_id = v.id " +
                    "JOIN zivali k ON o.krava_id = k.id " +
                    "JOIN biki_os b ON o.bik_id = b.id " +
                    "LEFT JOIN osebe vp ON o.veterinar_pregleda_id = vp.id " +
                    "WHERE o.id = @IdOsemenitve";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdOsemenitve", IdOsemenitve);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime? datumPregleda = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("datum_pregleda")))
                            {
                                datumPregleda = reader.GetDateTime(reader.GetOrdinal("datum_pregleda"));
                            }

                            DateTime? datumPresusitve = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("datum_presusitve")))
                            {
                                datumPresusitve = reader.GetDateTime(reader.GetOrdinal("datum_presusitve"));
                            }

                            string veterinarPregleda = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_ime")) &&
                                !reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_priimek")))
                            {
                                veterinarPregleda =
                                    reader["veterinar_pregleda_ime"].ToString() + " " +
                                    reader["veterinar_pregleda_priimek"].ToString();
                            }

                            string bik = reader["bik_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("bik_stevilka")) &&
                                !string.IsNullOrWhiteSpace(reader["bik_stevilka"].ToString()))
                            {
                                bik += " (" + reader["bik_stevilka"].ToString() + ")";
                            }

                            string krava = reader["krava_usesna"].ToString();

                            if (string.IsNullOrWhiteSpace(krava))
                            {
                                krava = reader["krava_ime"].ToString();
                            }

                            OsemenitveRazred osemenitev = new OsemenitveRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("zap_os")),
                                reader.GetInt32(reader.GetOrdinal("krava_id")),
                                reader.GetInt32(reader.GetOrdinal("bik_id")),
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader["veterinar_ime"].ToString() + " " + reader["veterinar_priimek"].ToString(),
                                reader["opombe"].ToString(),
                                bik,
                                krava,

                                datumPregleda,
                                reader["izzid_pregleda"].ToString(),
                                reader["nacin_pregleda"].ToString(),
                                reader["opombe_pregleda"].ToString(),
                                veterinarPregleda,

                                datumPresusitve,
                                reader["opombe_presusitve"].ToString(),
                                reader["kondicija_ob_presusitvi"].ToString()
                            );

                            osemenitev.VeterinarId = reader.GetInt32(reader.GetOrdinal("veterinar_id"));

                            if (!reader.IsDBNull(reader.GetOrdinal("veterinar_pregleda_id")))
                            {
                                osemenitev.VeterinarPregledaId = reader.GetInt32(reader.GetOrdinal("veterinar_pregleda_id"));
                            }
                            else
                            {
                                osemenitev.VeterinarPregledaId = 0;
                            }

                            return osemenitev;
                        }
                    }
                }
            }

            return null;
        }

        public int DodajOsemenitev(int zapOs, DateTime datum, int veterinarId, string opombe, int kravaId, int bikId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "INSERT INTO osemenitve (zap_os, datum, veterinar_id, opombe, krava_id, bik_id) " +
                    "VALUES (@ZapOs, @Datum, @VeterinarId, @Opombe, @KravaId, @BikId)", conn))
                {
                    command.Parameters.AddWithValue("@ZapOs", zapOs);
                    command.Parameters.AddWithValue("@Datum", datum);
                    command.Parameters.AddWithValue("@VeterinarId", veterinarId);

                    if (string.IsNullOrWhiteSpace(opombe))
                    {
                        command.Parameters.AddWithValue("@Opombe", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Opombe", opombe);
                    }

                    command.Parameters.AddWithValue("@KravaId", kravaId);
                    command.Parameters.AddWithValue("@BikId", bikId);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public bool PogledObstajaStOsemenitev(int kravaId, int zapOs)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM osemenitve WHERE krava_id = @KravaId AND zap_os = @ZapOs", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    cmd.Parameters.AddWithValue("@ZapOs", zapOs);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public int UrediOsemenitev(OsemenitveRazred o)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE osemenitve SET " +
                    "bik_id = @BikId, " +
                    "veterinar_id = @VeterinarId, " +
                    "opombe = @Opombe, " +
                    "datum_pregleda = @DatumPregleda, " +
                    "izzid_pregleda = @IzzidPregleda, " +
                    "nacin_pregleda = @NacinPregleda, " +
                    "opombe_pregleda = @OpombePregleda, " +
                    "veterinar_pregleda_id = @VeterinarPregledaId, " +
                    "datum_presusitve = @DatumPresusitve, " +
                    "opombe_presusitve = @OpombePresusitve, " +
                    "kondicija_ob_presusitvi = @KondicijaObPresusitvi " +
                    "WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Id", o.Id);
                    command.Parameters.AddWithValue("@BikId", o.BikId);
                    command.Parameters.AddWithValue("@VeterinarId", o.VeterinarId);
                    command.Parameters.AddWithValue("@Opombe", o.Opombe);

                    if (o.Datum_Pregleda.HasValue)
                    {
                        command.Parameters.AddWithValue("@DatumPregleda", o.Datum_Pregleda.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DatumPregleda", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@IzzidPregleda", o.Izzid_Pregleda);
                    command.Parameters.AddWithValue("@NacinPregleda", o.Nacin_Pregleda);
                    command.Parameters.AddWithValue("@OpombePregleda", o.Opombe_Pregleda);

                    if (o.VeterinarPregledaId != 0)
                    {
                        command.Parameters.AddWithValue("@VeterinarPregledaId", o.VeterinarPregledaId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@VeterinarPregledaId", DBNull.Value);
                    }

                    if (o.Datum_Presusitve.HasValue)
                    {
                        command.Parameters.AddWithValue("@DatumPresusitve", o.Datum_Presusitve.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@DatumPresusitve", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("@OpombePresusitve", o.Opombe_Presusitve);
                    command.Parameters.AddWithValue("@KondicijaObPresusitvi", o.Kondicija_ob_Presusitvi);

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        // Pojatve
        public List<PojatveRazred> PridobiPojatve(int idKrave)
        {
            List<PojatveRazred> pojatve = new List<PojatveRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT p.id, p.zap_st, p.krava_id, z.ime, z.usesna_stevilka, p.datum, p.konec_datum, p.opombe " +
                    "FROM pojatve p " +
                    "INNER JOIN zivali z ON p.krava_id = z.id " +
                    "WHERE p.krava_id = @IdKrave " +
                    "ORDER BY p.zap_st DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@IdKrave", idKrave);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string kravaIme = reader["ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("usesna_stevilka")) &&
                                !string.IsNullOrWhiteSpace(reader["usesna_stevilka"].ToString()))
                            {
                                kravaIme += " (" + reader["usesna_stevilka"].ToString() + ")";
                            }

                            DateTime? konecDatum = null;

                            if (!reader.IsDBNull(reader.GetOrdinal("konec_datum")))
                            {
                                konecDatum = reader.GetDateTime(reader.GetOrdinal("konec_datum"));
                            }

                            if (konecDatum.HasValue)
                            {
                                pojatve.Add(new PojatveRazred(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("zap_st")),
                                    reader.GetInt32(reader.GetOrdinal("krava_id")),
                                    kravaIme,
                                    reader.GetDateTime(reader.GetOrdinal("datum")),
                                    konecDatum.Value,
                                    reader["opombe"].ToString()
                                ));
                            }
                            else
                            {
                                pojatve.Add(new PojatveRazred(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("zap_st")),
                                    reader.GetInt32(reader.GetOrdinal("krava_id")),
                                    kravaIme,
                                    reader.GetDateTime(reader.GetOrdinal("datum")),
                                    reader["opombe"].ToString()
                                ));
                            }
                        }
                    }
                }
            }

            return pojatve;
        }

        public PojatveRazred PridobiPojatev(int idPojatve)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT p.*, z.ime AS krava_ime, z.usesna_stevilka AS krava_usesna " +
                    "FROM pojatve p " +
                    "INNER JOIN zivali z ON p.krava_id = z.id " +
                    "WHERE p.id = @IdPojatve";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPojatve", idPojatve);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string kravaIme = reader["krava_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("krava_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["krava_usesna"].ToString()))
                            {
                                kravaIme += " (" + reader["krava_usesna"].ToString() + ")";
                            }

                            DateTime? konecDatum = null;

                            if (!reader.IsDBNull(reader.GetOrdinal("konec_datum")))
                            {
                                konecDatum = reader.GetDateTime(reader.GetOrdinal("konec_datum"));
                            }

                            if (konecDatum.HasValue)
                            {
                                PojatveRazred pojatev = new PojatveRazred(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("zap_st")),
                                    reader.GetInt32(reader.GetOrdinal("krava_id")),
                                    kravaIme,
                                    reader.GetDateTime(reader.GetOrdinal("datum")),
                                    konecDatum.Value,
                                    reader["opombe"].ToString()
                                );

                                return pojatev;
                            }
                            else
                            {
                                PojatveRazred pojatev = new PojatveRazred(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("zap_st")),
                                    reader.GetInt32(reader.GetOrdinal("krava_id")),
                                    kravaIme,
                                    reader.GetDateTime(reader.GetOrdinal("datum")),
                                    reader["opombe"].ToString()
                                );

                                return pojatev;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public int PridobiSteviloPojatve(int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM pojatve WHERE krava_id = @KravaId", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool PogledObstajaStPojatev(int kravaId, int zapSt)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM pojatve WHERE krava_id = @KravaId AND zap_st = @ZapSt", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaId", kravaId);
                    cmd.Parameters.AddWithValue("@ZapSt", zapSt);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public int DodajPojatev(int zapSt, DateTime datum, string opombe, int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "INSERT INTO pojatve (zap_st, datum, opombe, krava_id) " +
                    "VALUES (@ZapSt, @Datum, @Opombe, @KravaId)", conn))
                {
                    command.Parameters.AddWithValue("@ZapSt", zapSt);
                    command.Parameters.AddWithValue("@Datum", datum.ToString("yyyy-MM-dd"));

                    if (string.IsNullOrWhiteSpace(opombe))
                    {
                        command.Parameters.AddWithValue("@Opombe", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Opombe", opombe);
                    }

                    command.Parameters.AddWithValue("@KravaId", kravaId);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public int UrediPojatev(PojatveRazred p)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE pojatve SET " +
                    "zap_st = @ZapSt, " +
                    "datum = @Datum, " +
                    "konec_datum = @KonecDatum, " +
                    "opombe = @Opombe " +
                    "WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Id", p.Id);
                    command.Parameters.AddWithValue("@ZapSt", p.ZaporednoStevilo);
                    command.Parameters.AddWithValue("@Datum", p.DatumPojatve.ToString("yyyy-MM-dd"));

                    if (p.KonecDatumPojatve.HasValue)
                    {
                        command.Parameters.AddWithValue("@KonecDatum", p.KonecDatumPojatve.Value.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@KonecDatum", DBNull.Value);
                    }

                    if (string.IsNullOrWhiteSpace(p.Opombe))
                    {
                        command.Parameters.AddWithValue("@Opombe", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Opombe", p.Opombe);
                    }

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        // telitve

        public List<TelitevRazred> PridobiTelitve(int idKrave)
        {
            List<TelitevRazred> telitve = new List<TelitevRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT t.*, " +
                    "m.ime AS mama_ime, m.usesna_stevilka AS mama_usesna, " +
                    "te.ime AS tele_ime, te.usesna_stevilka AS tele_usesna, " +
                    "b.ime AS bik_ime, b.stevilka AS bik_stevilka " +
                    "FROM telitve t " +
                    "INNER JOIN zivali m ON t.krava_mama_id = m.id " +
                    "INNER JOIN zivali te ON t.tele_id = te.id " +
                    "INNER JOIN biki_os b ON t.biki_id = b.id " +
                    "WHERE t.krava_mama_id = @IdKrave " +
                    "ORDER BY t.zap_telitev DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKrave", idKrave);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string mama = reader["mama_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("mama_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["mama_usesna"].ToString()))
                            {
                                mama += " (" + reader["mama_usesna"].ToString() + ")";
                            }

                            string tele = reader["tele_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("tele_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["tele_usesna"].ToString()))
                            {
                                tele += " (" + reader["tele_usesna"].ToString() + ")";
                            }

                            string bik = reader["bik_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("bik_stevilka")) &&
                                !string.IsNullOrWhiteSpace(reader["bik_stevilka"].ToString()))
                            {
                                bik += " (" + reader["bik_stevilka"].ToString() + ")";
                            }

                            TelitevRazred telitev = new TelitevRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("zap_telitev")),
                                reader["potek"].ToString(),
                                reader["rojstvo"].ToString(),
                                reader["kakovost_mleziva"].ToString(),

                                reader.GetInt32(reader.GetOrdinal("tele_id")),
                                tele,

                                reader.GetInt32(reader.GetOrdinal("biki_id")),
                                bik,

                                reader.GetInt32(reader.GetOrdinal("krava_mama_id")),
                                mama,

                                reader.GetDateTime(reader.GetOrdinal("datum"))
                            );

                            telitve.Add(telitev);
                        }
                    }
                }
            }

            return telitve;
        }

        public TelitevRazred PridobiTelitev(int idTelitve)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT t.*, " +
                    "m.ime AS mama_ime, m.usesna_stevilka AS mama_usesna, " +
                    "te.ime AS tele_ime, te.usesna_stevilka AS tele_usesna, " +
                    "b.ime AS bik_ime, b.stevilka AS bik_stevilka " +
                    "FROM telitve t " +
                    "INNER JOIN zivali m ON t.krava_mama_id = m.id " +
                    "INNER JOIN zivali te ON t.tele_id = te.id " +
                    "INNER JOIN biki_os b ON t.biki_id = b.id " +
                    "WHERE t.id = @IdTelitve";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdTelitve", idTelitve);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string mama = reader["mama_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("mama_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["mama_usesna"].ToString()))
                            {
                                mama += " (" + reader["mama_usesna"].ToString() + ")";
                            }

                            string tele = reader["tele_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("tele_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["tele_usesna"].ToString()))
                            {
                                tele += " (" + reader["tele_usesna"].ToString() + ")";
                            }

                            string bik = reader["bik_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("bik_stevilka")) &&
                                !string.IsNullOrWhiteSpace(reader["bik_stevilka"].ToString()))
                            {
                                bik += " (" + reader["bik_stevilka"].ToString() + ")";
                            }

                            TelitevRazred telitev = new TelitevRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("zap_telitev")),
                                reader["potek"].ToString(),
                                reader["rojstvo"].ToString(),
                                reader["kakovost_mleziva"].ToString(),

                                reader.GetInt32(reader.GetOrdinal("tele_id")),
                                tele,

                                reader.GetInt32(reader.GetOrdinal("biki_id")),
                                bik,

                                reader.GetInt32(reader.GetOrdinal("krava_mama_id")),
                                mama,

                                reader.GetDateTime(reader.GetOrdinal("datum"))
                            );

                            return telitev;
                        }
                    }
                }
            }

            return null;
        }

        public List<KraveRazred> PridobiZivaliBrezTelitve()
        {
            List<KraveRazred> zivali = new List<KraveRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT z.* " +
                    "FROM zivali z " +
                    "WHERE z.id NOT IN (SELECT tele_id FROM telitve) " +
                    "ORDER BY z.id DESC", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string ime = reader.GetString(reader.GetOrdinal("ime"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum_roj"));
                            string pasma = reader.GetString(reader.GetOrdinal("pasma"));

                            string imeMame = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_mame")))
                            {
                                imeMame = reader.GetString(reader.GetOrdinal("ime_mame"));
                            }

                            string imeOceta = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_oceta")))
                            {
                                imeOceta = reader.GetString(reader.GetOrdinal("ime_oceta"));
                            }

                            string usesna = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("usesna_stevilka")))
                            {
                                usesna = reader.GetString(reader.GetOrdinal("usesna_stevilka"));
                            }

                            string laktacija = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("laktacija")))
                            {
                                laktacija = reader.GetString(reader.GetOrdinal("laktacija"));
                            }

                            KraveRazred z = new KraveRazred(id, ime, datum, pasma, imeMame, imeOceta, usesna, laktacija);

                            zivali.Add(z);
                        }
                    }
                }
            }

            return zivali;
        }

        public List<KraveRazred> PridobiZivaliBrezTelitveAliTrenutno(int trenutniTeleId)
        {
            List<KraveRazred> zivali = new List<KraveRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT z.* " +
                    "FROM zivali z " +
                    "WHERE z.id NOT IN (SELECT tele_id FROM telitve WHERE tele_id != @TrenutniTeleId) " +
                    "ORDER BY z.id DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@TrenutniTeleId", trenutniTeleId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string ime = reader.GetString(reader.GetOrdinal("ime"));
                            DateTime datum = reader.GetDateTime(reader.GetOrdinal("datum_roj"));
                            string pasma = reader.GetString(reader.GetOrdinal("pasma"));

                            string imeMame = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_mame")))
                            {
                                imeMame = reader.GetString(reader.GetOrdinal("ime_mame"));
                            }

                            string imeOceta = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("ime_oceta")))
                            {
                                imeOceta = reader.GetString(reader.GetOrdinal("ime_oceta"));
                            }

                            string usesna = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("usesna_stevilka")))
                            {
                                usesna = reader.GetString(reader.GetOrdinal("usesna_stevilka"));
                            }

                            string laktacija = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("laktacija")))
                            {
                                laktacija = reader.GetString(reader.GetOrdinal("laktacija"));
                            }

                            KraveRazred z = new KraveRazred(id, ime, datum, pasma, imeMame, imeOceta, usesna, laktacija);

                            zivali.Add(z);
                        }
                    }
                }
            }

            return zivali;
        }

        public int DodajTelitev(int zapTelitev, string potek, string rojstvo, string kakovostMleziva, DateTime datum, int kravaMamaId, int teleId, int bikId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "INSERT INTO telitve (zap_telitev, potek, rojstvo, kakovost_mleziva, datum, krava_mama_id, tele_id, biki_id) " +
                    "VALUES (@ZapTelitev, @Potek, @Rojstvo, @KakovostMleziva, @Datum, @KravaMamaId, @TeleId, @BikId)", conn))
                {
                    command.Parameters.AddWithValue("@ZapTelitev", zapTelitev);
                    command.Parameters.AddWithValue("@Potek", potek);
                    command.Parameters.AddWithValue("@Rojstvo", rojstvo);
                    command.Parameters.AddWithValue("@KakovostMleziva", kakovostMleziva);
                    command.Parameters.AddWithValue("@Datum", datum.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@KravaMamaId", kravaMamaId);
                    command.Parameters.AddWithValue("@TeleId", teleId);
                    command.Parameters.AddWithValue("@BikId", bikId);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public bool PogledObstajaStTelitev(int kravaMamaId, int zapTelitev)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM telitve WHERE krava_mama_id = @KravaMamaId AND zap_telitev = @ZapTelitev", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaMamaId", kravaMamaId);
                    cmd.Parameters.AddWithValue("@ZapTelitev", zapTelitev);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public int PridobiSteviloTelitev(int kravaMamaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT COUNT(*) FROM telitve WHERE krava_mama_id = @KravaMamaId", conn))
                {
                    cmd.Parameters.AddWithValue("@KravaMamaId", kravaMamaId);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public int UrediTelitev(TelitevRazred t)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE telitve SET " +
                    "zap_telitev = @ZapTelitev, " +
                    "potek = @Potek, " +
                    "rojstvo = @Rojstvo, " +
                    "kakovost_mleziva = @KakovostMleziva, " +
                    "datum = @Datum, " +
                    "krava_mama_id = @KravaMamaId, " +
                    "tele_id = @TeleId, " +
                    "biki_id = @BikId " +
                    "WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Id", t.Id);
                    command.Parameters.AddWithValue("@ZapTelitev", t.ZaporednoStevilo);
                    command.Parameters.AddWithValue("@Potek", t.Potek);
                    command.Parameters.AddWithValue("@Rojstvo", t.Rojstvo);
                    command.Parameters.AddWithValue("@KakovostMleziva", t.KakovostMleziva);
                    command.Parameters.AddWithValue("@Datum", t.Datum.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@KravaMamaId", t.KravaMamaId);
                    command.Parameters.AddWithValue("@TeleId", t.TeleId);
                    command.Parameters.AddWithValue("@BikId", t.BikId);

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        // korekcije parkljev
        public List<KorekcijeParkljevRazred> PridobiKorekcijeParkljev(int idKrave)
        {
            List<KorekcijeParkljevRazred> korekcije = new List<KorekcijeParkljevRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT kp.*, " +
                    "z.ime AS krava_ime, z.usesna_stevilka AS krava_usesna, " +
                    "o.ime AS izvajalec_ime, o.priimek AS izvajalec_priimek " +
                    "FROM korekcije_parkljev kp " +
                    "INNER JOIN zivali z ON kp.krava_id = z.id " +
                    "INNER JOIN osebe o ON kp.izvajalec_id = o.id " +
                    "WHERE kp.krava_id = @IdKrave " +
                    "ORDER BY kp.datum DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKrave", idKrave);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string krava = reader["krava_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("krava_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["krava_usesna"].ToString()))
                            {
                                krava += " (" + reader["krava_usesna"].ToString() + ")";
                            }

                            string izvajalec = reader["izvajalec_ime"].ToString() + " " + reader["izvajalec_priimek"].ToString();

                            KorekcijeParkljevRazred korekcija = new KorekcijeParkljevRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("krava_id")),
                                krava,
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader["stanje"].ToString(),
                                reader["pripombe"].ToString(),
                                reader.GetInt32(reader.GetOrdinal("izvajalec_id")),
                                izvajalec
                            );

                            korekcije.Add(korekcija);
                        }
                    }
                }
            }

            return korekcije;
        }

        public KorekcijeParkljevRazred PridobiKorekcijoParkljev(int idKorekcije)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query =
                    "SELECT kp.*, " +
                    "z.ime AS krava_ime, z.usesna_stevilka AS krava_usesna, " +
                    "o.ime AS izvajalec_ime, o.priimek AS izvajalec_priimek " +
                    "FROM korekcije_parkljev kp " +
                    "INNER JOIN zivali z ON kp.krava_id = z.id " +
                    "INNER JOIN osebe o ON kp.izvajalec_id = o.id " +
                    "WHERE kp.id = @IdKorekcije";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdKorekcije", idKorekcije);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string krava = reader["krava_ime"].ToString();

                            if (!reader.IsDBNull(reader.GetOrdinal("krava_usesna")) &&
                                !string.IsNullOrWhiteSpace(reader["krava_usesna"].ToString()))
                            {
                                krava += " (" + reader["krava_usesna"].ToString() + ")";
                            }

                            string izvajalec = reader["izvajalec_ime"].ToString() + " " + reader["izvajalec_priimek"].ToString();

                            KorekcijeParkljevRazred korekcija = new KorekcijeParkljevRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("krava_id")),
                                krava,
                                reader.GetDateTime(reader.GetOrdinal("datum")),
                                reader["stanje"].ToString(),
                                reader["pripombe"].ToString(),
                                reader.GetInt32(reader.GetOrdinal("izvajalec_id")),
                                izvajalec
                            );

                            return korekcija;
                        }
                    }
                }
            }

            return null;
        }

        public int DodajKorekcijoParkljev(DateTime datum, string stanje, string pripombe, int izvajalecId, int kravaId)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "INSERT INTO korekcije_parkljev (datum, stanje, pripombe, izvajalec_id, krava_id) " +
                    "VALUES (@Datum, @Stanje, @Pripombe, @IzvajalecId, @KravaId)", conn))
                {
                    command.Parameters.AddWithValue("@Datum", datum.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Stanje", stanje);

                    if (string.IsNullOrWhiteSpace(pripombe))
                    {
                        command.Parameters.AddWithValue("@Pripombe", "Ni pripomb.");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Pripombe", pripombe);
                    }

                    command.Parameters.AddWithValue("@IzvajalecId", izvajalecId);
                    command.Parameters.AddWithValue("@KravaId", kravaId);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public int UrediKorekcijoParkljev(KorekcijeParkljevRazred k)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE korekcije_parkljev SET " +
                    "datum = @Datum, " +
                    "stanje = @Stanje, " +
                    "pripombe = @Pripombe, " +
                    "izvajalec_id = @IzvajalecId, " +
                    "krava_id = @KravaId " +
                    "WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Id", k.Id);
                    command.Parameters.AddWithValue("@Datum", k.Datum.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Stanje", k.Stanje);
                    command.Parameters.AddWithValue("@Pripombe", k.Pripombe);
                    command.Parameters.AddWithValue("@IzvajalecId", k.IzvajalecId);
                    command.Parameters.AddWithValue("@KravaId", k.KravaId);

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        // zdravila
        public List<ZdravilaRazred> PridobiZdravila()
        {
            List<ZdravilaRazred> zdravila = new List<ZdravilaRazred>();

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT id, zdravilo " +
                    "FROM zdravila " +
                    "ORDER BY zdravilo", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            zdravila.Add(new ZdravilaRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("zdravilo"))
                            ));
                        }
                    }
                }
            }

            return zdravila;
        }

        public ZdravilaRazred PridobiZdravilo(int idZdravila)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(
                    "SELECT id, zdravilo " +
                    "FROM zdravila " +
                    "WHERE id = @IdZdravila", conn))
                {
                    cmd.Parameters.AddWithValue("@IdZdravila", idZdravila);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ZdravilaRazred zdravilo = new ZdravilaRazred(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("zdravilo"))
                            );

                            return zdravilo;
                        }
                    }
                }
            }

            return null;
        }

        public int DodajZdravilo(string zdravilo)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "INSERT INTO zdravila (zdravilo) " +
                    "VALUES (@Zdravilo)", conn))
                {
                    command.Parameters.AddWithValue("@Zdravilo", zdravilo);

                    command.ExecuteNonQuery();
                }

                return 0;
            }
        }

        public int UrediZdravilo(ZdravilaRazred z)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE zdravila SET " +
                    "zdravilo = @Zdravilo " +
                    "WHERE id = @Id", conn))
                {
                    command.Parameters.AddWithValue("@Id", z.Id);
                    command.Parameters.AddWithValue("@Zdravilo", z.Zdravilo);

                    int rezultat = command.ExecuteNonQuery();

                    if (rezultat > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}