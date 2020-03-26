using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class CompleteOverrideLimitSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public CompleteOverrideLimitSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager)
            : base("CompleteOverrideLimit")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.CompleteOverrideLimit(
                _cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale,
                _cacheManager.RegisterNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
