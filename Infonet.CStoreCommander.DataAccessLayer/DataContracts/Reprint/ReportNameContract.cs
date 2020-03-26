namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reprint
{
    public class ReportNameContract
    {
        public string reportType { get; set; }
        public string reportName { get; set; }
        public bool isEnabled { get; set; }
        public bool dateEnabled { get; set; }
    }
}