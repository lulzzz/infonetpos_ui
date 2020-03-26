using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class ReturnSaleItems : Page
    {
        public ReturnSaleItemVM ReturnSaleItemVM { get; set; }
      = SimpleIoc.Default.GetInstance<ReturnSaleItemVM>();

        public ReturnSaleItems()
        {
            this.InitializeComponent();
            this.DataContext = ReturnSaleItemVM;
        }
    }
}
