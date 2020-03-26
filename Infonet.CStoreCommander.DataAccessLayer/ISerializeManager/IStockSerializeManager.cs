using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IStockSerializeManager
    {
        /// <summary>
        /// method to get all taxes
        /// </summary>
        /// <returns></returns>
        Task<TaxCodeModel> GetAllTaxesAsync();

        /// <summary>
        /// method to add stock
        /// </summary>
        /// <returns></returns>
        Task<bool> AddProdutcAsync(StockModel stock);

        /// <summary>
        /// Gets the available stock items based on the page index
        /// </summary>
        /// <param name="pageIndex">Page index for the Paged data</param>
        /// <returns>List of Stock Items</returns>
        Task<List<StockModel>> GetStockItems(int pageIndex);

        /// <summary>
        /// Gets the list of Searched Stock items
        /// </summary>
        /// <param name="searchText">Search term</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock items</returns>
        Task<List<StockModel>> SearchStockItems(string searchText, int pageIndex);

        /// <summary>
        /// Adds the Stock item to the sale
        /// </summary>
        /// <param name="stockCode">Stock code of the Stock item</param>
        /// <returns>True or False</returns>
        Task<bool> AddStockToSale(string stockCode);

        /// <summary>
        /// Method to get hot product pages
        /// </summary>
        /// <returns></returns>
        Task<List<HotProductPageModel>> GetHotProductPages();

        /// <summary>
        /// Method to get hot product for given pageId
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>list of hot products</returns>
        Task<List<HotProductModel>> GetHotProducts(int pageId);

        /// <summary>
        /// Gets the list of bottles 
        /// </summary>
        /// <param name="pageId">Page id</param>
        /// <returns>List of bottles</returns>
        Task<List<BottleDetail>> GetBottles(int pageId);

        /// <summary>
        /// Completes a Bottle Return sale
        /// </summary>
        /// <param name="sale">Bottle Return Sale</param>
        /// <returns>SaleModel</returns>
        Task<BottleReturn> AddBottleReturnSale(BottleReturnSale sale);

        Task<StockPrice> CheckStockPrice(string stockCode);

        Task<StockPrice> EditPrice(ChangePrice stockModel, bool isRegularPriceChange);
    }
}
