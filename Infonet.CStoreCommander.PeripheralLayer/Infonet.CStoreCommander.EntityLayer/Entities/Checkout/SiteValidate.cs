using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class SiteValidate
    {
        public string TreatyNumber { get; set; }
        public string TreatyCustomerName { get; set; }
        public string PermitNumber { get; set; }
        public bool IsFrmOverrideLimit { get; set; }
        public bool IsFngtr { get; set; }
        public string FngtrMessage { get; set; }
        public SaleSummary SaleSummary { get; set; }
        public TenderSummary TenderSummary { get; set; }
        public List<Tender> Tenders { get; set; }
        public bool RequireSignature { get; set; }
    }
}
