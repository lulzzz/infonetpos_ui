using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class UnsuspendSale : Page
    {
        public UnsuspendSaleVM UnsuspendSaleVM { get; set; }
          = SimpleIoc.Default.GetInstance<UnsuspendSaleVM>();

        public UnsuspendSale()
        {
            this.InitializeComponent();
            this.DataContext = UnsuspendSaleVM;
        }
    }
}
