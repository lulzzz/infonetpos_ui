using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.SettingsMenu.Maintenance;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.Service;

namespace Infonet.CStoreCommander.UI.View.Settings.MaintenanceOptions
{

    public sealed partial class SwitchUserOrChangePassword : Page
    {
        public SwitchUserVM SwitchUserVM { get; set; }
         = SimpleIoc.Default.GetInstance<SwitchUserVM>();

        public SwitchUserOrChangePassword()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                SwitchUserVM.ResetVM();
            }

            pwbNewPassword.KeyUp += NewPasswordKeyUp;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            pwbNewPassword.KeyUp -= NewPasswordKeyUp;
        }

        private void NewPasswordKeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                pwbConfirmPassword.Focus(FocusState.Keyboard);
            }
        }
    }
}
