using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Login;
using Infonet.CStoreCommander.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Login
{
    /// <summary>
    /// Tills Serialization Action
    /// </summary>
    internal class GetTillsSerializeAction : SerializeAction
    {
        private readonly InfonetLog _log;
        private readonly ILoginRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginRestClient">Login Rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public GetTillsSerializeAction(ILoginRestClient loginRestClient, ICacheManager cacheManager)
            : base("GetTills")
        {
            _restClient = loginRestClient;
            _cacheManager = cacheManager;
            _log = InfonetLogManager.GetLogger<LoginRestClient>();
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Tills</returns>
        protected override async Task<object> OnPerform()
        {
            var currentUser = new User
            {
                UserName = _cacheManager.UserName,
                Password = _cacheManager.Password,
                PosId = _cacheManager.LoginPolicies.PosID,
                TillNumber = _cacheManager.TillNumber
            };

            var user = JsonConvert.SerializeObject(currentUser);

            var content = new StringContent(user, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.GetActiveTillsAsync(content);

            //response ok to continue
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tills = new DeSerializer().MapTills(data);
                    return new Mapper().MapTills(tills);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
