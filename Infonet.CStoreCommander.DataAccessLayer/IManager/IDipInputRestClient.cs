using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IDipInputRestClient
    {
        Task<HttpResponseMessage> GetDipInput();

        Task<HttpResponseMessage> GetDipInputPrint();

        Task<HttpResponseMessage> SaveDipInput(HttpContent content);
    }
}
