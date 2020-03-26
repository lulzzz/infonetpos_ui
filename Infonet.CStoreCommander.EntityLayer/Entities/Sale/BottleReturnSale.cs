using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class BottleReturnSale : SaleBase
    {
        public List<BottleReturnSaleLine> SaleLines { get; set; }

        public decimal TotalAmount { get; set; }

        public BottleReturnSale()
        {
            SaleLines = new List<BottleReturnSaleLine>();
        }
    }

    public class BottleReturnSaleLine
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Amount { get; set; }

        public string StockCode { get; set; }

        public int LineNumber { get; set; }
    }
}
