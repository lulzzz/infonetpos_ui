using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class PaymentRestClient : IPaymentRestClient
    {
        private readonly ICacheManager _cacheManager;
        private const int PageSize = 50;

        public PaymentRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<HttpResponseMessage> PayByExactChange(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ExactChange, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> GetAllARCustomers(int pageIndex)
        {
            var url = string.Format(Urls.GetAllARCustomer, pageIndex, PageSize);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SearchARCustomers(string searchTearm, int pageIndex)
        {
            var url = string.Format(Urls.SearchARCustomer, searchTearm, pageIndex, PageSize);
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SaveARPayment(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.SaveARPayment, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SearchARCustomer(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.GetARCustomerByCustomerCode, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ValidateFleet()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.ValidateFleet, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> FleetPayment(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.FleetPayment, content, _cacheManager.AuthKey);
        }
    }
}
