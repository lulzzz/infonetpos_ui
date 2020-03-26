using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.GiveX;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class GiveXSerializeManager : SerializeManager, IGiveXSerializeManager
    {
        private readonly IGiveXRestClient _restClient;
        private readonly ICacheManager _cachemanager;

        public GiveXSerializeManager(IGiveXRestClient restClient,
            ICacheManager cachemanager)
        {
            _restClient = restClient;
            _cachemanager = cachemanager;
        }

        public async Task<Report> CloseBatch()
        {
            var action = new CloseBatchSerializeAction(_restClient, _cachemanager);
            await PerformTask(action);
            return (Report)action.ResponseValue;
        }

        /// <summary>
        /// Method to deactivate givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>salemodel</returns>
        public async Task<GivexSaleCard> DeactivateGivexCard(GiveXCard givexCard)
        {
            var action = new DeactivateGivexCardSerializeAction(_restClient, givexCard);
            await PerformTask(action);
            return (GivexSaleCard)action.ResponseValue;
        }

        /// <summary>
        /// method to get balance for given givex card
        /// </summary>
        /// <param name="givexCardNumber"></param>
        /// <returns>card balance for given card number</returns>
        public async Task<GiveXCardBalance> GetCardBalance(string givexCardNumber)
        {
            var action = new GetBalanceSerializeAction(_restClient, givexCardNumber);
            await PerformTask(action);
            return (GiveXCardBalance)action.ResponseValue;
        }

        /// <summary>
        /// Method to activate givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>sale model</returns>
        public async Task<GivexSaleCard> ActivateGivexCard(GiveXCard givexCard)
        {
            var action = new ActivateGivexCardSerializeAction(_restClient, givexCard);
            await PerformTask(action);
            return (GivexSaleCard)action.ResponseValue;
        }

        /// <summary>
        /// Method to add amount in givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns>sale model</returns>
        public async Task<GivexSaleCard> AddAmount(GiveXCard givexCard)
        {
            var action = new AddAmountSerializeAction(_restClient, givexCard);
            await PerformTask(action);
            return (GivexSaleCard)action.ResponseValue;
        }

        /// <summary>
        /// Method to set amount in givex card
        /// </summary>
        /// <param name="givexCard"></param>
        /// <returns></returns>
        public async Task<GivexSaleCard> SetAmount(GiveXCard givexCard)
        {
            var action = new SetAmountInGiveXCardSerializeAction(_restClient, givexCard);
            await PerformTask(action);
            return (GivexSaleCard)action.ResponseValue;
        }

        /// <summary>
        /// method to get stock code for givex
        /// </summary>
        /// <returns>stock code</returns>
        public async Task<string> GetStockCode()
        {
            var action = new GetGivexStockCodeSerializeAction(_restClient);
            await PerformTask(action);
            return (string)action.ResponseValue;
        }

        public async Task<GiveXReport> GetGiveXReport(string date)
        {
            var action = new GetGiveXReportSerializeAction(_restClient, date);
            await PerformTask(action);
            return (GiveXReport)action.ResponseValue;            
        }
    }
}
