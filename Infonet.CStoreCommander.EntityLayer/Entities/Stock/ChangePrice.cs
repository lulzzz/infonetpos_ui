using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Stock
{
    public class ChangePrice
    {
        public string StockCode { get; set; }
        public int TillNumber { get; set; }
        public int SaleNumber { get; set; }
        public int RegisterNumber { get; set; }
        public decimal RegularPrice { get; set; }
        public string PriceType { get; set; }
        public List<GridPrices> GridPricesContract { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public bool PerDollarChecked { get; set; }
        public bool IsEndDate { get; set; }
    }

    public class GridPrices
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}