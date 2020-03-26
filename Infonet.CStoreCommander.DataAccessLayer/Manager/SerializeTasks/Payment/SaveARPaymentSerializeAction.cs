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
    public class SaveARPaymentSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly ICacheManager _cacheManager;
        private ARPaymentContract _arPayment;

        public SaveARPaymentSerializeAction(IPaymentRestClient paymentRestClient,
            ICacheManager cacheManager,
            string customerCode, string amount)
            : base("SaveARPayment")
        {
            _paymentRestClient = paymentRestClient;
            _cacheManager = cacheManager;
            _arPayment= new ARPaymentContract
            {
                customerCode = customerCode,
                amount = amount,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                registerNumber = _cacheManager.RegisterNumber,
                isReturnMode = _cacheManager.IsReturn
            };
        }

        protected async override Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_arPayment)
                , Encoding.UTF8, ApplicationJSON);

            var response = await _paymentRestClient.SaveARPayment(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleSummary = new DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(saleSummary);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
