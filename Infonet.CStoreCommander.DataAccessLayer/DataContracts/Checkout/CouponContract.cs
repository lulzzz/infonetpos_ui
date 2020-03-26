namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class CouponContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public string transactionType { get; set; }
        public string couponNumber { get; set; }
        public bool blTillClose { get; set; }
        public string tenderCode { get; set; }
    }
}
