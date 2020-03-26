using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class BottleItems : Page
    {
        public BottleReturnsScreenVM BottleReturnsScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<BottleReturnsScreenVM>();

        public BottleItems()
        {
            this.InitializeComponent();
            DataContext = BottleReturnsScreenVM;
        }
    }
}
