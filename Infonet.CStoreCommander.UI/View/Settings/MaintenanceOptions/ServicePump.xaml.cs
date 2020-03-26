using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.SettingsMenu;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Settings.MaintenanceOptions
{
    public sealed partial class ServicePump : Page
    {
        public MaintenanceVM MaintenanceVM { get; set; }
          = SimpleIoc.Default.GetInstance<MaintenanceVM>();

        public ServicePump()
        {
            this.InitializeComponent();
        }
    }
}
