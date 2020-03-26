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
    public class FNGTRSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly FNGTRContract _fNGTRContract;

        public FNGTRSerializeAction(ICheckoutRestClient restClient,
                ICacheManager cacheManager, string phoneNumber) : base("FNGTR")
        {
            _restClient = restClient;

            _fNGTRContract = new FNGTRContract
            {
                phoneNumber = phoneNumber,
                registerNumber = cacheManager.RegisterNumber,
                saleNumber = cacheManager.SaleNumber,
                tillNumber = cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {

            var content = new StringContent(JsonConvert.SerializeObject(_fNGTRContract)
               , Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.FNGTR(content);
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
