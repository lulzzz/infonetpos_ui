using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class CustomerRestClient : ICustomerRestClient
    {
        private const int PageSize = 50;

        private readonly ICacheManager _cacheManager;

        public CustomerRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> AddCustomer(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddCustomer, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetAllCustomers(int pageIndex, bool loyalty)
        {
            var url = string.Format(loyalty ? Urls.GetAllLoyaltyCustomers : Urls.GetAllCustomers, PageSize, pageIndex);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SearchCustomers(string searchTerm,
            int pageIndex, bool loyalty)
        {
            var url = loyalty ? string.Format(Urls.SearchLoyaltyCustomers, WebUtility.UrlEncode(searchTerm), _cacheManager.SaleNumber, _cacheManager.TillNumber, PageSize, pageIndex)
                : string.Format(Urls.SearchCustomers, WebUtility.UrlEncode(searchTerm), _cacheManager.TillNumber, _cacheManager.SaleNumber, PageSize, pageIndex);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetCustomerForSale(string code, int saleNumber,
            int tillNumber, byte registerNumber)
        {
            var url = string.Format(Urls.SetCustomerForSale, code, tillNumber, saleNumber, registerNumber);
            var client = new HttpRestClient(true);
            return await client.PostAsync(url, null, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetloyalityCustomer(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.SetLoyalityCustomer, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetCustomerByCard(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.GetCustomerByCard, content, _cacheManager.AuthKey);
        }
    }
}
