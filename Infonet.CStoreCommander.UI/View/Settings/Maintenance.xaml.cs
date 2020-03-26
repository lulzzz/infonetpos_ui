using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.SettingsMenu;
using Windows.UI.Xaml.Controls;
using Infonet.CStoreCommander.UI.Service;

namespace Infonet.CStoreCommander.UI.View.Settings
{
    public sealed partial class Maintenance : Page
    {
        public MaintenanceVM MaintenanceVM { get; set; }
            = SimpleIoc.Default.GetInstance<MaintenanceVM>();


        public Maintenance()
        {
            this.InitializeComponent();

            NavigateService.Instance.MaintenanceFrame = frmMaintenance;

            ReInitalizeVM();
        }

        private void ReInitalizeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                MaintenanceVM.ResetVM();
            }
        }
    }
}
