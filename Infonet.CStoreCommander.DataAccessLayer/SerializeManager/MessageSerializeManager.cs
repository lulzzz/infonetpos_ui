using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Message;
using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Message;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Message;
using Infonet.CStoreCommander.DataAccessLayer.DataContracts.Common;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class MessageSerializeManager : SerializeManager,
        IMessageSerializeManager
    {
        private readonly IMessageRestClient _messageRestClient;
        private readonly ICacheManager _cacheManager;

        public MessageSerializeManager(IMessageRestClient messageRestClient,
            ICacheManager cacheManager)
        {
            _messageRestClient = messageRestClient;
            _cacheManager = cacheManager;
        }

        public async Task<bool> AddMessage(string index, string message)
        {
            var action = new AddMessageSerializeAction(_messageRestClient,
                _cacheManager, index, message);

            await PerformTask(action);
            var success = new Mapper().MapSuccess((SuccessContract)action.ResponseValue);
            return success.IsSuccess;
        }

        public async Task<List<Message>> GetMessage()
        {
            var action = new GetMessageSerializeAction(_messageRestClient);

            await PerformTask(action);
            var messages = new Mapper().MapMessages((List<MessageContract>)action.ResponseValue);
            return messages;
        }
    }
}
