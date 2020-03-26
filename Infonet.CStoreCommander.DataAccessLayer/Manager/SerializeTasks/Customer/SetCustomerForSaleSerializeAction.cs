using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Customer
{
    internal class SetCustomerForSaleSerializeAction : SerializeAction
    {
        private readonly ICacheManager _cacheManager;
        private readonly ICustomerRestClient _restClient;
        private readonly string _customerCode;

        public SetCustomerForSaleSerializeAction(ICustomerRestClient restClient,
            ICacheManager cacheManager, string customerCode) :
            base("SetCustomerForSale")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _customerCode = customerCode;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.SetCustomerForSale(_customerCode, 
                _cacheManager.SaleNumber, _cacheManager.TillNumberForSale, _cacheManager.RegisterNumber);

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSale(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}