using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class FleetCard : Page
    {
        public FleetTenderVM FleetTenderVM =
            SimpleIoc.Default.GetInstance<FleetTenderVM>();        

        public FleetCard()
        {
            this.InitializeComponent();
            DataContext = FleetTenderVM;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                FleetTenderVM.ResetVM();
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            txtCard.Focus(FocusState.Programmatic);
        }
    }
}
