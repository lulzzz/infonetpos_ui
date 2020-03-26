using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class FuelPriceRestClient : IFuelPriceRestClient
    {
        private readonly ICacheManager _cacheManager;

        public FuelPriceRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> LoadPriceIncrementsAndDecrements(bool taxExempt)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.LoadPriceIncrementsAndDecrements, taxExempt);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> LoadPricesToDisplay()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.LoadPricesToDisplay, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SavePricesToDisplay(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SavePricesToDisplay, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetPriceDecrement(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SetPriceDecrement, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetPriceIncrement(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SetPriceIncrement, content, _cacheManager.AuthKey);
        }

       
    }
}
