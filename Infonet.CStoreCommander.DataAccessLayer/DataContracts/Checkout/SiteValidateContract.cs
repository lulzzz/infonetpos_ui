using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class SiteValidateContract
    {
        public string treatyNumber;
        public string treatyCustomerName;
        public string permitNumber;
        public bool isFrmOverrideLimit;
        public List<SaleSummaryLineContract> saleSummary;
        public TenderSummaryContract tenderSummary;
        public TendersContract tenders;
        public bool isFngtr { get; set; }
        public string fngtrMessage { get; set; }
        public bool requireSignature { get; set; }
    }
}
