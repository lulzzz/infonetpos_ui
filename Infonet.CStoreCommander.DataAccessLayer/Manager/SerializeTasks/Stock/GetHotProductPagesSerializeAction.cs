using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    public class GetHotProductPagesSerializeAction : SerializeAction
    {
        private readonly IStockRestClient _restClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stockRestClient">Home rest client</param>
        public GetHotProductPagesSerializeAction(
            IStockRestClient stockRestClient) : base("HotProductPages")
        {
            _restClient = stockRestClient;
        }

        /// <summary>
        /// Serialization method
        /// </summary>
        /// <returns>Hot categories</returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetHotProductPages();

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var categories = new DeSerializer().MapHotProductPages(data);
                    return new Mapper().MapCategories(categories);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
