using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Reports;
using System.Collections.Generic;

namespace Infonet.CStoreCommander.DataAccessLayer.DataContracts.GiveX
{
    public class GiveXReportContract
    {
        public List<ReportDetailContract> reportDetails { get; set; }
        public ReportContract closeBatchReport { get; set; }
    }

    public class ReportDetailContract
    {
        public int id { get; set; }
        public string cashOut { get; set; }
        public string batchDate { get; set; }
        public string batchTime { get; set; }
        public string report { get; set; }
    }
}
