using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout.TaxExempt
{
    public sealed partial class TaxExemptOverLimit : Page
    {
        public OverLimitVM OverLimitVM =
            SimpleIoc.Default.GetInstance<OverLimitVM>();

        public TaxExemptOverLimit()
        {
            this.InitializeComponent();
            DataContext = OverLimitVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                OverLimitVM.ReInitialize();
            }
        }
    }
}
