using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using Infonet.CStoreCommander.EntityLayer.Entities.Reprint;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Reprint;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class ReprintSerializeManager : SerializeManager, IReprintSerializeManager
    {
        private readonly IReprintRestClient _reprintRestClient;

        public ReprintSerializeManager(IReprintRestClient reprintRestClient)
        {
            _reprintRestClient = reprintRestClient;
        }

        public async Task<List<Report>> GetReprintReport(string saleNumber, string saleDate, string reportType)
        {
            var action = new GetReprintReportSerializeAction(_reprintRestClient, saleNumber, saleDate, reportType);
            await PerformTask(action);
            return (List<Report>)action.ResponseValue;
        }

        public async Task<List<ReportName>> GetReprintReportName()
        {
            var action = new GetReprintReportNameSerializeAction(_reprintRestClient);
            await PerformTask(action);
            return (List<ReportName>)action.ResponseValue;

        }

        public async Task<ReprintReportSale> GetReprintReportSale(string reportType, string date)
        {
            var action = new GetReportSaleSerializeAction(_reprintRestClient, reportType, date);
            await PerformTask(action);
            return (ReprintReportSale)action.ResponseValue;
        }
    }
}
