using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class SaveGiftCertificatesSerializeAction : SerializeAction
    {
        private readonly IGiftCertificateRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly List<GiftCertificate> _giftCertificates;

        public SaveGiftCertificatesSerializeAction(
            IGiftCertificateRestClient restClient,
            ICacheManager cacheManager,
            List<GiftCertificate> giftCertificates)
            : base("SaveGiftCertificates")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _giftCertificates = giftCertificates;
        }

        protected override async Task<object> OnPerform()
        {
            var model = new
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                transactionType = "Sale",
                tenderCode = "GC",
                giftCerts = from g in _giftCertificates
                            select new
                            {
                                number = g.Number,
                                amount = g.Amount
                            }
            };

            var contract = JsonConvert.SerializeObject(model);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.SaveGiftCertificates(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var giftCertificates = new DeSerializer().MapTenderSummary(data);
                    return new Mapper().MapTenderSummary(giftCertificates);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
