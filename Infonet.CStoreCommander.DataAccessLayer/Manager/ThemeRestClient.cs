using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager
{
    public class ThemeRestClient : IThemeRestClient
    {
        public async Task<HttpResponseMessage> GetActiveTheme(string ipAddress)
        {
            var url = string.Format(Urls.GetActiveTheme, ipAddress);
            var client = new HttpRestClient(true);
            return await client.GetAsync(url);
        }
    }
}
