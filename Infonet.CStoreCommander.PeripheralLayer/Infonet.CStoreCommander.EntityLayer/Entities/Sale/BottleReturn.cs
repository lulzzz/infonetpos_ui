using Infonet.CStoreCommander.EntityLayer.Entities.Reports;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class BottleReturn
    {
        public Sale Sale { get; set; }
        public Report Receipt { get; set; }
        public LineDisplayModel LineDisplay { get; set; }
        public bool OpenCashDrawer { get; set; }
        public string ChangeDue { get; set; }
        public bool IsRefund { get; set; }
    }
}
