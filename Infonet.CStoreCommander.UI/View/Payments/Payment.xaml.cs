using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Payment;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Payments
{
    public sealed partial class Payment : Page
    {
        public PaymentVM PaymentVM { get; set; }
      = SimpleIoc.Default.GetInstance<PaymentVM>();

        public Payment()
        {
            this.InitializeComponent();
            this.DataContext = PaymentVM;
            NavigateService.Instance.PaymentFrame = frmPayment;

            ReInitalizeVM();
        }

        private void ReInitalizeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PaymentVM.ResetVM();
            }
        }
    }
}
