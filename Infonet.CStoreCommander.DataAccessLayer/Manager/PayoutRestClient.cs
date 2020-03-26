using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class PayoutRestClient : IPayoutRestClient
    {
        private readonly ICacheManager _cacheManager;

        public PayoutRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetVendorPayout()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetVendorPayout,
                _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> PayoutComplete(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.PayoutComplete, content,
                _cacheManager.AuthKey);
        }
    }
}
