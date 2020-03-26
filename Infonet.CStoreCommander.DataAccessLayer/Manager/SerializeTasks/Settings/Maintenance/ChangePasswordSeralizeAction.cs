using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Settings.Maintenance
{
    public class ChangePasswordSerializeAction : SerializeAction
    {
        private readonly ICacheManager _cacheManager;
        private readonly IMaintenanceRestClient _maintenanceRestClient;
        private readonly string _password;
        private readonly string _confirmPassword;

        public ChangePasswordSerializeAction(
            IMaintenanceRestClient maintenanceRestClient,
            ICacheManager cacheManager,
            string password,
            string confirmPassword) :
            base("ChangePassword")
        {
            _maintenanceRestClient = maintenanceRestClient;
            _cacheManager = cacheManager;
            _password = password;
            _confirmPassword = confirmPassword;
        }

        protected async override Task<object> OnPerform()
        {
            var changePasswordContract = new Mapper().MapChangePassword(_cacheManager, _password, _confirmPassword);
            var changePassword = JsonConvert.SerializeObject(changePasswordContract);
            var content = new StringContent(changePassword, Encoding.UTF8, ApplicationJSON);
            var response = await _maintenanceRestClient.ChangePassword(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                    var parsedData = new DeSerializer().MapChangePasswordSuccess(data);
                    return  new Mapper().MapChangePasswordSuccess(parsedData);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
