using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class AiteValidateContract
    {
        public string aiteCardNumber;
        public string aiteCardHolderName;
        public string barCode;
        public bool isFrmOverLimit;
        public List<SaleSummaryLineContract> saleSummary;
        public TenderSummaryContract tenderSummary;
        public TendersContract tenders;
    }
}
