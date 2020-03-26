using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    /// <summary>
    /// Contains business operations for GiveX
    /// </summary>
    public class GiveXBussinessLogic : IGiveXBussinessLogic
    {
        private readonly IGiveXSerializeManager _serializeManager;
        private readonly IReportsBussinessLogic _reportsBusinessLogic;
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serializeManager">GiveX Serialize manager</param>
        /// <param name="cacheManager">Cache manager</param>
        public GiveXBussinessLogic(IGiveXSerializeManager serializeManager,
            ICacheManager cacheManager, IReportsBussinessLogic reportsBusinessLogic)
        {
            _serializeManager = serializeManager;
            _cacheManager = cacheManager;
            _reportsBusinessLogic = reportsBusinessLogic;
        }

        /// <summary>
        /// Gets card balance for the given card number
        /// </summary>
        /// <param name="givexCardNumber">Card number of the GiveX card</param>
        /// <returns>New model of the GiveX Card balance</returns>
        public async Task<GiveXCardBalance> CardBalance(
            string givexCardNumber)
        {
            return await _serializeManager.GetCardBalance(givexCardNumber);
        }

        /// <summary>
        /// Deactivates GiveX card
        /// </summary>
        /// <param name="givexCardNumber">Card number of the GiveX</param>
        /// <param name="givexPrice">Price of the GiveX</param>
        /// <returns>Updated sale object</returns>
        public async Task<GivexSaleCard> DeactivateCard(
            string givexCardNumber, decimal givexPrice,
            string stockCodeForGivexCard)
        {
            var givexCard = MapGivexModel(givexCardNumber, givexPrice,
                stockCodeForGivexCard);
            return await _serializeManager.DeactivateGivexCard(givexCard);
        }

        /// <summary>
        /// Closes the batch
        /// </summary>
        /// <returns>Success model</returns>
        public async Task<Report> CloseBatch()
        {
            var report = await _serializeManager.CloseBatch();
            await _reportsBusinessLogic.SaveReport(report);
            return report;
        }

        /// <summary>
        /// Activates GiveX card
        /// </summary>
        /// <param name="givexCardNumber">GiveX Card number</param>
        /// <param name="givexPrice">GiveX Price</param>
        /// <returns>Updated sale object</returns>
        public async Task<GivexSaleCard> ActivateCard(string givexCardNumber,
            decimal givexPrice, string stockCodeForGivexCard)
        {
            var givexCard = MapGivexModel(givexCardNumber, givexPrice,
                stockCodeForGivexCard);
            return await _serializeManager.ActivateGivexCard(givexCard);
        }

        /// <summary>
        /// Adds amount to GiveX card
        /// </summary>
        /// <param name="givexCardNumber">GiveX card number</param>
        /// <param name="givexPrice">GiveX card price</param>
        /// <returns>Update sale object</returns>
        public async Task<GivexSaleCard> AddAmount(string givexCardNumber,
            decimal givexPrice, string stockCodeForGivexCard)
        {
            var givexCard = MapGivexModel(givexCardNumber, givexPrice,
                stockCodeForGivexCard);
            return await _serializeManager.AddAmount(givexCard);
        }

        /// <summary>
        /// Sets amount for a GiveX card
        /// </summary>
        /// <param name="givexCardNumber">GiveX Card number</param>
        /// <param name="givexPrice">GiveX Price</param>
        /// <returns>Updated sale model</returns>
        public async Task<GivexSaleCard> SetAmount(string givexCardNumber,
            decimal givexPrice, string stockCodeForGivexCard)
        {
            var givexCard = MapGivexModel(givexCardNumber, givexPrice,
                stockCodeForGivexCard);
            return await _serializeManager.SetAmount(givexCard);
        }

        /// <summary>
        /// Creates GiveX model from the data
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <param name="givexPrice"></param>
        /// <returns>GiveXCardModel</returns>
        private GiveXCard MapGivexModel(string givexCardNumber,
            decimal givexPrice, string stockCodeForGivexCard)
        {
            return new GiveXCard
            {
                GivexCardNumber = givexCardNumber,
                GivexPrice = givexPrice,
                SaleNumber = _cacheManager.SaleNumber,
                TillNumber = _cacheManager.TillNumberForSale,
                StockCodeForGivexCard = stockCodeForGivexCard
            };
        }

        /// <summary>
        /// method to get stock code for GiveX
        /// </summary>
        /// <returns>Stock code</returns>
        public async Task<string> GetGiveXStockCode()
        {
            return await _serializeManager.GetStockCode();
        }

        public async Task<GiveXReport> GetGiveXReport(string date)
        {
            return await _serializeManager.GetGiveXReport(date);
        }
    }
}
