using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    /// <summary>
    /// Rest client interface for Sale screen
    /// </summary>
    public interface ISaleRestClient
    {
        /// <summary>
        /// Initializes a new sale
        /// </summary>
        /// <returns>Response for New sale API</returns>
        Task<HttpResponseMessage> InitializeNewSale();

        /// <summary>
        /// Method to void sale 
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> VoidSale(HttpContent content);

        /// <summary>
        ///  Method to get all unsuspended sale
        /// </summary>
        /// <returns>Response for suspended sale API</returns>
        Task<HttpResponseMessage> GetAllSuspendedSale();

        /// <summary>
        /// Method to unsuspended sale
        /// </summary>
        /// <param name="saleNumber"></param>
        /// <param name="tillNumber"></param>
        /// <returns>Response for unsuspended sale API</returns>
        Task<HttpResponseMessage> UnsuspendSale(int saleNumber);

        /// <summary>
        /// Method to suspend sale
        /// </summary>
        /// <returns>api response</returns>
        Task<HttpResponseMessage> SuspendSale();

        /// <summary>
        /// Method to get sale list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetSaleList(int pageIndex);

        /// <summary>
        /// Writes off the sale 
        /// </summary>
        /// <param name="content">HTTP content payload</param>
        /// <returns></returns>
        Task<HttpResponseMessage> WriteOff(StringContent content);

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
        Task<HttpResponseMessage> UpdateSaleLine(int saleNumber, int tillNumber,
            int lineNumber, byte registerNumber,
            string discount, string discountType, string quantity, string price,
            string reason, string reasonType);

        /// <summary>
        /// Method to verify the stock restrictions before adding to sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="stockCode">Stock code</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="isReturn">Is Return</param>
        /// <returns>HTTP response from the API</returns>
        Task<HttpResponseMessage> VerifyStock(HttpContent content);

        /// <summary>
        /// Adds a stock item to an sale
        /// </summary>
        /// <returns>HTTP response of the Add stock API</returns>
        Task<HttpResponseMessage> AddStockToSale(HttpContent content);

        /// <summary>
        /// Method to get sale list by sale number 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="searchText"></param>
        /// <param name="saleDate"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SearchSaleList(int pageIndex, int searchText, string saleDate);

        /// <summary>
        /// Method to return sale
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> ReturnSale(HttpContent content);

        /// <summary>
        /// method to get sale by sale number
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <returns>api response</returns>
        Task<HttpResponseMessage> GetSaleBySaleNumber(int tillNumber, int saleNumber);

        /// <summary>
        /// Method to return sale items
        /// </summary>
        /// <param name="content"></param>
        /// <returns>HttpResponseMessage of API</returns>
        Task<HttpResponseMessage> ReturnSaleItems(HttpContent content);

        /// <summary>
        /// Removes the sale line item from the sale
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="lineNumber">Line number</param>
        /// <returns>HTTP Response from the API</returns>
        Task<HttpResponseMessage> RemoveSaleLine(int tillNumber,
            int saleNumber, int lineNumber);

        Task<HttpResponseMessage> SetTaxExemption(HttpContent content);

        Task<HttpResponseMessage> ValidateVoidSale();
    }
}