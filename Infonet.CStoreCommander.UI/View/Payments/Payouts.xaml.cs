using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Payment;
using MyToolkit.Extended.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Infonet.CStoreCommander.UI.View.Payments
{
    public sealed partial class Payouts : Page
    {
        public PayoutVM PayoutVM { get; set; }
      = SimpleIoc.Default.GetInstance<PayoutVM>();

        public Payouts()
        {
            this.InitializeComponent();
            this.DataContext = PayoutVM;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PayoutVM.ResetVM();
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as CustomTextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
        }

        private void PasswordBoxKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if(Helper.IsEnterKey(e))
            {
                VendorSearch.Focus(FocusState.Programmatic);
            }
        }

        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            txtAmountGiveX.Focus(FocusState.Programmatic);
        }
    }
}
