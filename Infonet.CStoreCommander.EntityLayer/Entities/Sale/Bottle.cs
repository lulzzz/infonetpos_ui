using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Sale
{
    public class Bottle
    {
        public string PageName { get; set; }

        public int PageId { get; set; }

        public List<BottleDetail> BottleDetails { get; set; }
    }

    public class BottleDetail
    {
        public string Description { get; set; }

        public string Product { get; set; }

        public int DefaultQuantity { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }
    }
}
