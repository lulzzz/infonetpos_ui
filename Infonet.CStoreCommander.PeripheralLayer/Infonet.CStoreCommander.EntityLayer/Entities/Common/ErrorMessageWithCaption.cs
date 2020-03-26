using Infonet.CStoreCommander.EntityLayer.Entities.Reports;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Common
{
    public class ErrorMessageWithCaption
    {
        public string Caption { get; set; }

        public Error Error { get; set; }

        public Report PriceReport { get; set; }

        public Report FuelPriceReport { get; set; }
    }
}
