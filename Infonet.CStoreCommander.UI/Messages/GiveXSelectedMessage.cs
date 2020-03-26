namespace Infonet.CStoreCommander.UI.Messages
{
    public class GiveXSelectedMessage
    {
        public decimal? Amount { get; set; }
        public string TenderCode { get; set; }
        public string TransactionType { get; set; }
        public string OutStandingAmount { get; set; }
        public string CardNumber { get; set; }
    }
}
