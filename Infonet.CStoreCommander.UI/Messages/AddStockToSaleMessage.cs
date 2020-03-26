namespace Infonet.CStoreCommander.UI.Messages
{
    public class AddStockToSaleMessage
    {
        public string RegularPrice { get; set; }

        public string Description { get; set; }

        public string AlternateCode { get; set; }

        public string StockCode { get; set; }

        public float Quantity { get; set; }

        public bool IsManuallyAdded { get; set; }
    }
}
