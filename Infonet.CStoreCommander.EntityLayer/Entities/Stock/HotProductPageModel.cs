namespace Infonet.CStoreCommander.EntityLayer.Entities.Stock
{
    public class HotProductPageModel
    {
        public int PageId { get; set; }

        public string PageName { get; set; }
    }

    public class HotProductModel
    {
        public int ButtonId { get; set; }

        public string StockCode { get; set; }

        public string Description { get; set; }

        public string DefaultQuantity { get; set; }

        public string Image { get; set; }
    }
}
