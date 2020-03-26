using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock
{
    public class ChangePriceContract
    {
        public string stockCode { get; set; }
        public int tillNumber { get; set; }
        public int saleNumber { get; set; }
        public int registerNumber { get; set; }
        public string regularPrice { get; set; }
        public string priceType { get; set; }
        public List<GridPricesContract> gridPrices { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public bool perDollarChecked { get; set; }
        public bool isEndDate { get; set; }
    }

    public class GridPricesContract
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
    }
}
