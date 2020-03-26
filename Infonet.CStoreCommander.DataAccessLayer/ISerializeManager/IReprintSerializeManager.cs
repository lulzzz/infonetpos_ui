using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reprint;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IReprintSerializeManager
    {
        Task<ReprintReportSale> GetReprintReportSale(string reportType, string date);

        Task<List<ReportName>> GetReprintReportName();

        Task<List<Report>> GetReprintReport(string saleNumber,string saleDate, string reportType);
    }
}
