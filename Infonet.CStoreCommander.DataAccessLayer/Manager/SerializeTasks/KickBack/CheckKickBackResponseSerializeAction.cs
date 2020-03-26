using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.KickBack
{
    public class CheckKickBackResponseSerializeAction : SerializeAction
    {
        private readonly IKickBackRestClient _restClient;
        private readonly bool _response;

        public CheckKickBackResponseSerializeAction(IKickBackRestClient restClient, bool response) 
            : base("CheckKickBackResponseSerializeAction")
        {
            _restClient = restClient;
            _response = response;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.CheckKickBackResponse(_response);
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
