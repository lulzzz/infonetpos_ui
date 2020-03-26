using Infonet.CStoreCommander.EntityLayer.Entities.Reports;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class WriteOff
    {
        public Sale Sale { get; set; }

        public Report Receipt { get; set; }

        public LineDisplayModel LineDisplay { get; set; }
    }
}
