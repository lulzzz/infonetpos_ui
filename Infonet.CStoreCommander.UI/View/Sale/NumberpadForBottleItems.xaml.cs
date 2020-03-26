using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Sale
{

    public sealed partial class NumberpadForBottleItems : Page
    {
        public BottleReturnsScreenVM BottleReturnsScreenVM { get; set; }
        = SimpleIoc.Default.GetInstance<BottleReturnsScreenVM>();

        public NumberpadForBottleItems()
        {
            this.InitializeComponent();
        }
    }
}
