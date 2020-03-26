using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class SoundsRestClient : ISoundRestClient
    {
        private readonly ICacheManager _cacheManager;

        public SoundsRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetSounds()
        {
            var client = new HttpRestClient(true);
            return await client.GetAsync(Urls.GetSounds, _cacheManager.AuthKey);
        }
    }
}
