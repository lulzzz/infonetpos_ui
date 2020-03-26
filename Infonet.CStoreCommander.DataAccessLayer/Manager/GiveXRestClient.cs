using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class GiveXRestClient : IGiveXRestClient
    {
        private readonly ICacheManager _cacheManager;

        public GiveXRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to check balance for given card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetCardBalance(string givexCardNumber)
        {
            var url = string.Format(Urls.GiveXBalance, givexCardNumber, _cacheManager.SaleNumber, _cacheManager.TillNumber);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to deactivate GiveX card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>API response for deactivating GiveX card</returns>
        public async Task<HttpResponseMessage> DeactivateGivexcard(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.DeactivateGivexCard, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to close batch
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CloseBatch(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.CloseBatch, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to activate GiveX Card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>API response </returns>
        public async Task<HttpResponseMessage> ActivateGivexCard(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.ActivateGiveXCard, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to add amount in  GiveX card
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> AddAmount(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.AddAmount, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// method to adjust amount of GiveX card
        /// </summary>
        /// <param name="content"></param>
        /// <returns>API response</returns>
        public async Task<HttpResponseMessage> SetAmount(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.AdjustAmount, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// method to get stock code for GiveX
        /// </summary>
        /// <returns>GiveX stock code</returns>
        public async Task<HttpResponseMessage> getGivexStockCode()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetGivexStockCode, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetGivexReport(string date)
        {
            var url = string.Format(Urls.GetGivexReport , date);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
