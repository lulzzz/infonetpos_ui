using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class SaleSummary
    {
        public List<SaleSummaryLine> Summary { get; set; }
    }

    public class SaleSummaryLine
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
