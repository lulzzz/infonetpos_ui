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
    public class GetStoreCreditSerializeAction : SerializeAction
    {
        private readonly IGiftCertificateRestClient _giftCertificateRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly StoreCreditContract _storeCreditContract;

        public GetStoreCreditSerializeAction(IGiftCertificateRestClient giftCertificateRestClient,
            ICacheManager cacheManager,
            string transactionType, string tenderCode,
            string amountEntered)
            : base("GetStoreCredit")
        {
            _cacheManager = cacheManager;

            _giftCertificateRestClient = giftCertificateRestClient;

            _storeCreditContract = new StoreCreditContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                transactionType = transactionType,
                tillClose = false,
                tender = new StoreCreditTenderContract
                {
                    tenderCode = tenderCode,
                    amountEntered = amountEntered
                }
            };

        }

        protected async override Task<object> OnPerform()
        {
            var contract = JsonConvert.SerializeObject(_storeCreditContract);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _giftCertificateRestClient.GetStoreCredits(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var giftCertificates = new DeSerializer().MapGiftCertificates(data);
                    return new Mapper().MapStoreCredit(giftCertificates);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
