using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class Tender : Page
    {
        public SaleSummaryVM SaleSummaryVM =
            SimpleIoc.Default.GetInstance<SaleSummaryVM>();

        public Tender()
        {
            this.InitializeComponent();
            DataContext = SaleSummaryVM;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}
