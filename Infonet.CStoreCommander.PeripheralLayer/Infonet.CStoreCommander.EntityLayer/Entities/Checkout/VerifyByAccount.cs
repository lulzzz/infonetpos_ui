using Infonet.CStoreCommander.EntityLayer.Entities.Common;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class VerifyByAccount
    {
        public bool IsPurchaseOrderRequired { get; set; }
        public Error CreditMessage { get; set; }
        public Error OverrideArLimitMessage { get; set; }
        public Error UnauthorizedMessage { get; set; }
        public bool IsMutiliPO { get; set; }
        public CardSwipeInformation CardSummary { get; set; }
    }
}
