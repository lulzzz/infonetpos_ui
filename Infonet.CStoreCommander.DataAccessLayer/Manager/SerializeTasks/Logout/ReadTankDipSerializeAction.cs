using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class ReadTankDipSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;

        public ReadTankDipSerializeAction(ILogoutRestClient restClient) 
            : base("ReadTankDip")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.ReadTankDip();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSuccess(data).success;
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
