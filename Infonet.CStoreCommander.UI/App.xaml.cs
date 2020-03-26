using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.BussinessLayer.IBussinessLogic;
using Infonet.CStoreCommander.EntityLayer.Entities;
using Infonet.CStoreCommander.Logging;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.View.Common;
using Infonet.CStoreCommander.UI.ViewModel;
using Infonet.CStoreCommander.Utils;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Globalization;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        private readonly InfonetLog _log = InfonetLogManager.GetLogger<App>();

        public static BackgroundTaskDeferral AppServiceDeferral = null;
        public static AppServiceConnection Connection = null;
        public static event EventHandler AppServiceConnected;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Resuming += OnResuming;
            this.Suspending += OnSuspending;
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            UnhandledException += UnhandledExceptionHandler;
        }

        private void OnResuming(object sender, object e)
        {
            Task.Run(async () =>
            {
                VMBase.ClaimOposPrinter();
            });
        }

        private void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            _log.Info("Unhandled Exception", e.Exception);
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            _log.Info(String.Format("Application Launched"));

            // Register all dependencies
            new DependencyResolver().RegisterDependencies();
            

            var rootFrame = Window.Current.Content as Frame;
            try
            {
                var themeBusinessLogic = SimpleIoc.Default.GetInstance<IThemeBusinessLogic>();
                var theme = await themeBusinessLogic.GetActiveTheme();

                LoadTheme(theme);
            }
            catch (Exception)
            {
                // Eating exception for theme 
            }


            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();
            }

            try
            {
                var policyBusinessLogic = SimpleIoc.Default.GetInstance<ILoginBussinessLogic>();
                var policies = await policyBusinessLogic.GetLoginPolicyAsync();

                var languageTag = new UtilityHelper().LanguageToLanguageTag(policies.PosLanguage);
                ApplicationConstants.Language = policies.PosLanguage;

                var cacheSize = rootFrame.CacheSize;
                rootFrame.CacheSize = 0;
                ApplicationLanguages.PrimaryLanguageOverride = languageTag;
                rootFrame.CacheSize = cacheSize;
            }
            catch (Exception ex)
            {
                _log.Info(ex.Message, ex);
            }

            App.Current.Resources["SystemControlHighlightListAccentHighBrush"] =
            App.Current.Resources["SystemControlHighlightListAccentMediumBrush"] =
                App.Current.Resources["SystemControlHighlightListAccentLowBrush"]
                = new SolidColorBrush(Color.FromArgb(255, 241, 241, 241));

            // Do not repeat Application initialization when the Window already has content,
            // just ensure that the window is active

            // Create a Frame to act as the navigation context and navigate to the first page

            rootFrame.NavigationFailed += OnNavigationFailed;

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //TODO: Load state from previously suspended application
            }

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;

            NavigateService.Instance.Frame = rootFrame;

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter

                    _log.Info(String.Format("Navigation to ExtendedSplashScreen"));
                    rootFrame.Navigate(typeof(MasterPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private void LoadTheme(Theme theme)
        {
            ApplicationConstants.ButtonConfirmationColor = GetSolidColorBrush(theme.ButtonBottomConfirmationColor);
            ApplicationConstants.ButtonWarningColor = GetSolidColorBrush(theme.ButtonBottomWarningColor);
            ApplicationConstants.ButtonFooterColor = GetSolidColorBrush(theme.ButtonBottomColor);
            App.Current.Resources["BackgroundColor1Dark"] = GetSolidColorBrush(theme.BackgroundColor1Dark);
            App.Current.Resources["BackgroundColor1Light"] = GetSolidColorBrush(theme.BackgroundColor1Light);
            App.Current.Resources["BackgroundColor2"] = GetSolidColorBrush(theme.BackgroundColor2);
            App.Current.Resources["ButtonBackgroundColor"] = GetSolidColorBrush(theme.HeaderBackgroundColor);
            App.Current.Resources["ButtonBottomColor"] = GetSolidColorBrush(theme.ButtonBottomColor);
            App.Current.Resources["ButtonBottomConfirmationColor"] = GetSolidColorBrush(theme.ButtonBottomConfirmationColor);
            App.Current.Resources["ButtonBottomWarningColor"] = GetSolidColorBrush(theme.ButtonBottomWarningColor);
            App.Current.Resources["ButtonForegroundColor"] = GetSolidColorBrush(theme.HeaderForegroundColor);
            App.Current.Resources["HeaderBackgroundColor"] = GetSolidColorBrush(theme.HeaderBackgroundColor);
            App.Current.Resources["HeaderForegroundColor"] = GetSolidColorBrush(theme.HeaderForegroundColor);
            App.Current.Resources["LabelTextForegroundColor"] = GetSolidColorBrush(theme.LabelTextForegroundColor);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            _log.Info(String.Format("Navigation failed at {0}", e.SourcePageType.FullName));
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            // connection established from the fulltrust process
            if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails)
            {
                AppServiceDeferral = args.TaskInstance.GetDeferral();
                args.TaskInstance.Canceled += OnTaskCanceled;

                if (args.TaskInstance.TriggerDetails is AppServiceTriggerDetails details)
                {
                    Connection = details.AppServiceConnection;
                    AppServiceConnected?.Invoke(this, null);
                }
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (AppServiceDeferral != null)
            {
                AppServiceDeferral.Complete();
            }
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            _log.Info(String.Format("Application Suspended"));

            var deferral = e.SuspendingOperation.GetDeferral();

            VMBase.ReleaseOposPrinter();

            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private SolidColorBrush GetSolidColorBrush(string hex)
        {
            if (hex.GetType() == typeof(string) && !string.IsNullOrEmpty(hex) && hex.IndexOf('#') != -1)
            {
                hex = hex.Replace("#", string.Empty);

                byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
                SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, r, g, b));
                return myBrush;
            }
            return null;
        }
    }
}
