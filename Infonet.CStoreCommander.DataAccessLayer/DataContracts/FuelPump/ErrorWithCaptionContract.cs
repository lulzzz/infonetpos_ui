using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.FuelPump
{
    public class ErrorWithCaptionContract
    {
        public Error error { get; set; }
        public string caption { get; set; }
        public ReportContract priceReport { get; set; }
        public ReportContract fuelPriceReport { get; set; }
    }
}
