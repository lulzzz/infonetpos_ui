using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Checkout
{
    public class GetTreatyNameSerializeAction : SerializeAction
    {
        private readonly ICheckoutRestClient _restClient;
        private readonly string _treatyNumber;
        private readonly string _captureMethod;

        public GetTreatyNameSerializeAction(ICheckoutRestClient restClient, 
            string treatyNumber,
            string captureMethod) : base("GetTreatyName")
        {
            _restClient = restClient;
            _treatyNumber = treatyNumber;
            _captureMethod = captureMethod;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _restClient.GetTreatyName(_treatyNumber, _captureMethod);
            var data = await response.Content.ReadAsStringAsync();
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapTreatyNumber(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
