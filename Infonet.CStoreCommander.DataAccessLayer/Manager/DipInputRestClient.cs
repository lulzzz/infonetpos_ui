using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class DipInputRestClient : IDipInputRestClient
    {
        private readonly ICacheManager _cacheManager;

        public DipInputRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetDipInput()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.DipInputGet, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetDipInputPrint()
        {
            var url = string.Format(Urls.DipInputPrint, _cacheManager.TillNumber,
                _cacheManager.ShiftNumber, _cacheManager.RegisterNumber);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveDipInput(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SaveDipInput, content, _cacheManager.AuthKey);
        }
    }
}
