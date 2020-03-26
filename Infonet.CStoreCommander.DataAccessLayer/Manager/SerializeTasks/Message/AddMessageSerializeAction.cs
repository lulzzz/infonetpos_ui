using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Message
{
    public class AddMessageSerializeAction : SerializeAction
    {
        private readonly IMessageRestClient _messageRestClient;
        private readonly ICacheManager _cacheManager;
        private readonly object _contract;

        public AddMessageSerializeAction(IMessageRestClient messageRestClient,
            ICacheManager cacheManager,
            string index, string message)
            : base("AddMessage")
        {
            _messageRestClient = messageRestClient;
            _cacheManager = cacheManager;
            _contract = new
            {
                index = index,
                message = message,
                tillNumber = _cacheManager.TillNumber
            };
        }

        protected async override Task<object> OnPerform()
        {
            var reason = JsonConvert.SerializeObject(_contract);
            var content = new StringContent(reason, Encoding.UTF8, ApplicationJSON);
            var response = await _messageRestClient.SaveMessage(content);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new DeSerializer().MapSuccess(data);
                default:
                    return await HandleExceptions(response);
            }
        }
    }
}
