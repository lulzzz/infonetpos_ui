using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.GiveX
{
    public class GiveXBalanceContract
    {
        public string cardNumber { get; set; }
        public string balance { get; set; }
        public ReportContract receipt { get; set; }
    }

    public class DeactivateGivexCardContract
    {
        public SaleContract sale { get; set; }
        public ReportContract receipt { get; set; }
    }
}
