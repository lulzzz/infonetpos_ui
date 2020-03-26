using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment
{
    public class SearchARCustomerSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly int _pageIndex;
        private readonly string _searchTerm;

        public SearchARCustomerSerializeAction(IPaymentRestClient paymentRestClient,
            int pageIndex, string searchTerm)
            : base("SearchARCustomer")
        {
            _pageIndex = pageIndex;
            _paymentRestClient = paymentRestClient;
            _searchTerm = searchTerm;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _paymentRestClient.SearchARCustomers(_searchTerm, _pageIndex);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapARCustomersList(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
