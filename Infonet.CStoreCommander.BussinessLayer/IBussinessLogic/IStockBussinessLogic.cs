using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IStockBussinessLogic
    {
        /// <summary>
        /// Gets all taxes
        /// </summary>
        /// <returns></returns>
        Task<TaxCodeModel> GetAllTaxesAsync();

        /// <summary>
        ///  Method to add new stock
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        Task<bool> AddProductAsync(StockModel stock);

        /// <summary>
        /// Gets the list of Stock items
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock Items</returns>
        Task<List<StockModel>> GetStockItems(int pageIndex);

        /// <summary>
        /// Gets the list of Searched Stock items
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock items</returns>
        Task<List<StockModel>> SearchStockItems(string searchText, int pageIndex);

        /// <summary>
        /// Method to save hot pages
        /// </summary>
        /// <returns></returns>
        Task<List<HotProductPageModel>> GetHotProductPages();

        /// <summary>
        /// Method to get hot products
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>List of hot products</returns>
        Task<List<HotProductModel>> GetHotProducts(int pageId);

        /// <summary>
        /// Gets the list of bottles for the Page
        /// </summary>
        /// <param name="pageId">Page Id</param>
        /// <returns>List of Bottles</returns>
        Task<List<BottleDetail>> GetBottles(int pageId);

        /// <summary>
        /// Completes a Bottle Return Sale
        /// </summary>
        /// <param name="sale">Bottle Return Sale</param>
        /// <returns>SaleModel</returns>
        Task<BottleReturn> CompleteBottleReturnSale(BottleReturnSale sale);
        

        Task<StockPrice> CheckStockPrice(string stockCode);

        Task<StockPrice> EditPrice(ChangePrice stockModel, bool isRegularPriceChange);
    }
}
