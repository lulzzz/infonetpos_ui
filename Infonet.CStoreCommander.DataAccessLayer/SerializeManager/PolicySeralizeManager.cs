using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class PolicySeralizeManager : SerializeManager, IPolicySerializeManager
    {
        private readonly IPolicyRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public PolicySeralizeManager(IPolicyRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        } 


        public async Task<Policy> GetAllPolicies(bool isRefresh)
        {
            var action = new GetAllPoliciesSerializeAction(_cacheManager, _restClient, isRefresh);

            await PerformTask(action);

            return (Policy)action.ResponseValue;
        }
    }
}
