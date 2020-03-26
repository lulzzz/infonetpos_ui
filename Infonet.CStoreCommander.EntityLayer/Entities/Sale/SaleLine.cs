namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class SaleLine
    {
        public int LineNumber { get; set; }
        public string StockCode { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string DiscountType { get; set; }
        public string DiscountRate { get; set; }
        public string Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public bool AllowPriceChange { get; set; }
        public bool AllowQuantityChange { get; set; }
        public bool AllowDiscountChange { get; set; }
        public bool AllowDiscountReason { get; set; }
        public bool AllowPriceReason { get; set; }
        public bool AllowReturnReason { get; set; }
        //Tony 03/19/2019
        public string Dept { get; set; }
        //Tony End
    }
}
