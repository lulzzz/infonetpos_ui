using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface ISaleBussinessLogic
    {
        /// <summary>
        /// Method to Void sale
        /// </summary>
        /// <param name="reasonForVoidingSale"></param>
        /// <returns></returns>
        Task<VoidSale> VoidSale(Reasons reasonForVoidingSale);

        /// <summary>
        /// Method to get all suspend sale
        /// </summary>
        /// <returns>list of suspend sale</returns>
        Task<List<SuspendedSale>> GetAllSuspendSales();

        /// <summary>
        ///  Method to Unsuspended sale
        /// </summary>
        /// <param name="saleNumber"></param>
        /// <param name="tillNumber"></param>
        /// <returns></returns>
        Task<Sale> UnsuspendSale(int saleNumber);

        /// <summary>
        /// Method to suspend sale
        /// </summary>
        /// <returns></returns>
        Task<SuspendedSale> SuspendSale();

        /// <summary>
        /// Initializes a new sale
        /// </summary>
        /// <returns>New sale</returns>
        Task<Sale> InitializeNewSale();

        /// <summary>
        /// Method to get sale List
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns>list of sale</returns>
        Task<List<SaleList>> GetSaleList(int pageIndex);

        /// <summary>
        /// Adds the Specified stock code into the Current sale
        /// </summary>
        /// <param name="stockCode">Stock code of the Product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>True or False</returns>
        Task<object> VerifyStockForSale(string stockCode,
            float quantity, GiftCard giftCard, bool isManuallyAdded);

        /// <summary>
        /// Adds a stock item to an ongoing sale
        /// </summary>
        /// <param name="stockCode">Stock code of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="isReturn">Is return mode</param>
        /// <param name="giftCard">Gift card</param>
        /// <returns></returns>
        Task<Sale> AddStockToSale(string stockCode, float quantity,
            bool isReturn, GiftCard giftCard, bool isManuallyAdded);

        /// <summary>
        /// Updates the ongoing sale with the applied values
        /// </summary>
        /// <param name="lineNumber">Line number</param>
        /// <param name="discount">Discount</param>
        /// <param name="discountType">Discount type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Price</param>
        /// <param name="reason">Reason</param>
        /// <param name="reasonType">Reason type</param>
        /// <returns>Updated sale</returns>
        Task<Sale> UpdateSale(int lineNumber, string discount, string discountType,
            string quantity, string price, string reason, string reasonType);

        /// <summary>
        /// Deletes the Sale line from the Current sale
        /// </summary>
        /// <param name="lineNumber">Line number of the sale line</param>
        /// <returns>Updated sale model</returns>
        Task<Sale> RemoveSaleLine(int lineNumber);

        /// <summary>
        /// Method to 
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
        /// <returns>SaleModel </returns>
        Task<GivexSaleCard> ReturnSale(int tillNumber, int saleTillNumber,
            int saleNumber, bool isCorrection, string reasonCode, string reason);

        /// <summary>
        /// Method to get sale by sale number
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <returns>sale model</returns>
        Task<Sale> GetSaleBySaleNumber(int tillNumber, int saleNumber);

        /// <summary>
        /// method to return sale items
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <param name="lineNumbers"></param>
        /// <returns>sale model</returns>
        Task<Sale> ReturnSaleItems(int tillNumber,
            int saleTillNumber, int saleNumber, List<int> lineNumbers,
            string reasonCode, string reasonType);

        /// <summary>
        /// Writes off the current sale
        /// </summary>
        /// <param name="reasonForWriteOff">Reason for writing off the sale</param>
        /// <returns>WriteOff model</returns>
        Task<WriteOff> WriteOff(Reasons reasonForWriteOff);

        Task<Sale> SetTaxExemption(string taxExemptionNumber);

        Task<bool> ValidateVoidSale();
    }
}
