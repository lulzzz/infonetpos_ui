using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Stock;
using Infonet.CStoreCommander.EntityLayer.Entities.Stock;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Stock;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class StockSerializeManager : SerializeManager, IStockSerializeManager
    {
        private readonly IStockRestClient _stockRestClient;
        private readonly ICacheManager _cacheManager;

        public StockSerializeManager(IStockRestClient stockRestClient,
            ICacheManager cacheManager)
        {
            _stockRestClient = stockRestClient;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// Method to get all taxes
        /// </summary>
        /// <returns></returns>
        public async Task<TaxCodeModel> GetAllTaxesAsync()
        {
            var action = new GetAllTaxesSerializeAction(_stockRestClient, _cacheManager);

            await PerformTask(action);

            return (TaxCodeModel)action.ResponseValue;
        }

        /// <summary>
        /// Method to add stock 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddProdutcAsync(StockModel stock)
        {
            var action = new AddProductSerializeAction(_stockRestClient, _cacheManager, stock);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }

        /// <summary>
        /// Gets the available stock items based on the page index
        /// </summary>
        /// <param name="pageIndex">Page index for the Paged data</param>
        /// <returns>List of Stock Items</returns>
        public async Task<List<StockModel>> GetStockItems(int pageIndex)
        {
            var action = new GetProductsSerializeAction(_stockRestClient, _cacheManager, pageIndex);

            await PerformTask(action);

            return (List<StockModel>)action.ResponseValue;
        }

        /// <summary>
        /// Gets the list of Searched Stock items
        /// </summary>
        /// <param name="searchText">Search term</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of Stock items</returns>
        public async Task<List<StockModel>> SearchStockItems(string searchText, int pageIndex)
        {
            var action = new SearchProductsSerializeAction(_stockRestClient,
                _cacheManager, searchText, pageIndex);

            await PerformTask(action);

            return (List<StockModel>)action.ResponseValue;
        }

        /// <summary>
        /// Adds the Stock item to the sale
        /// </summary>
        /// <param name="stockCode">Stock code of the Stock item</param>
        /// <returns>True or False</returns>
        public async Task<bool> AddStockToSale(string stockCode)
        {
            var action = new GetProductsSerializeAction(_stockRestClient, _cacheManager, 1);

            await PerformTask(action);

            return ((SuccessContract)(action.ResponseValue)).success;
        }

        /// <summary>
        /// Method to get Hot product pages
        /// </summary>
        /// <returns></returns>
        public async Task<List<HotProductPageModel>> GetHotProductPages()
        {
            var action = new GetHotProductPagesSerializeAction(_stockRestClient);

            await PerformTask(action);

            return ((List<HotProductPageModel>)(action.ResponseValue));
        }

        /// <summary>
        /// method to get hot products
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public async Task<List<HotProductModel>> GetHotProducts(int pageId)
        {
            var action = new GetHotProductSerializeAction(_stockRestClient, pageId);

            await PerformTask(action);

            return ((List<HotProductModel>)(action.ResponseValue));
        }

        /// <summary>
        /// Gets the list of bottles 
        /// </summary>
        /// <param name="pageId">Page id</param>
        /// <returns>List of bottles</returns>
        public async Task<List<BottleDetail>> GetBottles(int pageId)
        {
            var action = new GetBottlesSerializeAction(_stockRestClient, pageId);

            await PerformTask(action);

            return ((List<BottleDetail>)(action.ResponseValue));
        }

        /// <summary>
        /// Completes a Bottle Return sale
        /// </summary>
        /// <param name="sale">Bottle Return Sale</param>
        /// <returns>True or False</returns>
        public async Task<BottleReturn> AddBottleReturnSale(BottleReturnSale sale)
        {
            var action = new AddBottleReturnSaleSerializeAction(_stockRestClient,
                _cacheManager, sale);

            await PerformTask(action);

            var bottleReturnContract = (BottleReturnContract)action.ResponseValue;

            return new Mapper().MapBottleReturn(bottleReturnContract);
        }

        public async Task<StockPrice> EditPrice(ChangePrice stockModel, bool isRegularPriceChange)
        {
            var action = new PriceChangeSerializeAction(_stockRestClient,
              stockModel, isRegularPriceChange);

            await PerformTask(action);

            var stockPrice = (StockPriceContract)action.ResponseValue;

            return new Mapper().MapStockPrice(stockPrice);
        }

        public async Task<StockPrice> CheckStockPrice(string stockCode)
        {
            var action = new PriceCheckByCodeSerializeAction(_stockRestClient,
               stockCode);

            await PerformTask(action);

            var stockPrice = (StockPriceContract)action.ResponseValue;

            return new Mapper().MapStockPrice(stockPrice);
        }
    }
}
