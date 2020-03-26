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
    public class PaymentByAccountSerializeAction : SerializeAction
    {
        private readonly PaymentByAccountContract _paymentByAccountContract;
        private readonly ICacheManager _cacheManager;
        private readonly ICheckoutRestClient _restClient;

        public PaymentByAccountSerializeAction(ICheckoutRestClient restClient, 
            ICacheManager cacheManager,
            string purchaseOrder, bool overrideARLimit, string transactionType,
            bool tillClose, string tenderCode, decimal? amountEntered)
            : base("PaymentByAccount")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;

            _paymentByAccountContract = new PaymentByAccountContract
            {
                overrideArLimit = overrideARLimit,
                purchaseOrder = purchaseOrder,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                tillClose = tillClose,
                transactionType = transactionType,
                tender = new PaymentByAccountTender
                {
                    amountEntered = amountEntered.HasValue ? amountEntered.Value.ToString(CultureInfo.InvariantCulture): null,
                    tenderCode = tenderCode
                }
            };
        }

        protected async override Task<object> OnPerform()
        {
            var contract = JsonConvert.SerializeObject(_paymentByAccountContract);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.PaymentByAccount(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var paymentComplete =  new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(paymentComplete);
                case HttpStatusCode.NotFound:
                    throw PurchaseOrderRequiredException(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
