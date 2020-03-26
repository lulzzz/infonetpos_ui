using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class OverrideLimitDetailsSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public OverrideLimitDetailsSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager)
            : base("OverrideLimitDetails")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.OverrideLimitDetails(
                _cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale,_cacheManager.TreatyNumber, _cacheManager.TreatyName);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var overrideLimitDetails = new DeSerializer().MapOverrideLimitDetails(data);
                    return new Mapper().MapOverrideLimitDetails(overrideLimitDetails);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
