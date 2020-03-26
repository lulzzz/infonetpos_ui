using Infonet.CStoreCommander.EntityLayer.Entities.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.BussinessLayer.IBussinessLogic
{
    public interface IMessageBusinessLogic
    {
        Task<List<Message>> GetMessage();
        Task<bool> AddMessage(string index, string message);
    }
}
