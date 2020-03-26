using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class SaleRestClient : ISaleRestClient
    {
        private readonly ICacheManager _cacheManager;
        private const int PageSize = 50;

        public SaleRestClient(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// method to get all suspended sale
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        public async Task<HttpResponseMessage> GetAllSuspendedSale()
        {
            var url = string.Format(Urls.GetAllSuspendedSale, _cacheManager.TillNumber);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to initialize new sale
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> InitializeNewSale()
        {
            var url = string.Format(Urls.InitializeNewSale, _cacheManager.TillNumber, _cacheManager.RegisterNumber);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to void sale
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> VoidSale(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.VoidSale, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to unsuspend sale
        /// </summary>
        /// <param name="saleNumber"></param>
        /// <param name="tillNumber"></param>
        /// <returns>Response for unsuspended sale API</returns>
        public async Task<HttpResponseMessage> UnsuspendSale(int saleNumber)
        {
            var url = string.Format(Urls.UnsuspendSale, saleNumber, _cacheManager.TillNumber);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to suspend current sale
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SuspendSale()
        {
            var url = string.Format(Urls.SuspendSale, _cacheManager.SaleNumber, _cacheManager.TillNumberForSale);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to get sale list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns>response of get sale list API</returns>
        public async Task<HttpResponseMessage> GetSaleList(int pageIndex)
        {
            var url = string.Format(Urls.GetSaleList, pageIndex, PageSize);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to verify the stock restrictions before adding to sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="stockCode">Stock code</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="isReturn">Is Return</param>
        /// <returns>HTTP response from the API</returns>
        public async Task<HttpResponseMessage> VerifyStock(HttpContent content)
        {
            var url = string.Format(Urls.VerifyStock);
            var client = new HttpRestClient();
            return await client.PostAsync(url, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to search sale by sale number
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="searchText"></param>
        /// <param name="saleDate"></param>
        /// <returns>sale list</returns>
        public async Task<HttpResponseMessage> SearchSaleList(int pageIndex, int searchText, string saleDate)
        {
            var url = string.Format(Urls.SearchSaleList, pageIndex, PageSize, searchText == 0 ? "" :
                searchText.ToString(), saleDate);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Adds a stock item to an sale
        /// </summary>
        /// <returns>HTTP response of the Add stock API</returns>
        public async Task<HttpResponseMessage> AddStockToSale(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.AddStockToSale, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to return sale
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ReturnSale(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.ReturnSale, content, _cacheManager.AuthKey);
        }

        /// <summary>
        ///  Method to get sale by sale number 
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetSaleBySaleNumber(int tillNumber, int saleNumber)
        {
            var url = string.Format(Urls.GetSaleBySaleNumber, saleNumber, tillNumber);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Method to return sale items
        /// </summary>
        /// <param name="content"></param>
        /// <returns>HttpResponseMessage of API</returns>
        public async Task<HttpResponseMessage> ReturnSaleItems(HttpContent content)
        {
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.ReturnSaleItems, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Removes the sale line item from the sale
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="lineNumber">Line number</param>
        /// <returns>HTTP Response from the API</returns>
        public async Task<HttpResponseMessage> RemoveSaleLine(int tillNumber,
            int saleNumber, int lineNumber)
        {
            var url = string.Format(Urls.RemoveSaleLine, tillNumber, saleNumber, lineNumber);
            var client = new HttpRestClient(true);
            return await client.DeleteAsync(url, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Updates the existing Sale line item
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="lineNumber">Line number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="discount">Discount</param>
        /// <param name="discountType">Discount type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Price</param>
        /// <param name="reason">Reason</param>
        /// <param name="reasonType">Reason type</param>
        /// <returns>Sale model</returns>
        public async Task<HttpResponseMessage> UpdateSaleLine(int saleNumber, int tillNumber, int lineNumber, byte registerNumber,
             string discount, string discountType, string quantity, string price, string reason, string reasonType)
        {
            var updateSaleLineContract = new Mapper().MapUpdateSaleLineToSale(saleNumber, tillNumber, lineNumber, registerNumber,
             discount, discountType, quantity, price, reason, reasonType);
            var updateSale = JsonConvert.SerializeObject(updateSaleLineContract);
            var content = new StringContent(updateSale, Encoding.UTF8, "application/json");
            var client = new HttpRestClient(true);
            return await client.PostAsync(Urls.UpdateSaleLine, content, _cacheManager.AuthKey);
        }

        /// <summary>
        /// Writes off the sale 
        /// </summary>
        /// <param name="content">HTTP content payload</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> WriteOff(StringContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.WriteOff, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> SetTaxExemption(HttpContent content)
        {
            var client = new HttpRestClient();
            return await client.PostAsync(Urls.TaxExemption, content, _cacheManager.AuthKey);
        }

        public async Task<HttpResponseMessage> ValidateVoidSale()
        {
            var client = new HttpRestClient();
            var url = string.Format(Urls.ValidateVoidSale,  _cacheManager.SaleNumber, _cacheManager.TillNumber);
            return await client.GetAsync(url, _cacheManager.AuthKey);
        }
    }
}
