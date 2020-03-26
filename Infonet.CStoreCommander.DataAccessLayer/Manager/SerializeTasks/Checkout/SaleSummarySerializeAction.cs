using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class SaleSummarySerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly SaleSummaryContract _saleSummaryContract;
        private readonly double? _kickBackAmount;

        public SaleSummarySerializeAction(
            ICheckoutRestClient restClient,
            ICacheManager cacheManager,
            bool isAiteValidated,
            bool isSiteValidated,
             double? kickBackAmount) : base("SaleSummary")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _kickBackAmount = kickBackAmount;
            _saleSummaryContract = new SaleSummaryContract
            {
                isAiteValidated = isAiteValidated,
                isSiteValidated = isSiteValidated,
                registerNumber = _cacheManager.RegisterNumber,
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected override async Task<object> OnPerform()
        {
            var content = new StringContent(JsonConvert.SerializeObject(_saleSummaryContract),
                Encoding.UTF8, ApplicationJSON);

            var response = await _restClient.SaleSummary(content, _kickBackAmount);

            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleSummary = new DeSerializer().MapCheckoutSummary(data);
                    return new Mapper().MapCheckoutSummary(saleSummary);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
