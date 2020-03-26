using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout
{
    public class PaymentCompleteContract
    {
        public SaleContract newSale;
        public List<ReportContract> receipts;
        public List<LineDisplayContract> customerDisplays;
        public bool openCashDrawer;
        public string changeDue;
        public bool isRefund;
        public string limitExceedMessage { get; set; }
        public string kickabckServerError  { get; set; }
    }
}
