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
                    "SELECT  b.id, b.rejec, b.datum_roj, bp.id, bp.pasma, b.izboljsuje " +
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
                                reader.GetDateTime(2),
                                reader.GetInt32(3),
                                reader.GetString(4),
                                reader.GetString(5)
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
                    "SELECT b.id, b.rejec, b.datum_roj, bp.id, bp.pasma, b.izboljsuje " +
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
                                reader.GetDateTime(2),
                                reader.GetInt32(3),
                                reader.GetString(4),
                                reader.GetString(5)
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

        public void DodajBikaOs(string rejec, string datumRojstva, int pasmaBikId, string izboljsuje)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var command = new SQLiteCommand(
                    "INSERT INTO biki_os (rejec, datum_roj, biki_pasma_id, izboljsuje) " +
                    "VALUES (@Rejec, @DatumRojstva, @PasmaBikId, @Izboljsuje)", conn))
                {
                    command.Parameters.AddWithValue("@Rejec", rejec);
                    command.Parameters.AddWithValue("@DatumRojstva", datumRojstva);
                    command.Parameters.AddWithValue("@PasmaBikId", pasmaBikId);
                    command.Parameters.AddWithValue("@Izboljsuje", izboljsuje);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int UrediBikaOs(string rejec, string datumRojstva, int pasmaBikId, string izboljsuje, int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (var command = new SQLiteCommand(
                    "UPDATE biki_os SET rejec = @Rejec, datum_roj = @DatumRojstva, biki_pasma_id = @PasmaBikId, izboljsuje = @Izboljsuje WHERE id = @Id",
                    conn))
                {
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
                        iztok_mleka_ocena = @IztokMlekaOcena,
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
                        telesne_lastnosti_skupaj_ocena = @TelesneSposobnostiSkupajOcena

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
                    command.Parameters.AddWithValue("@IztokMlekaOcena", k.IztokMlekaOcena);
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

        public List<string> PridobiKontrolerje()
        {
            List<string> kontrolerji = new List<string>();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT o.ime, o.priimek FROM osebe o INNER JOIN zadolzitve_oseb zo ON o.zadolzitev_id = zo.id WHERE zo.zadolzitev = 'Kontrolor' ORDER BY o.ime, o.priimek", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kontrolerji.Add(reader.GetString(0) + " " + reader.GetString(1));
                        }
                    }
                }
            }
            return kontrolerji;
        }

        public int PridobiIdKontrolerjaPrekoImena(string ime, string priimek)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "SELECT o.id FROM osebe o INNER JOIN zadolzitve_oseb zo ON o.zadolzitev_id = zo.id WHERE zo.zadolzitev = 'Kontrolor' AND o.ime = @Ime AND o.priimek = @Priimek", conn))
                {
                    cmd.Parameters.AddWithValue("@Ime", ime);
                    cmd.Parameters.AddWithValue("@Priimek", priimek);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1;
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

    }
}