namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.System
{
    public class RegisterContract
    {
        public ScannerContract scanner { get; set; }
        public CashDrawerContract cashDrawer { get; set; }
        public CustomerDisplayContract customerDisplay { get; set; }
        public ReportPrinterContract report { get; set; }
        public ReceiptPrinterContract receipt { get; set; }
        public MsrContract msr { get; set; }
    }

    public class ScannerContract
    {
        public bool useScanner { get; set; }
        public bool useOposScanner { get; set; }
        public string port { get; set; }
        public string setting { get; set; }
        public string name { get; set; }
    }

    public class CashDrawerContract
    {
        public bool useCashDrawer { get; set; }
        public bool useOposCashDrawer { get; set; }
        public string name { get; set; }
        public int openCode { get; set; }
    }

    public class CustomerDisplayContract
    {
        public int port { get; set; }
        public string name { get; set; }
        public int displayCode { get; set; }
        public int displayLen { get; set; }
        public bool useCustomerDisplay { get; set; }
        public bool useOposCustomerDisplay { get; set; }
    }

    public class ReportPrinterContract
    {
        public bool useReportPrinter { get; set; }
        public bool useOposReportPrinter { get; set; }
        public string name { get; set; }
        public string driver { get; set; }
        public string font { get; set; }
        public int fontSize { get; set; }
    }

    public class ReceiptPrinterContract
    {
        public bool useReceiptPrinter { get; set; }
        public bool useOposReceiptPrinter { get; set; }
        public string name { get; set; }
        public string receiptDriver { get; set; }
    }

    public class MsrContract
    {
        public bool useMsr { get; set; }
        public bool useOposMsr { get; set; }
        public string name { get; set; }
    }
}
