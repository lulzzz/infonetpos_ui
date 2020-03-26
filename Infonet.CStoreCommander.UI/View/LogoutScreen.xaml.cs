using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Login;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View
{
    public sealed partial class LogoutScreen : Page
    {
        public LogoutScreenVM LogoutScreenVM { get; set; } =
            SimpleIoc.Default.GetInstance<LogoutScreenVM>();

        public LogoutScreen()
        {
            this.InitializeComponent();
            this.DataContext = LogoutScreenVM;
            ResetVM();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus(FocusState.Keyboard);
        }

        private void UserKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (txtPassword.IsEnabled)
            {
                if (Helper.IsEnterKey(e))
                {
                    txtPassword.Focus(FocusState.Keyboard);
                }
            }
        }

        private void ResetVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                LogoutScreenVM.ReInitialize();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Loaded -= OnLoaded;
            Loaded += OnLoaded;

            txtUser.KeyUp -= UserKeyUp;
            txtUser.KeyUp += UserKeyUp;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Loaded -= OnLoaded;
            txtUser.KeyUp -= UserKeyUp;
            ConfirmationPopup.Visibility = Visibility.Collapsed;
            errorPopup.Visibility = Visibility.Collapsed;
        }
    }
}
