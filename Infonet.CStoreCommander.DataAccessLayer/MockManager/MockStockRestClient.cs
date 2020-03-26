using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Infonet.CStoreCommander.DataAccessLayer.MockManager
{
    public class MockStockRestClient : IStockRestClient
    {
        private readonly IStorageFolder _storageInstalledFolder;

        public MockStockRestClient(IStorageService storageService)
        {
            _storageInstalledFolder = storageService.StorageFolder;
        }

        public async Task<HttpResponseMessage> AddProduct(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "AddProduct.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetStockItems(int pageIndex)
        {
            if (pageIndex == 1)
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(await Helper.GetOfflineResponse(
                            "StockItems.json",
                            _storageInstalledFolder),
                        Encoding.UTF8, "application/json")
                };
                return response;
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("[]",
                        Encoding.UTF8, "application/json")
            };
        }

        /// <summary>
        /// Gets the Http response for the Search Stock items API
        /// </summary>
        /// <param name="searchText">Search Keyword</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>Http response from the API</returns>
        public async Task<HttpResponseMessage> SearchStockItems(string searchText, int pageIndex)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "StockItems.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        /// <summary>
        /// Gets the Bottles 
        /// </summary>
        /// <param name="pageId">Page id</param>
        /// <returns>Http Response from the API</returns>
        public async Task<HttpResponseMessage> GetBottles(int pageId)
        {
            if (pageId == 1)
            {
                var response = new HttpResponseMessage
                {
                    Content = new StringContent(await Helper.GetOfflineResponse(
                            "Bottles.json",
                            _storageInstalledFolder),
                        Encoding.UTF8, "application/json")
                };
                return response;
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("[]",
                        Encoding.UTF8, "application/json")
            };
        }

        /// <summary>
        /// Adds a new Bottle Return sale
        /// </summary>
        /// <param name="content">Http Payload</param>
        /// <returns>Http response from the API</returns>
        public async Task<HttpResponseMessage> AddBottleReturnSale(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "Success.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetAllTaxes()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "AllTaxes.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetHotProductPages()
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "HotProductPages.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> GetHotProducts(int pageId)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "HotCategories.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> CheckPriceByCode(string stockCode)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                    "PriceCheck.json",
                    _storageInstalledFolder),
                    Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> ChangeRegularPrice(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                     "ChangePrice.json",
                     _storageInstalledFolder),
                     Encoding.UTF8, "application/json")
            };
            return response;
        }

        public async Task<HttpResponseMessage> ChangeSpecialPrice(HttpContent content)
        {
            var response = new HttpResponseMessage
            {
                Content = new StringContent(await Helper.GetOfflineResponse(
                    "ChangePrice.json",
                    _storageInstalledFolder),
                    Encoding.UTF8, "application/json")
            };
            return response;
        }
    }
}
