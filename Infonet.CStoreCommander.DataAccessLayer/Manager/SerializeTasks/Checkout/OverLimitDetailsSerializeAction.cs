using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class OverLimitDetailsSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public OverLimitDetailsSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager) : base("OverLimitDetails")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.OverLimitDetails(
                _cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var overlimitDetails = new DeSerializer().MapOverLimitDetails(data);
                    return new Mapper().MapOverLimitDetails(overlimitDetails);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
