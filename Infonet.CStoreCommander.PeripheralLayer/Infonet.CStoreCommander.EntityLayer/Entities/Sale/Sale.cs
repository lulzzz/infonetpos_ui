using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class Sale
    {
        public int TillNumber { get; set; }

        public int SaleNumber { get; set; }

        public string TotalAmount { get; set; }

        public decimal TotalDiscount { get; set; }

        public string CustomerName { get; set; }

        public int Taxes { get; set; }

        public string Summary { get; set; }

        public bool EnableExactChange { get; set; }
        public bool EnableWriteOffButton { get; set; }

        public List<Error> Errors { get; set; }

        public List<SaleLine> SaleLines { get; set; }

        public LineDisplayModel LineDisplay { get; set; }

        public bool HasCarwashProducts { get; set; }
    }
}
