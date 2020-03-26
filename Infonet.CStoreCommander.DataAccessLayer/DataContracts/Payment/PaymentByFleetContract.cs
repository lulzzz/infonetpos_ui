namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment
{
    public class PaymentByFleetContract
    {
        public string cardNumber { get; set; }
        public string amount { get; set; }
        public bool isSwiped { get; set; }
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
    }
}