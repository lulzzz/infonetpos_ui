using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.Utility;
using Infonet.CStoreCommander.UI.ViewModel.Stock;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Stock
{
    public sealed partial class AddStock : Page
    {
        public AddStockScreenVM AddStockScreenVM { get; set; }
            = SimpleIoc.Default.GetInstance<AddStockScreenVM>();

        public AddStock()
        {
            this.InitializeComponent();
            DataContext = AddStockScreenVM;
            txtStockCode.Focus(FocusState.Keyboard);

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                AddStockScreenVM.ReInitialize();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            txtStockCode.KeyUp += StockCodeKeyUp;
            txtDescription.KeyUp += DescriptionKeyUp;
            txtRegularPrice.KeyUp += RegularPriceKeyUp;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            txtStockCode.KeyUp -= StockCodeKeyUp;
            txtDescription.KeyUp -= DescriptionKeyUp;
            txtRegularPrice.KeyUp -= RegularPriceKeyUp;
        }

        private void RegularPriceKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                btnSave.Focus(FocusState.Pointer);
            }
        }

        private void DescriptionKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtRegularPrice.Focus(FocusState.Keyboard);
            }
        }

        private void StockCodeKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtDescription.Focus(FocusState.Keyboard);
            }
        }
    }
}
