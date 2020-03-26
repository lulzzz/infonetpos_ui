namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class GiftCertificate
    {
        public string Number { get; set; }
        public string SoldOn { get; set; }
        public decimal Amount { get; set; }
        public string ExpiresOn { get; set; }
        public bool IsExpired { get; set; }
    }
}
