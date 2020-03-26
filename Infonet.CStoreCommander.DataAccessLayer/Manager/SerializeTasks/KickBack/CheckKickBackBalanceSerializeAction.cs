using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.KickBack
{
    public class CheckKickBackBalanceSerializeAction : SerializeAction
    {
        private readonly IKickBackRestClient _restClient;
        private readonly string _pointCardNumber;

        public CheckKickBackBalanceSerializeAction(IKickBackRestClient restClient, string pointCardNumber)
            : base("CheckKickBackBalanceSerializeAction")
        {
            _restClient = restClient;
            _pointCardNumber = pointCardNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.CheckKickBackbalance(_pointCardNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var kickBackBalance = new DeSerializer().MapKickbackBalancePoints(data);
                    return new Mapper().MapKickBackbalancePoints(kickBackBalance);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
