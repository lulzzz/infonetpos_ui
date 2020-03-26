namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class AddStockToSaleContract
    {
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public int registerNumber { get; set; }
        public string stockCode { get; set; }
        public string quantity { get; set; }
        public bool isReturnMode { get; set; }
        public bool isManuallyAdded { get; set; }
        public GiftCardContract giftCard { get; set; }
    }

    public class GiftCardContract
    {
        public string cardNumber { get; set; }
        public int giftNumber { get; set; }
        public string price { get; set; }
    }
}
