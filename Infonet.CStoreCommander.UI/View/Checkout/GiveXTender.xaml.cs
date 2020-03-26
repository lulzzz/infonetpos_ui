using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class GiveXTender : Page
    {
        public GiveXTenderVM GiveXTenderVM =
        SimpleIoc.Default.GetInstance<GiveXTenderVM>();

        public GiveXTender()
        {
            this.InitializeComponent();
            DataContext = GiveXTenderVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                GiveXTenderVM.ReInitialize();
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            TextBox.Focus(FocusState.Programmatic);
        }
    }
}
