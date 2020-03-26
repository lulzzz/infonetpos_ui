using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class GetBottlesSerializeAction : SerializeAction
    {
        private readonly int _pageId;
        private readonly IStockRestClient _restClient;

        public GetBottlesSerializeAction(
            IStockRestClient restClient,
            int pageId) :
            base("GetBottles")
        {
            _restClient = restClient;
            _pageId = pageId;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetBottles(_pageId);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var bottles = new DeSerializer().MapBottles(data);
                    return new Mapper().MapBottles(bottles);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
