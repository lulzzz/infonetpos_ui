namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class PaymentByAccountContract
    {
        public string purchaseOrder { get; set; }
        public bool overrideArLimit { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string transactionType { get; set; }
        public bool tillClose { get; set; }
        public PaymentByAccountTender tender { get; set; }
    }

    public class PaymentByAccountTender
    {
        public string tenderCode { get; set; }
        public string amountEntered { get; set; }
    }
}