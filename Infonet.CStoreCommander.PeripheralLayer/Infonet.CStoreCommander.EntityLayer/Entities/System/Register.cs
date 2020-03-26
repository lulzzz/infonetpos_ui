namespace Infonet.CStoreCommander.EntityLayer.Entities.System
{
    public class Register
    {
        public Scanner Scanner { get; set; }
        public CashDrawer CashDrawer { get; set; }
        public CustomerDisplay CustomerDisplay { get; set; }
        public ReportPrinter Report { get; set; }
        public ReceiptPrinter Receipt { get; set; }
        public Msr Msr { get; set; }
    }

    public class Scanner
    {
        public bool UseScanner { get; set; }
        public bool UseOposScanner { get; set; }
        public string Port { get; set; }
        public string Setting { get; set; }
        public string Name { get; set; }
    }

    public class CashDrawer
    {
        public bool UseCashDrawer { get; set; }
        public bool UseOposCashDrawer { get; set; }
        public string Name { get; set; }
        public int OpenCode { get; set; }
    }

    public class CustomerDisplay
    {
        public int Port { get; set; }
        public string Name { get; set; }
        public int DisplayCode { get; set; }
        public int DisplayLen { get; set; }
        public bool UseCustomerDisplay { get; set; }
        public bool UseOposCustomerDisplay { get; set; }
    }

    public class ReportPrinter
    {
        public bool UseReportPrinter { get; set; }
        public bool UseOposReportPrinter { get; set; }
        public string Name { get; set; }
        public string Driver { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
    }

    public class ReceiptPrinter
    {
        public bool UseReceiptPrinter { get; set; }
        public bool UseOposReceiptPrinter { get; set; }
        public string Name { get; set; }
        public string ReceiptDriver { get; set; }
    }

    public class Msr
    {
        public bool UseMsr { get; set; }
        public bool UseOposMsr { get; set; }
        public string Name { get; set; }
    }
}
