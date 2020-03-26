using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Checkout;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Checkout;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class SaveStoreCreditSerializeAction : SerializeAction
    {
        private readonly IGiftCertificateRestClient _restClient;
        private readonly ICacheManager _cacheManager;
        private readonly SaveStoreCreditContract _saveStoreCreditContract;

        public SaveStoreCreditSerializeAction(
            IGiftCertificateRestClient restClient,
            ICacheManager cacheManager,
            string transactionType,
            string tenderCode,
            List<StoreCredit> giftCertificates) 
            : base("SaveStoreCredit")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
            _saveStoreCreditContract = new SaveStoreCreditContract
            {
                saleNumber = _cacheManager.SaleNumber,
                tillNumber = _cacheManager.TillNumberForSale,
                transactionType = transactionType,
                tenderCode = tenderCode,
                storeCredits = (from g in giftCertificates
                                select new StoreCreditsContract
                                {
                                    amountEntered = g.Amount.ToString(CultureInfo.InvariantCulture),
                                    number = g.Number
                                }).ToList()
            };
        }

        protected async override Task<object> OnPerform()
        {
            var contract = JsonConvert.SerializeObject(_saveStoreCreditContract);
            var content = new StringContent(contract, Encoding.UTF8, ApplicationJSON);
            var response = await _restClient.SaveStoreCredits(content);
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
