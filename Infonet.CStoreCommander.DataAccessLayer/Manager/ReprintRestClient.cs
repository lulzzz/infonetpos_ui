using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class ReprintRestClient : IReprintRestClient
    {
        private readonly ICacheManager _cacheManager;

        public ReprintRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetReprintReport(string saleNumber,string saleDate,
            string reportType)
        {
            var url = string.Format(Urls.GetReprintReport, saleNumber,
                saleDate, reportType);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetReprintReportName()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetReprintReportNames, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetReprintSales(string reportType, string date)
        {
            var url = string.Format(Urls.GetReprintSales, reportType, date);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
