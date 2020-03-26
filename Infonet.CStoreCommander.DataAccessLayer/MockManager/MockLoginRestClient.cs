using Infonet.CStoreCommander.DataAccessLayer.IManager;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockLoginRestClient : ILoginRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockLoginRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> GetActiveShiftsAsync(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("ActiveShifts.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetActiveTillsAsync(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("Tills.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetLoginPolicyAsync(string ipAddress)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("LoginPolicy.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> GetPasswordAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> LoginAsync(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("Login.json", _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
