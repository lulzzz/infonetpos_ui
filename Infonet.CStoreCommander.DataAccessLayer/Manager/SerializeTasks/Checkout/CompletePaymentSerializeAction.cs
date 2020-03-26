using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class CompletePaymentSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _transactionType;
        private readonly bool _issueStoreCredit;

        public CompletePaymentSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager, string transactionType, bool issueStoreCredit)
            : base("CompletePayment")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _transactionType = transactionType;
            _issueStoreCredit = issueStoreCredit;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                registerNumber = _cacheManager.RegisterNumber,
                transactionType = _transactionType,
                issueStoreCredit = _issueStoreCredit
            };

            var payload = JsonConvert.SerializeObject(contract);
            var content = new StringContent(payload, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.CompletePayment(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var paymentComplete = new DeSerializer().MapCompletePayment(data);
                    return new Mapper().MapCompletePayment(paymentComplete);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
