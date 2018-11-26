using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace AutoKauppaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutoKauppa;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection myConnection;
        public bool connectDatabase()
        {
            myConnection = new SqlConnection(connectionstring);
            try
            {
                myConnection.Open();
                myConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Virheilmoitukset:" + e);
                myConnection.Close();
                return false;
            }
        }
        public bool saveAutoIntoDatabase(AutoTesti newAuto)
        {
            try
            {
                myConnection = new SqlConnection(connectionstring);
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Auto (Hinta, Rekisteri_paivamaara, Moottorin_tilavuus, Mittarilukema, AutonMerkkiID, AutonMalliID, VaritID, PolttoaineID)" +
                    " VALUES(" + newAuto.Hinta1 + "," + "'" + String.Format("{0:M/d/yyyy}", newAuto.Rekisteri_paivamaara1) + "'" + "," + newAuto.Moottorin_tilavuus1 + "," + newAuto.Mittarilukema1 + "," + newAuto.AutonMerkkiID1 + "," + newAuto.AutonMalliID1 + "," + newAuto.VaritID1 + "," + newAuto.PolttoaineID1 + ")", myConnection);
                command.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
                return false;
            }
        }
        public List<AutonMerkki> getAllAutoMakersFromDatabase()
        {
            myConnection = new SqlConnection(connectionstring);
            AutonMerkki merkki;
            List<AutonMerkki> merkit = new List<AutonMerkki>();
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM AutonMerkki", myConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i = i + 2)
                {
                    merkki = new AutonMerkki();
                    merkki.ID1 = Convert.ToInt32(reader.GetValue(0));
                    merkki.Merkki1 = Convert.ToString(reader.GetValue(1));
                    merkit.Add(merkki);
                }
            }
            myConnection.Close();
            return merkit;
        }
        public List<AutonMalli> getAutoModelsByMakerId(int makerId)
        {
            myConnection = new SqlConnection(connectionstring);
            AutonMalli Malli;
            List<AutonMalli> Mallit = new List<AutonMalli>();
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM AutonMallit WHERE AutonMerkkiID = @makerid", myConnection);
            command.Parameters.AddWithValue("@makerid", makerId);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i = i + 3)
                {
                    Malli = new AutonMalli();
                    Malli.ID1 = Convert.ToInt32(reader.GetValue(0));
                    Malli.Auton_mallin_nimi1 = Convert.ToString(reader.GetValue(1));
                    Malli.AutonMerkkiID1 = Convert.ToInt32(reader.GetValue(2));
                    Mallit.Add(Malli);
                }
            }
            myConnection.Close();
            return Mallit;
        }
        public List<Polttoaine> getCarFuel()
        {
            myConnection = new SqlConnection(connectionstring);
            Polttoaine Polttoaine;
            List<Polttoaine> Polttoaineet = new List<Polttoaine>();
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Polttoaine", myConnection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i = i + 2)
                    {
                        Polttoaine = new Polttoaine();
                        Polttoaine.ID1 = Convert.ToInt32(reader.GetValue(0));
                        Polttoaine.Polttoaineen_nimi1 = Convert.ToString(reader.GetValue(1));
                        Polttoaineet.Add(Polttoaine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            myConnection.Close();
            return Polttoaineet;
        }
        public List<Vari> getColor()
        {
            myConnection = new SqlConnection(connectionstring);
            Vari Vari;
            List<Vari> Varit = new List<Vari>();
            myConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Varit", myConnection);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i = i + 2)
                    {
                        Vari = new Vari();
                        Vari.ID1 = Convert.ToInt32(reader.GetValue(0));
                        Vari.AutonVari1 = Convert.ToString(reader.GetValue(1));
                        Varit.Add(Vari);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            myConnection.Close();
            return Varit;
        }
        public AutoTesti Eka()
        {
            myConnection = new SqlConnection(connectionstring);
            AutoTesti haettuAuto = new AutoTesti();
            try
            {
                SqlCommand Eka = new SqlCommand("SELECT  top 1 * FROM Auto WHERE ID = (SELECT MIN(ID) FROM Auto) " + "ORDER BY ID ASC", myConnection);
                myConnection.Open();
                SqlDataReader EkaLukija;
                EkaLukija = Eka.ExecuteReader();
                while (EkaLukija.Read())
                {
                    int id;
                    decimal hinta;
                    DateTime aika;
                    decimal tilavuus;
                    int lukema;
                    int merkki;
                    int malli;
                    int vari;
                    int polttoaine;

                    if (int.TryParse(EkaLukija["ID"].ToString(), out id))
                    {
                        haettuAuto.ID1 = id;
                    }
                    if (decimal.TryParse(EkaLukija["Hinta"].ToString(), out hinta))
                    {
                        haettuAuto.Hinta1 = hinta;
                    }
                    if (DateTime.TryParse(EkaLukija["Rekisteri_paivamaara"].ToString(), out aika))
                    {
                        haettuAuto.Rekisteri_paivamaara1 = aika;
                    }
                    if (decimal.TryParse(EkaLukija["Moottorin_tilavuus"].ToString(), out tilavuus))
                    {
                        haettuAuto.Moottorin_tilavuus1 = tilavuus;
                    }
                    if (int.TryParse(EkaLukija["Mittarilukema"].ToString(), out lukema))
                    {
                        haettuAuto.Mittarilukema1 = lukema;
                    }
                    if (int.TryParse(EkaLukija["AutonMerkkiID"].ToString(), out merkki))
                    {
                        haettuAuto.AutonMerkkiID1 = merkki;
                    }
                    if (int.TryParse(EkaLukija["AutonMalliID"].ToString(), out malli))
                    {
                        haettuAuto.AutonMalliID1 = malli;
                    }
                    if (int.TryParse(EkaLukija["VaritID"].ToString(), out vari))
                    {
                        haettuAuto.VaritID1 = vari;
                    }
                    if (int.TryParse(EkaLukija["PolttoaineID"].ToString(), out polttoaine))
                    {
                        haettuAuto.PolttoaineID1 = polttoaine;
                    }
                }
                myConnection.Close();
                return haettuAuto;
            }
            catch (Exception virhe)
            {
                Console.WriteLine("Virheilmoitus: " + virhe);
                return null;
            }
        }
        public AutoTesti Seuraava(int NykyinenID)
        {
            myConnection = new SqlConnection(connectionstring);
            AutoTesti haettuAuto = new AutoTesti();
            try
            {
                SqlCommand Seuraava = new SqlCommand("SELECT  top 1 * FROM Auto WHERE ID > @NykyinenID " + "ORDER BY ID ASC", myConnection);
                Seuraava.Parameters.AddWithValue("@NykyinenID", NykyinenID);
                myConnection.Open();
                SqlDataReader SeuraavaLukija;
                SeuraavaLukija = Seuraava.ExecuteReader();
                while (SeuraavaLukija.Read())
                {
                    int id;
                    decimal hinta;
                    DateTime aika;
                    decimal tilavuus;
                    int lukema;
                    int merkki;
                    int malli;
                    int vari;
                    int polttoaine;

                    if (int.TryParse(SeuraavaLukija["ID"].ToString(), out id))
                    {
                        haettuAuto.ID1 = id;
                    }
                    if (decimal.TryParse(SeuraavaLukija["Hinta"].ToString(), out hinta))
                    {
                        haettuAuto.Hinta1 = hinta;
                    }
                    if (DateTime.TryParse(SeuraavaLukija["Rekisteri_paivamaara"].ToString(), out aika))
                    {
                        haettuAuto.Rekisteri_paivamaara1 = aika;
                    }
                    if (decimal.TryParse(SeuraavaLukija["Moottorin_tilavuus"].ToString(), out tilavuus))
                    {
                        haettuAuto.Moottorin_tilavuus1 = tilavuus;
                    }
                    if (int.TryParse(SeuraavaLukija["Mittarilukema"].ToString(), out lukema))
                    {
                        haettuAuto.Mittarilukema1 = lukema;
                    }
                    if (int.TryParse(SeuraavaLukija["AutonMerkkiID"].ToString(), out merkki))
                    {
                        haettuAuto.AutonMerkkiID1 = merkki;
                    }
                    if (int.TryParse(SeuraavaLukija["AutonMalliID"].ToString(), out malli))
                    {
                        haettuAuto.AutonMalliID1 = malli;
                    }
                    if (int.TryParse(SeuraavaLukija["VaritID"].ToString(), out vari))
                    {
                        haettuAuto.VaritID1 = vari;
                    }
                    if (int.TryParse(SeuraavaLukija["PolttoaineID"].ToString(), out polttoaine))
                    {
                        haettuAuto.PolttoaineID1 = polttoaine;
                    }
                }
                myConnection.Close();
                return haettuAuto;
            }
            catch (Exception virhe)
            {
                Console.WriteLine("Virheilmoitus: " + virhe);
                return null;
            }
        }
        public AutoTesti Edellinen(int NykyinenID)
        {
            myConnection = new SqlConnection(connectionstring);
            AutoTesti haettuAuto = new AutoTesti();
            try
            {
                SqlCommand Seuraava = new SqlCommand("SELECT  top 1 * FROM Auto WHERE ID < @NykyinenID " + "ORDER BY ID DESC", myConnection);
                Seuraava.Parameters.AddWithValue("@NykyinenID", NykyinenID);
                myConnection.Open();
                SqlDataReader EkaLukija;
                EkaLukija = Seuraava.ExecuteReader();
                while (EkaLukija.Read())
                {
                    int id;
                    decimal hinta;
                    DateTime aika;
                    decimal tilavuus;
                    int lukema;
                    int merkki;
                    int malli;
                    int vari;
                    int polttoaine;

                    if (int.TryParse(EkaLukija["ID"].ToString(), out id))
                    {
                        haettuAuto.ID1 = id;
                    }
                    if (decimal.TryParse(EkaLukija["Hinta"].ToString(), out hinta))
                    {
                        haettuAuto.Hinta1 = hinta;
                    }
                    if (DateTime.TryParse(EkaLukija["Rekisteri_paivamaara"].ToString(), out aika))
                    {
                        haettuAuto.Rekisteri_paivamaara1 = aika;
                    }
                    if (decimal.TryParse(EkaLukija["Moottorin_tilavuus"].ToString(), out tilavuus))
                    {
                        haettuAuto.Moottorin_tilavuus1 = tilavuus;
                    }
                    if (int.TryParse(EkaLukija["Mittarilukema"].ToString(), out lukema))
                    {
                        haettuAuto.Mittarilukema1 = lukema;
                    }
                    if (int.TryParse(EkaLukija["AutonMerkkiID"].ToString(), out merkki))
                    {
                        haettuAuto.AutonMerkkiID1 = merkki;
                    }
                    if (int.TryParse(EkaLukija["AutonMalliID"].ToString(), out malli))
                    {
                        haettuAuto.AutonMalliID1 = malli;
                    }
                    if (int.TryParse(EkaLukija["VaritID"].ToString(), out vari))
                    {
                        haettuAuto.VaritID1 = vari;
                    }
                    if (int.TryParse(EkaLukija["PolttoaineID"].ToString(), out polttoaine))
                    {
                        haettuAuto.PolttoaineID1 = polttoaine;
                    }
                }
                myConnection.Close();
                return haettuAuto;
            }
            catch (Exception virhe)
            {
                Console.WriteLine("Virheilmoitus: " + virhe);
                return null;
            }
        }
        public AutoTesti Vika()
        {
            myConnection = new SqlConnection(connectionstring);
            AutoTesti haettuAuto = new AutoTesti();
            try
            {
                SqlCommand Seuraava = new SqlCommand("SELECT  top 1 * FROM Auto WHERE ID = (SELECT MAX(ID) FROM Auto) " + "ORDER BY ID ASC", myConnection);
                myConnection.Open();
                SqlDataReader EkaLukija;
                EkaLukija = Seuraava.ExecuteReader();
                while (EkaLukija.Read())
                {
                    int id;
                    decimal hinta;
                    DateTime aika;
                    decimal tilavuus;
                    int lukema;
                    int merkki;
                    int malli;
                    int vari;
                    int polttoaine;

                    if (int.TryParse(EkaLukija["ID"].ToString(), out id))
                    {
                        haettuAuto.ID1 = id;
                    }
                    if (decimal.TryParse(EkaLukija["Hinta"].ToString(), out hinta))
                    {
                        haettuAuto.Hinta1 = hinta;
                    }
                    if (DateTime.TryParse(EkaLukija["Rekisteri_paivamaara"].ToString(), out aika))
                    {
                        haettuAuto.Rekisteri_paivamaara1 = aika;
                    }
                    if (decimal.TryParse(EkaLukija["Moottorin_tilavuus"].ToString(), out tilavuus))
                    {
                        haettuAuto.Moottorin_tilavuus1 = tilavuus;
                    }
                    if (int.TryParse(EkaLukija["Mittarilukema"].ToString(), out lukema))
                    {
                        haettuAuto.Mittarilukema1 = lukema;
                    }
                    if (int.TryParse(EkaLukija["AutonMerkkiID"].ToString(), out merkki))
                    {
                        haettuAuto.AutonMerkkiID1 = merkki;
                    }
                    if (int.TryParse(EkaLukija["AutonMalliID"].ToString(), out malli))
                    {
                        haettuAuto.AutonMalliID1 = malli;
                    }
                    if (int.TryParse(EkaLukija["VaritID"].ToString(), out vari))
                    {
                        haettuAuto.VaritID1 = vari;
                    }
                    if (int.TryParse(EkaLukija["PolttoaineID"].ToString(), out polttoaine))
                    {
                        haettuAuto.PolttoaineID1 = polttoaine;
                    }
                }
                myConnection.Close();
                return haettuAuto;
            }
            catch (Exception virhe)
            {
                Console.WriteLine("Virheilmoitus: " + virhe);
                return null;
            }
        }
    }
}
