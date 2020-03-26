using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class SystemRestClient : ISystemRestClient
    {
        private readonly ICacheManager _cacheManager;

        public SystemRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetRegisterSettings(byte registerNumber)
        {
            var url = string.Format(Urls.RegisterSettings, registerNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
