using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Checkout
{
    public class CompletePaymentBase
    {
        public Sale.Sale Sale { get; set; }
        public List<LineDisplayModel> LineDisplay { get; set; }
        public bool OpenCashDrawer { get; set; }
        public string ChangeDue { get; set; }
        public bool IsRefund { get; set; }
        public string LimitExceedMessage { get; set; }
    }

    public class CompletePayment: CompletePaymentBase
    {
        public string KickabckServerError { get; set; }
        public List<Report> Receipts { get; set; }
    }

    public class CommonPaymentComplete: CompletePaymentBase
    {
        public Report Receipt { get; set; }
    }
}
