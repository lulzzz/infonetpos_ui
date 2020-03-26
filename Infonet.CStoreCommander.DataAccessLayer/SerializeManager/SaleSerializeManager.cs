using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Home;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Sale;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    /// <summary>
    /// Interface for Sale Serialization manager
    /// </summary>
    public class SaleSerializeManager : SerializeManager, ISaleSerializeManager
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly ICacheManager _cacheManager;

        public SaleSerializeManager(ISaleRestClient saleRestClient,
            ICacheManager cacheManager)
        {
            _saleRestClient = saleRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<Sale> InitializeNewSale()
        {
            var action = new InitializeNewSaleSerializeAction(_saleRestClient,
                _cacheManager);

            await PerformTask(action);

            var sale = (Sale)action.ResponseValue;
            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;

            return sale; 
        }

        public async Task<VoidSale> VoidSale(Reasons reason)
        {
            var action = new VoidSaleSerializeAction(_saleRestClient,
                _cacheManager, reason);

            await PerformTask(action);

            return (VoidSale)action.ResponseValue;
        }

        /// <summary>
        /// Method to get all suspended sale
        /// </summary>
        /// <returns>list of suspended sales</returns>
        public async Task<List<SuspendedSale>> GetAllSuspendSale()
        {
            var action = new GetAllSuspendSaleSerializeAction(_saleRestClient);

            await PerformTask(action);

            return (List<SuspendedSale>)action.ResponseValue;
        }

        /// <summary>
        /// Method to unsuspended sale
        /// </summary>
        /// <param name="saleNumber"></param>
        /// <param name="tillNumber"></param>
        /// <returns></returns>
        public async Task<Sale> UnsuspendSale(int saleNumber)
        {
            var action = new UnsuspendSaleSerializeAction(_saleRestClient, saleNumber);

            await PerformTask(action);

           var sale = (Sale)action.ResponseValue;

            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;

            return sale;
        }

        /// <summary>
        /// method to suspend sale current sale
        /// </summary>
        /// <returns></returns>
        public async Task<SuspendedSale> SuspendSale()
        {
            var action = new SuspendSaleSerializeAction(_saleRestClient);

            await PerformTask(action);

            return (SuspendedSale)action.ResponseValue;
        }

        /// <summary>
        /// Method to get sale list
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns>list of sales</returns>
        public async Task<List<SaleList>> GetSaleList(int pageIndex)
        {
            var action = new GetSaleListSerializeAction(_saleRestClient, pageIndex);

            await PerformTask(action);

            return (List<SaleList>)action.ResponseValue;
        }

        /// <summary>
        /// Verifies the Stock added for the sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="stockCode">Stock code of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="isReturn">Is Return Mode</param>
        /// <returns>VerifyStock model</returns>
        public async Task<object> VerifyStock(int saleNumber,
            int tillNumber, int registerNumber, string stockCode, float quantity, GiftCard giftcard, bool isReturn, bool isManuallyAdded)

        {
            var action = new VerifyStockSerializeAction(
                _saleRestClient, saleNumber, tillNumber, registerNumber,
                stockCode, quantity, giftcard, isReturn, isManuallyAdded);

            await PerformTask(action);
            var response = action.ResponseValue;

            if (response.GetType() == typeof(Sale))
            {
                var sale = (Sale)action.ResponseValue;
                _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            }

            return action.ResponseValue;
        }

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
        public async Task<Sale> AddStockToSale(int saleNumber,
            int tillNumber, int registerNumber, string stockCode,
            float quantity, bool isReturn, GiftCard giftCard, bool isManuallyAdded)
        {
            var action = new AddStockToSaleSerializeAction(
                _saleRestClient, saleNumber, tillNumber,
                registerNumber, stockCode, quantity, isReturn, giftCard, isManuallyAdded);

            await PerformTask(action);

            var sale = (Sale)action.ResponseValue;
            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            return sale;
        }

        /// <summary>
        /// Method to search sale list by date and sale number
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="saleNumber"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<SaleList>> SearchSaleList(int pageIndex, int saleNumber, string date)
        {
            var action = new SearchSaleListSerializeAction(_saleRestClient, pageIndex, saleNumber, date);
            await PerformTask(action);
            return (List<SaleList>)action.ResponseValue;
        }

        /// <summary>
        /// Method to return sale
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <param name="isCorrection"></param>
        /// <returns></returns>
        public async Task<GivexSaleCard> ReturnSale(int tillNumber, int saleTillNumber,
            int saleNumber, bool isCorrection, string reasonCode, string reason)
        {
            var action = new ReturnSaleSerializeAction(_saleRestClient, saleNumber, tillNumber,
                saleTillNumber, isCorrection, reasonCode, reason);
            await PerformTask(action);
            return (GivexSaleCard)action.ResponseValue;
        }

        /// <summary>
        /// Method to get sale by sale number
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <returns></returns>
        public async Task<Sale> GetSaleByNumber(int tillNumber, int saleNumber)
        {
            var action = new GetSaleBySaleNumberSerializeAction(_saleRestClient, tillNumber, saleNumber);
            await PerformTask(action);
            var sale = (Sale)action.ResponseValue;

            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            return sale;
        }

        /// <summary>
        /// Method to return sale items
        /// </summary>
        /// <param name="tillNumber"></param>
        /// <param name="saleNumber"></param>
        /// <param name="lineNumbers"></param>
        /// <returns>sale model</returns>
        public async Task<Sale> ReturnSaleItems(int tillNumber,
            int saleTillNumber, int saleNumber, List<int> lineNumbers,
            string reasonCode, string reasonType)
        {
            var action = new ReturnSaleItemsSerializeAction(_saleRestClient, saleNumber,
                tillNumber, saleTillNumber, lineNumbers, reasonCode, reasonType);
            await PerformTask(action);
            var sale =  (Sale)action.ResponseValue;

            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            return sale;

        }

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
        public async Task<Sale> UpdateSaleLine(int tillNumber, int saleNumber, int lineNumber,
            byte registerNumber, string discount, string discountType, string quantity,
            string price, string reason, string reasonType)
        {
            var action = new UpdateSaleItemSerializeAction(_saleRestClient, tillNumber, saleNumber, lineNumber,
            registerNumber, discount, discountType, quantity, price, reason, reasonType);
            await PerformTask(action);
            var sale = (Sale)action.ResponseValue;
            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            return sale;
        }

        /// <summary>
        /// Removes the sale line from the Sale
        /// </summary>
        /// <param name="saleNumber">Sale number</param>
        /// <param name="tillNumber">Till number</param>
        /// <param name="lineNumber">Line number</param>
        /// <returns>Updated sale model</returns>
        public async Task<Sale> RemoveSaleLine(int saleNumber, int tillNumber, int lineNumber)
        {
            var action = new RemoveSaleItemSerializeAction(_saleRestClient,
                tillNumber, saleNumber, lineNumber);
            await PerformTask(action);
            var sale = (Sale)action.ResponseValue;
            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;
            return sale;
        }

        /// <summary>
        /// Writes off the current sale
        /// </summary>
        /// <param name="reasonForWriteOff">Reason for Writing off the sale</param>
        /// <returns>Write off Model</returns>
        public async Task<WriteOff> WriteOff(Reasons reasonForWriteOff)
        {
            var action = new WriteOffSerializeAction(_saleRestClient,
                _cacheManager,
                reasonForWriteOff);
            await PerformTask(action);
            return (WriteOff)action.ResponseValue;
        }

        public async Task<Sale> SetTaxExemption(string taxExemptionNumber)
        {
            var action = new SetTaxExemptionSerializeAction(_saleRestClient,
                  _cacheManager,
                  taxExemptionNumber);
            await PerformTask(action);
            var sale = (Sale)action.ResponseValue;
            _cacheManager.HasCarwashProductsInSale = sale.HasCarwashProducts;

            return sale;
        }

        public async Task<bool> ValidateVoidSale()
        {
            var action = new ValidateVoidSerializeAction(_saleRestClient);
            await PerformTask(action);
            return (bool)action.ResponseValue;
        }
    }
}
