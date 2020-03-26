using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Threading.Tasks;
using System.Net.Http;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class MessageRestClient : IMessageRestClient
    {
        private readonly ICacheManager _cacheManager;

        public MessageRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> GetAllMessage()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetMessage, _cacheManager.AuthKey);
        }


        public async Task<HttpResponseMessage> SaveMessage(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddMessage, content, _cacheManager.AuthKey);
        }
    }
}
