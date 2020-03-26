using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class GiftCertificateRestClient : IGiftCertificateRestClient
    {
        private readonly ICacheManager _cacheManager;

        public GiftCertificateRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Gets the gift certificates
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetGiftCertificates(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GetGiftCertificates, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Saves the gift certificates 
        /// </summary>
        /// <param name="content">HTTP Content</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SaveGiftCertificates(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SaveGiftCertificates, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetStoreCredits(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GetStoreCredit, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveStoreCredits(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SaveStoreCredit, content,_cacheManager.AuthKey);
        }
    }
}
