namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class StoreCreditContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string transactionType { get; set; }
        public bool tillClose { get; set; }
        public StoreCreditTenderContract tender { get; set; } 

    }

    public class StoreCreditTenderContract
    {
        public string tenderCode { get; set; }
        public string amountEntered { get; set; }
    }
}