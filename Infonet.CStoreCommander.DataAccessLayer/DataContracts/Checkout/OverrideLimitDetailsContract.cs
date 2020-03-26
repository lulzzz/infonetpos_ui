using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class OverrideLimitDetailsContract
    {
        public string caption { get; set; }
        public List<PurchaseItemContract> purchaseItems { get; set; }
        public List<OverrideCodeContract> overrideCodes { get; set; }
    }

    public class OverrideCodeContract
    {
        public int rowId { get; set; }
        public List<string> codes { get; set; }
    }

    public class PurchaseItemContract
    {
        public int productTypeId { get; set; }
        public string productId { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
        public string equivalentQuantity { get; set; }
        public string quotaUsed { get; set; }
        public string quotaLimit { get; set; }
        public string displayQuota { get; set; }
        public string fuelOverLimitText { get; set; }
    }
}
