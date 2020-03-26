using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Logout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class LogoutUserSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public LogoutUserSerializeAction(ILogoutRestClient restClient,
            ICacheManager cacheManager) : base("logoutUser")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var logoutContract = new LogoutContract
            {
                tillNumber = _cacheManager.TillNumber
            };

            var reason = JsonConvert.SerializeObject(logoutContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.Logout(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSuccess(data).success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
