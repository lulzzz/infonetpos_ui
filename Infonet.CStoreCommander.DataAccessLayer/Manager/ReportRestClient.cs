using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class ReportRestClient : IReportRestClient
    {
        private readonly ICacheManager _cacheManager;

        public ReportRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        //Tony 03/19/2019
        public async Task<HttpResponseMessage> GetReceiptHeader()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetReceiptHeader, _cacheManager.AuthKey);
        }
        //end
        public async Task<HttpResponseMessage> GetAllDepartment()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetAllDepartment, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetAllShift()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetAllShift, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetAllTill()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetAllTill, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetShiftCountReport(string departmentId,
            int tillNumber, int shiftNumber)
        {
            var url = string.Format(Urls.GetSaleCountReport, departmentId,
                    tillNumber, shiftNumber, _cacheManager.TillNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetFlashReport()
        {
            var url = string.Format(Urls.GetFlashReport, _cacheManager.TillNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetTillAuditReport()
        {
            var url = string.Format(Urls.GetTillAuditReport, _cacheManager.TillNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetKickBackReport(double kickBackBalancePoints)
        {
            var url = string.Format(Urls.GetKickBackBalanceReport, kickBackBalancePoints);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
