using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.EntityLayer.Entities.GiveX
{
    public class GiveXReport
    {
        public List<ReportDetail> ReportDetails { get; set; }
        public Report CloseBatchReport { get; set; }
    }

    public class ReportDetail
    {
        public int Id { get; set; }
        public string CashOut { get; set; }
        public string BatchDate { get; set; }
        public string BatchTime { get; set; }
        public string Report { get; set; }
    }
}
