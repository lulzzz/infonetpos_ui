using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash
{
    public class CompleteCashDrawContract
    {
        public string amount { get; set; }
        public string drawReason { get; set; }
        public int tillNumber { get; set; }
        public short registerNumber { get; set; }
        public List<CurrencyContract> coins { get; set; }
        public List<CurrencyContract> bills { get; set; }
    }
    public class CurrencyContract
    {
        public string currencyName { get; set; }
        public string value { get; set; }
        public int quantity { get; set; }
    }
}