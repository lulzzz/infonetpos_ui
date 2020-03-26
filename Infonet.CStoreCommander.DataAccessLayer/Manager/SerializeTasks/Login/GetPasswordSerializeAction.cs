using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Login
{
    public class GetPasswordSerializeAction : SerializeAction
    {
        private readonly ILoginRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public GetPasswordSerializeAction(ILoginRestClient loginRestClient,
            ICacheManager cacheManager) : base("GetPassword")
        {
            _restClient = loginRestClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetPasswordAsync(_cacheManager.UserName);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapPassword(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
