using Infonet.CStoreCommander.DataAccessLayer.IManager;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.DataAccessLayer.Manager.SerializeTasks.Theme;
using Infonet.CStoreCommander.EntityLayer.Entities;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.SerializeManager
{
    public class ThemeSerializeManager : SerializeManager, IThemeSerializeManager
    {
        private readonly IThemeRestClient _restClient;
        private readonly ICacheManager _cacheManager;

        public ThemeSerializeManager(IThemeRestClient restClient,
            ICacheManager cacheManager)
        {
            _restClient = restClient;
            _cacheManager = cacheManager;
        }

        public async Task<Theme> GetActiveTheme()
        {
            var action = new GetActiveThemeSerializeAction(_restClient, _cacheManager);

            await PerformTask(action);

            return (Theme)action.ResponseValue;
        }
    }
}
