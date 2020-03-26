using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Logout
{
    public class FinishTillCloseSerializeAction : SerializeAction
    {
        private readonly ILogoutRestClient _restClient;
        private readonly bool? _readTankDip;
        private readonly bool? _readTotalizer;

        public FinishTillCloseSerializeAction(ILogoutRestClient restClient,
            bool? readTankDip, bool? readTotalizer) : base("FinishTillClose")
        {
            _restClient = restClient;
            _readTankDip = readTankDip;
            _readTotalizer = readTotalizer;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.FinishTillClose(_readTankDip, _readTotalizer);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var finishTiilCloseContract = new DeSerializer().MapFinishTillClose(data);
                    return new Mapper().MapFinishTillClose(finishTiilCloseContract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
