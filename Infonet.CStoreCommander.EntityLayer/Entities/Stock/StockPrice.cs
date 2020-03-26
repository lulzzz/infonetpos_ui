using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Stock
{
    public class StockPrice
    {
        public string StockCode { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public List<string> SpecialPriceTypes { get; set; }
        public string RegularPriceText { get; set; }
        public string PriceTypeText { get; set; }
        public bool IsPriceVisible { get; set; }
        public bool IsAvQtyVisible { get; set; }
        public string AvailableQuantity { get; set; }
        public bool IsTaxExemptVisible { get; set; }
        public string TaxExemptPrice { get; set; }
        public string TaxExemptAvailable { get; set; }
        public bool IsSpecialPricingVisible { get; set; }
        public string FromDate { get; set; }
        public bool IsToDateVisible { get; set; }
        public string ToDate { get; set; }
        public bool IsEndDateChecked { get; set; }
        public bool IsActiveVendorPrice { get; set; }
        public bool IsPerDollarChecked { get; set; }
        public bool IsPerPercentageChecked { get; set; }
        public bool IsAddButtonVisible { get; set; }
        public bool IsRemoveButtonVisible { get; set; }
        public bool IsChangePriceEnable { get; set; }
        public SalePrice SalePrice { get; set; }
        public FirstUnitPrice FirstUnitPrice { get; set; }
        public IncrementalPrice IncrementalPrice { get; set; }
        public XForPrice XForPrice { get; set; }
        public LineDisplayModel LineDisplay { get; set; }
        public string Message { get; set; }
    }

    public class XForPrice
    {
        public int Columns { get; set; }
        public string ColumnText { get; set; }
        public string ColumnText2 { get; set; }
        public List<XForPriceGrids> CommonGrids { get; set; }
    }

    public class XForPriceGrids
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }

    public class IncrementalPrice
    {
        public int Columns { get; set; }
        public string ColumnText { get; set; }
        public string ColumnText2 { get; set; }
        public string ColumnText3 { get; set; }
        public List<IncrementalPriceGrids> CommonGrids { get; set; }
    }

    public class IncrementalPriceGrids
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }

    public class FirstUnitPrice
    {
        public int Columns { get; set; }
        public string ColumnText { get; set; }
        public string ColumnText2 { get; set; }
        public List<FirstUnitPriceGrids> CommonGrids { get; set; }
    }

    public class FirstUnitPriceGrids
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
    public class SalePrice
    {
        public int Columns { get; set; }
        public string ColumnText { get; set; }
        public List<SalePrices> CommonGrids { get; set; }
    }

    public class SalePrices
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}
