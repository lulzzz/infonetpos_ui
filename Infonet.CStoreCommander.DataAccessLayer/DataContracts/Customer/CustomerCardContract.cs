namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer
{
    public class CustomerCardContract
    {
        public string cardNumber { get; set; }
        public bool isLoyaltycard { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
    }
}
