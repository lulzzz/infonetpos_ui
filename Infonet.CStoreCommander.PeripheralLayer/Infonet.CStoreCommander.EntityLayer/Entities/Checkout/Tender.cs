namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class Tender
    {
        public string TenderCode { get; set; }
        public string TenderClass { get; set; }
        public string TenderName { get; set; }
        public string AmountEntered { get; set; }
        public string AmountValue { get; set; }
        public bool IsEnabled { get; set; }
        public decimal MaximumValue { get; set; }
        public decimal MinimumValue { get; set; }
        public string ImageSource { get; set; }
    }
}
