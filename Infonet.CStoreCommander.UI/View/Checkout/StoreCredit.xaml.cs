using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Checkout;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Checkout
{
    public sealed partial class StoreCredit : Page
    {
        public StoreCreditVM StoreCreditVM =
             SimpleIoc.Default.GetInstance<StoreCreditVM>();

        public StoreCredit()
        {
            this.InitializeComponent();
            this.DataContext = StoreCreditVM;
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                StoreCreditVM.ReInitialize();
            }
            Loaded -= UpdatedLayout;
            Loaded += UpdatedLayout;
        }

        private void UpdatedLayout(object sender, RoutedEventArgs e)
        {
            txtStoreCredit.Focus(FocusState.Keyboard);
        }
    }
}
