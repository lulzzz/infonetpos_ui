using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class RunAwaySerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public RunAwaySerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager) : base("RunAway")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var contract = new
            {
                tillNumber = _cacheManager.TillNumberForSale,
                saleNumber = _cacheManager.SaleNumber
            };

            var serializedObject = JsonConvert.SerializeObject(contract);
            var content = new StringContent(serializedObject, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.RunAway(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var completePayment = new DeSerializer().MapCommonCompletePayment(data);
                    return new Mapper().MapCommonCompletePayment(completePayment);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
