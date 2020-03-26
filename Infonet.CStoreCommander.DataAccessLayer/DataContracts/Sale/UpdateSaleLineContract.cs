namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class UpdateSaleLineContract
    {
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public int lineNumber { get; set; }
        public byte registerNumber { get; set; }
        public string discount { get; set; }
        public string discountType { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string reasonCode { get; set; }
        public string reasonType { get; set; }
    }
}
