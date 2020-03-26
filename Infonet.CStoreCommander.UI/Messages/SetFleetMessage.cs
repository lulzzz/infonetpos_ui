namespace Infonet.CStoreCommander.UI.Messages
{
    public class SetFleetMessage
    {
        public string TenderCode { get; set; }
        public string Amount { get; set; }
        public string OutStandingAmount { get; set; }
        public string TransactionType { get; set; }
        public string CardNumber { get; set; }
        public bool PerformAptTender { get; internal set; }
        public bool IsGasKing { get; set; }
        public string KickBackValue { get; set; }
        public double KickbackPoints { get; set; }
        public bool IsKickBackLinked { get; set; }
        public bool IsFleet { get; set; }
        public bool IsInvalidLoyaltyCard { get; set; }
    }
}
