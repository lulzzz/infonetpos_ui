using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class QiteValidate
    {
        public string BandMemberName { get; set; }
        public string BandMember { get; set; }
        public SaleSummary SaleSummary { get; set; }
        public TenderSummary TenderSummary { get; set; }
        public List<Tender> Tenders { get; set; }
    }
}
