using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class SwitchUserSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _logoutRestClient;
        private readonly User _userModel;

        public SwitchUserSerializeAction(ILogoutRestClient logoutRestClient,
            User userModel)
            : base("SwitchUser")
        {
            _logoutRestClient = logoutRestClient;
            _userModel = userModel;
        }

        protected override async Task<object> OnPerform()
        {
            var userContract = new Mapper().MapUserContract(_userModel);
            var user = JsonConvert.SerializeObject(userContract);
            var content = new StringContent(user, Encoding.UTF8, ApplicationJSON);
            var response = await _logoutRestClient.SwitchUser(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapLogin(data).authToken;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
