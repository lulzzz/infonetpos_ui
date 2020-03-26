using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class VendorCoupon : Page
    {
        public VendorCouponVM VendorCouponVM { get; set; } =
         SimpleIoc.Default.GetInstance<VendorCouponVM>();

        public VendorCoupon()
        {
            this.InitializeComponent();
            DataContext = VendorCouponVM;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                VendorCouponVM.ResetVM();
            }
        }
        

        private void VendorCouponLoaded(object sender, RoutedEventArgs e)
        {
            txtVendorCouponNumber.Focus(FocusState.Programmatic);
        }

        private void VendorCouponNumberKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtVendorSerialNumber.Focus(FocusState.Programmatic);
            }
        }
    }
}
