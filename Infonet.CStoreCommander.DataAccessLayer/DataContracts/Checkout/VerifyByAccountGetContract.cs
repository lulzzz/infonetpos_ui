using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class VerifyByAccountGetContract
    {
        public bool isPurchaseOrderRequired { get; set; }

        public Error creditMessage { get; set; }

        public Error overrideArLimitMessage { get; set; }

        public Error unauthorizedMessage { get; set; }

        public bool isMutiliPO { get; set; }

        public TenderInformationContract cardSummary { get; set; }
    }
}