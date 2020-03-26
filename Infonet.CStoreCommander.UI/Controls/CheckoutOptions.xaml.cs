using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class CheckoutOptions : UserControl
    {
        public CheckoutOptions()
        {
            this.InitializeComponent();
            this.DataContext = SaleGridVM;
         //   SaleGridVM.ReInitialize();
        }

        public SaleGridVM SaleGridVM { get; set; }
         = SimpleIoc.Default.GetInstance<SaleGridVM>();
    }
}
