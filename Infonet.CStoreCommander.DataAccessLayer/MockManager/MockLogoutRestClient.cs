using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockLogoutRestClient : ILogoutRestClient
    {

        private readonly IStorageFolder _storageInstalledFolder;

        public MockLogoutRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public Task<HttpResponseMessage> CloseTill()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> EndShift()
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> FinishTillClose(bool? readTankDip, bool? readTotalizer)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> Logout(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("Logout.json",
                _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> ReadTankDip()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> SwitchUser(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse("SwitchUser.json",
                _storageInstalledFolder), Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> UpdateTillClose(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ValidateTillClose()
        {
            throw new NotImplementedException();
        }
    }
}
