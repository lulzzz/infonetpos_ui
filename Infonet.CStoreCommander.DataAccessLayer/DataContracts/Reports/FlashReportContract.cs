using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports
{
    public class FlashReportContract
    {
        public TotalsContract totals { get; set; }
        public List<DepartmentsContract> departments { get; set; }
        public ReportContract report { get; set; }
    }

    public class TotalsContract
    {
        public string productSales { get; set; }
        public string lineDiscount { get; set; }
        public string invoiceDiscount { get; set; }
        public string salesAfterDiscount { get; set; }
        public string taxes { get; set; }
        public string charges { get; set; }
        public string refunded { get; set; }
        public string totalsReceipts { get; set; }
    }

    public class DepartmentsContract
    {
        public string department { get; set; }
        public string description { get; set; }
        public string netSales { get; set; }
    }
}

