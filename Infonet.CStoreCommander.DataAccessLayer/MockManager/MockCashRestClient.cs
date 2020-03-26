using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockCashRestClient : ICashRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockCashRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> CompleteCashCashdrop(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("CompleteCashDrop.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> CompleteCashDraw(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("CompleteCashDraw.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetAllTenders(string transactionType, bool billTillClose, string dropreason)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetAllTenders.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetCashButtons()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetCashButtons.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetCashDrawTypes()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("GetCashDrawTypes.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> OpenCashDrawer(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("OpenCashDrawer.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> UpdateTenderForCashdrop(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("UpdateTenderForCashdrop.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
