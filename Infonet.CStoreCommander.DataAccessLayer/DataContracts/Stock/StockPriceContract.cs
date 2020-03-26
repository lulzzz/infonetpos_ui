using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock
{
    public class StockPriceContract
    {
        public string stockCode { get; set; }
        public string description { get; set; }
        public string vendorId { get; set; }
        public List<string> specialPriceTypes { get; set; }
        public string regularPriceText { get; set; }
        public string priceTypeText { get; set; }
        public bool isPriceVisible { get; set; }
        public bool isAvQtyVisible { get; set; }
        public string availableQuantity { get; set; }
        public bool isTaxExemptVisible { get; set; }
        public string taxExemptPrice { get; set; }
        public string taxExemptAvailable { get; set; }
        public bool isSpecialPricingVisible { get; set; }
        public string fromDate { get; set; }
        public bool isToDateVisible { get; set; }
        public string toDate { get; set; }
        public bool isEndDateChecked { get; set; }
        public bool isActiveVendorPrice { get; set; }
        public bool isPerDollarChecked { get; set; }
        public bool isPerPercentageChecked { get; set; }
        public bool isAddButtonVisible { get; set; }
        public bool isRemoveButtonVisible { get; set; }
        public bool isChangePriceEnable { get; set; }
        public SalePriceContract salePrice { get; set; }
        public FirstUnitPriceContract firstUnitPrice { get; set; }
        public IncrementalPriceContract incrementalPrice { get; set; }
        public XForPriceContract xForPrice { get; set; }
        public LineDisplayContract customerDisplay { get; set; }
        public string message { get; set; }
    }

    public class XForPriceContract
    {
        public int columns { get; set; }
        public string columnText { get; set; }
        public string columnText2 { get; set; }
        public List<xForPriceGridsContract> xForPriceGrids { get; set; }
    }

    public class xForPriceGridsContract
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
    }

    public class IncrementalPriceContract
    {
        public int columns { get; set; }
        public string columnText { get; set; }
        public string columnText2 { get; set; }
        public string columnText3 { get; set; }
        public List<IncrementalPriceGridsContract> incrementalPriceGrids { get; set; }
    }

    public class IncrementalPriceGridsContract
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
    }
    public class FirstUnitPriceContract
    {
        public int columns { get; set; }
        public string columnText { get; set; }
        public string columnText2 { get; set; }
        public List<FirstUnitPriceGridsContract> firstUnitPriceGrids { get; set; }
    }

    public class FirstUnitPriceGridsContract
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
    }
    public class SalePriceContract
    {
        public int columns { get; set; }
        public string columnText { get; set; }
        public List<SalePricesContract> salePrices { get; set; }
    }

    public class SalePricesContract
    {
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
    }
}
