using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class Coupon : Page
    {
        public CouponVM CouponVM = 
            SimpleIoc.Default.GetInstance<CouponVM>();

        public Coupon()
        {
            this.InitializeComponent();
            DataContext = CouponVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                CouponVM.ReInitialize();
            }
        }
    }
}
