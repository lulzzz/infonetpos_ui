using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class VerifyMultiPOSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly string _purchaseOrder;

        public VerifyMultiPOSerializeAction(ICheckoutRestClient restClient,string purchaseOrder) 
            : base("VerifyMultiPO")
        {
            _restClient = restClient;
            _purchaseOrder = purchaseOrder;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.VerifyMultiPO(_purchaseOrder);

            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var succcess = new DeSerializer().MapSuccess(data);
                    return new Mapper().MapSuccess(succcess);
                   default:
                    return await HandleExceptions(response);
            }
        }
    }
}
