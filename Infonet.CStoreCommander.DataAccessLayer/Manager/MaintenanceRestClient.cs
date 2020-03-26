using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class MaintenanceRestClient : IMaintenanceRestClient
    {
        private readonly ICacheManager _cacheManager;

        public MaintenanceRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to get data from change password API
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ChangePassword(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ChangePassword, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CloseBatch(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.MaintenanceCloseBatch, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> Initialize(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.Initialize, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetPostPay(bool isPostPay)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.PostPay, isPostPay);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetPrepay(bool isPrepay)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.Prepay, isPrepay);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
