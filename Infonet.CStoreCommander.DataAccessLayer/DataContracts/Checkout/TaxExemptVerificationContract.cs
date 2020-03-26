using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class TaxExemptVerificationContract
    {
        public bool processSiteReturn;
        public bool processSiteSale;
        public bool processSiteSaleRemoveTax;
        public bool processAite;
        public bool processQite;
        public Error confirmMessage;
        public string treatyName { get; set; }
        public string treatyNumber { get; set; }
    }
}
