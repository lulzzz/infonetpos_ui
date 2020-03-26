using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class RemoveSiteTaxSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _treatyNumber;
        private readonly int _captureMethod;
        private readonly string _treatyName;
        private string _documentNumber;

        public RemoveSiteTaxSerializeAction(ICheckoutRestClient restClient,
                ICacheManager cacheManager,
                string treatyNumber,
                int captureMethod,
                string treatyName,
                string documentNumber) : base("RemoveSiteTax")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _treatyNumber = treatyNumber;
            _treatyName = treatyName;
            _documentNumber = documentNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.RemoveSiteTax(
                _cacheManager.SaleNumber, _cacheManager.TillNumberForSale,
                _cacheManager.RegisterNumber, _treatyNumber, _captureMethod,
                _treatyName, _documentNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var siteValidateContract = new DeSerializer().MapSiteValidate(data);
                    return new Mapper().MapSiteValidate(siteValidateContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
