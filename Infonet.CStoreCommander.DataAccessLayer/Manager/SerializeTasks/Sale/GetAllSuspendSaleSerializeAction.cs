using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class GetAllSuspendSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _saleRestClient;
        public GetAllSuspendSaleSerializeAction(ISaleRestClient saleRestClient)
            : base("GetAllSuspendSale")
        {
            _saleRestClient = saleRestClient;
        }

        protected override async Task<object> OnPerform()
        {
            var response = await _saleRestClient.GetAllSuspendedSale();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSales =  new DeSerializer().MapSuspendedSale(data);
                    return new Mapper().MapSuspendedSales(suspendedSales);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
