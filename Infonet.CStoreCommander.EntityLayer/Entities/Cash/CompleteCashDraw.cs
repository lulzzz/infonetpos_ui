using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Cash
{
    public class CompleteCashDraw
    {
        public decimal Amount { get; set; }
        public string DrawReason { get; set; }
        public int TillNumber { get; set; }
        public byte RegisterNumber { get; set; }
        public List<Currency> Coins { get; set; }
        public List<Currency> Bills { get; set; }
    }

    public class Currency
    {
        public string CurrencyName { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
