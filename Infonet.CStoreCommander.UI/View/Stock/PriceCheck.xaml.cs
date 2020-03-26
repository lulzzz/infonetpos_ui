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
    public sealed partial class PriceCheck : Page
    {
        public PriceCheckVM PriceCheckVM { get; set; }
          = SimpleIoc.Default.GetInstance<PriceCheckVM>();

        public PriceCheck()
        {
            this.InitializeComponent();
            this.DataContext = PriceCheckVM;

            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                PriceCheckVM.ResetVM();
            }
        }

        private void AttachEvents()
        {
            txtStockCode.KeyUp -= TxtStockCodeKeyUp;
            txtAvailableQuantity.KeyUp -= TxtStockCodeKeyUp;
            txtRegularPrice.KeyUp -= TxtStockCodeKeyUp;

            txtStockCode.KeyUp += TxtStockCodeKeyUp;
            txtRegularPrice.KeyUp += TxtStockCodeKeyUp;
            txtAvailableQuantity.KeyUp += TxtStockCodeKeyUp;

            this.Loaded -= PriceCheck_Loaded;
            this.Loaded += PriceCheck_Loaded;
        }

        private void TxtStockCodeKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
            }
        }

        private void PriceCheck_Loaded(object sender, RoutedEventArgs e)
        {
            txtStockCode.Focus(FocusState.Keyboard);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            AttachEvents();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            this.Loaded -= PriceCheck_Loaded;

            txtStockCode.KeyUp -= TxtStockCodeKeyUp;
            txtAvailableQuantity.KeyUp -= TxtStockCodeKeyUp;
            txtRegularPrice.KeyUp -= TxtStockCodeKeyUp;
        }
    }
}
