using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reprint;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class ReprintBusinessLogic : IReprintBusinessLogic
    {
        private readonly IReprintSerializeManager _reprintSerializeManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        public ReprintBusinessLogic(IReprintSerializeManager reprintSerializeManager,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _reportsBusinessLogic = reportsBusinessLogic;
            _reprintSerializeManager = reprintSerializeManager;
        }

        public async Task<List<Report>> GetReprintReport(string saleNumber, string saleDate, string reportType)
        {
            var response = await _reprintSerializeManager.GetReprintReport(saleNumber, saleDate, reportType);

            foreach(var report in response)
            {
                await _reportsBusinessLogic.SaveReport(report);
            }

            return response;
        }

        public async Task<List<ReportName>> GetReprintReportName()
        {
            return await _reprintSerializeManager.GetReprintReportName();
        }

        public async Task<ReprintReportSale> GetReprintReportSale(string reportType,
            string date)
        {
            return await _reprintSerializeManager.GetReprintReportSale(reportType, date);
        }
    }
}
