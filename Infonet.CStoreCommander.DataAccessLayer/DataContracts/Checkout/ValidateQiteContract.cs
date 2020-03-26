using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Cash;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class ValidateQiteContract
    {
        public string bandMemberName;
        public string bandMember;
        public List<SaleSummaryLineContract> saleSummary;
        public TenderSummaryContract tenderSummary;
        public TendersContract tenders;
    }
}
