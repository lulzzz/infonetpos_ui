using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Sale
{
    public class ValidateVoidSerializeAction : SerializeAction
    {
        private readonly ISaleRestClient _restClient;

        public ValidateVoidSerializeAction(ISaleRestClient restClient) : base("ValidateVoid")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.ValidateVoidSale();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var result = new DeSerializer().MapSuccess(data);
                    return result.success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
