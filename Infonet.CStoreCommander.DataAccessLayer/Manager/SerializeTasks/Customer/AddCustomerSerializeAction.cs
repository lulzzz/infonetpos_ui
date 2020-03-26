using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    internal class AddCustomerSerializeAction : SerializeAction
    {
        private readonly ICustomerRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly EntityLayer.Entities.Customer.Customer _customer;

        public AddCustomerSerializeAction(
            ICustomerRestClient customerRestClient,
            ICacheManager cacheManager,
            EntityLayer.Entities.Customer.Customer customer) : base("AddCustomer")
        {
            _restClient = customerRestClient;
            _cacheManager = cacheManager;
            _customer = customer;
        }

        /// <summary>
        /// Method to add loyalty customer 
        /// </summary>
        /// <returns></returns>
        protected override async Task<object> OnPerform()
        {
            var customerContract = new Mapper().MapCustomer(_customer);
            var customer = JsonConvert.SerializeObject(customerContract);
            var content = new StringContent(customer, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.AddCustomer(content);

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var parsedData = new DeSerializer().MapSuccess(data);
                    return parsedData.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
