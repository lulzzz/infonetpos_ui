using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Common
{
    public class GetReasonListSerializeAction : SerializeAction
    {
        private readonly string _reason;
        private readonly IReasonListRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public GetReasonListSerializeAction(
            IReasonListRestClient reasonListRestClient,
            ICacheManager cacheManager,
            string reason) : base("GetReasonList")
        {
            _restClient = reasonListRestClient;
            _cacheManager = cacheManager;
            _reason = reason;
        }

        /// <summary>
        /// Method to get reason list
        /// </summary>
        /// <returns></returns>
        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetReasonList(_reason);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var parsedData = new DeSerializer().MapReasonList(data);
                    return new Mapper().MapReasonsList(parsedData);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
