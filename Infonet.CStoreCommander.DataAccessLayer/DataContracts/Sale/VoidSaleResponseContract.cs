using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class VoidSaleResponseContract
    {
        public SaleContract newSale;
        public ReportContract paymentReceipt;
        public List<LineDisplayContract> customerDisplays;
        public bool openCashDrawer;
        public string changeDue;
        public bool isRefund;
    }
}
