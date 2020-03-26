using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Infonet.CStoreCommander.UI.Service;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class BottleReturns : Page
    {
        public BottleReturnsScreenVM BottleReturnsScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<BottleReturnsScreenVM>();

        public BottleReturns()
        {
            this.InitializeComponent();
            this.DataContext = BottleReturnsScreenVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                BottleReturnsScreenVM.ReInitialize();
            }
        }
    }
}
