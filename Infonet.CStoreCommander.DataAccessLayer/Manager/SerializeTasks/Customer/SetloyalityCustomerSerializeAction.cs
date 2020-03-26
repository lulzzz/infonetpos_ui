using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    class SetLoyalityCustomerSerializeAction : SerializeAction
    {
        private readonly ICustomerRestClient _customerRestClient;
        private readonly EntityLayer.Entities.Customer.Customer _customerModel;

        public SetLoyalityCustomerSerializeAction(ICustomerRestClient customerRestClient,
            EntityLayer.Entities.Customer.Customer customerModel)
            : base("SetLoyalityCustomer")
        {
            _customerRestClient = customerRestClient;
            _customerModel = customerModel;
        }

        protected async override Task<object> OnPerform()
        {
            var customerContract = new Mapper().MapCustomer(_customerModel);
            var customer = JsonConvert.SerializeObject(customerContract);
            var content = new StringContent(customer, Encoding.UTF8, ApplicationJSON);

            var response = await _customerRestClient.SetloyalityCustomer(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSuccess(data).success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
