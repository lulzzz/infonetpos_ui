using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.Reprint
{
    public class ReprintReportSale
    {
        public bool IsPaymentSale { get; set; }
        public bool IsCloseBatchSale { get; set; }
        public bool IsPayInsideSale { get; set; }
        public bool IsPayAtPumpSale { get; set; }
        public List<PayAtPumpSale> PayAtPumpSales { get; set; }
        public List<PayInsideSales> PayInsideSales { get; set; }
        public List<PaymentSales> PaymentSales { get; set; }
        public List<CloseBatchSales> CloseBatchSales { get; set; }
    }
    public class PayAtPumpSale
    {
        public string SaleNumber { get; set; }
        public string Volume { get; set; }
        public string Amount { get; set; }
        public string Pump { get; set; }
        public string Grade { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class PayInsideSales
    {
        public string SaleNumber { get; set; }
        public string SoldOn { get; set; }
        public string Time { get; set; }
        public string Amount { get; set; }
        public string Customer { get; set; }
    }
    public class PaymentSales
    {
        public string SaleNumber { get; set; }
        public string SoldOn { get; set; }
        public string Time { get; set; }
        public string Amount { get; set; }
    }

    public class CloseBatchSales
    {
        public string BatchNumber { get; set; }
        public string TerminalId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Report { get; set; }
    }
}
