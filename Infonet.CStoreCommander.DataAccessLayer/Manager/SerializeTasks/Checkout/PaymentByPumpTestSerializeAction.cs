using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class PaymentByPumpTestSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _checkoutRestClient;
        private readonly ICacheManager _cacheManager;


        public PaymentByPumpTestSerializeAction(ICheckoutRestClient checkoutRestClient,
            ICacheManager cacheManager) : base("PaymentByPumpTest")
        {
            _checkoutRestClient = checkoutRestClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };

            var content = new StringContent(JsonConvert.SerializeObject(contract)
               , Encoding.UTF8, ApplicationJSON);
            var response = await _checkoutRestClient.PumpTest(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var paymentComplete = new DeSerializer().MapCommonCompletePayment(data);
                    return new Mapper().MapCommonCompletePayment(paymentComplete);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
