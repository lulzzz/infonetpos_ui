using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class ValidateTillCloseSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;

        public ValidateTillCloseSerializeAction(ILogoutRestClient restClient)
            : base("ValidateTillClose")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.ValidateTillClose();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var validatetillContract = new DeSerializer().MapValidateTill(data);
                    return new Mapper().MapValidateTill(validatetillContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
