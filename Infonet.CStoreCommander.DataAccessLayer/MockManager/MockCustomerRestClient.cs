using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockCustomerRestClient : ICustomerRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockCustomerRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> AddCustomer(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                    "AddCustomer.json",
                    _storageInstalledFolder),
                    Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetAllCustomers(int pageIndex, bool loyalty)
        {
            if (pageIndex == 1)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(await Helper.GetOfflineResponse(
                            "Customers.json",
                            _storageInstalledFolder),
                        Encoding.UTF8, "application/json")
                };
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("",
                        Encoding.UTF8, "application/json")
            };
        }

        public Task<HttpResponseMessage> GetCustomerByCard(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> SearchCustomers(string searchTerm,
            int pageIndex, bool loyalty)
        {
            if (pageIndex == 1)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(await Helper.GetOfflineResponse(
                            "SearchCustomers.json",
                            _storageInstalledFolder),
                        Encoding.UTF8, "application/json")
                };
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("",
                        Encoding.UTF8, "application/json")
            };
        }

        public async Task<HttpResponseMessage> SetCustomerForSale(string code, int saleNumber, int tillNumber,
            byte registerNumber)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                            "SetCustomerForSale.json",
                            _storageInstalledFolder),
                        Encoding.UTF8, "application/json")
            };
        }

        public async Task<HttpResponseMessage> SetloyalityCustomer(HttpContent content)
        {
            return new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                             "VoidSale.json",
                             _storageInstalledFolder),
                         Encoding.UTF8, "application/json")
            };
        }
    }
}
