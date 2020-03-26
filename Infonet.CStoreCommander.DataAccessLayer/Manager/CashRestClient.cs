using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class CashRestClient : ICashRestClient
    {
        private readonly ICacheManager _cacheManager;

        public CashRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> CompleteCashCashdrop(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.CompleteCashDrop, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CompleteCashDraw(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.CompleteCashDraw, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetAllTenders(string transactionType,
            bool billTillClose,
            string dropreason)
        {
            var client = new HttpRestClient(true);
            var url = string.Format(Urls.GetAllTenders, transactionType, _cacheManager.SaleNumber, _cacheManager.TillNumber, billTillClose, dropreason);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetCashButtons()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetCashButtons, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetCashDrawTypes()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetCashDrawTypes, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> OpenCashDrawer(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.OpenCashDrawer, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UpdateTenderForCashdrop(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.UpdateTenders, content, _cacheManager.AuthKey);
        }
    }
}
