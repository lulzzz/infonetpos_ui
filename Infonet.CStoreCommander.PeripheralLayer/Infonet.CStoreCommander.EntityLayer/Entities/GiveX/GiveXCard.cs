using Infonet.CStoreCommander.EntityLayer.Entities.Reports;

namespace Infonet.CStoreCommander.EntityLayer.Entities.GiveX
{
    public class GiveXCard
    {
        public string GivexCardNumber { get; set; }
        public decimal GivexPrice { get; set; }
        public int TillNumber { get; set; }
        public int SaleNumber { get; set; }
        public string StockCodeForGivexCard { get; set; }
    }

    public class GivexSaleCard
    {
        public Sale.Sale Sale { get; set; }
        public Report Report { get; set; }
    }
}
