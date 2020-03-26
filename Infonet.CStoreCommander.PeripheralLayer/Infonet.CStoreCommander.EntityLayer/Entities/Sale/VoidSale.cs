using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class VoidSale
    {
        public Sale Sale { get; set; }
        public Report Receipt { get; set; }
        public List<LineDisplayModel> LineDisplays { get; set; }
        public bool OpenCashDrawer { get; set; }
        public string ChangeDue { get; set; }
        public bool IsRefund { get; set; }
    }
}
