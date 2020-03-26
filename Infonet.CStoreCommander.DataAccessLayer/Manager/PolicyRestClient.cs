using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class PolicyRestClient : IPolicyRestClient
    {
        private readonly ICacheManager _cacheManager;

        public PolicyRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetAllPolicies()
        {
            var client = new HttpRestClient(true);
            var url = string.Format(Urls.Policies, _cacheManager.RegisterNumber);
            return await client.GetAsync(url,  _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> RefreshPolicies()
        {
            var client = new HttpRestClient(true);
            var url = string.Format(Urls.RefreshPolicies, _cacheManager.TillNumber,_cacheManager.SaleNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
