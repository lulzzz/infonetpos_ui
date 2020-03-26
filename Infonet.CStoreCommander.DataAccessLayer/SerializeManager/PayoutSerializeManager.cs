using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payout;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payout;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class PayoutSerializeManager : SerializeManager,
        IPayoutSerializeManager
    {
        private readonly IPayoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public PayoutSerializeManager(IPayoutRestClient restClient,
           ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        public async Task<CommonPaymentComplete> CompletePayout(List<Tax> taxes,
            string vendorCode, string amount, string reasonCode)
        {
            var action = new CompletePayoutSerializeAction(_restClient,
                _cacheManager, taxes, vendorCode, amount, reasonCode);

            await PerformTask(action);
            var paymentComplete =
                new Mapper().MapCommonCompletePayment((CommonPaymentCompleteContract)action.ResponseValue);
            return paymentComplete;
        }

        public async Task<VendorPayout> GetVendorPayout()
        {
            var action = new GetVendorPayoutSerializeAction(_restClient, _cacheManager);

            await PerformTask(action);
            var paymentComplete =
                new Mapper().MapVenderPayout((VendorPayoutContract)action.ResponseValue);
            return paymentComplete;
        }
    }
}
