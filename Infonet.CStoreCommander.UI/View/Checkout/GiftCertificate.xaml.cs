using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class GiftCertificate : Page
    {
        public GiftCertificateVM GiftCertificateVM =
            SimpleIoc.Default.GetInstance<GiftCertificateVM>();

        public GiftCertificate()
        {
            this.InitializeComponent();
            DataContext = GiftCertificateVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                GiftCertificateVM.ReInitialize();
            }
        }
    }
}
