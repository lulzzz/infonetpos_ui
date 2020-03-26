using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment
{
    public class GetARCustomerByCustomerCodeSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly string _cardNumber;

        public GetARCustomerByCustomerCodeSerializeAction(IPaymentRestClient paymentRestClient,
            string cardNumber)
            : base("GetARCustomerByCustomerCode")
        {
            _paymentRestClient = paymentRestClient;
            _cardNumber = cardNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var arContract = JsonConvert.SerializeObject(_cardNumber);
            var content = new StringContent(arContract, Encoding.UTF8, ApplicationJSON);

            var response = await _paymentRestClient.SearchARCustomer(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapARCustomer(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
