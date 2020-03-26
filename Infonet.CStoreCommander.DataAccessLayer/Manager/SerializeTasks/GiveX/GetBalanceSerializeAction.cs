using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.GiveX
{
    public class GetBalanceSerializeAction : SerializeAction
    {
        private readonly IGiveXRestClient _restClient;
        private readonly string _givexCardNumber;

        public GetBalanceSerializeAction(IGiveXRestClient restClient,
            string givexCardNumber)
            : base("GetBalance")
        {
            _restClient = restClient;
            _givexCardNumber = givexCardNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetCardBalance(_givexCardNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var contract = new DeSerializer().MapGiveXCardBalance(data);
                    return new Mapper().MapGiveXCardBalance(contract);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
