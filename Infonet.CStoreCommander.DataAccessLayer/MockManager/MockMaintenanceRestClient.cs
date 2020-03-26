using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockMaintenanceRestClient : IMaintenanceRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockMaintenanceRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;

        }
        /// <summary>
        /// Method to change password
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async  Task<HttpResponseMessage> ChangePassword(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "PasswordChange.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public Task<HttpResponseMessage> CloseBatch(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> Initialize(HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SetPostPay(bool isPostPay)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> SetPrepay(bool isPrepay)
        {
            throw new NotImplementedException();
        }
    }
}
