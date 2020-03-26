using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Payment;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment
{
    public class PaymentByFleetSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly PaymentByFleetContract _paymentByFleetContract;

        public PaymentByFleetSerializeAction(IPaymentRestClient paymentRestClient,
            ICacheManager cacheManager,
            string cardNumber, string amount, bool isSwipe)
            : base("PaymentByFleet")
        {
            _paymentRestClient = paymentRestClient;
            _cacheManager = cacheManager;

            _paymentByFleetContract = new PaymentByFleetContract
            {
                amount = amount,
                cardNumber = cardNumber,
                isSwiped = isSwipe,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_paymentByFleetContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _paymentRestClient.FleetPayment(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleSumarry = new DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(saleSumarry);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
