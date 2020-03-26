using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Payment
{
    public class ExactChange
    {
        public Sale.Sale NewSale { get; set; }
        public Report Report { get; set; }
        public List<LineDisplayModel> LineDisplays { get; set; }
        public bool OpenCashDrawer { get; set; }
        public bool IsRefund { get; set; }
        public string LimitExceedMessage { get; set; }
    }
}