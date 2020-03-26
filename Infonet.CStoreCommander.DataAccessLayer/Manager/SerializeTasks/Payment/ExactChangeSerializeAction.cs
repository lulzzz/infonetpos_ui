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
    public class ExactChangeSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly ICacheManager _cacheManager;

        public ExactChangeSerializeAction(IPaymentRestClient paymentRestClient,
            ICacheManager cacheManager)
            : base("ExactChange")
        {
            _paymentRestClient = paymentRestClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var payByExactChangeContract = new PayByExactChangeContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };

            var reason = JsonConvert.SerializeObject(payByExactChangeContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _paymentRestClient.PayByExactChange(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var exactChangeContract = new DeSerializer().MapExactChange(data);
                    return new Mapper().MapExactChange(exactChangeContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
