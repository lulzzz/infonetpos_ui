using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class OverLimitDetails
    {
        public bool IsGasReasons { get; set; }
        public bool IsTobaccoReasons { get; set; }
        public bool IsPropaneReasons { get; set; }
        public List<ExplanationReason> GasReasons { get; set; }
        public List<ExplanationReason> TobaccoReasons { get; set; }
        public List<ExplanationReason> PropaneReasons { get; set; }
        public List<TaxExemptSaleLine> TaxExemptSale { get; set; }
    }

    public class ExplanationReason
    {
        public int ExplanationCode { get; set; }
        public string Reason { get; set; }
    }

    public class TaxExemptSaleLine
    {
        public string Type { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string RegularPrice { get; set; }
        public string TaxFreePrice { get; set; }
        public string ExemptedTax { get; set; }
        public string QuotaUsed { get; set; }
        public string QuotaLimit { get; set; }
    }
}
