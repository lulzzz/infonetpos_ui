namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class SaleListContract
    {
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string totalAmount { get; set; }
        public bool allowCorrection { get; set; }
        public bool allowReason { get; set; }
    }
}