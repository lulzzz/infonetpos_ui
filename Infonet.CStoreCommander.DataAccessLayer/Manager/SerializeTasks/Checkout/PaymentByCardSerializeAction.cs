using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class PaymentByCardSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly PaymentContract _paymentContract;

        public PaymentByCardSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager,  string tenderCode,
            string amountUsed,  string transactionType,
        string cardNumber, string poNumber = "") 
            : base("PaymentByCard")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;

            _paymentContract = new PaymentContract
            {
                tenderCode = tenderCode,
                transactionType = transactionType,
                amountUsed = amountUsed,
                cardNumber = cardNumber,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                poNumber = poNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_paymentContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.PaymentByCard(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var tenderSummary = new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(tenderSummary);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
