using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common
{
    public class GetAllPoliciesSerializeAction : SerializeAction
    {
        private readonly IPolicyRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly bool _isRefresh;
        public GetAllPoliciesSerializeAction(ICacheManager cacheManager,
            IPolicyRestClient restClient,
            bool isRefresh) : base("GetAllPolicies")
        {
            _cacheManager = cacheManager;
            _restClient = restClient;
            _isRefresh = isRefresh;
        }

        protected override async Task<object> OnPerform()
        {
            var response = new HttpResponseMessage();
            if (!_isRefresh)
            {
                response = await _restClient.GetAllPolicies();
            }
            else
            {
                response = await _restClient.RefreshPolicies();
            }

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var policyContract = new DeSerializer().MapPolicies(data);
                    return new Mapper().MapPolicies(policyContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
