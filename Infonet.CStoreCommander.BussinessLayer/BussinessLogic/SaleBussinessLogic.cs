using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Business operations for Sales
    /// </summary>
    public class SaleBussinessLogic : ISaleBussinessLogic
    {
        private readonly ISaleSerializeManager _serializeManager;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager">Sale serialize manager</param>
        /// <param name="cacheBusinessLogic">Cache Business Logic Manager</param>
        public SaleBussinessLogic(ISaleSerializeManager serializeManager,
            ICacheBusinessLogic cacheBusinessLogic,
            IReportsBussinessLogic reportsBusinessLogic)
        {
            _serializeManager = serializeManager;
            _cacheBusinessLogic = cacheBusinessLogic;
            _reportsBusinessLogic = reportsBusinessLogic;
        }

        /// <summary>
        /// Returns all suspended sales
        /// </summary>
        /// <returns>List of Suspended sales</returns>
        public async Task<List<SuspendedSale>> GetAllSuspendSales()
        {
            return await _serializeManager.GetAllSuspendSale();
        }

        /// <summary>
        /// Suspends the ongoing sale
        /// </summary>
        /// <returns>Suspended sale</returns>
        public async Task<SuspendedSale> SuspendSale()
        {
            return await _serializeManager.SuspendSale();
        }

        /// <summary>
        /// Unsuspend a sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <returns></returns>
        public async Task<Sale> UnsuspendSale(int saleNumber)
        {
            return await _serializeManager.UnsuspendSale(saleNumber);
        }

        /// <summary>
        /// Voids the ongoing sale
        /// </summary>
        /// <param name="reason">Reason for voiding sale</param>
        /// <returns>Updated sale model</returns>
        public async Task<VoidSale> VoidSale(Reasons reason)
        {
            var response = await _serializeManager.VoidSale(reason);

            if (response.Receipt != null)
            {
                await _reportsBusinessLogic.SaveReport(response.Receipt);
            }

            return response;
        }

        /// <summary>
        /// Initializes a new sale
        /// </summary>
        /// <returns>Updated sale model</returns>
        public async Task<Sale> InitializeNewSale()
        {
            return await _serializeManager.InitializeNewSale();
        }

        /// <summary>
        /// Returns list of completed sales
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <returns>List of sales</returns>
        public async Task<List<SaleList>> GetSaleList(int pageIndex)
        {
            return await _serializeManager.GetSaleList(pageIndex);
        }

        /// <summary>
        /// Adds the Specified stock code into the Current sale
        /// </summary>
        /// <param name="stockCode">Stock code of the Product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <returns>Verify stock model</returns>
        public async Task<object> VerifyStockForSale(string stockCode,
            float quantity, GiftCard giftCard, bool isManuallyAdded)
        {
            return await _serializeManager
                .VerifyStock(_cacheBusinessLogic.SaleNumber,
                _cacheBusinessLogic.TillNumberForSale, _cacheBusinessLogic.RegisterNumber,
                stockCode, quantity, giftCard,
                _cacheBusinessLogic.IsReturn, isManuallyAdded);
            
        }

        /// <summary>
        /// Adds a stock item to an ongoing sale
        /// </summary>
        /// <param name="stockCode">Stock code of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="isReturn">Is return mode</param>
        /// <param name="giftCard">Gift card</param>
        /// <returns>Updated sale model</returns>
        public async Task<Sale> AddStockToSale(string stockCode,
            float quantity, bool isReturn, GiftCard giftCard, bool isManuallyAdded)
        {
            // Setting Gift card quantity for sale if Gift card is send
            if (giftCard != null && giftCard.GiftNumber != 0)
            {
                quantity = giftCard.Quantity;
                _cacheBusinessLogic.GiftNumber = giftCard.GiftNumber + 1;
            }

            return await _serializeManager
                .AddStockToSale(_cacheBusinessLogic.SaleNumber,
                    _cacheBusinessLogic.TillNumberForSale, 1, stockCode,
                    quantity, isReturn, giftCard, isManuallyAdded);
        }

        /// <summary>
        /// Deletes the Sale line from the ongoing sale
        /// </summary>
        /// <param name="lineNumber">Line number of the sale line</param>
        /// <returns>Updated sale model</returns>
        public async Task<Sale> RemoveSaleLine(int lineNumber)
        {
            return await _serializeManager
                .RemoveSaleLine(_cacheBusinessLogic.SaleNumber,
                    _cacheBusinessLogic.TillNumberForSale, lineNumber);
        }

        /// <summary>
        /// Searches for completed sales
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="date">Date</param>
        /// <returns></returns>
        public async Task<List<SaleList>> SearchSaleList(int pageIndex,
            int saleNumber, string date)
        {
            return await _serializeManager.SearchSaleList(pageIndex,
                saleNumber, date);
        }

        /// <summary>
        /// Returns a sale
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="isCorrection">Sale is for correction or not</param>
        /// <returns>sale model</returns>
        public async Task<GivexSaleCard> ReturnSale(int tillNumber,
            int saleTillNumber, int saleNumber, bool isCorrection,
            string reasonCode, string reason)
        {
            return await _serializeManager.ReturnSale(tillNumber, saleTillNumber,
                saleNumber, isCorrection, reasonCode, reason);
        }

        /// <summary>
        /// Returns Sale by sale number
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <returns></returns>
        public async Task<Sale> GetSaleBySaleNumber(int tillNumber,
            int saleNumber)
        {
            var sale = await _serializeManager.GetSaleByNumber(tillNumber, saleNumber);
            return sale;
        }

        /// <summary>
        /// Returns sale items
        /// </summary>
        /// <param name="tillNumber">Till number</param>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="lineNumbers">List of Line numbers to be returned</param>
        /// <returns>sale object</returns>
        public async Task<Sale> ReturnSaleItems(int tillNumber,
            int saleTillNumber, int saleNumber,
            List<int> lineNumbers, string reasonCode, string reasonType)
        {
            return await _serializeManager.ReturnSaleItems(tillNumber,
                saleTillNumber, saleNumber, lineNumbers, reasonCode, reasonType);
        }

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
        public async Task<Sale> UpdateSale(int lineNumber, string discount, string discountType,
            string quantity, string price, string reason, string reasonType)
        {
            return await _serializeManager.UpdateSaleLine(
                _cacheBusinessLogic.TillNumberForSale,
                _cacheBusinessLogic.SaleNumber,
                lineNumber, _cacheBusinessLogic.RegisterNumber,
                discount, discountType, quantity,
                price, reason, reasonType);
        }

        /// <summary>
        /// Writes off the current sale
        /// </summary>
        /// <param name="reasonForWriteOff">Reason for writing off the sale</param>
        /// <returns>New WriteOff Model</returns>
        public async Task<WriteOff> WriteOff(Reasons reasonForWriteOff)
        {
            var writeOff = await _serializeManager.WriteOff(reasonForWriteOff);

            await _reportsBusinessLogic.SaveReport(writeOff.Receipt);

            return writeOff;
        }

        public async Task<Sale> SetTaxExemption(string taxExemptionNumber)
        {
            return await _serializeManager.SetTaxExemption(taxExemptionNumber);
        }

        public async Task<bool> ValidateVoidSale()
        {
            return await _serializeManager.ValidateVoidSale();
        }
    }
}
