using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities;
using Infonet.CStoreCommander.EntityLayer.Exception;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.PeripheralLayer;
using Infonet.CStoreCommander.PeripheralLayer.Interfaces;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using System;
using System.Linq;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;

namespace Infonet.CStoreCommander.UI.ViewModel
{
    public class ExtendedSplashScreenVM : VMBase
    {
        private readonly InfonetLog _log;
        private readonly ILoginBussinessLogic _loginBussinessLogic;
        private readonly IThemeBusinessLogic _themeBusinessLogic;

        public ExtendedSplashScreenVM(ILoginBussinessLogic loginBussinessLogic,
            IThemeBusinessLogic themeBusinessLogic)
        {
            _log = InfonetLogManager.GetLogger<ExtendedSplashScreenVM>();
            _loginBussinessLogic = loginBussinessLogic;
            _themeBusinessLogic = themeBusinessLogic;
            // TODO: Not a correct way to create Data file
            new Helper().EnsureDataFileExists();
            SimpleIoc.Default.GetInstance<UtilsVM>();
            SetLoginPolicy();
            CacheBusinessLogic.FramePriorSwitchUserNavigation = string.Empty;
            CacheBusinessLogic.AreFuelPricesSaved = true;
            CacheBusinessLogic.PreviousAuthKey = string.Empty;

            try
            {
                new SignaturePad();
            }
            catch (Exception ex)
            {

            }
        }

        private async void SetLoginPolicy()
        {
            LoadingService.ShowLoadingStatus(true);
            var startTime = DateTime.Now;
            _log.Info("SetLoginPolicy method call started");
            try
            {
                CacheBusinessLogic.IpAddress = GetLocalIp();
                var loginPolicy = await _loginBussinessLogic.GetLoginPolicyAsync();

                var registerNumber = await new Helper().GetRegisterNumber();
                CacheBusinessLogic.RegisterNumber = (byte)registerNumber;
                NavigateService.Instance.NavigateToLogin();
            }
            catch (InternalServerException ex)
            {
                ShowNotification(ex.Error.Message, ShutDownApplication, ShutDownApplication, ApplicationConstants.ButtonWarningColor);
            }
            catch (ApiDataException ex)
            {
                _log.Warn(ex);
                ShowNotification(ex.Error.Message, ShutDownApplication, ShutDownApplication
                    , ApplicationConstants.ButtonWarningColor);
            }
            catch (NullReferenceException ex)
            {
                _log.Warn(ex);

                ShowNotification(ApplicationConstants.ApiNotConnected, ShutDownApplication, ShutDownApplication,
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (ArgumentException ex)
            {
                _log.Warn(ex);

                ShowNotification(ApplicationConstants.LanguageNotSupported, ShutDownApplication, ShutDownApplication,
                    ApplicationConstants.ButtonWarningColor);
            }
            catch (Exception ex)
            {
                _log.Warn(ex);
                ShowNotification(ApplicationConstants.SomethingBadHappned, ShutDownApplication, ShutDownApplication,
                    ApplicationConstants.ButtonWarningColor);
            }
            finally
            {
                LoadingService.ShowLoadingStatus(false);
                var endTime = DateTime.Now;
                _log.Info(string.Format("Time Taken In Login Page is {0}ms", (endTime - startTime).TotalMilliseconds));
            }
        }

        private void LoadTheme(Theme theme)
        {
            var myResourceDictionary = new ResourceDictionary();

            myResourceDictionary.Add(nameof(theme.BackgroundColor1Dark), theme.BackgroundColor1Dark);
            myResourceDictionary.Add(nameof(theme.BackgroundColor1Light), theme.BackgroundColor1Light);
            myResourceDictionary.Add(nameof(theme.BackgroundColor2), theme.BackgroundColor2);
            myResourceDictionary.Add(nameof(theme.ButtonBackgroundColor), theme.ButtonBackgroundColor);
            myResourceDictionary.Add(nameof(theme.ButtonBottomColor), theme.ButtonBottomColor);
            myResourceDictionary.Add(nameof(theme.ButtonBottomConfirmationColor), theme.ButtonBottomConfirmationColor);
            myResourceDictionary.Add(nameof(theme.ButtonBottomWarningColor), theme.ButtonBottomWarningColor);
            myResourceDictionary.Add(nameof(theme.ButtonForegroundColor), theme.ButtonForegroundColor);
            myResourceDictionary.Add(nameof(theme.HeaderBackgroundColor), theme.HeaderBackgroundColor);
            myResourceDictionary.Add(nameof(theme.HeaderForegroundColor), theme.HeaderForegroundColor);
            myResourceDictionary.Add(nameof(theme.LabelTextForegroundColor), theme.LabelTextForegroundColor);
            Application.Current.Resources.MergedDictionaries.Remove(myResourceDictionary);
        }

        /// <summary>
        /// Gets the Ip of the local system
        /// </summary>
        /// <returns></returns>
        private string GetLocalIp()
        {
            var startTime = DateTime.Now;
            _log.Info("GetLocalIP method call started");
            var internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

            if (internetConnectionProfile?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .FirstOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == internetConnectionProfile.NetworkAdapter.NetworkAdapterId);

            var endTime = DateTime.Now;
            _log.Info(string.Format("GetLocalIP method call ended in {0}ms", (endTime - startTime).TotalMilliseconds));
            return hostname?.CanonicalName;
        }
    }
}
