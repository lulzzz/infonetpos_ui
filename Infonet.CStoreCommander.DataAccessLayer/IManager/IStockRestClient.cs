using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IStockRestClient
    {
        /// <summary>
        /// Method to get all taxes
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAllTaxes();

        /// <summary>
        /// Method to add product in stock
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> AddProduct(HttpContent content);

        /// <summary>
        /// Gets the Http response for the Get Stock items API
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>Http response from the API</returns>
        Task<HttpResponseMessage> GetStockItems(int pageIndex);

        /// <summary>
        /// Method to get hot product pages
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetHotProductPages();

        /// <summary>
        /// Method to get hot products
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetHotProducts(int pageId);

        /// <summary>
        /// Gets the Http response for the Search Stock items API
        /// </summary>
        /// <param name="searchText">Search Keyword</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>Http response from the API</returns>
        Task<HttpResponseMessage> SearchStockItems(string searchText, int pageIndex);

        /// <summary>
        /// Gets the Bottles 
        /// </summary>
        /// <param name="pageId">Page id</param>
        /// <returns>Http Response from the API</returns>
        Task<HttpResponseMessage> GetBottles(int pageId);

        /// <summary>
        /// Adds a new Bottle Return sale
        /// </summary>
        /// <param name="content">Http Payload</param>
        /// <returns>Http response from the API</returns>
        Task<HttpResponseMessage> AddBottleReturnSale(HttpContent content);
        Task<HttpResponseMessage> CheckPriceByCode(string stockCode);
        Task<HttpResponseMessage> ChangeRegularPrice(HttpContent content);
        Task<HttpResponseMessage> ChangeSpecialPrice(HttpContent content);
    }
}
