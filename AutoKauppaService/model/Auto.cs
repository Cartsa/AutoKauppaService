using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.model
{
    public class Auto
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
}
