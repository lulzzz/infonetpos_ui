using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class PaymentByGivexSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _checkoutRestClient;
        private readonly ICacheManager _cacheManager;
        private GivexPaymentContract _givexPaymentContract;

        public PaymentByGivexSerializeAction(ICheckoutRestClient checkoutRestClient,
            ICacheManager cacheManager, string cardNumber, decimal? amount,
            string transactionType, string tenderCode) : base("SaveGivexPayment")
        {
            _checkoutRestClient = checkoutRestClient;
            _cacheManager = cacheManager;
            _givexPaymentContract = new GivexPaymentContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                tenderCode = tenderCode,
                amount = amount.HasValue ? amount.Value.ToString(CultureInfo.InvariantCulture) : null,
                givexCardNumber = cardNumber,
                transactionType = transactionType
            };
        }

        protected async override Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_givexPaymentContract)
                , Encoding.UTF8, ApplicationJSON);
            var response = await _checkoutRestClient.PaymentByGivex(content);
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
