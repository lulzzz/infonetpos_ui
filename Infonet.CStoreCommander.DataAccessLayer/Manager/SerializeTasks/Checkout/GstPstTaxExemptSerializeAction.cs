using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class GstPstTaxExemptSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _treatyNumber;

        public GstPstTaxExemptSerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string treatyNumber) : base("GstPstTaxExempt")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _treatyNumber = treatyNumber;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _restClient.GstPstTaxExempt(_cacheManager.SaleNumber,
                _cacheManager.TillNumberForSale, _cacheManager.ShiftNumber,
                _cacheManager.RegisterNumber, _treatyNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var validateAite = new DeSerializer().MapValidateAITE(data);
                    return new Mapper().MapValidateAITE(validateAite);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
