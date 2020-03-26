using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class LogoutRestClient : ILogoutRestClient
    {
        private readonly ICacheManager _cacheManager;

        public LogoutRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to switch user
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SwitchUser(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.ChangeUser, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> Logout(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.LogoutUser, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CloseTill()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.CloseTill, _cacheManager.TillNumber, _cacheManager.SaleNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ReadTankDip()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.ReadTankDip, _cacheManager.TillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> EndShift()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.EndShift, _cacheManager.TillNumber, _cacheManager.SaleNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ValidateTillClose()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.ValidateTillClose, _cacheManager.TillNumber, _cacheManager.SaleNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> UpdateTillClose(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.UpdateTillClose, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> FinishTillClose(bool? readTankDip, bool? readTotalizer)
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.FinishTillClose, _cacheManager.TillNumber, 
                _cacheManager.RegisterNumber, readTankDip, readTotalizer);

            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
