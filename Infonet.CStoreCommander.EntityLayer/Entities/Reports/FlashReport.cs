using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Reports
{
    public class FlashReport
    {
        public Totals Totals { get; set; }
        public List<Departments> Departments { get; set; }
        public Report Report { get; set; }

        public FlashReport()
        {
            Departments = new List<Departments>();
        }
    }

    public class Totals
    {
        public string ProductSales { get; set; }
        public string LineDiscount { get; set; }
        public string InvoiceDiscount { get; set; }
        public string SalesAfterDiscount { get; set; }
        public string Taxes { get; set; }
        public string Charges { get; set; }
        public string Refunded { get; set; }
        public string TotalsReceipts { get; set; }
    }

    public class Departments
    {
        public string Department { get; set; }
        public string Description { get; set; }
        public string NetSales { get; set; }
    }
}
