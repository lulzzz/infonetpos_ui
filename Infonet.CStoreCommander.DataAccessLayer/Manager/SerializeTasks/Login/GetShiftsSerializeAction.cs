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
    /// Get shifts Serialization Action
    /// </summary>
    internal class GetShiftsSerializeAction : SerializeAction
    {
        private readonly InfonetLog _log;
        private readonly ILoginRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loginRestClient">Login rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public GetShiftsSerializeAction(ILoginRestClient loginRestClient, ICacheManager cacheManager)
            : base("GetShifts")
        {
            _restClient = loginRestClient;
            _cacheManager = cacheManager;
            _log = InfonetLogManager.GetLogger<LoginRestClient>();
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Active shifts</returns>
        protected override async Task<object> OnPerform()
        {
            var currentUser = new User
            {
                UserName = _cacheManager.UserName,
                Password = _cacheManager.Password,
                TillNumber = _cacheManager.TillNumber,
                PosId = _cacheManager.LoginPolicies.PosID
            };

            var user = JsonConvert.SerializeObject(currentUser);
            var content = new StringContent(user, Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.GetActiveShiftsAsync(content);

            //response ok to continue
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    //validate json 
                    var parsedData = new DeSerializer().MapShifts(data);
                    return new Mapper().MapActiveShifts(parsedData);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
