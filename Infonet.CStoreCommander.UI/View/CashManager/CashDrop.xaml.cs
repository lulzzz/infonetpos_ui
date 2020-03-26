using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.CashManager;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.CashManager
{
    public sealed partial class CashDrop : Page
    {
        public CashDropVM CashDropVM { get; set; }
          = SimpleIoc.Default.GetInstance<CashDropVM>();

        public CashDrop()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                CashDropVM.ResetVM();
            }
        }
    }
}
