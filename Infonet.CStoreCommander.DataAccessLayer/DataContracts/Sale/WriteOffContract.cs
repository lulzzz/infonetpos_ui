using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale
{
    public class WriteOffContract
    {
        public SaleContract newSale;
        public ReportContract writeOffReceipt;
        public LineDisplayContract customerDisplay;
    }
}
