using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;

namespace AutoKauppaService
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        bool connectDatabase();
        [OperationContract]
        bool saveAutoIntoDatabase(AutoTesti newAuto);
        [OperationContract]
        List<AutonMerkki> getAllAutoMakersFromDatabase();
        [OperationContract]
        List<AutonMalli> getAutoModelsByMakerId(int makerId);
        [OperationContract]
        List<Polttoaine> getCarFuel();
        [OperationContract]
        List<Vari> getColor();
        [OperationContract]
        AutoTesti Eka();
        [OperationContract]
        AutoTesti Seuraava(int NykyinenID);
        [OperationContract]
        AutoTesti Edellinen(int NykyinenID);
        [OperationContract]
        AutoTesti Vika();
        // TODO: Add your service operations here
    }
    [DataContract]
    public class AutonMalli
    {
        int ID;
        string Auton_mallin_nimi;
        int AutonMerkkiID;
        [DataMember]
        public int ID1 { get => ID; set => ID = value; }
        [DataMember]
        public string Auton_mallin_nimi1 { get => Auton_mallin_nimi; set => Auton_mallin_nimi = value; }
        [DataMember]
        public int AutonMerkkiID1 { get => AutonMerkkiID; set => AutonMerkkiID = value; }
    }
    public class AutoTesti
    {

        int ID;
        decimal Hinta;
        DateTime Rekisteri_paivamaara;
        decimal Moottorin_tilavuus;
        int Mittarilukema;
        int AutonMerkkiID;
        int AutonMalliID;
        int VaritID;
        int PolttoaineID;
        public int ID1 { get => ID; set => ID = value; }
        public decimal Hinta1 { get => Hinta; set => Hinta = value; }
        public DateTime Rekisteri_paivamaara1 { get => Rekisteri_paivamaara; set => Rekisteri_paivamaara = value; }
        public decimal Moottorin_tilavuus1 { get => Moottorin_tilavuus; set => Moottorin_tilavuus = value; }
        public int Mittarilukema1 { get => Mittarilukema; set => Mittarilukema = value; }
        public int AutonMerkkiID1 { get => AutonMerkkiID; set => AutonMerkkiID = value; }
        public int AutonMalliID1 { get => AutonMalliID; set => AutonMalliID = value; }
        public int VaritID1 { get => VaritID; set => VaritID = value; }
        public int PolttoaineID1 { get => PolttoaineID; set => PolttoaineID = value; }
    }
    public class Polttoaine
    {
        int ID;
        string Polttoaineen_nimi;
        public int ID1 { get => ID; set => ID = value; }
        public string Polttoaineen_nimi1 { get => Polttoaineen_nimi; set => Polttoaineen_nimi = value; }
    }
    public class Vari
    {
        int ID;
        string AutonVari;
        public int ID1 { get => ID; set => ID = value; }
        public string AutonVari1 { get => AutonVari; set => AutonVari = value; }
    }
    public class AutonMerkki
    {
        int ID;
        string Merkki;
        public int ID1 { get => ID; set => ID = value; }
        public string Merkki1 { get => Merkki; set => Merkki = value; }
    }

}
