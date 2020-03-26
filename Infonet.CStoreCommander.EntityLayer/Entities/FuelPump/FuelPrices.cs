using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.FuelPump
{
    public class FuelPrices
    {
        public List<Price> Prices { get; set; }
        public Report Report { get; set; }
        public bool IsTaxExemptionVisible { get; set; }
        public bool IsReadTotalizerEnabled { get; set; }
        public bool IsReadTotalizerChecked { get; set; }
        public bool IsPricesToDisplayEnabled { get; set; }
        public bool IsPricesToDisplayChecked { get; set; }
        public bool IsReadTankDipEnabled { get; set; }
        public bool IsReadTankDipChecked { get; set; }
        public bool CanReadTotalizer { get; set; }
        public bool CanSelectPricesToDisplay { get; set; }
        public bool IsExitEnabled { get; set; }
        public bool IsErrorEnabled { get; set; }
        public string Caption { get; set; }
        public bool IsCashPriceEnabled { get; set; }
        public bool IsCreditPriceEnabled { get; set; }
        public bool IsTaxExemptedCashPriceEnabled { get; set; }
        public bool IsTaxExemptedCreditPriceEnabled { get; set; }
        public bool IsIncrementEnabled { get; set; }
        public bool IsGrouped { get; set; }
    }

    public class Price
    {
        public int Row { get; set; }

        public string Grade { get; set; }

        public short GradeId { get; set; }

        public string Tier { get; set; }

        public short TierId { get; set; }

        public string Level { get; set; }

        public short LevelId { get; set; }

        public string CashPrice { get; set; }

        public string CreditPrice { get; set; }

        public string TaxExemptedCashPrice { get; set; }

        public string TaxExemptedCreditPrice { get; set; }
    }

    public class BasePriceWithReport
    {
        public Price Price { get; set; }
        public Report Report { get; set; }
    }
}
