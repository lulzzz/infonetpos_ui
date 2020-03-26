using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Sale;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class SetTaxExemptionSerializeAction : SerializeAction
    {
        private ISaleRestClient _saleRestClient;
        private TaxExemptionContract _taxExemptionContract;

        public SetTaxExemptionSerializeAction(ISaleRestClient saleRestClient,
            ICacheManager cacheManager,
            string taxExemptionCode)
             : base("SetTaxExemption")
        {
            _saleRestClient = saleRestClient;
            _taxExemptionContract = new TaxExemptionContract
            {
                saleNumber = cacheManager.SaleNumber,
                tillNumber = cacheManager.TillNumber,
                taxExemptionCode = taxExemptionCode
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_taxExemptionContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);

            var response = await _saleRestClient.SetTaxExemption(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var saleContract = new DeSerializer().MapSale(data);
                    return new Mapper().MapSale(saleContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
