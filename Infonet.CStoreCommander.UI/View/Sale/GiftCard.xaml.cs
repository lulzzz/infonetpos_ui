using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml.Input;
using Infonet.CStoreCommander.UI.Utility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GiftCard : Page
    {
        public SaleGridVM SaleGridVM { get; set; } =
            SimpleIoc.Default.GetInstance<SaleGridVM>();

        public GiftCard()
        {
            this.InitializeComponent();
            DataContext = SaleGridVM;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AttachEvents();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            DetachEvents();
        }

        private void DetachEvents()
        {
            txtGiftNumber.KeyUp -= GiftNumberKeyUp;
            txtPrice.KeyUp -= PriceKeyUp;
        }

        private void AttachEvents()
        {
            txtGiftNumber.KeyUp += GiftNumberKeyUp;
            txtPrice.KeyUp += PriceKeyUp;
        }

        private void GiftNumberKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtPrice.Focus(FocusState.Keyboard);
            }
        }

        private void PriceKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Helper.IsEnterKey(e))
            {
                txtQuantity.Focus(FocusState.Keyboard);
            }
        }
    }
}
