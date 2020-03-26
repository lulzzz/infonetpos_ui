using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Infonet.CStoreCommander.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Login
{
    /// <summary>
    /// Login Serialization Action
    /// </summary>
    internal class LoginSerializeAction : SerializeAction
    {
        private readonly InfonetLog _log;
        private readonly ILoginRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginRestClient">Login rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public LoginSerializeAction(ILoginRestClient loginRestClient, ICacheManager cacheManager) :
            base("Login")
        {
            _restClient = loginRestClient;
            _cacheManager = cacheManager;
            _log = InfonetLogManager.GetLogger<LoginRestClient>();
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Authentication token</returns>
        protected override async Task<object> OnPerform()
        {
            var currentUser = new User
            {
                UserName = _cacheManager.UserName,
                Password = _cacheManager.Password,
                TillNumber = _cacheManager.TillNumber,
                PosId = _cacheManager.LoginPolicies.PosID,
                ShiftNumber = _cacheManager.LoginPolicies.UseShifts ? _cacheManager.ShiftNumber : default(int),
                FloatAmount = _cacheManager.LoginPolicies.ProvideTillFloat ?
                Convert.ToDecimal(_cacheManager.CashFloat, CultureInfo.InvariantCulture) : default(decimal),
                ShiftDate = _cacheManager.ShiftDate
            };

            var user = JsonConvert.SerializeObject(currentUser);
            var content = new StringContent(user, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.LoginAsync(content);

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var loginContract = new DeSerializer().MapLogin(data);
                    return new Mapper().MapLogin(loginContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
