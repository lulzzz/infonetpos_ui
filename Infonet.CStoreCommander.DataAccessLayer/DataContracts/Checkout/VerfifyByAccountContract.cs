namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class VerfifyByAccountContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string transactionType { get; set; }
        public bool tillClose { get; set; }
        public AccountTenderContract tender { get; set; }

    }

    public class AccountTenderContract
    {
        public string tenderCode { get; set; }
        public string amountEntered { get; set; }
    }
}