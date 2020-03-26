using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class SuspendSaleSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;
        public SuspendSaleSerializeAction(ISaleRestClient restClient) 
            : base("SuspendSale")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.SuspendSale();
            var data = await response.Content.ReadAsStringAsync();
            switch(response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var suspendedSale = new DeSerializer().MapSuspendSale(data);
                    return  new Mapper().MapSuspendedSale(suspendedSale);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
