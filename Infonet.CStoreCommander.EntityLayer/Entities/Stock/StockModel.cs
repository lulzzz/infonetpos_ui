namespace Infonet.CStoreCommander.EntityLayer.Entities.Stock
{
    public class StockModel
    {
        public string StockCode { get; set; }

        public string Description { get; set; }

        public decimal? RegularPrice { get; set; }

        public string AlternateCode { get; set; }

        public TaxCodeModel TaxCodes { get; set; } = new TaxCodeModel();
    }
}
