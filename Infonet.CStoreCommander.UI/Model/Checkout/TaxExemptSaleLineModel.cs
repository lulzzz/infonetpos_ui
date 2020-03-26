namespace Infonet.CStoreCommander.UI.Model.Checkout
{
    public class TaxExemptSaleLineModel
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
