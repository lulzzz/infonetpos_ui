using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.EntityLayer.Entities.Common;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class VoidSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly Reasons _reason;

        public VoidSaleSerializeAction(
            ISaleRestClient saleRestClient,
            ICacheManager cacheManager,
            Reasons reason) :
            base("VoidSale")
        {
            _saleRestClient = saleRestClient;
            _cacheManager = cacheManager;
            _reason = reason;
        }

        protected override async Task<object> OnPerform()
        {
            var reasonContract = new Mapper().MapReason(_reason, _cacheManager);
            var reason = JsonConvert.SerializeObject(reasonContract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _saleRestClient.VoidSale(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var voidSale = new DeSerializer().MapVoidSaleResponse(data);
                    return new Mapper().MapVoidSale(voidSale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
