using GalaSoft.MvvmLight.Ioc;
using Infonet.CStoreCommander.UI.Service;
using Infonet.CStoreCommander.UI.ViewModel.Sale;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Infonet.CStoreCommander.UI.View.Sale
{
    public sealed partial class ReturnSale : Page
    {
        public ReturnSaleVM ReturnSaleVM { get; set; }
       = SimpleIoc.Default.GetInstance<ReturnSaleVM>();

        public ReturnSale()
        {
            this.InitializeComponent();
            this.DataContext = ReturnSaleVM;
            ReturnSaleVM.SearchTextFieldName = nameof(txtbSearchSale);
            ReinitializeVM();

            Loaded -= OnLoaded;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            txtbSearchSale.Focus(FocusState.Keyboard);
        }

        private void ReinitializeVM()
        {
            if (!NavigateService.Instance.IsNavigatedFromFreezeScreen)
            {
                ReturnSaleVM.Reset();
            }
        }
    }
}
