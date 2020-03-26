using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockPolicyRestClient : IPolicyRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockPolicyRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> GetAllPolicies()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("Policy.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> RefreshPolicies()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("Policy.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
