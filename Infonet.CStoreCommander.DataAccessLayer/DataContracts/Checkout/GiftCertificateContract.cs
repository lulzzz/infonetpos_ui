namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class GiftCertificateContract
    {
        public string number { get; set; }
        public string soldOn { get; set; }
        public string amount { get; set; }
        public string expiresOn { get; set; }
        public bool isExpired { get; set; }
    }
}
