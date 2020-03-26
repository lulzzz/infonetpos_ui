using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.DataAccessLayer.ISerializeManager;
using Infonet.CStoreCommander.EntityLayer.Entities;
using System.Linq;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace Infonet.CStoreCommander.BussinessLayer.BussinessLogic
{
    public class ThemeBusinessLogic : IThemeBusinessLogic
    {
        private readonly IThemeSerializeManager _serializeManager;
        private readonly ICacheBusinessLogic _cacheBusinessLogic;

        public ThemeBusinessLogic(IThemeSerializeManager serializeManager,
            ICacheBusinessLogic cacheBusinessLogic)
        {
            _serializeManager = serializeManager;
            _cacheBusinessLogic = cacheBusinessLogic;
        }

        public async Task<Theme> GetActiveTheme()
        {
            var internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

            if (internetConnectionProfile?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .FirstOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == internetConnectionProfile.NetworkAdapter.NetworkAdapterId);
            _cacheBusinessLogic.IpAddress = hostname?.CanonicalName;

            var result = await _serializeManager.GetActiveTheme();

            return new Theme
            {
                BackgroundColor1Dark = result.BackgroundColor1Dark ?? "#000000",
                BackgroundColor1Light = result.BackgroundColor1Light ?? "#000000",
                BackgroundColor2 = result.BackgroundColor2 ?? "#000000",
                ButtonBackgroundColor = result.ButtonBackgroundColor ?? "#000000",
                ButtonBottomColor = result.ButtonBottomColor ?? "#000000",
                ButtonBottomConfirmationColor = result.ButtonBottomConfirmationColor ?? "#000000",
                ButtonBottomWarningColor = result.ButtonBottomWarningColor ?? "#000000",
                ButtonForegroundColor = result.ButtonForegroundColor ?? "#000000",
                HeaderBackgroundColor = result.HeaderBackgroundColor ?? "#000000",
                HeaderForegroundColor = result.HeaderForegroundColor ?? "#000000",
                LabelTextForegroundColor = result.LabelTextForegroundColor ?? "#000000"
            };
        }
    }
}
