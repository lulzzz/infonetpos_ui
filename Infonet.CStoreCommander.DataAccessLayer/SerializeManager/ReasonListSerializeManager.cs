using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common;
using Infonet.CStoreCommander.DataAccessLayer.IManager;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class ReasonListSerializeManager : SerializeManager, IReasonListSerializeManager
    {

        private readonly IReasonListRestClient _reasonListRestClient;
        private readonly ICacheManager _cacheManager;

        public ReasonListSerializeManager(IReasonListRestClient reasonListRestClient,
            ICacheManager cacheManager)
        {
            _reasonListRestClient = reasonListRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<ReasonsList> GetReasonList(string reason)
        {
            var action = new GetReasonListSerializeAction(_reasonListRestClient,
                 _cacheManager, reason);

            await PerformTask(action);

            return (ReasonsList)action.ResponseValue;
        }
    }
}
