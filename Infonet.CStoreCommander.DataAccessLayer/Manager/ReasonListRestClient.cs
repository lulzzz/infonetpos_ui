using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class ReasonListRestClient : IReasonListRestClient
    {
        private ICacheManager _cacheManager;

        public ReasonListRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to get reason Lists
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetReasonList(string reason)
        {
            var url = string.Format(Urls.GetReasonList, reason);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
