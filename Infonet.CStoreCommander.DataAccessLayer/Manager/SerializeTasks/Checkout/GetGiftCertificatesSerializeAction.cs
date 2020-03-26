using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class GetGiftCertificatesSerializeAction : SerializeAction
    {
        private readonly IGiftCertificateRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly decimal? _amount;
        private readonly string _tenderCode;
        private readonly string _transactionType;

        public GetGiftCertificatesSerializeAction(
            IGiftCertificateRestClient restClient,
            ICacheManager cacheManager,
            decimal? amount, string tenderCode, string transactionType)
            : base("GetGiftCertificates")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _amount = amount;
            _tenderCode = tenderCode;
            _transactionType = transactionType;
        }

        protected override async Task<object> OnPerform()
        {
            var model = new
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                transactionType = _transactionType,
                tillClose = false,
                tender = new
                {
                    tenderCode = _tenderCode,
                    amount = _amount
                }
            };

            var contract = JsonConvert.SerializeObject(model);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.GetGiftCertificates(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var giftCertificates = new DeSerializer().MapGiftCertificates(data);
                    return new Mapper().MapGiftCertificates(giftCertificates);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
