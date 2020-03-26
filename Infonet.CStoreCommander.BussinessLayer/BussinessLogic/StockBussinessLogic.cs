using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Stock 
    /// </summary>
    public class StockBussinessLogic : IStockBussinessLogic
    {
        private readonly IStockSerializeManager _serializeManager;
        private readonly IReportsBussinessLogic _reportBussinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager">Serialize manager</param>
        public StockBussinessLogic(IStockSerializeManager serializeManager,
            IReportsBussinessLogic reportBussinessLogic)
        {
            _serializeManager = serializeManager;
            _reportBussinessLogic = reportBussinessLogic;
        }

        /// <summary>
        /// Adds a new stock record
        /// </summary>
        /// <param name="stock">Stock model</param>
        /// <returns>True if added otherwise not</returns>
        public async Task<bool> AddProductAsync(StockModel stock)
        {
            return await _serializeManager.AddProdutcAsync(stock);
        }

        /// <summary>
        /// Returns list of Stock items
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock Items</returns>
        public async Task<List<StockModel>> GetStockItems(int pageIndex)
        {
            return await _serializeManager.GetStockItems(pageIndex);
        }

        /// <summary>
        /// Returns list of Searched Stock items
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock items</returns>
        public async Task<List<StockModel>> SearchStockItems(string searchText,
            int pageIndex)
        {
            return await _serializeManager.SearchStockItems(searchText, pageIndex);
        }

        /// <summary>
        /// Returns all Taxes
        /// </summary>
        /// <returns>Tax model</returns>
        public async Task<TaxCodeModel> GetAllTaxesAsync()
        {
            return await _serializeManager.GetAllTaxesAsync();
        }

        /// <summary>
        /// Returns hot product pages
        /// </summary>
        /// <returns>List of hot product pages</returns>
        public async Task<List<HotProductPageModel>> GetHotProductPages()
        {
            return await _serializeManager.GetHotProductPages();
        }

        /// <summary>
        /// Returns list of Hot products for the Page id
        /// </summary>
        /// <param name="pageId">Page id of the hot product page</param>
        /// <returns>List of Hot products</returns>
        public async Task<List<HotProductModel>> GetHotProducts(int pageId)
        {
            return await _serializeManager.GetHotProducts(pageId);
        }

        /// <summary>
        /// Returns list of bottles for the Page Id
        /// </summary>
        /// <param name="pageId">Page Id</param>
        /// <returns>List of Bottles</returns>
        public async Task<List<BottleDetail>> GetBottles(int pageId)
        {
            return await _serializeManager.GetBottles(pageId);
        }

        //TODO: Move this to Sale business logic
        /// <summary>
        /// Completes a Bottle Return Sale
        /// </summary>
        /// <param name="sale">Bottle Return Sale</param>
        /// <returns>True when sale is completed and False if not</returns>
        public async Task<BottleReturn> CompleteBottleReturnSale(BottleReturnSale sale)
        {
            var response = await _serializeManager.AddBottleReturnSale(sale);
            await _reportBussinessLogic.SaveReport(response.Receipt);

            return response;
        }

        public async Task<StockPrice> CheckStockPrice(string stockCode)
        {
            return await _serializeManager.CheckStockPrice(stockCode);
        }

        public async Task<StockPrice> EditPrice(ChangePrice stockModel, bool isRegularPriceChange)
        {
            return await _serializeManager.EditPrice(stockModel, isRegularPriceChange);
        }
    }
}
