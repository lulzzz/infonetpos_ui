using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    internal class GetAllCustomersSerializeAction : SerializeAction
    {
        private readonly ICustomerRestClient _restClient;
        private readonly int _pageIndex;
        private readonly bool _loyalty;

        public GetAllCustomersSerializeAction(
            ICustomerRestClient customerRestClient,
            int pageIndex, bool loyalty) : base("GetAllCustomers")
        {
            _restClient = customerRestClient;
            _pageIndex = pageIndex;
            _loyalty = loyalty;
        }

        /// <summary>
        /// Method to add loyalty customer 
        /// </summary>
        /// <returns></returns>
        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GetAllCustomers(_pageIndex, _loyalty);

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapCustomers(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
