using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    internal class GetProductsSerializeAction : SerializeAction
    {
        private ICacheManager _cacheManager;
        private IStockRestClient _stockRestClient;
        private int _pageIndex;


        public GetProductsSerializeAction(IStockRestClient stockRestClient,
            ICacheManager cacheManager, 
            int pageIndex) : base("GetProducts")
        {
            _stockRestClient = stockRestClient;
            _cacheManager = cacheManager;
            _pageIndex = pageIndex;
        }

        /// <summary>
        /// Gets the list of Stock items available
        /// </summary>
        /// <returns>List of Stock items</returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _stockRestClient.GetStockItems(_pageIndex);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    //validate json
                    var stockItems = new DeSerializer().MapStockItems(data);
                    return new Mapper().MapStocks(stockItems);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}