using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class CloseTillSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;

        public CloseTillSerializeAction(ILogoutRestClient restClient) 
            : base("CloseTill")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.CloseTill();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var closeTillContract = new DeSerializer().MapTillClose(data);
                    return new Mapper().MapTillClose(closeTillContract);

                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
