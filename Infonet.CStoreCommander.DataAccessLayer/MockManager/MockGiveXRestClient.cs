using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockGiveXRestClient : IGiveXRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockGiveXRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> ActivateGivexCard(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GivexDeactivateCardBalance.json",
               _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> AddAmount(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GivexDeactivateCardBalance.json",
              _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> CloseBatch(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("VoidSale.json",
             _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> DeactivateGivexcard(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GivexDeactivateCardBalance.json",
              _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Method to get mock data for givex card balance.
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetCardBalance(string givexCardNumber)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GivexCardBalance.json",
               _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> GetGivexReport(string date)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> getGivexStockCode()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("StockCode.json",
               _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> SetAmount(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GivexDeactivateCardBalance.json",
              _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
