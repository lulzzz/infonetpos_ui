using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Cash
{
    public class CashDrawTypes
    {
        public List<Coins> Coins { get; set; }
        public List<Bills> Bills { get; set; }     
    }

    public class Coins
    {
        public string CurrencyName { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
    }

    public class Bills
    {
        public string CurrencyName { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
    }

}
