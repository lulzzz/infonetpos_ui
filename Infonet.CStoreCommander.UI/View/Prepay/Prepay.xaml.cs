using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.PumpOptions;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Prepay
{
    public sealed partial class Prepay : Page
    {
        public PrepayVM PrepayVM { get; set; }
   = SimpleIoc.Default.GetInstance<PrepayVM>();

        public Prepay()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PrepayVM.ResetVM();
            }
        }
    }
}
