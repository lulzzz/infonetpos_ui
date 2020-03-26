using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    internal class SearchCustomersSerializeAction : SerializeAction
    {
        private readonly int _pageIndex;
        private readonly ICustomerRestClient _restClient;
        private readonly string _searchTerm;
        private readonly bool _loyalty;

        public SearchCustomersSerializeAction(ICustomerRestClient restClient,
            string searchTerm, int pageIndex, bool loyalty) : 
            base("SearchCustomers")
        {
            _restClient = restClient;
            _searchTerm = searchTerm;
            _pageIndex = pageIndex;
            _loyalty = loyalty;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.SearchCustomers(_searchTerm,
                _pageIndex, _loyalty);

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var customers = new DeSerializer().MapCustomers(data);
                    return new Mapper().MapCustomers(customers);
                case HttpStatusCode.Accepted:
                    var sale = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(sale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}