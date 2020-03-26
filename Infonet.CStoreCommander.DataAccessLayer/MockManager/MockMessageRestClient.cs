using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockMessageRestClient : IMessageRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockMessageRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> GetAllMessage()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                   "Messages.json",
                   _storageInstalledFolder),
                   Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> SaveMessage(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                  "VoidSale.json",
                  _storageInstalledFolder),
                  Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
