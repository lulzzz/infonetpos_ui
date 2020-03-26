using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockReasonListRestClient : IReasonListRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockReasonListRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> GetReasonList(string reason)
        {
            HttpResponseMessage response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("ReasonList.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
