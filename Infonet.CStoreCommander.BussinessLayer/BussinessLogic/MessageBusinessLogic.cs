using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infonet.CStoreCommander.EntityLayer.Entities.Message;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class MessageBusinessLogic : IMessageBusinessLogic
    {
        private readonly IMessageSerializeManager _seralizeManager;
        
        public MessageBusinessLogic(IMessageSerializeManager seralizeManager)
        {
            _seralizeManager = seralizeManager;
        }

        public async Task<bool> AddMessage(string index, string message)
        {
            return await _seralizeManager.AddMessage(index, message);
        }

        public async Task<List<Message>> GetMessage()
        {
            return await _seralizeManager.GetMessage();
        }
    }
}
