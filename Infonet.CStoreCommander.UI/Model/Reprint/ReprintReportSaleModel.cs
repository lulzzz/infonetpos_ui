using GalaSoft.MvvmLight;

namespace Infonet.CStoreCommander.UI.Model.Reprint
{
    public class ReprintReportSaleModel : ViewModelBase
    {
        public bool IsPaymentSale { get; set; }
        public bool IsCloseBatchSale { get; set; }
        public bool IsPayInsideSale { get; set; }
        public bool IsPayAtPumpSale { get; set; }
    }

    public class PayAtPumpSaleModel
    {
        public string SaleNumber { get; set; }
        public string Volume { get; set; }
        public string Amount { get; set; }
        public string Pump { get; set; }
        public string Grade { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class PayInsideSalesModel
    {
        public string SaleNumber { get; set; }
        public string SoldOn { get; set; }
        public string Time { get; set; }
        public string Amount { get; set; }
        public string Customer { get; set; }
    }
    public class PaymentSalesModel
    {
        public string SaleNumber { get; set; }
        public string SoldOn { get; set; }
        public string Time { get; set; }
        public string Amount { get; set; }
    }

    public class CloseBatchSalesModel
    {
        public string BatchNumber { get; set; }
        public string TerminalId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Report { get; set; }
    }
}
