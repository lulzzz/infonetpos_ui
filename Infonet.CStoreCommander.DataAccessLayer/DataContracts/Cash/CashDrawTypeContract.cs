using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class CashDrawTypeContract
    {
        public List<CoinsContract> coins { get; set; }
        public List<BillsContract> bills { get; set; }
    }

    public class CoinsContract
    {
        public string currencyName { get; set; }
        public string value { get; set; }
        public string image { get; set; }
    }

    public class BillsContract
    {
        public string currencyName { get; set; }
        public string value { get; set; }
        public string image { get; set; }
    }

}
