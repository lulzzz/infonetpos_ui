using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class TenderSummary
    {
        public string Summary1 { get; set; }
        public string Summary2 { get; set; }
        public bool EnableCompletePayment { get; set; }
        public bool DisplayNoReceiptButton { get; set; }
        public bool EnableRunAway { get; set; }
        public string OutstandingAmount { get; set; }
        public string IssueStoreCreditMessage { get; set; }
        public bool EnablePumpTest { get; set; }
        public List<Tender> Tenders { get; set; }
        public List<Report> Report { get; set; }
        public List<Error> Messages { get; set; }
        public List<SaleVendorCoupon> VendorCoupons { get; set; }
        public LineDisplayModel LineDisplay { get; set; }
    }

    public class SaleVendorCoupon
    {
        public string SerialNumber { get; set; }
        public string Coupon { get; set; }
    }
}
