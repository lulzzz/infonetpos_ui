using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class AiteValidate
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string BarCode { get; set; }
        public SaleSummary SaleSummary { get; set; }
        public TenderSummary TenderSummary { get; set; }
        public List<Tender> Tenders { get; set; }
        public bool IsOverLimit { get; set; }
    }
}
