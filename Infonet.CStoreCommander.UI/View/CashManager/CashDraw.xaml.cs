using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.CashManager;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.CashManager
{
    public sealed partial class CashDraw : Page
    {
        public CashDrawVM CashDrawVM { get; set; }
        = SimpleIoc.Default.GetInstance<CashDrawVM>();

        public CashDraw()
        {
            this.InitializeComponent();

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                CashDrawVM.ResetVM();
            }
        }
    }
}
