namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class PurchaseItemModel
    {
        public int TypeId { get; set; }
        public string Id { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string EquivalentQuantity { get; set; }
        public string QuotaUsed { get; set; }
        public string QuotaLimit { get; set; }
        public string DisplayQuota { get; set; }
        public string MaxLimitMessage { get; set; }
    }
}
