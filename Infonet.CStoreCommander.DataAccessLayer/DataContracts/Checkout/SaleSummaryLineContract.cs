using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class SaleSummaryLineContract
    {
        public string key;
        public string value;
    }

    public class TenderSummaryContract
    {
        public string summary1;
        public string summary2;
        public string outstandingAmount;
        public bool enableCompletePayment;
        public bool displayNoReceiptButton;
        public bool enableRunAway;
        public bool enablePumpTest { get; set; }
        public string issueStoreCreditMessage { get; set; }
        public List<TenderContract> tenders;
        public List<ReportContract> receipts;
        public List<ErrorContract> messages;
        public List<SaleVendorCouponContract> vendorCoupons { get; set; }
        public LineDisplayContract customerDisplay { get; set; }
    }

    public class CheckoutSummaryContract
    {
        public List<SaleSummaryLineContract> saleSummary;
        public TenderSummaryContract tenderSummary;
    }

    public class SaleVendorCouponContract
    {
        public string serialNumber { get; set; }
        public string coupon { get; set; }
    }
}
