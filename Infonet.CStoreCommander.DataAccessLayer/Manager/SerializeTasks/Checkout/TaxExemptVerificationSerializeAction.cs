using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class TaxExemptVerificationSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public TaxExemptVerificationSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager) : base("TaxExemptVerification")
        {
            _cacheManager = cacheManager;
            _restClient = restClient;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.VerifyTaxExempt(
                _cacheManager.SaleNumber, _cacheManager.TillNumberForSale,
                _cacheManager.RegisterNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var taxExemptVerification = new DeSerializer().MapTaxExemptVerification(data);
                    return new Mapper().MapTaxExemptVerification(taxExemptVerification);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
