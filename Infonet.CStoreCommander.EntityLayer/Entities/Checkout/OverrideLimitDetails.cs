using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class OverrideLimitDetails
    {
        public string Caption { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        public List<OverrideCode> OverrideCodes { get; set; }
    }

    public class OverrideCode
    {
        public int RowId { get; set; }
        public List<string> Codes { get; set; }
    }

    public class PurchaseItem
    {
        public int ProductTypeId { get; set; }
        public string ProductId { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string EquivalentQuantity { get; set; }
        public string QuotaUsed { get; set; }
        public string QuotaLimit { get; set; }
        public string DisplayQuota { get; set; }
        public string FuelOverLimitText { get; set; }
    }
}
