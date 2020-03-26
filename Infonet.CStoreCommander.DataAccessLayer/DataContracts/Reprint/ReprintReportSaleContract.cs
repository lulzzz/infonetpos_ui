using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reprint
{
    public class ReprintReportSaleContract
    {
        public bool isPaymentSale { get; set; }
        public bool isCloseBatchSale { get; set; }
        public bool isPayInsideSale { get; set; }
        public bool isPayAtPumpSale { get; set; }
        public List<PayAtPumpSaleContract> payAtPumpSales { get; set; }
        public List<PayInsideSalesContract> payInsideSales { get; set; }
        public List<PaymentSalesContract> paymentSales { get; set; }
        public List<CloseBatchSalesContract> closeBatchSales { get; set; }

    }

    public class PayAtPumpSaleContract
    {
        public string saleNumber { get; set; }
        public string volume { get; set; }
        public string amount { get; set; }
        public string pump { get; set; }
        public string grade { get; set; }
        public string date { get; set; }
        public string time { get; set; }        
    }

    public class PayInsideSalesContract
    {
        public string saleNumber { get; set; }
        public string soldOn { get; set; }
        public string time { get; set; }
        public string amount { get; set; }
        public string customer { get; set; }
    }
    public class PaymentSalesContract
    {
        public string saleNumber { get; set; }
        public string soldOn { get; set; }
        public string time { get; set; }
        public string amount { get; set; }
    }

    public class CloseBatchSalesContract
    {
        public string batchNumber { get; set; }
        public string terminalId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string report { get; set; }
    }
}