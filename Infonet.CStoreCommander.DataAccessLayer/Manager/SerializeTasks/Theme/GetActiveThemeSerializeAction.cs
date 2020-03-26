using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.Utility;
using System.Net;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Theme
{
    public class GetActiveThemeSerializeAction : SerializeAction
    {
        private readonly IThemeRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public GetActiveThemeSerializeAction(
            IThemeRestClient restClient,
            ICacheManager cacheManager)
            : base("GetActiveTheme")
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        protected async override Task<object> OnPerform()
        {
            var ipAddress = _cacheManager.IpAddress;
#if DEBUG
          //  ipAddress = "172.16.0.42";
#endif

            var response = await _restClient.GetActiveTheme(ipAddress);
            var data = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var parsedData = new DeSerializer().MapTheme(data);
                    var theme = new Mapper().MapTheme(parsedData);
                    return theme;
                default:
                    return await HandleExceptions(response);
            }

        }
    }
}
