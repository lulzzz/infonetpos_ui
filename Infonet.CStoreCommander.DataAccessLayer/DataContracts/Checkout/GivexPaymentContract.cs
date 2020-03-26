namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class GivexPaymentContract
    {
        public string givexCardNumber { get; set; }
        public string amount { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public string transactionType { get; set; }
        public string tenderCode { get; set; }
    }
}
