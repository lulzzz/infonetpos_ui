using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.KickBack
{
    public class ValidateGasKingSerializeAction : SerializeAction
    {
        private readonly IKickBackRestClient _restClient;

        public ValidateGasKingSerializeAction(IKickBackRestClient restClient)
            : base("CheckKickBackSerializeAction")
        {
            _restClient = restClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.ValidateGasKing();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var gasKingResponse = new DeSerializer().MapValidateGasKing(data);
                    return new Mapper().MapValidateGasKing(gasKingResponse);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
