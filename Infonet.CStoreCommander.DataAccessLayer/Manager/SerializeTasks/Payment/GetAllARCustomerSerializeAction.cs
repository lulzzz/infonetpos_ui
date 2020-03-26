using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Payment
{
    public class GetAllARCustomerSerializeAction : SerializeAction
    {
        private readonly IPaymentRestClient _paymentRestClient;
        private readonly int _pageIndex;

        public GetAllARCustomerSerializeAction(IPaymentRestClient paymentRestClient,
            int pageIndex)
            : base("GetAllARCustomer")
        {
            _pageIndex = pageIndex;
            _paymentRestClient = paymentRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _paymentRestClient.GetAllARCustomers(_pageIndex);
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
