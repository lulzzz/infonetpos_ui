using Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPrice
{
    public class SetGroupPriceWithReport
    {
        public FuelPriceContract price { get; set; }
        public ReportContract report { get; set; }
    }
}
