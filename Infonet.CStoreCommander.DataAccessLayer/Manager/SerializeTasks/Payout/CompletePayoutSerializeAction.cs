using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Payout;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payout
{
    public class CompletePayoutSerializeAction : SerializeAction
    {
        private readonly IPayoutRestClient _payoutRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly CompletePayoutContract _completePayoutContract;

        public CompletePayoutSerializeAction(IPayoutRestClient payoutRestClient,
            ICacheManager cacheManager,
            List<Tax> taxes,
            string vendorCode,
            string amount,
            string reasonCode)
            : base("CompletePayout")
        {
            _cacheManager = cacheManager;
            _payoutRestClient = payoutRestClient;
            _completePayoutContract = new CompletePayoutContract
            {
                registerNumber = _cacheManager.RegisterNumber,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                amount = amount,
                reasonCode = reasonCode,
                vendorCode = vendorCode,
                taxes = (from s in taxes
                         select new TaxContract
                         {
                             amount = s.Amount,
                             code = s.Code,
                             description = s.Description
                         }).ToList()
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_completePayoutContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _payoutRestClient.PayoutComplete(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapCommonCompletePayment(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
