namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class CheckoutSummary
    {
        public SaleSummary SaleSummary { get; set; }
        public TenderSummary TenderSummary { get; set; }

        public bool IsDeletePrepay { get; set; }

        public CheckoutSummary()
        {
            SaleSummary = new SaleSummary();
            TenderSummary = new TenderSummary();
        }
    }
}
