using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.KickBack
{
    public class VerifyKickBackSerializeAction : SerializeAction
    {
        private readonly IKickBackRestClient _restClient;
        private readonly string _phoneNumber;
        public readonly string _pointCardNumber;

        public VerifyKickBackSerializeAction(IKickBackRestClient restClient, string phoneNumber,
            string pointCardNumber)
            : base("VerifyKickBackSerializeAction")
        {
            _restClient = restClient;
            _phoneNumber = phoneNumber;
            _pointCardNumber = pointCardNumber;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.VerifyKickBack(_pointCardNumber, _phoneNumber);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var kickbackResponse = new DeSerializer().MapVerifyKickbackContract(data);
                    return new Mapper().MapVerifyKickback(kickbackResponse);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
