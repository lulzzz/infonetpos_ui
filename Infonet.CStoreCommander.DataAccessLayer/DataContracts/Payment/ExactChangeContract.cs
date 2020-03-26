using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment
{
    public class ExactChangeContract
    {
        public SaleContract newSale { get; set; }
        public bool openCashDrawer { get; set; }
        public bool isRefund { get; set; }
        public ReportContract paymentReceipt { get; set; }
        public List<LineDisplayContract> customerDisplays { get; set; }
        public string limitExceedMessage { get; set; }
    }
}
