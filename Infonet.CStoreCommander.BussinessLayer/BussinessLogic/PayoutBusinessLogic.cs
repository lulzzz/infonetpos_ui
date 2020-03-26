using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class PayoutBusinessLogic : IPayoutBusinessLogic
    {
        private readonly IPayoutSerializeManager _seralizemanager;
        private readonly ICacheBusinessLogic _cacheManager;
        private readonly IReportsBussinessLogic _reportBussinessLogic;

        public PayoutBusinessLogic(IPayoutSerializeManager seralizemanager,
            ICacheBusinessLogic cacheManager,
            IReportsBussinessLogic reportBussinessLogic)
        {
            _seralizemanager = seralizemanager;
            _cacheManager = cacheManager;
            _reportBussinessLogic = reportBussinessLogic;
        }

        public async Task<CommonPaymentComplete> CompletePayout(List<Tax> taxes,
            string vendorCode, string amount, string reasonCode)
        {
            var response = await _seralizemanager.CompletePayout(taxes,
                string.IsNullOrEmpty(vendorCode) ? string.Empty : vendorCode,
                string.IsNullOrEmpty(amount) ? string.Empty : amount,
                string.IsNullOrEmpty(reasonCode) ? string.Empty : reasonCode);

            await _reportBussinessLogic.SaveReport(response.Receipt);
            return response;
        }

        public async Task<VendorPayout> GetVendorPayout()
        {
            return await _seralizemanager.GetVendorPayout();
        }
    }
}
