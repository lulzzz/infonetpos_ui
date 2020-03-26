namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class SaleSummaryContract
    {
        public int saleNumber { get; set; }
        public int tillNumber { get; set; }
        public int registerNumber { get; set; }
        public bool isSiteValidated { get; set; }
        public bool isAiteValidated { get; set; }
    }
}