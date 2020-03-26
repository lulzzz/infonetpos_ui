using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Customer;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    public class GetCustomerByCardSerializeAction : SerializeAction
    {
        private readonly ICustomerRestClient _restClient;
        private readonly CustomerCardContract _customerContract;

        public GetCustomerByCardSerializeAction(
            ICustomerRestClient customerRestClient,
            string cardNumber,
            bool isLoyaltycard,
            int saleNumber,
            int tillNumber) : base("GetCustomerByCard")
        {
            _restClient = customerRestClient;
            _customerContract = new CustomerCardContract
            {
                cardNumber = cardNumber,
                isLoyaltycard = isLoyaltycard,
                saleNumber = saleNumber,
                tillNumber = tillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_customerContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.GetCustomerByCard(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapCustomer(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
