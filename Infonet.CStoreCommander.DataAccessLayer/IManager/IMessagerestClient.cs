using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IMessageRestClient
    {
        Task<HttpResponseMessage> GetAllMessage();

        Task<HttpResponseMessage> SaveMessage(HttpContent content);
    }
}
