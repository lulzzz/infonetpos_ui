using Infonet.CStoreCommander.EntityLayer.Entities.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface IMessageSerializeManager
    {
        Task<List<Message>> GetMessage();

        Task<bool> AddMessage(string index, string message);
    }
}
