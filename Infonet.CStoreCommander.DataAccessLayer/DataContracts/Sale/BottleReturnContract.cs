using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class BottleReturnContract
    {
        public SaleContract newSale;
        public ReportContract paymentReceipt;
        public LineDisplayContract customerDisplay;
        public bool openCashDrawer;
        public string changeDue;
        public bool isRefund;
    }
}
