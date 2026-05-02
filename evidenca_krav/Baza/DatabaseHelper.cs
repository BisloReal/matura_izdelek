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
                {
                    return -1;
                }

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
                    "SELECT z.id, z.ime, z.datum_roj, z.pasma, z.ime_mame, z.ime_oceta, z.usesna_stevilka " +
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
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL", conn))
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
                    "SELECT b.id, b.rejec, b.datum_roj, b.izboljsuje, bp.pasma " +
                    "FROM biki_os b " +
                    "INNER JOIN biki_pasme bp ON b.biki_pasma_id = bp.id", conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            biki.Add(new BikiOsRazred(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2),
                                reader.GetString(4),
                                reader.GetString(3)
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
                    "SELECT b.id, b.rejec, b.datum_roj, b.izboljsuje, bp.pasma, bp.id" +
                    "FROM biki_os b " +
                    "INNER JOIN biki_pasme tz bp b.biki_pasma_id = bp.id WHERE b.id = @Id", conn))
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
                                reader.GetInt16(3),
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
                using (var cmd = new SQLiteCommand("SELECT pasma FROM biki_pasme", conn))
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
                    "WHERE tz.tip = @tipZiv AND ok.krava_id IS NULL", conn))
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
                                k.odsvetovaniBiki = reader.GetString(reader.GetOrdinal("odsvetovani_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("primerni_biki")))
                            {
                                k.primerniBiki = reader.GetString(reader.GetOrdinal("primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("najbolj_primerni_biki")))
                            {
                                k.najboljPrimerniBiki = reader.GetString(reader.GetOrdinal("najbolj_primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("teza")))
                            {
                                k.teza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("teza")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena")))
                            {
                                k.iztokMlekaOcena = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("obseg_prsi_cm")))
                            {
                                k.obsegPrsi = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("obseg_prsi_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_cm")))
                            {
                                k.visinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("visina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_cm")))
                            {
                                k.globinaTelesa = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("globina_telesa_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_spredaj_ocena")))
                            {
                                k.sirinaVspredaj = reader.GetInt32(reader.GetOrdinal("sirina_spredaj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("hrbet_ocena")))
                            {
                                k.hrbetOcena = reader.GetInt32(reader.GetOrdinal("hrbet_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_cm")))
                            {
                                k.dolzinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("dolzina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_cm")))
                            {
                                k.sednaSirina = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("sedna_sirina_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("nagib_kriza_ocena")))
                            {
                                k.nagibKrizaOcena = reader.GetInt32(reader.GetOrdinal("nagib_kriza_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_kolka_ocena")))
                            {
                                k.polozajKolkaOcena = reader.GetInt32(reader.GetOrdinal("polozaj_kolka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("skocni_sklep_ocena")))
                            {
                                k.skocniSklepOcena = reader.GetInt32(reader.GetOrdinal("skocni_sklep_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("izraz_skoc_sklepa_ocena")))
                            {
                                k.izrazSkocSklepaOcena = reader.GetInt32(reader.GetOrdinal("izraz_skoc_sklepa_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("bicelj_ocena")))
                            {
                                k.biceljOcena = reader.GetInt32(reader.GetOrdinal("bicelj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("parklji_ocena")))
                            {
                                k.parkljiOcena = reader.GetInt32(reader.GetOrdinal("parklji_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_vimena_ocena")))
                            {
                                k.dolzinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("dolzina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("pripetost_vimena_ocena")))
                            {
                                k.pripetostVimenaOcena = reader.GetInt32(reader.GetOrdinal("pripetost_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_mlecnega_zrcala_ocena")))
                            {
                                k.visinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("visina_mlecnega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_mlenega_zrcala_ocena")))
                            {
                                k.sirinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("sirina_mlenega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_vimena_ocena")))
                            {
                                k.globinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("globina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dno_vimena_ocena")))
                            {
                                k.dnoVimenaOcena = reader.GetInt32(reader.GetOrdinal("dno_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_cent_vezi_ocena")))
                            {
                                k.globinaCentVeziOcena = reader.GetInt32(reader.GetOrdinal("globina_cent_vezi_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_seskov_ocena")))
                            {
                                k.dolzinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("dolzina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("debelina_seskov_ocena")))
                            {
                                k.debelinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("debelina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenost_prednjih_seskov_ocena")))
                            {
                                k.namenostPrednjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenost_prednjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena")))
                            {
                                k.namenostZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena")))
                            {
                                k.polozajZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("omisicanost_ocena")))
                            {
                                k.omisicanostOcena = reader.GetInt32(reader.GetOrdinal("omisicanost_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kondicija_ocena")))
                            {
                                k.kondicijaOcena = reader.GetInt32(reader.GetOrdinal("kondicija_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_izracun_ocena")))
                            {
                                k.visinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("visina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_izracun_ocena")))
                            {
                                k.globinaTelesaIzracunOcena = reader.GetInt32(reader.GetOrdinal("globina_telesa_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_izracun_ocena")))
                            {
                                k.dolzinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("dolzina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_izracun_ocena")))
                            {
                                k.sednaSirinaIzracunOcena = reader.GetInt32(reader.GetOrdinal("sedna_sirina_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("okvir_ocena")))
                            {
                                k.okvirOcena = reader.GetInt32(reader.GetOrdinal("okvir_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kriz_ocena")))
                            {
                                k.krizOcena = reader.GetInt32(reader.GetOrdinal("kriz_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("noge_ocena")))
                            {
                                k.nogeOcena = reader.GetInt32(reader.GetOrdinal("noge_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("vime_ocena")))
                            {
                                k.vimeOcena = reader.GetInt32(reader.GetOrdinal("vime_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena")))
                            {
                                k.telesneSposobnostiSkupajOcena = reader.GetInt32(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena"));
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
                                k.odsvetovaniBiki = reader.GetString(reader.GetOrdinal("odsvetovani_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("primerni_biki")))
                            {
                                k.primerniBiki = reader.GetString(reader.GetOrdinal("primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("najbolj_primerni_biki")))
                            {
                                k.najboljPrimerniBiki = reader.GetString(reader.GetOrdinal("najbolj_primerni_biki"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("teza")))
                            {
                                k.teza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("teza")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("iztok_mleka_ocena")))
                            {
                                k.iztokMlekaOcena = reader.GetInt32(reader.GetOrdinal("iztok_mleka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("obseg_prsi_cm")))
                            {
                                k.obsegPrsi = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("obseg_prsi_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_cm")))
                            {
                                k.visinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("visina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_cm")))
                            {
                                k.globinaTelesa = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("globina_telesa_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_spredaj_ocena")))
                            {
                                k.sirinaVspredaj = reader.GetInt32(reader.GetOrdinal("sirina_spredaj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("hrbet_ocena")))
                            {
                                k.hrbetOcena = reader.GetInt32(reader.GetOrdinal("hrbet_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_cm")))
                            {
                                k.dolzinaKriza = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("dolzina_kriza_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_cm")))
                            {
                                k.sednaSirina = Convert.ToSingle(reader.GetValue(reader.GetOrdinal("sedna_sirina_cm")));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("nagib_kriza_ocena")))
                            {
                                k.nagibKrizaOcena = reader.GetInt32(reader.GetOrdinal("nagib_kriza_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_kolka_ocena")))
                            {
                                k.polozajKolkaOcena = reader.GetInt32(reader.GetOrdinal("polozaj_kolka_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("skocni_sklep_ocena")))
                            {
                                k.skocniSklepOcena = reader.GetInt32(reader.GetOrdinal("skocni_sklep_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("izraz_skoc_sklepa_ocena")))
                            {
                                k.izrazSkocSklepaOcena = reader.GetInt32(reader.GetOrdinal("izraz_skoc_sklepa_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("bicelj_ocena")))
                            {
                                k.biceljOcena = reader.GetInt32(reader.GetOrdinal("bicelj_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("parklji_ocena")))
                            {
                                k.parkljiOcena = reader.GetInt32(reader.GetOrdinal("parklji_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_vimena_ocena")))
                            {
                                k.dolzinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("dolzina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("pripetost_vimena_ocena")))
                            {
                                k.pripetostVimenaOcena = reader.GetInt32(reader.GetOrdinal("pripetost_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_mlecnega_zrcala_ocena")))
                            {
                                k.visinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("visina_mlecnega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sirina_mlenega_zrcala_ocena")))
                            {
                                k.sirinaMlecnegaZrcalaOcena = reader.GetInt32(reader.GetOrdinal("sirina_mlenega_zrcala_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_vimena_ocena")))
                            {
                                k.globinaVimenaOcena = reader.GetInt32(reader.GetOrdinal("globina_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dno_vimena_ocena")))
                            {
                                k.dnoVimenaOcena = reader.GetInt32(reader.GetOrdinal("dno_vimena_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_cent_vezi_ocena")))
                            {
                                k.globinaCentVeziOcena = reader.GetInt32(reader.GetOrdinal("globina_cent_vezi_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_seskov_ocena")))
                            {
                                k.dolzinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("dolzina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("debelina_seskov_ocena")))
                            {
                                k.debelinaSeskovOcena = reader.GetInt32(reader.GetOrdinal("debelina_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenost_prednjih_seskov_ocena")))
                            {
                                k.namenostPrednjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenost_prednjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena")))
                            {
                                k.namenostZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("namenbnost_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena")))
                            {
                                k.polozajZadnjihSeskovOcena = reader.GetInt32(reader.GetOrdinal("polozaj_zadnjih_seskov_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("omisicanost_ocena")))
                            {
                                k.omisicanostOcena = reader.GetInt32(reader.GetOrdinal("omisicanost_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kondicija_ocena")))
                            {
                                k.kondicijaOcena = reader.GetInt32(reader.GetOrdinal("kondicija_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("visina_kriza_izracun_ocena")))
                            {
                                k.visinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("visina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("globina_telesa_izracun_ocena")))
                            {
                                k.globinaTelesaIzracunOcena = reader.GetInt32(reader.GetOrdinal("globina_telesa_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("dolzina_kriza_izracun_ocena")))
                            {
                                k.dolzinaKrizaIzracunOcena = reader.GetInt32(reader.GetOrdinal("dolzina_kriza_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("sedna_sirina_izracun_ocena")))
                            {
                                k.sednaSirinaIzracunOcena = reader.GetInt32(reader.GetOrdinal("sedna_sirina_izracun_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("okvir_ocena")))
                            {
                                k.okvirOcena = reader.GetInt32(reader.GetOrdinal("okvir_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("kriz_ocena")))
                            {
                                k.krizOcena = reader.GetInt32(reader.GetOrdinal("kriz_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("noge_ocena")))
                            {
                                k.nogeOcena = reader.GetInt32(reader.GetOrdinal("noge_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("vime_ocena")))
                            {
                                k.vimeOcena = reader.GetInt32(reader.GetOrdinal("vime_ocena"));
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena")))
                            {
                                k.telesneSposobnostiSkupajOcena = reader.GetInt32(reader.GetOrdinal("telesne_lastnosti_skupaj_ocena"));
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
                    command.Parameters.AddWithValue("@Id", k.id);
                    command.Parameters.AddWithValue("@Ime", k.ime);
                    command.Parameters.AddWithValue("@DatumRojstva", k.datumRoj);
                    command.Parameters.AddWithValue("@Pasma", k.pasma);
                    command.Parameters.AddWithValue("@ImeMame", k.imeMame);
                    command.Parameters.AddWithValue("@ImeOceta", k.imeOceta);
                    command.Parameters.AddWithValue("@UsesnaStevilka", k.usesnaSt);
                    command.Parameters.AddWithValue("@Laktacija", k.laktacija);

                    command.Parameters.AddWithValue("@OdsvetovaniBiki", k.odsvetovaniBiki);
                    command.Parameters.AddWithValue("@PrimerniBiki", k.primerniBiki);
                    command.Parameters.AddWithValue("@NajboljPrimerniBiki", k.najboljPrimerniBiki);
                    command.Parameters.AddWithValue("@Teza", k.teza);
                    command.Parameters.AddWithValue("@IztokMlekaOcena", k.iztokMlekaOcena);
                    command.Parameters.AddWithValue("@ObsegPrsi", k.obsegPrsi);
                    command.Parameters.AddWithValue("@VisinaKriza", k.visinaKriza);
                    command.Parameters.AddWithValue("@GlobinaTelesa", k.globinaTelesa);
                    command.Parameters.AddWithValue("@SirinaVspredaj", k.sirinaVspredaj);
                    command.Parameters.AddWithValue("@HrbetOcena", k.hrbetOcena);
                    command.Parameters.AddWithValue("@DolzinaKriza", k.dolzinaKriza);
                    command.Parameters.AddWithValue("@SednaSirina", k.sednaSirina);
                    command.Parameters.AddWithValue("@NagibKrizaOcena", k.nagibKrizaOcena);
                    command.Parameters.AddWithValue("@PolozajKolkaOcena", k.polozajKolkaOcena);
                    command.Parameters.AddWithValue("@SkocniSklepOcena", k.skocniSklepOcena);
                    command.Parameters.AddWithValue("@IzrazSkocSklepaOcena", k.izrazSkocSklepaOcena);

                    command.Parameters.AddWithValue("@BiceljOcena", k.biceljOcena);
                    command.Parameters.AddWithValue("@ParkljiOcena", k.parkljiOcena);
                    command.Parameters.AddWithValue("@DolzinaVimenaOcena", k.dolzinaVimenaOcena);
                    command.Parameters.AddWithValue("@PripetostVimenaOcena", k.pripetostVimenaOcena);
                    command.Parameters.AddWithValue("@VisinaMlecnegaZrcalaOcena", k.visinaMlecnegaZrcalaOcena);
                    command.Parameters.AddWithValue("@SirinaMlecnegaZrcalaOcena", k.sirinaMlecnegaZrcalaOcena);
                    command.Parameters.AddWithValue("@GlobinaVimenaOcena", k.globinaVimenaOcena);
                    command.Parameters.AddWithValue("@DnoVimenaOcena", k.dnoVimenaOcena);
                    command.Parameters.AddWithValue("@GlobinaCentVeziOcena", k.globinaCentVeziOcena);
                    command.Parameters.AddWithValue("@DolzinaSeskovOcena", k.dolzinaSeskovOcena);
                    command.Parameters.AddWithValue("@DebelinaSeskovOcena", k.debelinaSeskovOcena);
                    command.Parameters.AddWithValue("@NamenostPrednjihSeskovOcena", k.namenostPrednjihSeskovOcena);
                    command.Parameters.AddWithValue("@NamenostZadnjihSeskovOcena", k.namenostZadnjihSeskovOcena);
                    command.Parameters.AddWithValue("@PolozajZadnjihSeskovOcena", k.polozajZadnjihSeskovOcena);
                    command.Parameters.AddWithValue("@OmisicanostOcena", k.omisicanostOcena);
                    command.Parameters.AddWithValue("@KondicijaOcena", k.kondicijaOcena);

                    command.Parameters.AddWithValue("@VisinaKrizaIzracunOcena", k.visinaKrizaIzracunOcena);
                    command.Parameters.AddWithValue("@GlobinaTelesaIzracunOcena", k.globinaTelesaIzracunOcena);
                    command.Parameters.AddWithValue("@DolzinaKrizaIzracunOcena", k.dolzinaKrizaIzracunOcena);
                    command.Parameters.AddWithValue("@SednaSirinaIzracunOcena", k.sednaSirinaIzracunOcena);
                    command.Parameters.AddWithValue("@OkvirOcena", k.okvirOcena);
                    command.Parameters.AddWithValue("@KrizOcena", k.krizOcena);
                    command.Parameters.AddWithValue("@NogeOcena", k.nogeOcena);
                    command.Parameters.AddWithValue("@VimeOcena", k.vimeOcena);
                    command.Parameters.AddWithValue("@TelesneSposobnostiSkupajOcena", k.telesneSposobnostiSkupajOcena);

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