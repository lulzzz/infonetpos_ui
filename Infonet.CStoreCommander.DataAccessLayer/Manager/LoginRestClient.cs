using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class LoginRestClient : ILoginRestClient
    {
        public async Task<HttpResponseMessage> GetActiveTillsAsync(HttpContent content)
        {
            var httpClient = new HttpRestClient(true);
            return await httpClient.PostAsync(Urls.ActiveTills, content);
        }

        public async Task<HttpResponseMessage> GetLoginPolicyAsync(string ipAddress)
        {
            var httpClient = new HttpRestClient(true);
            var url = string.Format(Urls.LoginPolicies, ipAddress);
            return await httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> GetActiveShiftsAsync(HttpContent content)
        {
            var httpClient = new HttpRestClient(true);
            return await httpClient.PostAsync(Urls.ActiveShifts, content);
        }

        public async Task<HttpResponseMessage> LoginAsync(HttpContent content)
        {
            var httpClient = new HttpRestClient(true);
            return await httpClient.PostAsync(Urls.Login, content);
        }

        public async Task<HttpResponseMessage> GetPasswordAsync(string userName)
        {
            var url = string.Format(Urls.GetPassword, userName);
            var httpClient = new HttpRestClient(true);
            return await httpClient.GetAsync(url);
        }
    }
}
