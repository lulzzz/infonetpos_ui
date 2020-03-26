using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;

namespace Infonet.CStoreCommander.UI.Controls
{
    public sealed partial class ReturnsPopup : UserControl
    {
        public SaleGridVM SaleGridVM { get; set; }
        = SimpleIoc.Default.GetInstance<SaleGridVM>();

        public ReturnsPopup()
        {
            this.InitializeComponent();
            this.DataContext = SaleGridVM;
        //    SaleGridVM.ReInitialize();
        }
    }
}
