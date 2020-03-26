using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class VerifyByAccountSerializeAction : SerializeAction
    {

        private readonly ICheckoutRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly VerfifyByAccountContract _verifyByAccountContract;

        public VerifyByAccountSerializeAction(ICheckoutRestClient restClient,
            ICacheManager cacheManager, string transactionType, bool tillClose,
            string tenderCode, decimal? amountEntered)
            : base("VerifyByAccount")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _verifyByAccountContract = new VerfifyByAccountContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumber,
                tillClose = tillClose,
                transactionType = transactionType,
                tender = new AccountTenderContract
                {
                    amountEntered = amountEntered.HasValue ? amountEntered.Value.ToString(CultureInfo.InvariantCulture) : null,
                    tenderCode = tenderCode
                }
            };
        }

        protected async override Task<object> OnPerform()
        {
            var verifyContract = JsonConvert.SerializeObject(_verifyByAccountContract);
            var content = new StringContent(verifyContract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.VerifyByAccount(content);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var verifyByAccount = new DeSerializer().VerifyByAccount(data);
                    return new Mapper().VerifyByAccount(verifyByAccount);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
