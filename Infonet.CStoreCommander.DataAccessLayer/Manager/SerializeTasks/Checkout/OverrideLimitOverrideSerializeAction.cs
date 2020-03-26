using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class OverrideLimitOverrideSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly string _item;
        private readonly string _overrideCode;
        private readonly string _documentNumber;
        private readonly string _documentDetail;

        public OverrideLimitOverrideSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            string item,
            string overrideCode,
            string documentNumber,
            string documentDetail)
            : base("OverrideLimitOverride")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _item = item;
            _overrideCode = overrideCode;
            _documentNumber = documentNumber;
            _documentDetail = documentDetail;
        }

        protected async override Task<object> OnPerform()
        {
            var model = new
            {
                tillNumber = _cacheManager.TillNumberForSale,
                saleNumber = _cacheManager.SaleNumber,
                itemNumber = _item,
                overrideCode = _overrideCode,
                documentNumber = _documentNumber,
                documentDetail = _documentDetail
            };

            var contract = JsonConvert.SerializeObject(model);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.OverrideLimitOverride(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var success = new DeSerializer().MapSuccess(data);
                    return success.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
