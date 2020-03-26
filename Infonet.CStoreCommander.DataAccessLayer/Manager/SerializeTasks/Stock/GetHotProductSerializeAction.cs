using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    public class GetHotProductSerializeAction : SerializeAction
    {
        private readonly int _pageId;
        private readonly IStockRestClient _restClient;

        public GetHotProductSerializeAction(
            IStockRestClient restClient,
            int pageId) :
            base("GetHotProduct")
        {
            _restClient = restClient;
            _pageId = pageId;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetHotProducts(_pageId);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var hotProducts = new DeSerializer().MapHotProduct(data);
                    return new Mapper().MapHotProduct(hotProducts);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
