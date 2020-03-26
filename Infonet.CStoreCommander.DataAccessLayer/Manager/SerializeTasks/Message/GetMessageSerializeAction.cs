using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Message
{
    class GetMessageSerializeAction : SerializeAction
    {
        private readonly IMessageRestClient _messageRestClient;

        public GetMessageSerializeAction(IMessageRestClient messageRestClient)
            : base("GetMessage")
        {
            _messageRestClient = messageRestClient;
        }

        protected async override Task<object> OnPerform()
        {
            var response = await _messageRestClient.GetAllMessage();
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapMessage(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
