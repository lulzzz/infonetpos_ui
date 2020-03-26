using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface ISystemRestClient
    {
        Task<HttpResponseMessage> GetRegisterSettings(byte registerNumber);
    }
}
