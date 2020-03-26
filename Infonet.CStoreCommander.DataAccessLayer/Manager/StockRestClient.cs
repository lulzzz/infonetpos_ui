using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class StockRestClient : IStockRestClient
    {
        private const int PageSize = 50;
        private readonly ICacheManager _cacheManager;

        public StockRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to add product in stock
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> AddProduct(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.AddProduct, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the HTTP response for the Get Stock items API
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>HTTP response from the API</returns>
        public async Task<HttpResponseMessage> GetStockItems(int pageIndex)
        {
            var url = string.Format(Urls.GetStockItems, PageSize, pageIndex);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the HTTP response for the Search Stock items API
        /// </summary>
        /// <param name="searchText">Search Keyword</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>HTTP response from the API</returns>
        public async Task<HttpResponseMessage> SearchStockItems(string searchText, int pageIndex)
        {
            var url = string.Format(Urls.SearchStockItems, searchText,
                    PageSize, pageIndex);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Gets the Bottles 
        /// </summary>
        /// <param name="pageId">Page id</param>
        /// <returns>HTTP Response from the API</returns>
        public async Task<HttpResponseMessage> GetBottles(int pageId)
        {
            var url = string.Format(Urls.GetBottlesUrl, pageId);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Adds a new Bottle Return sale
        /// </summary>
        /// <param name="content">HTTP Payload</param>
        /// <returns>HTTP response from the API</returns>
        public async Task<HttpResponseMessage> AddBottleReturnSale(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.BottleReturnUrl, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to fetch taxes form API
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAllTaxes()
        {
            var client = new HttpRestClient();
            return await client.GetAsync(Urls.GetAllTaxes, _cacheManager.AuthKey);
        }

        /// <summary>
        /// method to get hot product pages
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetHotProductPages()
        {
            var client = new HttpRestClient(true);
            return await client.GetAsync(Urls.HotProductPages, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to get hot products
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetHotProducts(int pageId)
        {
            var url = string.Format(Urls.HotProducts, pageId);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> CheckPriceByCode(string stockCode)
        {
            var url = string.Format(Urls.PriceCheckByCode, WebUtility.UrlEncode(stockCode), _cacheManager.TillNumber,
                    _cacheManager.SaleNumber, _cacheManager.RegisterNumber);
           
            var client = new HttpRestClient();
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ChangeRegularPrice(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.EditRegularPrice, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ChangeSpecialPrice(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.EditSpecialPrice, content, _cacheManager.AuthKey);
        }
    }
}
