using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout.TaxExempt
{
    public sealed partial class SITE : Page
    {
        public SiteVM SiteVM =
            SimpleIoc.Default.GetInstance<SiteVM>();

        public SITE()
        {
            this.InitializeComponent();
            DataContext = SiteVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                SiteVM.ReInitialize();
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            TreatyNumber.Focus(FocusState.Programmatic);
        }
    }
}
