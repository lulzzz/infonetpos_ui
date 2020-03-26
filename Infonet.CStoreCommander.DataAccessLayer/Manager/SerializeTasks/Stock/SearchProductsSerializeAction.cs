using System.Net;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock
{
    internal class SearchProductsSerializeAction : SerializeAction
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly int _pageIndex;
        private readonly string _searchText;


        public SearchProductsSerializeAction(IStockRestClient stockRestClient,
            ICacheManager cacheManager, string searchText,
            int pageIndex) : base("SearchProducts")
        {
            _stockRestClient = stockRestClient;
            _searchText = searchText;
            _pageIndex = pageIndex;
        }

        /// <summary>
        /// Gets the list of Stock items available
        /// </summary>
        /// <returns>List of Stock items</returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _stockRestClient.SearchStockItems(_searchText, _pageIndex);
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
