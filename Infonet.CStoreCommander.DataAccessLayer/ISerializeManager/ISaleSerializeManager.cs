using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    /// <summary>
    /// Interface for Sale Serialization manager
    /// </summary>
    public interface ISaleSerializeManager
    {
        /// <summary>
        /// Initializes a new sale
        /// </summary>
        /// <returns>Sale</returns>
        Task<Sale> InitializeNewSale();

        /// <summary>
        /// Method to void sale
        /// </summary>
        /// <returns></returns>
        Task<VoidSale> VoidSale(Reasons reason);

        /// <summary>
        /// Method to get all suspend sale
        /// </summary>
        /// <returns>list of suspend sale</returns>
        Task<List<SuspendedSale>> GetAllSuspendSale();

        /// <summary>
        /// Method to unsuspended sale
        /// </summary>
        /// <param name="saleNumber"></param>
        /// <param name="tillNumber"></param>
        /// <returns></returns>
        Task<Sale> UnsuspendSale(int saleNumber);

        /// <summary>
        /// Method to suspend Sale
        /// </summary>
        /// <returns></returns>
        Task<SuspendedSale> SuspendSale();

        /// <summary>
        /// Method to get sale list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns>list of sale list</returns>
        Task<List<SaleList>> GetSaleList(int pageIndex);

        /// <summary>
        /// Verifies the Stock added for the sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="stockCode">Stock code of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="isReturn">Is Return Mode</param>
        /// <returns>VerifyStock model</returns>
        Task<object> VerifyStock(int saleNumber,
            int tillNumber, int registerNumber, string stockCode, float quantity, GiftCard giftcard, bool isReturn, bool isManuallyAdded);

        /// <summary>
        /// Adds a stock to the sale 
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="stockCode">Stock code</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="isReturn">Is return</param>
        /// <param name="giftCard">Gift card</param>
        /// <returns>Sale model of the updated sale</returns>
        Task<Sale> AddStockToSale(int saleNumber, int tillNumber,
            int registerNumber, string stockCode, float quantity,
            bool isReturn, GiftCard giftCard, bool isManuallyAdded);

        /// <summary>
        /// method to search sale list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="saleNumber"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<List<SaleList>> SearchSaleList(int pageIndex, int saleNumber, string date);


        /// <summary>
        /// Method to return sale
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <param name="isCorrection"></param>
        /// <returns>returns sale</returns>
        Task<GivexSaleCard> ReturnSale(int tillNumber, int saleTillNumber,
            int saleNumber, bool isCorrection, string reasonCode, string reason);

        /// <summary>
        /// Method to get sale by sale number
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <returns></returns>
        Task<Sale> GetSaleByNumber(int tillNumber, int saleNumber);


        /// <summary>
        /// Method to return sale items
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <param name="lineNumbers"></param>
        /// <returns>SaleModel</returns>
        Task<Sale> ReturnSaleItems(int tillNumber,
            int saleTillNumber, int saleNumber, List<int> lineNumbers,
            string reasonCode, string reasonType);

        /// <summary>
        /// Updates the sale line of the current sale with the supplied values
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="lineNumber">Line number</param>
        /// <param name="registerNumber">Register number</param>
        /// <param name="discount">Discount</param>
        /// <param name="discountType">Discount type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Price</param>
        /// <param name="reason">Reason code</param>
        /// <param name="reasonType">Reason type</param>
        /// <returns>Updated Sale</returns>
        Task<Sale> UpdateSaleLine(int tillNumber, int saleNumber, int lineNumber,
            byte registerNumber, string discount, string discountType, string quantity,
            string price, string reason, string reasonType);

        /// <summary>
        /// Removes the sale line from the Sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="lineNumber">Line number</param>
        /// <returns>Updated sale model</returns>
        Task<Sale> RemoveSaleLine(int saleNumber, int tillNumber, int lineNumber);

        /// <summary>
        /// Writes off the current sale
        /// </summary>
        /// <param name="reasonForWriteOff">Reason for Writing off the sale</param>
        /// <returns>Write off Model</returns>
        Task<WriteOff> WriteOff(Reasons reasonForWriteOff);

        Task<Sale> SetTaxExemption(string taxExemptionNumber);

        Task<bool> ValidateVoidSale();
    }
}
