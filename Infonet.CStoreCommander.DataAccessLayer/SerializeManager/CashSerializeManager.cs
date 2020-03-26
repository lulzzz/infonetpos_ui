using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Cash;
using Infonet.CStoreCommander.EntityLayer.Entities.Cash;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Reports;
using System.Collections.Generic;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class CashSerializeManager : SerializeManager, ICashSerializeManager
    {
        private readonly ICashRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public CashSerializeManager(ICashRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        public async Task<CashDrawTypes> GetCashDrawTypes()
        {
            var action = new GetCashDrawTypesSerializeAction(_restClient);

            await PerformTask(action);

            return (CashDrawTypes)action.ResponseValue;
        }

        public async Task<Report> CompleteCashDraw(CompleteCashDraw completeCashDraw)
        {
            var action = new CompleteCashDrawSerializeAction(_restClient, completeCashDraw);

            await PerformTask(action);

            return (Report)action.ResponseValue;
        }

        public async Task<List<Tender>> GetAllTenders(string transactionType,
            bool billTillClose, string dropreason)
        {
            var action = new GetAllTendersSerializeAction(_restClient,
                transactionType, billTillClose, dropreason);

            await PerformTask(action);

            return (List<Tender>)action.ResponseValue;
        }

        public async Task<List<CashButtons>> GetCashButtons()
        {
            var action = new GetCashButtonsSerializeAction(_restClient);

            await PerformTask(action);

            return (List<CashButtons>)action.ResponseValue;
        }

        public async Task<UpdateTenderGet> UpdateTender(UpdateTenderPost updatedTender)
        {
            var action = new UpdateTenderForCashDropSerializeAction(_restClient,
                updatedTender, _cacheManager);

            await PerformTask(action);

            return (UpdateTenderGet)action.ResponseValue;
        }

        public async Task<Report> CompleteCashDrop(CompleteCashDrop completeCashDrop)
        {
            var action = new CompleteCashDropSerializeAction(_restClient,
               completeCashDrop);

            await PerformTask(action);

            return (Report)action.ResponseValue;
        }

        public async Task<bool> OpenCashDrawer(string openDrawerReason)
        {
            var action = new OpenCashDrawerSerializeAction(_restClient,
               _cacheManager,openDrawerReason);

            await PerformTask(action);

            return (bool)action.ResponseValue;
        }
    }
}
