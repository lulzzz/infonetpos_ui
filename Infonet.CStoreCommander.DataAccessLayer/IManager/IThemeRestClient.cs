using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.IManager
{
    public interface IThemeRestClient
    {
        Task<HttpResponseMessage> GetActiveTheme(string ipAddress);
    }
}
