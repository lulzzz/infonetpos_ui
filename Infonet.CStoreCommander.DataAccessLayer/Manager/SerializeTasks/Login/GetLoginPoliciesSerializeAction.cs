using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Login
{
    /// <summary>
    /// Login policies Serialization Action
    /// </summary>
    internal class GetLoginPoliciesSerializeAction : SerializeAction
    {
        private readonly ILoginRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="loginRestClient">Login Rest client</param>
        /// <param name="cacheManager">Cache manager</param>
        public GetLoginPoliciesSerializeAction(
            ILoginRestClient loginRestClient,
            ICacheManager cacheManager) : base("LoginPolicies")
        {
            _restClient = loginRestClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Login policies</returns>
        protected override async Task<object> OnPerform()
        {
            var ipAddress = _cacheManager.IpAddress;
            // Hard coded IP for development 
//#if DEBUG
//        //    ipAddress = "172.16.0.42";
//#endif
            var response = await _restClient.GetLoginPolicyAsync(ipAddress);

            //response ok to continue
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    //validate json 
                    var loginPolicies = new DeSerializer().MapLoginPolicies(data);
                    return new Mapper().MapLoginPolicies(loginPolicies);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
